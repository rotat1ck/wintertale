using AuthService.Application.DTOs.Requests;
using AuthService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.WebApi.Controllers {
    [ApiController]
    [Route("api/v1/auth/[controller]")]
    public class VerifyController : Controller {
        private readonly IVerifyService service;

        public VerifyController(IVerifyService service) {
            this.service = service;
        }

        [HttpGet]
        public async Task VerifyAsync(PhoneVerificationRequest request, CancellationToken cancellationToken) {
            Response.ContentType = "text/event-stream";
            Response.Headers["Cache-Control"] = "no-cache";
            Response.Headers["Connection"] = "keep-alive";

            using var writer = new StreamWriter(Response.Body);

            if (await service.CheckVerificationAsync(request) != null) {
                await service.WriteEventAsync(writer, "Номер телефона уже верифицирован");
                return;
            }

            using var token = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            await service.InitializeVerificationAsync(request, writer, token);

            token.CancelAfter(TimeSpan.FromMinutes(5));
            await service.SubscribeAsync(request, writer, token);
        }

        [HttpPost("result")]
        public async Task WebhookResultAsync() {
            var form = await HttpContext.Request.ReadFormAsync();

            form.TryGetValue("data[0]", out var dataValue);
            var parts = dataValue.ToString().Split('\n', StringSplitOptions.RemoveEmptyEntries);

            if (parts[0] == "callcheck_status") {
                await service.PublishAsync(parts[1], parts[2]);
            }

            await HttpContext.Response.WriteAsync("100");
        }
    }
}
