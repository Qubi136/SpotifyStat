using Microsoft.VisualBasic.ApplicationServices;
using SpotifyAPI.Web;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SpotifyStat
{
    public class DBControl
    {
        private string connectionString;

        public DBControl()
        {
            var connStringSettings = ConfigurationManager.ConnectionStrings["DBTime"];
            if (connStringSettings != null)
            {
                connectionString = connStringSettings.ConnectionString;
            }
            else
            {
                throw new InvalidOperationException("Connection string 'DBTime' is not found.");
            }
        }

        public void UpdateOverallListeningTime(string id, TimeSpan playedTime)
        {
            string selectQuery = "SELECT PlayedTime FROM SpotifyTIME WHERE Id = @Id";
            string updateQuery = "UPDATE SpotifyTIME SET PlayedTime = @PlayedTime WHERE Id = @Id";
            string insertQuery = "INSERT INTO SpotifyTIME (Id, PlayedTime) VALUES (@Id, @PlayedTime)";

            TimeSpan existingTime = TimeSpan.Zero;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                try
                {
                    using (SqlCommand selectCommand = new SqlCommand(selectQuery, sqlConnection))
                    {
                        selectCommand.Parameters.AddWithValue("@Id", id);
                        var result = selectCommand.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            existingTime = (TimeSpan)result;
                        }
                    }

                    TimeSpan updatedTime = existingTime + playedTime;

                    using (SqlCommand selectCommand = new SqlCommand(selectQuery, sqlConnection))
                    {
                        selectCommand.Parameters.AddWithValue("@Id", id);
                        var result = selectCommand.ExecuteScalar();
                        bool recordExists = (result != null && result != DBNull.Value);

                        if (recordExists)
                        {
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, sqlConnection))
                            {
                                updateCommand.Parameters.AddWithValue("@Id", id);
                                updateCommand.Parameters.AddWithValue("@PlayedTime", updatedTime);
                                updateCommand.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            using (SqlCommand insertCommand = new SqlCommand(insertQuery, sqlConnection))
                            {
                                insertCommand.Parameters.AddWithValue("@Id", id);
                                insertCommand.Parameters.AddWithValue("@PlayedTime", updatedTime);
                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error in UpdateOverallListeningTime: {ex.Message}");
                }
            }
        }

        public void UpdateLocalListeningTime(string userId, string itemId, string itemType, TimeSpan playedTime)
        {
            string selectQuery = "SELECT ListeningTime FROM UserListeningTime WHERE UserId = @UserId AND ItemId = @ItemId AND ItemType = @ItemType";
            string updateQuery = "UPDATE UserListeningTime SET ListeningTime = @ListeningTime WHERE UserId = @UserId AND ItemId = @ItemId AND ItemType = @ItemType";
            string insertQuery = "INSERT INTO UserListeningTime (UserId, ItemId, ItemType, ListeningTime) VALUES (@UserId, @ItemId, @ItemType, @ListeningTime)";

            TimeSpan existingTime = TimeSpan.Zero;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                try
                {
                    using (SqlCommand selectCommand = new SqlCommand(selectQuery, sqlConnection))
                    {
                        selectCommand.Parameters.AddWithValue("@UserId", userId);
                        selectCommand.Parameters.AddWithValue("@ItemId", itemId);
                        selectCommand.Parameters.AddWithValue("@ItemType", itemType);
                        var result = selectCommand.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            existingTime = (TimeSpan)result;
                        }
                    }

                    TimeSpan updatedTime = existingTime + playedTime;

                    using (SqlCommand selectCommand = new SqlCommand(selectQuery, sqlConnection))
                    {
                        selectCommand.Parameters.AddWithValue("@UserId", userId);
                        selectCommand.Parameters.AddWithValue("@ItemId", itemId);
                        selectCommand.Parameters.AddWithValue("@ItemType", itemType);
                        var result = selectCommand.ExecuteScalar();
                        bool recordExists = (result != null && result != DBNull.Value);

                        if (recordExists)
                        {
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, sqlConnection))
                            {
                                updateCommand.Parameters.AddWithValue("@UserId", userId);
                                updateCommand.Parameters.AddWithValue("@ItemId", itemId);
                                updateCommand.Parameters.AddWithValue("@ItemType", itemType);
                                updateCommand.Parameters.AddWithValue("@ListeningTime", updatedTime);
                                updateCommand.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            using (SqlCommand insertCommand = new SqlCommand(insertQuery, sqlConnection))
                            {
                                insertCommand.Parameters.AddWithValue("@UserId", userId);
                                insertCommand.Parameters.AddWithValue("@ItemId", itemId);
                                insertCommand.Parameters.AddWithValue("@ItemType", itemType);
                                insertCommand.Parameters.AddWithValue("@ListeningTime", updatedTime);
                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error in UpdateLocalListeningTime: {ex.Message}");
                }
            }
        }

        public List<ListeningInfo> GetLocalListeningTime(string userId)
        {
            string selectQuery = @"
        SELECT ItemId, ItemType, ListeningTime 
        FROM UserListeningTime 
        WHERE UserId = @UserId";

            List<ListeningInfo> listeningTimes = new List<ListeningInfo>();

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand selectCommand = new SqlCommand(selectQuery, sqlConnection))
                {
                    selectCommand.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string itemId = reader["ItemId"].ToString();
                            string itemType = reader["ItemType"].ToString();
                            TimeSpan listeningTime = (TimeSpan)reader["ListeningTime"];

                            listeningTimes.Add(new ListeningInfo
                            {
                                ItemId = itemId,
                                ItemType = itemType,
                                ListeningTime = listeningTime
                            });
                        }
                    }
                }
            }
            listeningTimes = listeningTimes.OrderByDescending(lt => lt.ListeningTime).ToList();

            return listeningTimes;
        }

        public class ListeningInfo
        {
            public string ItemId { get; set; }
            public string ItemType { get; set; }
            public TimeSpan ListeningTime { get; set; }
        }

        public TimeSpan GetOverallListeningTime(PrivateUser user)
        {
            string selectQuery = "SELECT PlayedTime FROM SpotifyTIME WHERE Id = @Id";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand selectCommand = new SqlCommand(selectQuery, sqlConnection))
                {
                    selectCommand.Parameters.AddWithValue("@Id", user.Id);

                    var result = selectCommand.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return (TimeSpan)result;
                    }
                    else
                    {
                        MessageBox.Show("No data found for the given Id.");
                        return TimeSpan.Zero;
                    }
                }
            }
        }

    }
}
