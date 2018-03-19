using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace AiXinYaoYeV2.Database
{
    public class AiXinYaoYeDb
    {
        public AiXinYaoYeDb(string connection, ILogger logger)
        {
            _connectionString = connection;
            _logger = logger;
        }
        private string _connectionString;
        private ILogger _logger;

        public UserProfile GetUserProfile(string openId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("select * from hygl where openID=@openid", connection);
                    cmd.Parameters.AddWithValue("openid", openId);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    sdr.Read();
                    var result = new UserProfile();
                    result.Name = sdr["hyxm"].ToString();
                    result.CardNum = sdr["hybh"].ToString();
                    result.Bonus = (Convert.ToDouble(sdr["jf"])).ToString("F2");
                    result.Balance = (Convert.ToDouble(sdr["czye"])).ToString("F2");
                    return result;
                }
                catch (Exception e)
                {
                    this._logger.LogError(123, e, e.Message);
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public UserProfile GetUserProfileByHybh(string hybh)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("select * from hygl where hybh=@hybh", connection);
                    cmd.Parameters.AddWithValue("hybh", hybh);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    sdr.Read();
                    var result = new UserProfile();
                    result.Name = sdr["hyxm"].ToString();
                    result.CardNum = sdr["hybh"].ToString();
                    result.Bonus = (Convert.ToDouble(sdr["jf"])).ToString("F2");
                    result.Balance = (Convert.ToDouble(sdr["czye"])).ToString("F2");
                    return result;
                }
                catch (Exception e)
                {
                    this._logger.LogError(123, e, e.Message);
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void AddOpenId(string hybh,string openId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("update hygl set openID = @openid where hybh = @hybh", connection);
                    cmd.Parameters.AddWithValue("hybh", hybh);
                    cmd.Parameters.AddWithValue("openid", openId);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    sdr.Read();
                }
                catch (Exception e)
                {
                    this._logger.LogError(123, e, e.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }

    public class UserProfile
    {
        public string Name { get; set; }
        public string CardNum { get; set; }
        public string Bonus { get; set; }
        public string Balance { get; set; }
    }
}