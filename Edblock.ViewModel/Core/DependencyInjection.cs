using Edblock.ViewModel.Factories;
using Edblock.ViewModel.SymbolsVM;
using Microsoft.Extensions.DependencyInjection;

namespace Edblock.ViewModel.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<ActionSymbolVm>();
        services.AddTransient<ConditionSymbolVm>();
        services.AddSingleton<Mapper>();
        services.AddSingleton<FactorySymbolVm>();

        return services;
    }
}