using Wintertale.Client.Views;
using Wintertale.Client.Views.Auth;
using Wintertale.Client.Views.Dashboard;

namespace Wintertale.Client {
    public partial class AppShell : Shell {
        public AppShell() {
            InitializeComponent();

            Routing.RegisterRoute("LoginPage", typeof(LoginPage));
            Routing.RegisterRoute("RegistrationStartPage", typeof(RegistrationStartPage));
            Routing.RegisterRoute("VerifyPage", typeof(VerifyPage));
            Routing.RegisterRoute("RegistrationFinalizePage", typeof(RegistrationFinalizePage));

            Routing.RegisterRoute("DashboardPage", typeof(DashboardPage));
        }
    }
}
