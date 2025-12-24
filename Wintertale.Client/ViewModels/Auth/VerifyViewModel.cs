using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

using Wintertale.Client.Common.DTOs.Requests.Auth;
using Wintertale.Client.Services.Auth;

namespace Wintertale.Client.ViewModels.Auth {
    [INotifyPropertyChanged]
    internal partial class VerifyViewModel {
        private readonly IAuthService service;

        public VerifyViewModel(IAuthService service) {
            this.service = service;
        }

        [ObservableProperty]
        private string phone;

        [ObservableProperty]
        private ObservableCollection<string> messages = new();

        [RelayCommand]
        private async Task Verify() {
            Messages.Add("Starting");
            var request = new PhoneVerificationRequest {
                phone = Phone
            };

            try {
                await foreach (var message in service.VerifyAsync(request)) {
                    Messages.Add(message);
                }
            } catch (HttpRequestException ex) {
                Messages.Add(ex.Message);
            }
        }
    }
}
