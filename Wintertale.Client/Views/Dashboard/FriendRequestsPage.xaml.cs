using Wintertale.Client.ViewModels.Dashboard;

namespace Wintertale.Client.Views.Dashboard;

public partial class FriendRequestsPage : ContentPage {
	public FriendRequestsPage() {
		InitializeComponent();
		BindingContext = App.ServiceProvider.GetService<DashboardViewModel>();
	}
}