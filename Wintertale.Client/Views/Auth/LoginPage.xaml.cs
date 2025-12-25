using Wintertale.Client.ViewModels.Auth;

namespace Wintertale.Client.Views.Auth;

public partial class LoginPage : ContentPage {
	public LoginPage() {
		InitializeComponent();
		var vm = App.ServiceProvider.GetService<LoginViewModel>();
        BindingContext = vm;
        vm.RefreshAsync();
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) {
		passwordEntry.IsPassword = !passwordEntry.IsPassword;
    }
}