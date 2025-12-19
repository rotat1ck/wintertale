using AuthService.Application.Common.Exceptions;

namespace AuthService.WebApi.Middlewares {
    public class ExceptionsMiddleware {
        private readonly RequestDelegate next;

        public ExceptionsMiddleware(RequestDelegate next) {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context) {
            try {
                await next(context);
            } catch (NotFoundException ex) {
                await ExceptionHandler.HandleExceptionAsync(context, 404, ex.Message);
            } catch (FormatException ex) {
                await ExceptionHandler.HandleExceptionAsync(context, 400, ex.Message);
            } catch (InvalidActionException ex) {
                await ExceptionHandler.HandleExceptionAsync(context, 400, ex.Message);
            } catch (UnauthorizedAccessException ex) {
                await ExceptionHandler.HandleExceptionAsync(context, 401, ex.Message);
            } catch (AccessDeniedException ex) {
                await ExceptionHandler.HandleExceptionAsync(context, 403, ex.Message);
            } catch (Exception ex) {
                await ExceptionHandler.HandleExceptionAsync(context, 500, ex.Message);
            }
        }
    }
}
