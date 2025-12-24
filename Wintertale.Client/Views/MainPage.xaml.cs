using CommunityToolkit.Maui.Extensions;

namespace Wintertale.Client.Views {
    public partial class MainPage : ContentPage {
        LoginPage loginPage = new LoginPage();

        public MainPage() {
            InitializeComponent();
            Navigation.PushAsync(loginPage);
        }

        private void Button_Clicked(object sender, EventArgs e) {
            Navigation.PushAsync(loginPage);
        }
    }
}
