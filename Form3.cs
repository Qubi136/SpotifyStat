using SpotifyAPI.Web;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using System.Configuration;
using static SpotifyStat.DBControl;
using System.Resources;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using System.Globalization;

namespace SpotifyStat
{
    public partial class Form3 : Form
    {
        private FullTrack? PreviousTrack;
        private SpotifyService _spotifyService;
        private PrivateUser user;
        private ImageList imageList;
        private System.Windows.Forms.Timer _checkCurrentTrackTimer;
        private DateTime _trackStartTime;
        private TimeSpan _listeningTime;
        private DBControl DBtime;
        private bool isAuthorized = false;
        private ResourceManager resManager;

        public Form3(SpotifyService sps)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            _spotifyService = sps;
            DBtime = new DBControl();
            LoadUserProfile();
            this.FormClosed += Form3_FormClosed;
            InitializeListView();
            InitializeTimer();
        }

        private void InitializeListView()
        {
            listView1.View = View.Details;
            imageList = new ImageList();
            imageList.ImageSize = new Size(40, 40);
            listView1.SmallImageList = imageList;
        }

        private void MakePictureBoxRound(PictureBox pictureBox)
        {
            Bitmap bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                GraphicsPath path = new GraphicsPath();
                path.AddEllipse(0, 0, pictureBox.Width, pictureBox.Height);

                pictureBox.Region = new Region(path);

                g.Clear(pictureBox.BackColor);

                g.DrawImage(pictureBox.Image, 0, 0, pictureBox.Width, pictureBox.Height);
            }
            pictureBox.Image = bitmap;
        }

        private async Task LoadUserProfile()
        {
            try
            {
                user = await _spotifyService.GetCurrentUserProfile();
                string profileImageUrl = user.Images.Count > 0 ? user.Images[0].Url : "https://cdn-icons-png.freepik.com/512/634/634795.png";
                string userName = user.DisplayName;

                labelUserName.Text = userName;
                pictureBox1.Load(profileImageUrl);
                MakePictureBoxRound(pictureBox1);
                ElementsOrder();
                Show();

                this.TopMost = true;
                isAuthorized = true;
                this.TopMost = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user profile: {ex.Message}");
            }
        }
        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            TimeSpan playedTime = TimeSpan.FromSeconds(Math.Round(_listeningTime.TotalSeconds));
            if (user != null && PreviousTrack != null)
            {
                DBtime.UpdateOverallListeningTime(user.Id, playedTime);
                DBtime.UpdateLocalListeningTime(user.Id, PreviousTrack.Id, "Track", playedTime);
                DBtime.UpdateLocalListeningTime(user.Id, PreviousTrack.Album.Id, "Album", playedTime);
            }
            Application.Exit();
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            listView1.Columns.Clear();
            listView1.Items.Clear();
            listView1.Columns.Add(resManager.GetString("TrackName"), Int32.Parse(resManager.GetString("TopTrackNameLength")));
            listView1.Columns.Add(resManager.GetString("Artist"), Int32.Parse(resManager.GetString("ArtistLength")));
            listView1.Columns.Add(resManager.GetString("Album"), Int32.Parse(resManager.GetString("AlbumLength")));
            listView1.Columns.Add(resManager.GetString("Duration"), Int32.Parse(resManager.GetString("DurationLength")));
            string selectedTimeRange = ComboBox1.SelectedItem?.ToString() ?? "For 4 weeks";
            var topTracksResponse = await _spotifyService.GetUsersTopTracks(selectedTimeRange);
            foreach (var track in topTracksResponse.Items)
            {
                TimeSpan duration = TimeSpan.FromMilliseconds(track.DurationMs);
                string albumImageUrl = track.Album.Images.FirstOrDefault()?.Url;
                if (!string.IsNullOrEmpty(albumImageUrl))
                {
                    using (var wc = new WebClient())
                    {
                        byte[] bytes = wc.DownloadData(albumImageUrl);
                        using (var ms = new System.IO.MemoryStream(bytes))
                        {
                            imageList.Images.Add(track.Id, System.Drawing.Image.FromStream(ms));
                        }
                    }
                }
                ListViewItem item = new ListViewItem(new string[]
                {
                    track.Name,
                    track.Artists[0].Name,
                    track.Album.Name,
                    $"{duration.Minutes}:{duration.Seconds:D2}",
                });
                if (imageList.Images.ContainsKey(track.Id))
                {
                    item.ImageKey = track.Id;
                }

                item.Tag = track.ExternalUrls["spotify"];
                listView1.Items.Add(item);
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var item = listView1.GetItemAt(e.X, e.Y);
                if (item != null && item.Tag != null)
                {
                    string trackUrl = item.Tag.ToString();
                    try
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = trackUrl,
                            UseShellExecute = true
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка открытия ссылки: {ex.Message}");
                    }
                }
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            listView1.Columns.Clear();
            listView1.Items.Clear();
            listView1.Columns.Add(resManager.GetString("ArtistName"), Int32.Parse(resManager.GetString("ArtistNameLength")));
            listView1.Columns.Add(resManager.GetString("Genre"), Int32.Parse(resManager.GetString("GenreLegnth")));
            listView1.Columns.Add(resManager.GetString("Popularity"), Int32.Parse(resManager.GetString("PopularityLength")));
            listView1.Columns.Add(resManager.GetString("Followers"), Int32.Parse(resManager.GetString("FollowersLength")));
            string selectedTimeRange = ComboBox2.SelectedItem?.ToString() ?? "For 4 weeks";
            var topArtistsResponse = await _spotifyService.GetUsersTopArtists(selectedTimeRange);
            foreach (var artist in topArtistsResponse.Items)
            {
                string albumImageUrl = artist.Images.FirstOrDefault()?.Url;
                if (!string.IsNullOrEmpty(albumImageUrl))
                {
                    using (var wc = new WebClient())
                    {
                        byte[] bytes = wc.DownloadData(albumImageUrl);
                        using (var ms = new System.IO.MemoryStream(bytes))
                        {
                            imageList.Images.Add(artist.Id, System.Drawing.Image.FromStream(ms));
                        }
                    }
                }
                ListViewItem item = new ListViewItem(new string[]
                {
                    artist.Name,
                    artist.Genres.Count > 0 ? artist.Genres[0] : "No genre",
                    artist.Popularity.ToString(),
                    artist.Followers.Total.ToString()
                });
                if (imageList.Images.ContainsKey(artist.Id))
                {
                    item.ImageKey = artist.Id;
                }

                item.Tag = artist.ExternalUrls["spotify"];
                listView1.Items.Add(item);
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            listView1.Columns.Clear();
            listView1.Items.Clear();
            listView1.Columns.Add(resManager.GetString("TrackName"), Int32.Parse(resManager.GetString("HistoryTrackNameLength")));
            listView1.Columns.Add(resManager.GetString("Artist"), Int32.Parse(resManager.GetString("ArtistLength")));
            listView1.Columns.Add(resManager.GetString("Album"), Int32.Parse(resManager.GetString("AlbumLength")));
            listView1.Columns.Add(resManager.GetString("Duration"), Int32.Parse(resManager.GetString("DurationLength")));
            listView1.Columns.Add(resManager.GetString("Date"), 72);
            var topTracksResponse = await _spotifyService.GetHistory();
            foreach (var historyItem in topTracksResponse.Items)
            {
                TimeSpan duration = TimeSpan.FromMilliseconds(historyItem.Track.DurationMs);
                string albumImageUrl = historyItem.Track.Album.Images.FirstOrDefault()?.Url;
                if (!string.IsNullOrEmpty(albumImageUrl))
                {
                    using (var wc = new WebClient())
                    {
                        byte[] bytes = wc.DownloadData(albumImageUrl);
                        using (var ms = new System.IO.MemoryStream(bytes))
                        {
                            imageList.Images.Add(historyItem.Track.Id, System.Drawing.Image.FromStream(ms));
                        }
                    }
                }
                ListViewItem item = new ListViewItem(new string[]
                {
                    historyItem.Track.Name,
                    historyItem.Track.Artists[0].Name,
                    historyItem.Track.Album.Name,
                    $"{duration.Minutes}:{duration.Seconds:D2}",
                    historyItem.PlayedAt.ToString()
                });
                if (imageList.Images.ContainsKey(historyItem.Track.Id))
                {
                    item.ImageKey = historyItem.Track.Id;
                }

                item.Tag = historyItem.Track.ExternalUrls["spotify"];
                listView1.Items.Add(item);
            }
        }

        private void InitializeTimer()
        {
            _checkCurrentTrackTimer = new System.Windows.Forms.Timer();
            _checkCurrentTrackTimer.Interval = 500;
            _checkCurrentTrackTimer.Tick += CheckCurrentTrack;
            _checkCurrentTrackTimer.Start();
        }

        private async void CheckCurrentTrack(object sender, EventArgs e)
        {
            if (isAuthorized == false)
            {
                return;
            }
            else
            {
                try
                {
                    var currentPlaying = await _spotifyService.GetCurrentlyPlayingTrack();

                    if (currentPlaying != null && currentPlaying.IsPlaying)
                    {
                        var track = currentPlaying.Item as FullTrack;
                        if (track != null)
                        {
                            if (PreviousTrack == null || PreviousTrack.Id != track.Id)
                            {
                                ResetListeningTime(false, PreviousTrack);
                                PreviousTrack = track;
                                _trackStartTime = DateTime.Now;
                            }
                            else
                            {
                                _listeningTime = DateTime.Now - _trackStartTime;
                                DisplayCurrentTrack(track);
                            }
                        }
                        else
                        {
                            var resMang = new ResourceManager("SpotifyStat.Form3", typeof(Form3).Assembly);
                            label1.Text = resMang.GetString("label1.Text");
                            ListeningTime.Text = "";
                            newText();
                            PictureOfSong.Image = null;
                            ResetListeningTime(false, PreviousTrack);
                        }
                    }
                    else
                    {
                        var resMang = new ResourceManager("SpotifyStat.Form3", typeof(Form3).Assembly);
                        label1.Text = resMang.GetString("label1.Text");
                        ListeningTime.Text = "";
                        newText();
                        PictureOfSong.Image = null;
                        ResetListeningTime(false, PreviousTrack);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error checking current track: {ex.Message}");
                }
            }
        }

        private void ResetListeningTime(bool IsPlaying, FullTrack track)
        {
            if (!IsPlaying)
            {
                TimeSpan playedTime = TimeSpan.FromSeconds(Math.Round(_listeningTime.TotalSeconds));
                if (user != null && track != null)
                {
                    DBtime.UpdateOverallListeningTime(user.Id, playedTime);
                    DBtime.UpdateLocalListeningTime(user.Id, track.Id, "Track", playedTime);
                    DBtime.UpdateLocalListeningTime(user.Id, track.Album.Id, "Album", playedTime);
                }
            }
            label2.Text = "00:00";
            trackState.Value = 0;
            PlayResume.Image = Properties.Resources.Кнопка_играть;
            PreviousTrack = null;
            _listeningTime = TimeSpan.Zero;
        }

        private async void DisplayCurrentTrack(FullTrack track)
        {
            PictureOfSong.Load(track.Album.Images[0].Url);
            TimeSpan roundedListeningTime = TimeSpan.FromSeconds(Math.Round(_listeningTime.TotalSeconds));
            string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}",
                roundedListeningTime.Hours,
                roundedListeningTime.Minutes,
                roundedListeningTime.Seconds);
            var playback = await _spotifyService.GetPlayback();
            var duration = track.DurationMs;
            var progress = playback.ProgressMs;

            TimeSpan progressTimeSpan = TimeSpan.FromMilliseconds(progress);
            label2.Text = $"{progressTimeSpan.Minutes:D2}:{progressTimeSpan.Seconds:D2}";

            trackState.Maximum = (int)duration;
            trackState.Value = (int)progress;
  
            label1.Text = $"{track.Name}";

            ListeningTime.Text = "   " + resManager.GetString("ListeningTimeText") + $"\n{formattedTime}";
            ListeningTime.TextAlign = ContentAlignment.TopCenter;

            PlayResume.Image = Properties.Resources.Кнопка_пауза_;

            newText();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (user != null)
            {
                var time = DBtime.GetOverallListeningTime(user);
                MessageBox.Show(resManager.GetString("OverallTime") + $" = {time}");
            }
            else
            {
                MessageBox.Show("User not loaded.");
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            listView1.Columns.Clear();
            listView1.Items.Clear();
            listView1.Columns.Add(resManager.GetString("TrackName"), 150);
            listView1.Columns.Add(resManager.GetString("Artist"), 120);
            listView1.Columns.Add(resManager.GetString("Album"), 155);
            listView1.Columns.Add(resManager.GetString("Duration"), Int32.Parse(resManager.GetString("DurationLength")));
            listView1.Columns.Add(resManager.GetString("ListeningTime"), 140);
            if (user != null)
            {
                List<ListeningInfo> listeningInfoList = DBtime.GetLocalListeningTime(user.Id);
                foreach (var listeningInfo in listeningInfoList)
                {
                    if (listeningInfo.ItemType == "Track")
                    {
                        var track = await _spotifyService.GetTrackById(listeningInfo.ItemId);
                        TimeSpan duration = TimeSpan.FromMilliseconds(track.DurationMs);
                        TimeSpan roundedListeningTime = TimeSpan.FromSeconds(Math.Round(listeningInfo.ListeningTime.TotalSeconds));
                        string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}",
                            roundedListeningTime.Hours,
                            roundedListeningTime.Minutes,
                            roundedListeningTime.Seconds);
                        string albumImageUrl = track.Album.Images.FirstOrDefault()?.Url;
                        if (!string.IsNullOrEmpty(albumImageUrl))
                        {
                            using (var wc = new WebClient())
                            {
                                byte[] bytes = wc.DownloadData(albumImageUrl);
                                using (var ms = new System.IO.MemoryStream(bytes))
                                {
                                    imageList.Images.Add(track.Id, System.Drawing.Image.FromStream(ms));
                                }
                            }
                        }
                        ListViewItem item = new ListViewItem(new string[]
                        {
                    track.Name,
                    track.Artists[0].Name,
                    track.Album.Name,
                    $"{duration.Minutes}:{duration.Seconds:D2}",
                    formattedTime,
                        });
                        if (imageList.Images.ContainsKey(track.Id))
                        {
                            item.ImageKey = track.Id;
                        }

                        item.Tag = track.ExternalUrls["spotify"];
                        listView1.Items.Add(item);
                    }

                }
            }
            else
            {
                MessageBox.Show("User not loaded.");
            }
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            listView1.Columns.Clear();
            listView1.Items.Clear();
            listView1.Columns.Add(resManager.GetString("AlbumName"), 170);
            listView1.Columns.Add(resManager.GetString("Artist"), 110);
            listView1.Columns.Add(resManager.GetString("Genre"), 100);
            listView1.Columns.Add(resManager.GetString("AlbumCapacity"), Int32.Parse(resManager.GetString("AlbumCapacityLength")));
            listView1.Columns.Add(resManager.GetString("ListeningTime"), 140);
            if (user != null)
            {
                List<ListeningInfo> listeningInfoList = DBtime.GetLocalListeningTime(user.Id);
                foreach (var listeningInfo in listeningInfoList)
                {
                    if (listeningInfo.ItemType == "Album")
                    {
                        var album = await _spotifyService.GetAlbumById(listeningInfo.ItemId);
                        var artist = await _spotifyService.GetArtistById(album.Artists[0].Id);
                        TimeSpan roundedListeningTime = TimeSpan.FromSeconds(Math.Round(listeningInfo.ListeningTime.TotalSeconds));
                        string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}",
                            roundedListeningTime.Hours,
                            roundedListeningTime.Minutes,
                            roundedListeningTime.Seconds);
                        string albumImageUrl = album.Images.FirstOrDefault()?.Url;
                        if (!string.IsNullOrEmpty(albumImageUrl))
                        {
                            using (var wc = new WebClient())
                            {
                                byte[] bytes = wc.DownloadData(albumImageUrl);
                                using (var ms = new System.IO.MemoryStream(bytes))
                                {
                                    imageList.Images.Add(album.Id, System.Drawing.Image.FromStream(ms));
                                }
                            }
                        }
                        ListViewItem item = new ListViewItem(new string[]
                        {
                    album.Name,
                    album.Artists[0].Name,
                    artist.Genres.Count > 0 ? artist.Genres[0] : "No genre",
                    album.TotalTracks.ToString(),
                    formattedTime,
                        });
                        if (imageList.Images.ContainsKey(album.Id))
                        {
                            item.ImageKey = album.Id;
                        }

                        item.Tag = album.ExternalUrls["spotify"];
                        listView1.Items.Add(item);
                    }

                }
            }
            else
            {
                MessageBox.Show("User not loaded.");
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            resetInterfaceLanguage();
        }

        private async void PlayResume_Click(object sender, EventArgs e)
        {
            var currentPlaying = await _spotifyService.GetCurrentlyPlayingTrack();
            if (currentPlaying.IsPlaying)
            {
                await _spotifyService.Pause();
                PlayResume.Image = Properties.Resources.Кнопка_играть;
            }
            else if (!currentPlaying.IsPlaying)
            {
                await _spotifyService.Play();
                PlayResume.Image = Properties.Resources.Кнопка_пауза_;
            }
        }

        private async void pictureBox3_Click(object sender, EventArgs e)
        {
            await _spotifyService.PreviousTrack();
        }

        private async void pictureBox4_Click(object sender, EventArgs e)
        {
            await _spotifyService.NextTrack();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ContextMenuShow();
        }
        private void ContextMenuShow()
        {
            if (Settings == null || pictureBox2 == null)
            {
                MessageBox.Show("ContextMenuStrip или PictureBox не инициализированы.");
                return;
            }

            Point pictureBoxLocation = pictureBox2.PointToScreen(Point.Empty);

            Point menuLocation = new Point(pictureBoxLocation.X, pictureBoxLocation.Y + pictureBox2.Height);

            Console.WriteLine($"PictureBox Location: {pictureBoxLocation}");
            Console.WriteLine($"Menu Location: {menuLocation}");

            if (Settings.Items.Count == 0)
            {
                MessageBox.Show("ContextMenuStrip не содержит элементов.");
                return;
            }

            Settings.Show(menuLocation);
        }

        private void SetUkr_Click(object sender, EventArgs e)
        {
            SetDefaultLanguage("uk-UA");
        }

        private void SetEng_Click(object sender, EventArgs e)
        {
            SetDefaultLanguage("en-EN");
        }
        private void SetRus_Click(object sender, EventArgs e)
        {
            SetDefaultLanguage("ru-RU");
        }
        private void SetDefaultLanguage(string language)
        {
            Properties.Settings.Default.Language = $"{language}";
            Properties.Settings.Default.Save();
            Thread.CurrentThread.CurrentUICulture = new CultureInfo($"{language}");
            Controls.Clear();
            LanguageSettings.DropDown.Close();
            InitializeComponent();
            resetInterfaceLanguage();
            InitializeListView();
            LoadUserProfile();
            ElementsOrder();
        }

        private void resetInterfaceLanguage()
        {
            resManager = new ResourceManager("SpotifyStat.CustomStrings", typeof(Form3).Assembly);
            ComboBox1.UpdateText(resManager);
            ComboBox2.UpdateText(resManager);
            LanguageSettings.DropDown.BackColor = Color.FromArgb(18, 18, 18);
            this.Cursor = Cursors.Default;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
