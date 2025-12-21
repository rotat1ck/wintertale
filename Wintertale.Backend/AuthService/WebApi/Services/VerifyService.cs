using AuthService.Application.Common.Exceptions;
using AuthService.Application.DTOs.Requests;
using AuthService.Application.Interfaces.Providers;
using AuthService.Application.Interfaces.Repositories;
using AuthService.Application.Interfaces.Services;
using AuthService.Domain.Enums;
using AuthService.Domain.Models;
using System.Text.Json;

namespace AuthService.WebApi.Services {
    public class VerifyService : IVerifyService {
        private readonly IVerifyRepository repository;
        private readonly IRedisProvider redisProvider;
        private readonly HttpClient client;
        private readonly IConfiguration configuration;

        public VerifyService(IVerifyRepository repository, IRedisProvider redisProvider, HttpClient client, IConfiguration configuration) {
            this.repository = repository;
            this.redisProvider = redisProvider;
            this.client = client;
            this.configuration = configuration;
        }

        public async Task<Verification?> CheckVerificationAsync(PhoneVerificationRequest request) {
            var verification = await repository.GetVerificationByPhoneAsync(request.phone);
            return verification?.status == (int)VerificationEnum.Success ? verification : null;
        }

        public async Task InitializeVerificationAsync(PhoneVerificationRequest request, StreamWriter writer, CancellationTokenSource cancellationToken) {
            var response = await client.GetAsync($"https://sms.ru/callcheck/add?api_id={configuration["SMSRU_APIKEY"]}&phone={request.phone}&json=1", cancellationToken.Token);
            var jsonStr = await response.Content.ReadAsStringAsync(cancellationToken.Token);
            using var json = JsonDocument.Parse(jsonStr);

            if (json.RootElement.TryGetProperty("check_id", out var id)) {
                var pendingVerification = await repository.GetVerificationByPhoneAsync(request.phone);
                await WriteEventAsync(writer, "Continue");
                if (pendingVerification != null) {
                    pendingVerification.check_id = id.ToString();
                    pendingVerification.updated_at = DateTime.UtcNow;
                    await repository.UpdateVerificationAsync(pendingVerification);
                } else {
                    await repository.CreateVerificationAsync(new Verification {
                        check_id = id.ToString(),
                        phone = request.phone,
                    });
                }
            }
        }

        public async Task SubscribeAsync(PhoneVerificationRequest request, StreamWriter writer, CancellationTokenSource cancellationToken) {
            TaskCompletionSource<bool> completionSource = new();

            await redisProvider.SubscribeAsync($"verification:{request.phone}", async (message) => {
                await WriteEventAsync(writer, message);
                completionSource.SetResult(true);
            });
            
            try {
                await completionSource.Task.WaitAsync(cancellationToken.Token);
            } catch (OperationCanceledException) {
                await WriteEventAsync(writer, "Время верификации истекло, повторите попытку");
            } finally {
                await redisProvider.UnsubscribeAsync($"verification:{request.phone}");
            }
        }

        public async Task PublishAsync(string checkId, string callcheck_status) {
            var verification = await repository.GetVerificationByCheckIdAsync(checkId) ??
                throw new UnprocessableException();
            
            string? message;

            if (callcheck_status == "401") {
                verification.status = (int)VerificationEnum.Success;
                message = "Номер успешно подтвержден";
            } else {
                verification.status = (int)VerificationEnum.Failed;
                message = "Время верификации истекло, повторите попытку";
            }

            await repository.UpdateVerificationAsync(verification);
            await redisProvider.PublishAsync($"verification:{verification.phone}", message);
        }

        public async Task WriteEventAsync(StreamWriter writer, string eventText) {
            await writer.WriteAsync($"data: {eventText}\n\n");
            await writer.FlushAsync();
        }
    }
}
