using Microsoft.VisualBasic.ApplicationServices;
using SpotifyAPI.Web;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace SpotifyStat
{
    public partial class Form1 : Form
    {
        private SpotifyService _spotifyService;

        public Form1()
        {
            InitializeComponent();
            AllInCenter();
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            _spotifyService = new SpotifyService();
        }


        private async void Form1_Load(object sender, EventArgs e)
        {
            //await _spotifyService.InitializeSpotify();
        }

        private async void pictureBox1_Click(object sender, EventArgs e)
        {
            if (LanguageSettings.IsDropDown)
            {
                LanguageSettings.Close();
            }
            await HandlePictureBoxClickAsync();
        }

        private async Task HandlePictureBoxClickAsync()
        {
            await _spotifyService.Authorize();
            Form3 newForm = new Form3(_spotifyService);

            newForm.Shown += (sender, args) =>
            {
                this.Invoke((Action)(() => this.Hide()));
            };
        }
        private void OpenNewForm_Click(object sender, EventArgs e)
        {
            Form2 newForm = new Form2();
            newForm.Show();
        }

        private void OpenNewForm_Click_1(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
        private void OpenNewForm_MouseEnter(object sender, EventArgs e)
        {
            OpenNewForm.Cursor = Cursors.Hand;
        }

        private void OpenNewForm_MouseLeave(object sender, EventArgs e)
        {
            OpenNewForm.Cursor = Cursors.Default;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ContextMenuShow();
        }

        private void UkrSet_Click(object sender, EventArgs e)
        {
            SetDefaultLanguage("uk-UA");
        }

        private void EngSet_Click(object sender, EventArgs e)
        {
            SetDefaultLanguage("en-EN");
        }

        private void RusSet_Click(object sender, EventArgs e)
        {
            SetDefaultLanguage("ru-RU");
        }

        private void CloseMenu_Click(object sender, EventArgs e)
        {
            LanguageSettings.Close();
        }

        private void ContextMenuShow()
        {
            Point pictureBoxLocation = Settings.PointToScreen(Point.Empty);

            Point menuLocation = new Point(pictureBoxLocation.X, pictureBoxLocation.Y + Settings.Height);

            LanguageSettings.Show(menuLocation);
        }

        private void SetDefaultLanguage(string language)
        {
            Properties.Settings.Default.Language = $"{language}";
            Properties.Settings.Default.Save();
            Thread.CurrentThread.CurrentUICulture = new CultureInfo($"{language}");
            Controls.Clear();
            LanguageSettings.Close();
            InitializeComponent();
            ContextMenuShow();
            AllInCenter();
        }
    }
}