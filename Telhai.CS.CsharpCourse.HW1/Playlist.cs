using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telhai.CS.CsharpCourse.HW1.Salma
{
    public class Playlist
    {
        private static int counter = 0;  // מונה ליצירת מזהה אוטומטי לכל שיר

        public int Id { get; }
        public string Name { get;  set; }
        public List<Song> Songs { get; set; }

        public Playlist()
        {
            counter++;
            Id = counter;
            Songs = new List<Song>();
        }

        public Playlist(string name) : this()
        {
            SetName(name);
        }

        public Playlist(string name, List<Song> songs) : this(name)
        {
            Songs = songs ?? new List<Song>();
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 3)
                throw new Exception("Playlist name must be at least 3 characters.");
            Name = name;
        }

       
        public void AddSong(Song s)
        {
            Songs.Add(s);
        }

        public void RemoveSong(int songId)
        {
            Song s = FindSong(songId);
            if (s != null)
                Songs.Remove(s);
            else
                throw new Exception("Song ID not found.");
        }

        public Song FindSong(int songId)
        {
            return Songs.Find(s => s.Id == songId);
        }


        // חישוב משך כל השירים
        public double GetTotalDuration()
        {
            double total = 0;
            foreach (var s in Songs)
                total += s.Duration;
            return total;
        }

        public void Print()
        {
            Console.WriteLine(ToString());
        }


        // מחרוזת המתארת את הפלייליסט והשירים שבו

        public override string ToString()
        {
            string info = $"Playlist: [ID={Id}, Name={Name}, Songs Count={Songs.Count}]\n";
            info += "Songs:\n";

            foreach (var s in Songs)
                info += "   " + s.ToString() + "\n";

            return info;

        }
    }
}
