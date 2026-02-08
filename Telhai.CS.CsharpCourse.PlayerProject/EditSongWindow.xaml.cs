using System.Windows;

namespace Telhai.CS.CsharpCourse.PlayerProject
{
    public partial class EditSongWindow : Window
    {
        public EditSongWindow(SongMetadata metadata)
        {
            InitializeComponent();
            DataContext = new EditSongViewModel(metadata);
        }
    }
}
