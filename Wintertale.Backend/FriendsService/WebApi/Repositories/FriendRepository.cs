using FriendsService.Application.Interfaces.Repositories;
using Domain.Enums;
using Domain.Models;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace FriendsService.WebApi.Repositories {
    public class FriendRepository : IFriendRepository {
        private readonly AppDbContext context;

        public FriendRepository(AppDbContext context) {
            this.context = context;
        }

        public async Task<List<Friend>> GetFriendsAsync(string requesterId) {
            Guid userId = Guid.Parse(requesterId);

            var friends = await context.Friends.Where(f => f.user_id_requester == userId || f.user_id_receiver == userId).ToListAsync();
            return friends;
        }

        public async Task<List<Friend>> GetAcceptedFriendsAsync(string requesterId) {
            Guid userId = Guid.Parse(requesterId);

            return await context.Friends
                .Where(f =>
                    (f.user_id_requester == userId || f.user_id_receiver == userId) &&
                    f.status == (int)FriendStatusEnum.Accepted)
                .ToListAsync();
        }

        public async Task<List<Friend>> GetPendingFriendsAsync(string requesterId) {
            Guid userId = Guid.Parse(requesterId);

            return await context.Friends
                .Where(f =>
                    (f.user_id_requester == userId || f.user_id_receiver == userId) &&
                    f.status == (int)FriendStatusEnum.Send)
                .ToListAsync();
        }

        public async Task<Friend?> GetFriendByUserAsync(User targetUser, string requesterId) {
            Guid userId = Guid.Parse(requesterId);

            return await context.Friends.FirstOrDefaultAsync(f => 
                (f.user_id_requester == targetUser.id || f.user_id_receiver == targetUser.id) &&
                (f.user_id_requester == userId || f.user_id_receiver == userId)
            );
        }

        public async Task<Friend> CreateFriendAsync(Friend friend) {
            context.Friends.Add(friend);
            await context.SaveChangesAsync();
            return friend;
        }

        public async Task<Friend> UpdateFriendAsync(Friend friend) {
            friend.updated_at = DateTime.UtcNow;
            context.Friends.Update(friend);
            await context.SaveChangesAsync();
            return friend;
        }

        public async Task RemoveFriendAsync(Friend friend) {
            context.Friends.Remove(friend);
            await context.SaveChangesAsync();
        }
    }
}
