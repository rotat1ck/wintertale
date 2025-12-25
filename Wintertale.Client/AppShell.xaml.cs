using Wintertale.Client.Views;
using Wintertale.Client.Views.Auth;

namespace Wintertale.Client {
    public partial class AppShell : Shell {
        public AppShell() {
            InitializeComponent();

            Routing.RegisterRoute("LoginPage", typeof(LoginPage));
            Routing.RegisterRoute("RegistrationStartPage", typeof(RegistrationStartPage));
        }
    }
}
