using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace TwitterLibrary
{
    public class TwitterUser
    {
        public string AvatarURL { get; private set; }
        public string BackgroundURL { get; private set; }
        public string Description { get; private set; }
        public int FollowersCount { get; private set; }
        public int FollowingCount { get; private set; }
        public bool? IsProtected { get; private set; }
        public bool? IsVerified { get; private set; }
        public string Username { get; private set; }
        public string DisplayName { get; private set; }
        public int TweetCount { get; private set; }
        public long Id { get; private set; }

        public TwitterUser(TweetSharp.TwitterUser user)
        {
            Id = user.Id;
            Username = user.ScreenName;
            DisplayName = user.Name;
            Description = user.Description;
            AvatarURL = user.ProfileImageUrl;
            BackgroundURL = user.ProfileBackgroundImageUrl;
            TweetCount = user.StatusesCount;
            FollowingCount = user.FriendsCount;
            FollowersCount = user.FollowersCount;
            IsProtected = user.IsProtected;
            IsVerified = user.IsVerified;
        }
    }
}
