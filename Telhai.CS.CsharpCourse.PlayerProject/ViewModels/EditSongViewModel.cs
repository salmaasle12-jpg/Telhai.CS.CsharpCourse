using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Telhai.CS.CsharpCourse.PlayerProject
{
    public class EditSongViewModel : INotifyPropertyChanged
    {
        private readonly SongMetadataCacheService _cache;

        public string FilePath { get; }

        public string Title { get; set; }
        public string Artist { get; }
        public string Album { get; }

        public ObservableCollection<string> ExtraImages { get; set; }

        public ImageSource CoverImage { get; }

        public ICommand AddImageCommand { get; }
        public ICommand RemoveImageCommand { get; }
        public ICommand SaveCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public EditSongViewModel(SongMetadata meta)
        {
            _cache = new SongMetadataCacheService();

            FilePath = meta.FilePath;
            Title = meta.Title;
            Artist = meta.Artist;
            Album = meta.Album;

            ExtraImages = new ObservableCollection<string>(meta.ExtraImagePaths ?? new());

            if (!string.IsNullOrWhiteSpace(meta.CoverUrl))
                CoverImage = new BitmapImage(new System.Uri(meta.CoverUrl));

            AddImageCommand = new RelayCommand(AddImage);
            RemoveImageCommand = new RelayCommand(RemoveImage, () => ExtraImages.Count > 0);
            SaveCommand = new RelayCommand(Save);
        }

        private void AddImage()
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Images (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg"
            };

            if (dlg.ShowDialog() == true)
                ExtraImages.Add(dlg.FileName);
        }

        private void RemoveImage()
        {
            if (ExtraImages.Count > 0)
                ExtraImages.RemoveAt(ExtraImages.Count - 1);
        }

        private void Save()
        {
            var old = _cache.GetByPath(FilePath);

            var meta = new SongMetadata
            {
                FilePath = FilePath,
                Title = Title,
                Artist = old?.Artist ?? Artist,
                Album = old?.Album ?? Album,
                CoverUrl = old?.CoverUrl ?? "",
                ExtraImagePaths = ExtraImages.ToList()
            };

            _cache.SaveOrUpdate(meta);
        }
    }
}
