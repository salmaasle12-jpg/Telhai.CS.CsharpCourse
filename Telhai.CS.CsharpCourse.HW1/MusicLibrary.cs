using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telhai.CS.CsharpCourse.HW1.Salma
{
    public class MusicLibrary
    {
        public List<Playlist> Playlists { get; set; } // רשימת פלייליסטים במערכת

        public MusicLibrary()
        {
            Playlists = new List<Playlist>();
        }

        public void AddPlaylist(Playlist p)
        {
            Playlists.Add(p);
        }
        // החזרת פלייליסט לפי ID
        public Playlist GetPlaylist(int id)
        {
            return Playlists.Find(p => p.Id == id);
        }
        // מחיקת פלייליסט לפי ID
        public void RemovePlaylist(int id)
        {
            Playlist p = GetPlaylist(id);
            if (p != null)
                Playlists.Remove(p);
            else
                throw new Exception("Playlist not found!");
        }
        // החזרת שמות כל הפלייליסטים
        public List<string> GetPlaylistNames()
        {
            List<string> names = new List<string>();
            foreach (var p in Playlists)
                names.Add(p.Name);
            return names;
        }
    }
}
