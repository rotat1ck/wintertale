namespace Wintertale.Client.Views.Auth;

public partial class RegistrationFinalizePage : ContentPage
{
	public RegistrationFinalizePage()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) {
		passwordEntry.IsPassword = !passwordEntry.IsPassword;
    }
}