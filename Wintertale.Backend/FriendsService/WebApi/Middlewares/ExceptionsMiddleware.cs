using FriendsService.Application.Common.Exceptions;

namespace FriendsService.WebApi.Middlewares {
    public class ExceptionsMiddleware {
        private readonly RequestDelegate next;

        public ExceptionsMiddleware(RequestDelegate next) {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context) {
            try {
                await next(context);
            } catch (FormatException ex) {
                await ExceptionHandler.HandleExceptionAsync(context, 400, ex.Message);
            } catch (InvalidActionException ex) {
                await ExceptionHandler.HandleExceptionAsync(context, 400, ex.Message);
            } catch (UnauthorizedAccessException ex) {
                await ExceptionHandler.HandleExceptionAsync(context, 401, ex.Message);
            } catch (AccessDeniedException ex) {
                await ExceptionHandler.HandleExceptionAsync(context, 403, ex.Message);
            } catch (NotFoundException ex) {
                await ExceptionHandler.HandleExceptionAsync(context, 404, ex.Message);
            } catch (UnprocessableException ex) {
                await ExceptionHandler.HandleExceptionAsync(context, 422, ex.Message);
            } catch {
                await ExceptionHandler.HandleExceptionAsync(context, 500, "Internal server error");
            }
        }
    }
}
