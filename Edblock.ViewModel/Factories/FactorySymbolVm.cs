using System.Reflection;
using Edblock.ViewModel.SymbolsVM;
using Microsoft.Extensions.DependencyInjection;

namespace Edblock.ViewModel.Factories;

public class FactorySymbolVm
{
    private readonly Dictionary<string, Func<SymbolVm>> _symbolVmByName;
    private readonly IServiceProvider _serviceProvider;
    
    public FactorySymbolVm(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _symbolVmByName = ConfigurationSymbolVmByName();
    }

    public SymbolVm Create(string nameType)
    {
        if (_symbolVmByName.TryGetValue(nameType, out var factory))
        {
            return factory.Invoke();
        }
        throw new ArgumentException($"Type with name {nameType} not found.");
    }

    private Dictionary<string, Func<SymbolVm>> ConfigurationSymbolVmByName()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var types = assembly.GetTypes();

        return types
            .Where(t => t.IsSubclassOf(typeof(SymbolVm)))
            .ToDictionary(
                type => type.Name,
                type => (Func<SymbolVm>)(() =>
                {
                    try
                    {
                        return (SymbolVm)_serviceProvider.GetRequiredService(type);
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException($"Cannot create instance of {type.Name}", ex);
                    }
                })
            );
    }
}