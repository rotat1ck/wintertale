using AuthService.Application.Common.Extensions;
using AuthService.Persistence.Data;
using AuthService.Persistence.Jwt;

namespace AuthService.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.AppConfigureJWT();
            builder.AppPersistData();

            var app = builder.Build();

            app.AppRun();
        }
    }
}
