using System.Globalization;

namespace Telhai.CS.CsharpCourse.HW1.Salma
{
    public class Program
    {
        static MusicLibrary library = new MusicLibrary();
        static List<Song> allSongs = new List<Song>();

        static void Main(string[] args)
        {
            SeedSampleData();

            while (true)
            {
                ShowMenu();
                Console.Write("Choose option: ");
                var input = Console.ReadLine();
                Console.WriteLine();

                try
                {
                    switch (input)
                    {
                        case "1": CreateNewPlaylist(); break;
                        case "2": CreateNewSong(); break;
                        case "3": AddSongToPlaylist(); break;
                        case "4": ShowPlaylistSongs(); break;
                        case "5": RemoveSongFromPlaylistById(); break;
                        case "6": ListAllPlaylists(); break;
                        case "8": RemovePlaylistById(); break;
                        case "0": Console.WriteLine("Exiting..."); return;
                        default: Console.WriteLine("Invalid option."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("=== (By Salma Asle 326184082 - מוזיקה ספריית תפריט) ===");
            Console.WriteLine("1  - Create new playlist");
            Console.WriteLine("2  - Create new song");
            Console.WriteLine("3  - Add song to existing playlist");
            Console.WriteLine("4  - Show all songs in a playlist");
            Console.WriteLine("5  - Remove song by Id from playlist");
            Console.WriteLine("6  - List all playlists");
            Console.WriteLine("8  - Remove playlist by Id");
            Console.WriteLine("0  - Exit");
            Console.WriteLine();
        }

        // ------------------------- SEED DATA -------------------------

        static void SeedSampleData()
        {
            try
            {
                var s1 = new Song("Yesterday", "The Beatles", 1965, 2.45, Genre.Rock);
                var s2 = new Song("Blue in Green", "Miles Davis", 1959, 5.37, Genre.Jazz);
                var s3 = new Song("Symphony No.5", "Beethoven", 1808, 7.2, Genre.Classical);

                allSongs.AddRange(new[] { s1, s2, s3 });

                var p1 = new Playlist("Favorites");
                p1.AddSong(s1);
                p1.AddSong(s2);

                var p2 = new Playlist("Classics");
                p2.AddSong(s3);

                library.AddPlaylist(p1);
                library.AddPlaylist(p2);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Seeding error: " + ex.Message);
            }
        }

        // ------------------------- CREATE SONG -------------------------

        static void CreateNewSong()
        {
            Console.Write("Title: ");
            var title = Console.ReadLine();
            Console.Write("Artist: ");
            var artist = Console.ReadLine();
            Console.Write("Year: ");
            var yearStr = Console.ReadLine();
            Console.Write("Duration (minutes): ");
            var durStr = Console.ReadLine();

            Console.WriteLine("Genres: " + string.Join(", ", Enum.GetNames(typeof(Genre))));
            Console.Write("Genre: ");
            var genreStr = Console.ReadLine();

            if (!int.TryParse(yearStr, out int year))
                throw new Exception("Invalid year format.");
            if (!double.TryParse(durStr, NumberStyles.Float, CultureInfo.InvariantCulture, out double duration))
                throw new Exception("Invalid duration format.");
            if (!Enum.TryParse<Genre>(genreStr, true, out Genre genre))
                throw new Exception("Invalid genre.");

            var s = new Song(title, artist, year, duration, genre);
            allSongs.Add(s);

            Console.WriteLine("Song created:");
            Console.WriteLine(s.ToString());
        }

        // ------------------------- CREATE PLAYLIST -------------------------

        static void CreateNewPlaylist()
        {
            Console.Write("Enter playlist name: ");
            var name = Console.ReadLine();

            var p = new Playlist(name);
            library.AddPlaylist(p);

            Console.WriteLine($"Playlist created: {p}");
        }

        // ------------------------- ADD SONG TO PLAYLIST -------------------------

        static void AddSongToPlaylist()
        {
            Console.Write("Playlist Id: ");
            if (!int.TryParse(Console.ReadLine(), out int pid))
                throw new Exception("Invalid playlist id.");

            var playlist = library.GetPlaylist(pid);
            if (playlist == null)
            {
                Console.WriteLine("Playlist not found.");
                return;
            }

            Console.WriteLine("\nAvailable songs:");
            foreach (var s in allSongs)
                Console.WriteLine(s.ToString());

            Console.Write("Song Id to add: ");
            if (!int.TryParse(Console.ReadLine(), out int sid))
                throw new Exception("Invalid song id.");

            Song song = allSongs.FirstOrDefault(x => x.Id == sid);
            if (song == null)
            {
                Console.WriteLine("Song not found.");
                return;
            }

            playlist.AddSong(song);
            Console.WriteLine("Song added successfully.");
        }

        // ------------------------- SHOW SONGS IN PLAYLIST -------------------------

        static void ShowPlaylistSongs()
        {
            Console.Write("Playlist Id: ");
            if (!int.TryParse(Console.ReadLine(), out int pid))
                throw new Exception("Invalid playlist id.");

            Playlist playlist = library.GetPlaylist(pid);
            if (playlist == null)
            {
                Console.WriteLine("Playlist not found.");
                return;
            }

            playlist.Print();
            Console.WriteLine($"Total Duration = {playlist.GetTotalDuration()} minutes");
        }

        // ------------------------- REMOVE SONG -------------------------

        static void RemoveSongFromPlaylistById()
        {
            Console.Write("Playlist Id: ");
            if (!int.TryParse(Console.ReadLine(), out int pid))
                throw new Exception("Invalid playlist id.");

            var playlist = library.GetPlaylist(pid);
            if (playlist == null)
            {
                Console.WriteLine("Playlist not found.");
                return;
            }

            Console.Write("Song Id to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int sid))
                throw new Exception("Invalid song id.");

            playlist.RemoveSong(sid);
            Console.WriteLine("Song removed.");
        }

        // ------------------------- LIST PLAYLISTS -------------------------

        static void ListAllPlaylists()
        {
            Console.WriteLine("Playlists:");
            foreach (var p in library.Playlists)
                Console.WriteLine(p.ToString());
        }

        // ------------------------- REMOVE PLAYLIST -------------------------

        static void RemovePlaylistById()
        {
            Console.Write("Playlist Id: ");
            if (!int.TryParse(Console.ReadLine(), out int pid))
                throw new Exception("Invalid playlist id.");

            library.RemovePlaylist(pid);

            Console.WriteLine("Playlist removed.");
        }
    }
}
