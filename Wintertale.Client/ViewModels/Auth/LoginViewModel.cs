using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Wintertale.Client.ViewModels.Auth {
    [INotifyPropertyChanged]
    internal partial class LoginViewModel  {
        public LoginViewModel() {

        }

        [ObservableProperty]
        private string name = "Test";

        [RelayCommand]
        private async Task Back() {
            await Shell.Current.Navigation.PopAsync();    
        }
    }
}
