using BusinessLogicLayer.Mappers;
using BusinessLogicLayer.ServiceContracts;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Validators;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssemblyContaining<OrderItemUpdateRequestValidator>();

            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(OrderAddRequestToOrderMappingProfile).Assembly));

            services.AddScoped<IOrdersService, OrdersService>();

            return services;
        }   
    }
}
