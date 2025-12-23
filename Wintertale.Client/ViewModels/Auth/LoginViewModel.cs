using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Wintertale.Client.ViewModels.Auth {
    [INotifyPropertyChanged]
    internal partial class LoginViewModel : ObservableObject {
        public LoginViewModel() {

        }

        [ObservableProperty]
        public string name = "Test";
    }
}
