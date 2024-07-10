using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Http;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpotifyStat
{
    public partial class Form1 : Form
    {
        private EmbedIOAuthServer _server;
        private SpotifyClient spotify;

        

        public Form1()
        {
            InitializeComponent();
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (labelUserName.Text == "Login")
            {
                AllInCenter();
            }
            else
            {
                labelUserName.Location = new Point(0, 0);
                pictureBox1.Location = new Point(labelUserName.Width, 0);
            }
        }
        private void MakePictureBoxRound(PictureBox pictureBox)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, pictureBox.Width, pictureBox.Height);
            pictureBox.Region = new Region(path);
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            AllInCenter();
            await InitializeSpotify();
            MakePictureBoxRound(pictureBox1);
        }

        private async Task InitializeSpotify()
        {
            var config = SpotifyClientConfig.CreateDefault();
            var request = new ClientCredentialsRequest("clientId", "clientSecret");
            var response = await new OAuthClient(config).RequestToken(request);

            spotify = new SpotifyClient(config.WithToken(response.AccessToken));
        }

        private async Task LoadUserProfile()
        {
            var me = await spotify.UserProfile.Current();
            string profileImageUrl = null;

            if (me.Images.Count > 0)
            {
                profileImageUrl = me.Images[0].Url;
            }
            var UserName = me.DisplayName;
            Invoke(new Action(() =>
            {
                labelUserName.Text = UserName;
                if (!string.IsNullOrEmpty(profileImageUrl))
                {
                    pictureBox1.Load(profileImageUrl);
                }
                else
                {
                    pictureBox1.Load("https://cdn-icons-png.freepik.com/512/634/634795.png");
                    MessageBox.Show("User does not have a profile picture.");
                }
                labelUserName.Location = new Point(0, 0);
                pictureBox1.Location = new Point(labelUserName.Width, 0);
            }));
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Cursor = Cursors.Hand;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Cursor = Cursors.Default;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                _server = new EmbedIOAuthServer(new Uri(redirectUri), 5543);
                await _server.Start();

                _server.AuthorizationCodeReceived += OnAuthorizationCodeReceived;
                _server.ErrorReceived += OnErrorReceived;

                var request = new LoginRequest(_server.BaseUri, clientId, LoginRequest.ResponseType.Code)
                {
                    Scope = new List<string> { Scopes.UserReadEmail }
                };

                BrowserUtil.Open(request.ToUri());
            });
        }

        private async Task OnAuthorizationCodeReceived(object sender, AuthorizationCodeResponse response)
        {
            await _server.Stop();
            var config = SpotifyClientConfig.CreateDefault();
            var tokenResponse = await new OAuthClient(config).RequestToken(
                new AuthorizationCodeTokenRequest(clientId, clientSecret, response.Code, new Uri(redirectUri))
            );

            spotify = new SpotifyClient(tokenResponse.AccessToken);
            await LoadUserProfile();
        }
        private async Task OnErrorReceived(object sender, string error, string state)
        {
            Console.WriteLine($"Aborting authorization, error received: {error}");
            await _server.Stop();
        }

        
    }
}
