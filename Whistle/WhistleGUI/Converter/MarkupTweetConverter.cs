using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using TwitterLibrary;
using TwitterLibrary.Entity;

namespace WhistleGUI.Converter
{
    public static class MarkupTweetConverter
    {
        public static InlineCollection Convert(ITweet tweet)
        {
            string tweetContent = RemoveMediaLinksFromContent(tweet.Content, tweet.Media);
            var orderedURLs = GetSortedURLsByAppereanceOrder(tweetContent, tweet.HashTags, tweet.Mentions, tweet.URLs);

            var paragraph = new Paragraph();
            int pointer = 0;

            foreach (var url in orderedURLs)
            {
                int linkIndex = tweetContent.IndexOf(url.LinkText, StringComparison.InvariantCultureIgnoreCase);
                var preText = tweetContent.Substring(pointer, linkIndex - pointer);
                paragraph.Inlines.Add(new Run(preText));
                Hyperlink hyperlink = CreateHyperlink(url);
                paragraph.Inlines.Add(hyperlink);
                pointer = linkIndex + url.LinkText.Length;
            }

            if (paragraph.Inlines.Count <= 0)
            {
                paragraph.Inlines.Add(new Run(tweetContent));
            }

            return paragraph.Inlines;
        }

        private static Hyperlink CreateHyperlink(URL url)
        {
            var hyperlink = new Hyperlink();
            if (url is HashTag || url is Mention)
            {
                hyperlink.Inlines.Add(url.LinkText);
            }
            else
            {
                hyperlink.Inlines.Add(url.Destination.Host + url.Destination.PathAndQuery);
            }
            hyperlink.NavigateUri = url.Destination;
            hyperlink.RequestNavigate += (sender, e) => Process.Start(e.Uri.ToString());
            hyperlink.TextDecorations = null;
            return hyperlink;
        }

        private static string RemoveMediaLinksFromContent(string tweetContent, IEnumerable<Media> mediaLinks)
        {
            foreach (var media in mediaLinks)
            {
                tweetContent = tweetContent.Replace(media.LinkText, String.Empty);
            }
            return tweetContent;
        }

        private static IEnumerable<URL> GetSortedURLsByAppereanceOrder(string tweetContent, IEnumerable<HashTag> hashtags, IEnumerable<Mention> mentions, IEnumerable<URL> urls)
        {
            var orderedURLs = new List<URL>();

            var allURLs = new List<URL>();
            allURLs.AddRange(hashtags);
            allURLs.AddRange(urls);
            allURLs.AddRange(mentions);

            if (allURLs.Count > 0)
            {
                var dict = new List<KeyValuePair<URL, int>>();
                allURLs.ForEach(url => dict.Add(new KeyValuePair<URL, int>(url, tweetContent.IndexOf(url.LinkText, StringComparison.InvariantCultureIgnoreCase))));
                dict.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
                dict.ForEach(pair => orderedURLs.Add(pair.Key));
            }

            return orderedURLs;
        }
    }
}
