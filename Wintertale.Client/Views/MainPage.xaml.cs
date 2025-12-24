using CommunityToolkit.Maui.Extensions;
using Wintertale.Client.Views.Auth;

namespace Wintertale.Client.Views {
    public partial class MainPage : ContentPage {
        LoginPage loginPage;
        VerifyPage verifyPage;

        public MainPage() {
            InitializeComponent();

            loginPage = new LoginPage();
            verifyPage = new VerifyPage();

            Navigation.PushAsync(loginPage);
        }

        private async void Button_Clicked(object sender, EventArgs e) {
            await Navigation.PushAsync(loginPage);
        }
    }
}
