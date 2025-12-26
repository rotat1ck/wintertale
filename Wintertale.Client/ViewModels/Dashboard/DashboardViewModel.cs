using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
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

        [ObservableProperty]
        private ObservableCollection<FriendResponse> pendingFriends;

        public async void ApplyQueryAttributes(IDictionary<string, object> query) {
            currentUser = query["CurrentUser"] as LoginResponse;

            if (currentUser!.utc_offset != DateTimeOffset.Now.Offset.TotalMinutes) {
                var request = new UpdateUtcOffsetRequest {
                    utc_offset = DateTimeOffset.Now.Offset.TotalMinutes
                };
                await service.UpdateUtcOffsetAsync(request);
            }

            Friends = new(await service.GetFriendListAsync());
            PendingFriends = new(await service.GetPendingFriendsReceivedAsync());
        }

        [RelayCommand]
        private async Task OpenFriends() {
            var navigationParameters = new Dictionary<string, object> {
                { "CurrentUser", currentUser! },
            };

            await Shell.Current.GoToAsync("FriendRequestsPage", navigationParameters);
        }

        [RelayCommand]
        private async Task Back() {
            await Shell.Current.GoToAsync("..");
        }

        [ObservableProperty]
        private string friendPhone;

        [RelayCommand]
        private async Task CreateFriend() {
            string formattedPhone = Regex.Replace(FriendPhone, @"\D", "");
            if (!Regex.IsMatch(formattedPhone, @"^7\d{10}$")) {
                await Shell.Current.DisplayAlertAsync("Ошибка валидации", formattedPhone, "ОК");
                return;
            }

            var request = new CreateFriendRequest {
                phone = formattedPhone
            };

            try {
                var response = await service.CreateFriendAsync(request);
                await Shell.Current.DisplayAlertAsync("Успех", $"Приглашение отправлено пользователю: {formattedPhone}", "ОК");
            } catch (Exception ex) {
                await Shell.Current.DisplayAlertAsync("Ошибка приглашения", ex.Message, "ОК");
            }
        }

        [RelayCommand]
        private async Task UpdateFriend(FriendResponse friend) {
            var request = new UpdateFriendRequest {
                phone = friend.phone,
                status = 1
            };

            try {
                await service.UpdateFriendAsync(request);
                PendingFriends.Remove(friend);
                Friends.Add(friend);
            } catch (Exception ex) {
                await Shell.Current.DisplayAlertAsync("Ошибка принятия запроса", ex.Message, "ОК");
            }
        }

        [RelayCommand]
        private async Task RemovePendingFriend(FriendResponse friend) {
            var request = new RemoveFriendRequest {
                phone = friend.phone
            };

            try {
                await service.RemoveFriendAsync(request);
                PendingFriends.Remove(friend);
            } catch (Exception ex) {
                await Shell.Current.DisplayAlertAsync("Ошибка удаления", ex.Message, "ОК");
            }
        }

        [RelayCommand]
        private async Task RemoveFriend(FriendResponse friend) {
            var request = new RemoveFriendRequest {
                phone = friend.phone
            };
            if (await Shell.Current.DisplayAlertAsync("Удалить из друзей", $"Вы действительно хотите удалить {friend.fname} из списка друзей?", "Удалить друга", "Отменить")) {
                try {
                    await service.RemoveFriendAsync(request);
                    Friends.Remove(friend);
                } catch (Exception ex) {
                    await Shell.Current.DisplayAlertAsync("Ошибка удаления", ex.Message, "ОК");
                }
            }
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
