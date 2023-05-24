using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace MtlsDemo.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            builder.Services.Configure<KestrelServerOptions>(options =>
            {
                options.ConfigureHttpsDefaults(options =>
                {
                    options.AllowAnyClientCertificate();
                    options.CheckCertificateRevocation = false;
                    options.ClientCertificateValidation = ClientCertificateValidation;
                    options.ClientCertificateMode = ClientCertificateMode.AllowCertificate;
                });
            });

            // Add services to the container.

            builder.Services.AddControllers();

            ConfigureServices(builder.Services);

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }

        private static bool ClientCertificateValidation(X509Certificate2 arg1, X509Chain? arg2, SslPolicyErrors arg3)
        {
            return true;
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services
               .AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
               .AddCertificate(options =>
               {
                   options.RevocationMode = X509RevocationMode.NoCheck;
                   options.AllowedCertificateTypes = CertificateTypes.All;

                   options.Events = new CertificateAuthenticationEvents
                   {
                       OnAuthenticationFailed = p =>
                       {
                           Console.WriteLine(p.Exception);
                           return Task.CompletedTask;

                       },
                       OnCertificateValidated = context =>
                       {
                           // Add certificate to HttpContext.Items
                           context.HttpContext.Items["ClientCertificate"] = context.ClientCertificate;
                           return Task.CompletedTask;
                       }
                   };
               });
        }
    }
}