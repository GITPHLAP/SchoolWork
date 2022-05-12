using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using WS_Server.Models;

namespace WS_Server.DAL
{
    public class TweetHandler
    {
        Random random = new Random();
        List<Data120161205TrumpTwitterAll> avaliableTweets;

        //public List<Data120161205TrumpTwitterAll> AllTweets { get => allTweets; }

        public TweetHandler()
        {
            avaliableTweets = GetTweetsFromJson();
        }

        private List<Data120161205TrumpTwitterAll> GetTweetsFromJson()
        {

            using (StreamReader r = new StreamReader("tweets.json"))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<JsonRoot>(json).Data120161205TrumpTwitterAll;

            }
        }

        public MyTweet GetRandomTweet()
        {

            Data120161205TrumpTwitterAll newTweet = avaliableTweets[random.Next(avaliableTweets.Count)];

            //Delete from avaliable tweets
            avaliableTweets.Remove(newTweet);

            // Convert to MyTweet object
            return new MyTweet { Date = DateTime.ParseExact($"{newTweet.Date} {newTweet.Time}",
                    "M/d/yyyy h:mm:ss tt", 
                    CultureInfo.InvariantCulture),
                Client = newTweet.Client,
                Tweet = newTweet.Tweet };
        }
    }
}
