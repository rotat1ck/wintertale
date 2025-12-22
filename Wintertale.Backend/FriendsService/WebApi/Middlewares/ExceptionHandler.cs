using System.Text.Json;

namespace FriendsService.WebApi.Middlewares {
    public static class ExceptionHandler {
        public static async Task HandleExceptionAsync(HttpContext context, int statusCode, string message) {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var response = new { error = message };
            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}
