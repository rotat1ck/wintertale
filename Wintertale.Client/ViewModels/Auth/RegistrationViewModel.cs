using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
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
            var request = new PhoneVerificationRequest {
                phone = Phone
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
            var request = new RegisterRequest {
                fname = Name,
                phone = Phone,
                password = Password
            };


        }
    }
}
