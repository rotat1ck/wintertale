using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using Wintertale.Client.Common.DTOs.Responses.Auth;
using Wintertale.Client.Services.Friends;

namespace Wintertale.Client.ViewModels.Dashboard {
    [INotifyPropertyChanged]
    internal partial class DashboardViewModel : IQueryAttributable {
        private readonly IFriendService service;

        public DashboardViewModel(IFriendService service) {
            this.service = service;
        }
        private LoginResponse? currentUser { get; set; }

        public void ApplyQueryAttributes(IDictionary<string, object> query) {
            currentUser = query["CurrentUser"] as LoginResponse;

            if (currentUser!.utc_offset != DateTimeOffset.Now.Offset.TotalMinutes) {
                Debug.WriteLine($"Offset не совпадает: {DateTimeOffset.Now.Offset.TotalMinutes}");
            }
        }

        [RelayCommand]
        private async Task OpenFriends() {

        }

        private void StartCountDown() {
            Task.Run(async () => {
                while (true) {
                    await UpdateCountdown();
                }
            });
        }

        private async Task UpdateCountdown() {
            
        }
    }
}
