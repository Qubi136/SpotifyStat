using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Http;
using System;
using System.Configuration;
using System.Threading.Tasks;
using static SpotifyAPI.Web.PersonalizationTopRequest;

namespace SpotifyStat
{
    public class SpotifyService
    {
        private EmbedIOAuthServer? _server;
        private SpotifyClient? _spotify;
        private PrivateUser? user;
        private Paging<FullTrack>? topTr;
        private Paging<FullArtist>? topAr;
        private string refreshToken;

        private static readonly string clientId = ConfigurationManager.AppSettings["clientId"];
        private static readonly string clientSecret = ConfigurationManager.AppSettings["clientSecret"];
        private static readonly string redirectUri = "http://localhost:5543/callback";

        public async Task Authorize()
        {
            _server = new EmbedIOAuthServer(new Uri(redirectUri), 5543);
            await _server.Start();

            _server.AuthorizationCodeReceived += OnAuthorizationCodeReceived;
            _server.ErrorReceived += OnErrorReceived;

            var request = new LoginRequest(_server.BaseUri, clientId, LoginRequest.ResponseType.Code)
            {
                Scope = new List<string>
                {
                    Scopes.UserReadEmail,
                    Scopes.UserTopRead,
                    Scopes.UserReadPrivate,
                    Scopes.UserReadRecentlyPlayed,
                    Scopes.UserReadCurrentlyPlaying,
                    Scopes.UserModifyPlaybackState,
                    Scopes.UserReadPlaybackPosition,
                    Scopes.UserReadPlaybackState
                }
            };

            BrowserUtil.Open(request.ToUri());
        }
        private async Task OnAuthorizationCodeReceived(object sender, AuthorizationCodeResponse response)
        {
            await _server.Stop();
            var config = SpotifyClientConfig.CreateDefault();
            var tokenResponse = await new OAuthClient(config).RequestToken(
                new AuthorizationCodeTokenRequest(clientId, clientSecret, response.Code, new Uri(redirectUri))
            );

            refreshToken = tokenResponse.RefreshToken;
            /*if (string.IsNullOrEmpty(refreshToken))
            {
                MessageBox.Show("Refresh token is not available after authorization.");
            }
            else
            {
                MessageBox.Show("Refresh token obtained successfully.");
            }*/

            _spotify = new SpotifyClient(tokenResponse.AccessToken);
            user = await _spotify.UserProfile.Current();
        }

        private async Task OnErrorReceived(object sender, string error, string state)
        {
            MessageBox.Show($"Aborting authorization, error received: {error}");
            await _server.Stop();
        }
        public async Task<T> ExecuteWithTokenRefresh<T>(Func<Task<T>> action)
        {
            try
            {
                return await action();
            }
            catch (APIUnauthorizedException)
            {
                await RefreshToken();
                return await action();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error executing Spotify API request: {ex.Message}");
                throw;
            }
        }
        public async Task<PrivateUser> GetCurrentUserProfile()
        {
            while (user == null)
            {
                await Task.Delay(1);
            }
            return await ExecuteWithTokenRefresh(async () =>
            {
                return user;
            });
        }
        private PersonalizationTopRequest.TimeRange GetTimeRangeEnum(string timeRange)
        {
            return timeRange switch
            {
                "For 4 weeks" or "За 4 тижня" or "За 4 недели" => PersonalizationTopRequest.TimeRange.ShortTerm,
                "For 6 months" or "За 6 місяців" or "За 6 месяцев" => PersonalizationTopRequest.TimeRange.MediumTerm,
                "For one year" or "За один рік" or "За один год" => PersonalizationTopRequest.TimeRange.LongTerm,
                _ => PersonalizationTopRequest.TimeRange.MediumTerm,
            };
        }
        public async Task<Paging<FullTrack>> GetUsersTopTracks(string timeRange = "For 4 weeks")
        {
            return await ExecuteWithTokenRefresh(async () =>
            {
                PersonalizationTopRequest.TimeRange timeRangeEnum = GetTimeRangeEnum(timeRange);
                topTr = await _spotify.Personalization.GetTopTracks(new PersonalizationTopRequest
                {
                    TimeRangeParam = timeRangeEnum,
                    Limit = 15
                });
                return topTr;
            });
        }


        public async Task<Paging<FullArtist>> GetUsersTopArtists(string timeRange = "For 4 weeks")
        {
            return await ExecuteWithTokenRefresh(async () =>
            {
                PersonalizationTopRequest.TimeRange timeRangeEnum = GetTimeRangeEnum(timeRange);
                topAr = await _spotify.Personalization.GetTopArtists(new PersonalizationTopRequest
                {
                    TimeRangeParam = timeRangeEnum,
                    Limit = 15
                });
                return topAr;
            });
        }

        public async Task<CursorPaging<PlayHistoryItem>> GetHistory()
        {
            return await ExecuteWithTokenRefresh(async () =>
            {
                string timeZoneId = "FLE Standard Time";
                var history = await _spotify.Player.GetRecentlyPlayed();

                TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                foreach (var item in history.Items)
                {
                    DateTime utcDateTime = item.PlayedAt;
                    DateTime targetDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, targetTimeZone);
                    item.PlayedAt = targetDateTime;
                }

                return history;
            });
        }

        public async Task<CurrentlyPlaying> GetCurrentlyPlayingTrack()
        {
            return await ExecuteWithTokenRefresh(async () =>
            {
                var track = await _spotify.Player.GetCurrentlyPlaying(new PlayerCurrentlyPlayingRequest());
                return track;
            });
        }
        public async Task<FullTrack> GetTrackById(string id)
        {
            return await ExecuteWithTokenRefresh(async () =>
            {
                var track = await _spotify.Tracks.Get(id);
                return track;
            });
        }

        public async Task<FullAlbum> GetAlbumById(string id)
        {
            return await ExecuteWithTokenRefresh(async () =>
            {
                var album = await _spotify.Albums.Get(id);
                return album;
            });
        }

        public async Task<FullArtist> GetArtistById(string id)
        {
            return await ExecuteWithTokenRefresh(async () =>
            {
                var artist = await _spotify.Artists.Get(id);
                return artist;
            });
        }
        public async Task Play()
        {
            await ExecuteWithTokenRefresh(async () =>
            {
                await _spotify.Player.ResumePlayback();
                return true; 
            });
        }

        public async Task Pause()
        {
            await ExecuteWithTokenRefresh(async () =>
            {
                await _spotify.Player.PausePlayback();
                return true; 
            });
        }

        public async Task NextTrack()
        {
            await ExecuteWithTokenRefresh(async () =>
            {
                await _spotify.Player.SkipNext();
                return true;  
            });
        }

        public async Task PreviousTrack()
        {
            await ExecuteWithTokenRefresh(async () =>
            {
                await _spotify.Player.SkipPrevious();
                return true;  
            });
        }

        public async Task<CurrentlyPlayingContext> GetPlayback()
        {
            return await ExecuteWithTokenRefresh(async () =>
            {
                var Playback = await _spotify.Player.GetCurrentPlayback();
                return Playback;
            });
        }
        public async Task RefreshToken()
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new InvalidOperationException("Refresh token is not available");
            }

            var config = SpotifyClientConfig.CreateDefault();
            var tokenResponse = await new OAuthClient(config).RequestToken(
                new AuthorizationCodeRefreshRequest(clientId, clientSecret, refreshToken)
            );

            _spotify = new SpotifyClient(tokenResponse.AccessToken);
            refreshToken = tokenResponse.RefreshToken;  
        }
    }
}