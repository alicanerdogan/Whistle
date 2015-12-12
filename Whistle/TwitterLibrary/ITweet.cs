using System;
using System.Collections.Generic;
using TwitterLibrary.Entity;

namespace TwitterLibrary
{
    public enum TweetType
    {
        NONE,
        REPLY,
        RETWEET
    }

    public interface ITweet
    {
        string Content { get; }
        IEnumerable<HashTag> HashTags { get; }
        long Id { get; }
        int LikeCount { get; }
        IEnumerable<Media> Media { get; }
        IEnumerable<Mention> Mentions { get; }
        TwitterUser Owner { get; }
        string RawContent { get; }
        string RelativeTime { get; }
        int ReplyCount { get; }
        int RetweetCount { get; }
        TwitterUser Retweeter { get; }
        DateTime TimeStamp { get; }
        TweetType Type { get; }
        IEnumerable<URL> URLs { get; }
    }
}