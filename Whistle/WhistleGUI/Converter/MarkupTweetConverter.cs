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
            string tweetContent = tweet.Content;

            var paragraph = new Paragraph();

            var orderedURLs = new List<URL>();

            var allURLs = new List<URL>();
            allURLs.AddRange(tweet.Media);
            allURLs.AddRange(tweet.HashTags);
            allURLs.AddRange(tweet.URLs);
            allURLs.AddRange(tweet.Mentions);

            if (allURLs.Count > 0)
            {
                var dict = new List<KeyValuePair<URL, int>>();
                allURLs.ForEach(url => dict.Add(new KeyValuePair<URL, int>(url, tweetContent.IndexOf(url.LinkText, StringComparison.InvariantCultureIgnoreCase))));
                dict.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
                dict.ForEach(pair => orderedURLs.Add(pair.Key));

                int pointer = 0;
                foreach (var url in orderedURLs)
                {
                    int linkIndex = tweetContent.IndexOf(url.LinkText, StringComparison.InvariantCultureIgnoreCase);
                    var preText = tweetContent.Substring(pointer, linkIndex - pointer);
                    paragraph.Inlines.Add(new Run(preText));
                    var hyperlink = new Hyperlink();
                    hyperlink.Inlines.Add(url.LinkText);
                    hyperlink.NavigateUri = url.Destination;
                    hyperlink.RequestNavigate += (sender, e) => Process.Start(e.Uri.ToString());
                    hyperlink.TextDecorations = null;
                    paragraph.Inlines.Add(hyperlink);
                    pointer = linkIndex + url.LinkText.Length;
                }
            }
            else
            {
                paragraph.Inlines.Add(new Run(tweetContent));
            }

            return paragraph.Inlines;
        }
    }
}
