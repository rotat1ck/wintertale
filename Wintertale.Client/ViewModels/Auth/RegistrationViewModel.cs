using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Wintertale.Client.Common.DTOs.Requests.Auth;
using Wintertale.Client.Services.Auth;

namespace Wintertale.Client.ViewModels.Auth {
    [INotifyPropertyChanged]
    internal partial class RegistrationViewModel {
        private readonly IAuthService service;

        public RegistrationViewModel(IAuthService service) {
            this.service = service;
        }

        [ObservableProperty]
        private string phone;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string password;

        [RelayCommand]
        private async Task Verify() {
            string formattedPhone = Regex.Replace(Phone, @"\D", "");
            if (!Regex.IsMatch(formattedPhone, @"^7\d{10}$")) {
                await Shell.Current.DisplayAlertAsync("Ошибка валидации", formattedPhone, "ОК");
                return;
            }

            var request = new PhoneVerificationRequest {
                phone = formattedPhone
            };

            try {
                using var cts = new CancellationTokenSource();
                await foreach (var message in service.VerifyAsync(request, cts.Token)) {
                    switch (message) {
                        case "Continue":
                            await Shell.Current.GoToAsync("VerifyPage");
                            break;
                        case "Номер телефона уже верифицирован":
                            cts.Cancel();
                            await Shell.Current.GoToAsync("RegistrationFinalizePage");
                            break;
                        case "Номер успешно подтвержден":
                            cts.Cancel();
                            await Back();
                            await Shell.Current.GoToAsync("RegistrationFinalizePage");
                            break;
                        default:
                            cts.Cancel();
                            await Shell.Current.DisplayAlertAsync("Непредвиденная ошибка", message, "Вернуться");
                            await Back();
                            break;
                    }
                }
            } catch (Exception ex) {
                await Shell.Current.DisplayAlertAsync("Ошибка верификации", ex.Message, "Вернуться");
                await Back();
            }
        }

        [RelayCommand]
        private async Task Back() {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task Register() {
            if (Name.Length > 24) {
                await Shell.Current.DisplayAlertAsync("Ошибка валидации", "Длина имени не должна превышать 24 символа", "ОК");
                return;
            }

            var request = new RegisterRequest {
                fname = Name,
                phone = Regex.Replace(Phone, @"\D", ""),
                password = Password
            };

            try {
                var response = await service.RegisterAsync(request);
                await Shell.Current.GoToAsync("DashboardPage");
            } catch (Exception ex) {
                await Shell.Current.DisplayAlertAsync("Ошибка регистрации", ex.Message, "ОК");
            }
        }
    }
}
