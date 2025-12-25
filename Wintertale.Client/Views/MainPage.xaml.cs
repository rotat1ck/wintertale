using CommunityToolkit.Maui.Extensions;
using Wintertale.Client.Views.Auth;

namespace Wintertale.Client.Views {
    public partial class MainPage : ContentPage {
        public MainPage() {
            Shell.Current.GoToAsync("LoginPage");
        }
    }
}
