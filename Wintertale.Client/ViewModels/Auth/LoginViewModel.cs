using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;
using Wintertale.Client.Common.DTOs.Requests.Auth;
using Wintertale.Client.Services.Auth;

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

        public async Task RefreshAsync() {
            try {
                await service.RefreshTokenAsync();
                await Shell.Current.GoToAsync("DashboardPage");
            } catch {
                
            }
        }

        [RelayCommand]
        private async Task Login() {
            string formattedPhone = Regex.Replace(Phone, @"\D", "");
            if (!Regex.IsMatch(formattedPhone, @"^7\d{10}$")) {
                await Shell.Current.DisplayAlertAsync("Ошибка валидации", formattedPhone, "ОК");
                return;
            }

            var loginRequest = new LoginRequest {
                phone = formattedPhone,
                password = Password,
            };

            try {
                var response = await service.LoginAsync(loginRequest);
                await Shell.Current.GoToAsync("DashboardPage");
            } catch (HttpRequestException ex) {
                await Shell.Current.DisplayAlertAsync("Ошибка входа", ex.Message, "ОК");
            }
        }

        [RelayCommand]
        private async Task OpenRegister() {
            await Shell.Current.GoToAsync("RegistrationStartPage", true);
        }
    }
}
