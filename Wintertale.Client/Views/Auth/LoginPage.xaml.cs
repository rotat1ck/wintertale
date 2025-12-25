using Wintertale.Client.ViewModels.Auth;

namespace Wintertale.Client.Views.Auth;

public partial class LoginPage : ContentPage {
	public LoginPage() {
		InitializeComponent();
		BindingContext = App.ServiceProvider.GetService<LoginViewModel>();
	}
}