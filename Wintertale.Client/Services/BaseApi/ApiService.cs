using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.ServerSentEvents;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Wintertale.Client.Services.BaseApi {
    internal class ApiService : BaseApiService, IApiService {
        public HttpClient client { get; set; }

        public ApiService(HttpClient client) {
            this.client = client;
            client.Timeout = TimeSpan.FromMinutes(5);
            this.BaseUri = "https://wintertale.rotatick.ru/";
        }

        public async Task<TResponse> HttpAsync<TRequest, TResponse>(HttpMethod method, string uri, TRequest? request) {
            string endPoint = $"{BaseUri}/{uri.TrimStart('/')}";

            var requestMessage = new HttpRequestMessage {
                RequestUri = new Uri(endPoint),
                Method = method
            };

            if (request != null) {
                requestMessage.Content = JsonContent.Create(request);
            }

            var response = await client.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode) {
                await HandleErrorResponse(response.Content);
            }

            var result = await response.Content.ReadFromJsonAsync<TResponse>()
                ?? throw new HttpRequestException($"Ошибка десереализации");

            return result;
        }

        public async IAsyncEnumerable<string> SseAsync<TRequest>(string uri, TRequest? request, [EnumeratorCancellation] CancellationToken cancellationToken = default) {
            string endPoint = $"{BaseUri}/{uri.TrimStart('/')}";

            var requestMessage = new HttpRequestMessage {
                RequestUri = new Uri(endPoint),
                Method = HttpMethod.Get
            };
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/event-stream"));
            requestMessage.Headers.Add("Cache-Control", "no-cache");
            requestMessage.Headers.ConnectionClose = false;

            if (request != null) {
                requestMessage.Content = JsonContent.Create(request);
            }

            using var response = await client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

            using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            await foreach (SseItem<string> e in SseParser.Create(stream).EnumerateAsync(cancellationToken)) {
                yield return e.Data;
            }
        }

        private async Task<string> HandleErrorResponse(HttpContent content) {
            string errorMessage = "Что-то пошло не так, повторите попытку позже";
            try {
                var error = JsonSerializer.Deserialize<Dictionary<string, string>>(await content.ReadAsStringAsync())!;
                if (error.ContainsKey("error")) {
                    errorMessage = error["error"];
                }
            } catch {

            }

            throw new HttpRequestException(errorMessage);
        }
    }
}
