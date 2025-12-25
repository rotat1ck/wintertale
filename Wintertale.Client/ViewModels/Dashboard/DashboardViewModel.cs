using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Wintertale.Client.Common.DTOs.Requests.Friends;
using Wintertale.Client.Common.DTOs.Responses.Auth;
using Wintertale.Client.Common.DTOs.Responses.Friends;
using Wintertale.Client.Services.Friends;

namespace Wintertale.Client.ViewModels.Dashboard {
    [INotifyPropertyChanged]
    internal partial class DashboardViewModel : IQueryAttributable {
        private readonly IFriendService service;
        private readonly DateTime target;

        public DashboardViewModel(IFriendService service) {
            this.service = service;
            this.target = new DateTime(DateTime.Now.Year + 1, 1, 1, 0, 0, 0);

            StartCountdown();
        }
        private LoginResponse? currentUser { get; set; }

        [ObservableProperty]
        private ObservableCollection<FriendResponse> friends;

        public async void ApplyQueryAttributes(IDictionary<string, object> query) {
            currentUser = query["CurrentUser"] as LoginResponse;

            if (currentUser!.utc_offset != DateTimeOffset.Now.Offset.TotalMinutes) {
                var request = new UpdateUtcOffsetRequest {
                    utc_offset = DateTimeOffset.Now.Offset.TotalMinutes
                };
                await service.UpdateUtcOffsetAsync(request);
            }

            Friends = new(await service.GetFriendListAsync());
        }

        [RelayCommand]
        private async Task OpenFriends() {

        }

        [RelayCommand]
        private async Task RemoveFriend(FriendResponse friend) {
            Debug.WriteLine($"Removing: {friend.fname}");

        }

        [ObservableProperty]
        private string days;

        [ObservableProperty]
        private string hours;

        [ObservableProperty]
        private string minutes;

        [ObservableProperty]
        private string seconds;

        private void StartCountdown() {
            Task.Run(async () => {
                while (true) {
                    await UpdateCountdown();
                    await Task.Delay(1000);
                }
            });
        }

        private async Task UpdateCountdown() {
            var timeLeft = target - DateTime.Now;

            MainThread.BeginInvokeOnMainThread(() => {
                Days = timeLeft.Days.ToString("D2");
                Hours = timeLeft.Hours.ToString("D2");
                Minutes = timeLeft.Minutes.ToString("D2");
                Seconds = timeLeft.Seconds.ToString("D2");
            });
        }
    }
}
