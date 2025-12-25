using Wintertale.Client.ViewModels.Auth;

namespace Wintertale.Client.Views.Auth;

public partial class RegistrationStartPage : ContentPage {
	public RegistrationStartPage() {
		InitializeComponent();
		BindingContext = App.ServiceProvider.GetService<RegistrationViewModel>();
	}
}