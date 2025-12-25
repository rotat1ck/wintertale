using Wintertale.Client.ViewModels.Dashboard;

namespace Wintertale.Client.Views.Dashboard;

public partial class DashboardPage : ContentPage {
	public DashboardPage() {
		InitializeComponent();
		BindingContext = App.ServiceProvider.GetService<DashboardViewModel>();
	}
}