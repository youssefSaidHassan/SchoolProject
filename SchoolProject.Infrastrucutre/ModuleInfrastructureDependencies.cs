﻿using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Abstract.Functions;
using SchoolProject.Infrastructure.Abstract.Procedures;
using SchoolProject.Infrastructure.Abstract.Views;
using SchoolProject.Infrastructure.InfrastructureBases;
using SchoolProject.Infrastructure.Repositories;
using SchoolProject.Infrastructure.Repositories.Functions;
using SchoolProject.Infrastructure.Repositories.Procedures;
using SchoolProject.Infrastructure.Repositories.Views;

namespace SchoolProject.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            // Replace IStudentRepository with the actual implementation, e.g., StudentRepository
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IInstructorRepository, InstructorRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            // views
            services.AddScoped<IViewRepository<ViewDepartment>, ViewDepartmentRepository>();
            // Procedure
            services.AddScoped<IDepartmentStudentCountProcRepository, DepartmentStudentCountProcRepository>();
            // functions
            services.AddScoped<IInstructorFunctionsRepository, InstructorFunctionsRepository>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}
