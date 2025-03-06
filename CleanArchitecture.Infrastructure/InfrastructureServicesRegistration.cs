using ComplexCalculator.Application.Contracts.Admin;
using ComplexCalculator.Application.Contracts.Calculator;
using ComplexCalculator.Infrastructure.Services.Admin;
using ComplexCalculator.Infrastructure.Services.CalculatorService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCalculator.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<ICalculator, CalculatorService>();
            services.AddTransient<IAdmin, AdminService>();
            //services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            return services;
        }
    }
}
