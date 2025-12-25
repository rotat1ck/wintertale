using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Wintertale.Client.Common.DTOs.Requests.Auth;
using Wintertale.Client.Services.Auth;
using Wintertale.Client.Views;

namespace Wintertale.Client.ViewModels.Auth {
    [INotifyPropertyChanged]
    internal partial class LoginViewModel  {
        private readonly IAuthService service;

        public LoginViewModel(IAuthService service) {
            this.service = service;
        }

        [ObservableProperty]
        private string phone;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private ObservableCollection<string> messages = new();


        [RelayCommand]
        private async Task Back() {
            await Shell.Current.Navigation.PopAsync();    
        }

        [RelayCommand]
        private async Task Login() {
            var loginRequest = new LoginRequest {
                phone = Phone,
                password = Password,
            };

            try {
                Messages.Add("Запрос отправлен");
                var response = await service.LoginAsync(loginRequest);
                Messages.Add($"Токен: {response.token}");
            } catch (HttpRequestException ex) {
                Messages.Add(ex.Message);
            }
        }

        [RelayCommand]
        private async Task OpenRegister() {
            await Shell.Current.GoToAsync("RegistrationStartPage", true);
        }
    }
}
