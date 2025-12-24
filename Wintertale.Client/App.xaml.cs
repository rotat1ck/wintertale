using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Wintertale.Client {
    public partial class App : Application {
        public static IServiceProvider ServiceProvider { get; private set; }

        public App(IServiceProvider serviceProvider) {
            InitializeComponent();

            ServiceProvider = serviceProvider;
            Ioc.Default.ConfigureServices(serviceProvider);
        }

        protected override Window CreateWindow(IActivationState? activationState) {
            return new Window(new AppShell());
        }
    }
}