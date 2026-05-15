using BusinessLogicLayer.Mappers;
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

            return services;
        }   
    }
}
