using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Telhai.CS.CsharpCourse.PlayerProject.Services;

namespace Telhai.CS.CsharpCourse.PlayerProject
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private List<MusicTrack> library = new();
        private const string LIBRARY_FILE = "library.json";

        private MediaPlayer player = new();
        private int currentIndex = -1;

        private CancellationTokenSource _cts;

        private readonly ItunesService _itunesService = new();
        private readonly SongMetadataCacheService _cacheService = new();

        // slideshow
        private DispatcherTimer _slideTimer;
        private List<string> _slideImages = new();
        private int _slideIndex = 0;

        // UI properties
        public string SongTitle { get; set; } = "";
        public string ArtistName { get; set; } = "";
        public string AlbumName { get; set; } = "";
        public string FilePath { get; set; } = "";
        public ImageSource CoverImage { get; set; } = null;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string n) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            LoadLibrary();
            UpdateLibraryUI();

            CoverImage = null;
            OnPropertyChanged(nameof(CoverImage));
        }

        // ---------------- SETTINGS ----------------
        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow win = new();
            win.OnScanCompleted += tracks =>
            {
                foreach (var t in tracks)
                    if (!library.Any(x => x.FilePath == t.FilePath))
                        library.Add(t);

                SaveLibrary();
                UpdateLibraryUI();
            };
            win.ShowDialog();
        }

        // ---------------- SAVE/LOAD ----------------
        private void SaveLibrary()
        {
            File.WriteAllText(LIBRARY_FILE,
                JsonSerializer.Serialize(library, new JsonSerializerOptions { WriteIndented = true }));
        }

        private void LoadLibrary()
        {
            if (File.Exists(LIBRARY_FILE))
            {
                library = JsonSerializer.Deserialize<List<MusicTrack>>(
                    File.ReadAllText(LIBRARY_FILE)) ?? new List<MusicTrack>();
            }
        }

        private void UpdateLibraryUI()
        {
            songsListBox.ItemsSource = null;
            songsListBox.ItemsSource = library;
        }

        // ---------------- ADD/REMOVE ----------------
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog { Filter = "MP3 (*.mp3)|*.mp3" };
            if (dlg.ShowDialog() == true)
            {
                library.Add(new MusicTrack
                {
                    Title = Path.GetFileNameWithoutExtension(dlg.FileName),
                    FilePath = dlg.FileName
                });

                SaveLibrary();
                UpdateLibraryUI();
            }
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (songsListBox.SelectedItem is MusicTrack t)
            {
                library.Remove(t);
                SaveLibrary();
                UpdateLibraryUI();
            }
        }

        // ---------------- LISTBOX (single click) ----------------
        // requirement: single click shows metadata from JSON if exists, otherwise local title + path
        private void SongsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (songsListBox.SelectedItem is MusicTrack track)
            {
                FilePath = track.FilePath;
                OnPropertyChanged(nameof(FilePath));

                var meta = _cacheService.GetByPath(track.FilePath);
                if (meta != null)
                {
                    ApplyMetadata(meta);
                }
                else
                {
                    SongTitle = track.Title;
                    ArtistName = "";
                    AlbumName = "";
                    CoverImage = null;

                    OnPropertyChanged(nameof(SongTitle));
                    OnPropertyChanged(nameof(ArtistName));
                    OnPropertyChanged(nameof(AlbumName));
                    OnPropertyChanged(nameof(CoverImage));
                }
            }
        }

        // ---------------- LISTBOX (double click) ----------------
        private void SongsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (songsListBox.SelectedIndex != -1)
                PlayTrack(songsListBox.SelectedIndex);
        }

        // ---------------- PLAY BUTTON ----------------
        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (songsListBox.SelectedIndex != -1)
                PlayTrack(songsListBox.SelectedIndex);
        }

        private void BtnPause_Click(object sender, RoutedEventArgs e) => player.Pause();

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex < library.Count - 1)
                PlayTrack(currentIndex + 1);
        }

        private void BtnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex > 0)
                PlayTrack(currentIndex - 1);
        }

        private void volumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
            => player.Volume = volumeSlider.Value;

        // ---------------- PLAY TRACK ----------------
        private async void PlayTrack(int index)
        {
            if (index < 0 || index >= library.Count) return;

            StopSlideShow();

            currentIndex = index;
            var track = library[index];

            player.Open(new Uri(track.FilePath));
            player.Volume = volumeSlider.Value;
            player.Play();

            // show local info immediately
            FilePath = track.FilePath;
            SongTitle = track.Title;
            ArtistName = "";
            AlbumName = "";
            CoverImage = null;

            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(SongTitle));
            OnPropertyChanged(nameof(ArtistName));
            OnPropertyChanged(nameof(AlbumName));
            OnPropertyChanged(nameof(CoverImage));

            // if cached -> show immediately (NO API)
            var cached = _cacheService.GetByPath(track.FilePath);
            if (cached != null)
            {
                ApplyMetadata(cached);
                return;
            }

            // cancel old API call
            _cts?.Cancel();
            _cts = new CancellationTokenSource();

            await LoadMetadataAsync(track, _cts.Token);
        }

        // ---------------- API HELPERS ----------------
        private string BuildSearchTerm(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return "";

            string s = fileName;

            s = s.Replace("ytmp3free.cc", "");
            s = s.Replace("youtube", "");
            s = s.Replace("official", "");
            s = s.Replace("video", "");

            s = s.Replace("_", " ");
            s = s.Replace("-", " ");

            while (s.Contains("  "))
                s = s.Replace("  ", " ");

            return s.Trim();
        }

        private async Task LoadMetadataAsync(MusicTrack track, CancellationToken token)
        {
            try
            {
                string term = BuildSearchTerm(track.Title);

                var r = await _itunesService.SearchOneAsync(term, token);
                if (r == null) return;

                var meta = new SongMetadata
                {
                    FilePath = track.FilePath,
                    Title = r.TrackName ?? track.Title,
                    Artist = r.ArtistName ?? "",
                    Album = r.AlbumName ?? "",
                    CoverUrl = r.ArtworkUrl ?? "",
                    ExtraImagePaths = new List<string>()
                };

                _cacheService.SaveOrUpdate(meta);

                // update UI only if still same song
                if (currentIndex >= 0 && currentIndex < library.Count &&
                    library[currentIndex].FilePath == track.FilePath)
                {
                    ApplyMetadata(meta);
                }
            }
            catch
            {
                // requirement: on API error show local title + full path
                SongTitle = track.Title;
                FilePath = track.FilePath;

                OnPropertyChanged(nameof(SongTitle));
                OnPropertyChanged(nameof(FilePath));
            }
        }

        private void ApplyMetadata(SongMetadata m)
        {
            SongTitle = m.Title;
            ArtistName = m.Artist;
            AlbumName = m.Album;

            OnPropertyChanged(nameof(SongTitle));
            OnPropertyChanged(nameof(ArtistName));
            OnPropertyChanged(nameof(AlbumName));

            StartSlideShow(m);
        }

        // ---------------- SLIDE SHOW ----------------
        private void StartSlideShow(SongMetadata meta)
        {
            StopSlideShow();

            if (meta.ExtraImagePaths != null && meta.ExtraImagePaths.Count > 0)
                _slideImages = meta.ExtraImagePaths;
            else if (!string.IsNullOrWhiteSpace(meta.CoverUrl))
                _slideImages = new List<string> { meta.CoverUrl };
            else
            {
                CoverImage = null;
                OnPropertyChanged(nameof(CoverImage));
                return;
            }

            _slideIndex = 0;
            ShowSlideImage();

            _slideTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(3) };
            _slideTimer.Tick += (s, e) => NextSlide();
            _slideTimer.Start();
        }

        private void StopSlideShow()
        {
            _slideTimer?.Stop();
            _slideTimer = null;
        }

        private void NextSlide()
        {
            if (_slideImages.Count == 0) return;
            _slideIndex = (_slideIndex + 1) % _slideImages.Count;
            ShowSlideImage();
        }

        private void ShowSlideImage()
        {
            string img = _slideImages[_slideIndex];

            if (img.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                CoverImage = new BitmapImage(new Uri(img, UriKind.Absolute));
            else
                CoverImage = new BitmapImage(new Uri(img, UriKind.RelativeOrAbsolute));

            OnPropertyChanged(nameof(CoverImage));
        }

        // ---------------- EDIT WINDOW ----------------
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (songsListBox.SelectedItem is not MusicTrack t)
                return;

            var meta = _cacheService.GetByPath(t.FilePath) ?? new SongMetadata
            {
                FilePath = t.FilePath,
                Title = t.Title,
                Artist = "",
                Album = "",
                CoverUrl = "",
                ExtraImagePaths = new List<string>()
            };

            _cacheService.SaveOrUpdate(meta);

            EditSongWindow win = new EditSongWindow(meta);
            win.Owner = this;
            win.ShowDialog();

            // refresh after edit
            var updated = _cacheService.GetByPath(t.FilePath);
            if (updated != null)
                ApplyMetadata(updated);
        }
    }
}
