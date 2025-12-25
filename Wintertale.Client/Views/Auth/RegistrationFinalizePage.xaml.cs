using Wintertale.Client.ViewModels.Auth;

namespace Wintertale.Client.Views.Auth;

public partial class RegistrationFinalizePage : ContentPage {
	public RegistrationFinalizePage() {
		InitializeComponent();
        BindingContext = App.ServiceProvider.GetService<RegistrationViewModel>();
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) {
		passwordEntry.IsPassword = !passwordEntry.IsPassword;
    }
}