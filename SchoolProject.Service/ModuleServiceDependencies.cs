using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.AuthServices.Implementation;
using SchoolProject.Service.AuthServices.Interfaces;
using SchoolProject.Service.Implementation;

namespace SchoolProject.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IDepartmentServices, DepartmentServices>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            return services;
        }
    }
}
