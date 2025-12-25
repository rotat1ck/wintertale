using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Threading.Tasks;
using Wintertale.Client.Common.DTOs.Requests.Friends;
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

        public async void ApplyQueryAttributes(IDictionary<string, object> query) {
            currentUser = query["CurrentUser"] as LoginResponse;

            if (currentUser!.utc_offset != DateTimeOffset.Now.Offset.TotalMinutes) {
                var request = new UpdateUtcOffsetRequest {
                    utc_offset = DateTimeOffset.Now.Offset.TotalMinutes
                };
                await service.UpdateUtcOffsetAsync(request);
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
