using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Edblock.View.Views;
using Edblock.ViewModel.Components;
using Edblock.ViewModel.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Edblock.View;

public class App : Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        IServiceCollection services = new ServiceCollection();
        
        services.AddSingleton<Mapper>();
        services.AddSingleton<EditorVm>();
        services.AddApplication();
        
        _serviceProvider = services.BuildServiceProvider();
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = _serviceProvider.GetRequiredService<EditorVm>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}