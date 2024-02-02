using Application.PolicyManagement.Commands.Create;
using Application.Shared.Behaviors;
using Domain.CustomerManagement.Repository;
using Domain.PolicyManagement.Repository;
using Domain.ProductManagement.Repository;
using Domain.Shared;
using FluentValidation;
using Infrastructure.DataAccess;
using Infrastructure.Repositories.CustomerManagement;
using Infrastructure.Repositories.PolicyManagement;
using Infrastructure.Repositories.ProductManagement;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Insurance.Api
{
    public class DependencyResolver
    {
        private readonly string? _dbConnection;

        public DependencyResolver(IConfiguration configuration)
        {
            _dbConnection = configuration.GetConnectionString("InsuranceDb");
        }

        public IServiceCollection Resolve(IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPolicyRepository, PolicyRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<EFDbContext>(options =>
            {
                options.UseSqlServer(_dbConnection);
            });

            services.AddSingleton<DapperDbContext>();

            services.AddMediatR(new[]
            {
                 typeof(CreatePolicyCommand).GetTypeInfo().Assembly,
            });

            services.AddValidatorsFromAssembly(typeof(CreatePolicyCommand).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));


            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
