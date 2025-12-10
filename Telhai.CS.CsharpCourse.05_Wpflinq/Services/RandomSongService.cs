using System;
using System.Collections.Generic;
using Telhai.CS.CsharpCourse._05_WpfLinq.Models;

namespace Telhai.CS.CsharpCourse._05_WpfLinq.Services
{
    // Service that generates random songs (Singleton)
    public class RandomSongService : ISongService
    {
        // Single instance (Singleton pattern)
        private static readonly RandomSongService _instance = new RandomSongService();
        public static RandomSongService Instance => _instance;

        // Random generator
        private Random rnd = new Random();

        // Sample artists list
        private string[] artists =
        {
            "Adele","Coldplay","Queen","ABBA","Arik Einstein",
            "Beatles","Madonna","Eyal Golan"
        };

        // Sample song titles list
        private string[] titles =
        {
            "Hello","Fix You","Bohemian","Dancing Queen","Song A",
            "Song B","Song C","Song D","Song E","Song F","Song G"
        };

        // Private constructor (Singleton)
        private RandomSongService() { }

        // Generates a list of random songs
        public List<Song> GenerateSongs(int count)
        {
            List<Song> list = new List<Song>();

            // Create 'count' random songs
            for (int i = 0; i < count; i++)
            {
                list.Add(new Song
                {
                    // Unique song ID
                    Id = Guid.NewGuid(),

                    // Random artist
                    Artist = artists[rnd.Next(artists.Length)],

                    // Random title
                    Title = titles[rnd.Next(titles.Length)],

                    // Random duration between 2 and 10 minutes
                    Duration = (float)Math.Round(2.0 + rnd.NextDouble() * 8.0, 2)
                });
            }

            return list;
        }
    }
}

