using CommunityToolkit.Mvvm.DependencyInjection;
using Wintertale.Client.ViewModels.Auth;

namespace Wintertale.Client.Views.Auth;

public partial class VerifyPage : ContentPage{
	public VerifyPage() {
		InitializeComponent();
		BindingContext = App.ServiceProvider.GetService<VerifyViewModel>();
	}
}