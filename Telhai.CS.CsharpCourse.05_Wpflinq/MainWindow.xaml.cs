/*
 Name: Salma Asle
 ID: 326184082
 
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Telhai.CS.CsharpCourse._05_WpfLinq.Models;
using Telhai.CS.CsharpCourse._05_WpfLinq.Services;

namespace Telhai.CS.CsharpCourse._05_WpfLinq
{
    public partial class MainWindow : Window
    {
        ISongService service = RandomSongService.Instance;
        List<Song> songs = new List<Song>();

        public MainWindow()
        {
            InitializeComponent();
        }

        // LOAD 50 SONGS
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            songs = service.GenerateSongs(50);
            songsListBox.ItemsSource = songs;
            UpdateStats(songs);
        }

        // SORT BY ARTIST
        private void btnSortArtist_Click(object sender, RoutedEventArgs e)
        {
            var sorted = songs.OrderBy(s => s.Artist).ThenBy(s => s.Title).ToList();
            songsListBox.ItemsSource = sorted;
            UpdateStats(sorted);
        }

        // SORT BY DURATION
        private void btnSortDuration_Click(object sender, RoutedEventArgs e)
        {
            var sorted = songs.OrderBy(s => s.Duration).ToList();
            songsListBox.ItemsSource = sorted;
            UpdateStats(sorted);
        }

        // SHORT HITS < 3
        private void btnShortHits_Click(object sender, RoutedEventArgs e)
        {
            var shortHits = songs.Where(s => s.Duration < 3).OrderBy(s => s.Duration).ToList();
            songsListBox.ItemsSource = shortHits;
            UpdateStats(shortHits);
        }

        // GROUP BY ARTIST
        private void btnGroupByArtist_Click(object sender, RoutedEventArgs e)
        {
            var msg = songs
                .GroupBy(s => s.Artist)
                .Select(g => $"{g.Key}: {g.Count()} songs");

            MessageBox.Show(string.Join(Environment.NewLine, msg));
        }

        // ADD SONG
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!float.TryParse(txtDuration.Text, out float d) || d < 1 || d > 10)
            {
                MessageBox.Show("Duration must be between 1 and 10");
                return;
            }

            songs.Add(new Song
            {
                Id = Guid.NewGuid(),
                Artist = txtArtist.Text,
                Title = txtTitle.Text,
                Duration = d
            });

            songsListBox.ItemsSource = null;
            songsListBox.ItemsSource = songs;
            UpdateStats(songs);

            txtArtist.Text = txtTitle.Text = txtDuration.Text = "";
        }

        // SEARCH
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string term = txtSearch.Text.ToLower();
            var result = songs
                .Where(s => s.Artist.ToLower().Contains(term) ||
                            s.Title.ToLower().Contains(term))
                .ToList();

            songsListBox.ItemsSource = result;
            UpdateStats(result);
        }

        // STATISTICS
        private void UpdateStats(List<Song> list)
        {
            if (list.Count == 0)
            {
                txtTotalDuration.Text = txtAverageLength.Text = txtLongestSong.Text = "-";
                return;
            }

            txtTotalDuration.Text = list.Sum(s => s.Duration).ToString("0.00");
            txtAverageLength.Text = list.Average(s => s.Duration).ToString("0.00");

            var longest = list.OrderByDescending(s => s.Duration).First();
            txtLongestSong.Text = $"{longest.Title} ({longest.Duration:0.00})";
        }
    }
}


