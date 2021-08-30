using CreditService.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace CreditService.WebApi
{
    public static class DomainServiceExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services) {
            return services.AddSingleton<CreditDecisionMaker>();
        }
    }
}
