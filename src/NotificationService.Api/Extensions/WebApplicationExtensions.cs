
using NotificationService.Infrastructure.Identity.DataSeeding;

namespace NotificationService.Api.Extensions
{
    public static class WebApplicationExtensions
    {



        public static async Task<WebApplication> InitializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var identityDbInitializer = services.GetRequiredService<IIdentityDbInitializer>();
            await identityDbInitializer.InitilaizeAssync();

            return app;
        }
    }
}
