using System;
using System.Data.SqlClient;

namespace AiXinYaoYeV2.Database
{
    public class AiXinYaoYeDb
    {
        public AiXinYaoYeDb(string connection)
        {
            _connectionString = connection;
        }
        private string _connectionString;
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
                    result.Bonus = Convert.ToDecimal(sdr["jf"].ToString());
                    result.Balance = Convert.ToDecimal(sdr["czye"]);
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
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
                    result.Bonus = Convert.ToDecimal(sdr["jf"].ToString());
                    result.Balance = Convert.ToDecimal(sdr["czye"]);
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
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
                    Console.WriteLine(e);
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
        public decimal Bonus { get; set; }
        public decimal Balance { get; set; }
    }
}