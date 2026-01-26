using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace NotificationService.Application.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {


        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            
            return services;
        }   
    }
}
