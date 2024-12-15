using Microsoft.Extensions.Logging;
using Passwords.Model;
using Passwords.Repositories;
using Passwords.Repositories.Interfaces;

namespace Passwords
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });
        
            // services
            builder.Services.AddEntityFrameworkSqlite().AddDbContext<PasswordsDb>();
            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddScoped<ICodeRepository, CodeRepository>();
            builder.Services.AddScoped<IDataRepository, DataRepository>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
