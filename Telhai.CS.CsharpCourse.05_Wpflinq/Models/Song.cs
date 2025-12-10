using System;

namespace Telhai.CS.CsharpCourse._05_WpfLinq.Models
{
    public class Song
    {
        public Guid Id { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public float Duration { get; set; }

        public override string ToString()
        {
            return $"{Id} | {Artist} | {Title} | {Duration} min";
        }
    }
}

