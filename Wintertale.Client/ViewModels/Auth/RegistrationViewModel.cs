using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
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

        [RelayCommand]
        private async Task Verify() {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task Back() {
            await Shell.Current.GoToAsync("..");
        }
    }
}
