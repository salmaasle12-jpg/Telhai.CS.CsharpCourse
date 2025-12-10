using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telhai.CS.CsharpCourse.HW1.Salma
{

    // Enum של סוגי מוזיקה
    public enum Genre
    {
        Pop, Rock, Jazz, Classical, HipHop, Electronic, Other
    }
    public class Song
    {

        private static int counter = 0; // מונה ליצירת מזהה אוטומטי לכל שיר
        public int Id { get; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public int Year { get; set; }
        public double Duration { get; set; }
        public Genre Genre { get; set; }

        public Song()
        {
            counter++;
            Id = counter;
        }


        public Song(string title, string artist, int year, double duration, Genre genre) : this()
        {
            setTitle(title);
            SetArtist(artist);
            SetYear(year);
            SetDuration(duration);
            SetGenre(genre);
        }

        // ---------- בדיקות ולידציה ----------

        private void setTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title) || title.Length < 2)
                throw new Exception("Title must be at least 2 characters and not empty!");
            Title = title;
        }

        private void SetArtist(string artist)
        {
            if (string.IsNullOrWhiteSpace(artist))
                throw new Exception("Artist cannot be empty.");
            Artist = artist;
        }

        private void SetYear(int year)
        {
            if (year < 1900 || year > 2025)
                throw new Exception("Year must be between 1900 and 2025!");
            Year = year;
        }

        private void SetDuration(double duration)
        {
            if (duration <= 0 || duration > 20)
                throw new Exception("Duration must be > 0 and ≤ 20 minutes.");
            Duration = duration;
        }

        private void SetGenre(Genre genre)
        {
            Genre = genre;
        }

   

        public void Print()
        {
            Console.WriteLine(ToString());
        }

        public override string ToString()
        {
            return $"Song: [[{Id}], Title={Title}, Artist={Artist}, Year={Year}, [{Duration}] min, Genre={Genre}]";
        }
    }
}
