using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AiXinYaoYe.Database
{
    public class AiXinYaoYeDb
    {
        private static string _connectionString = "";
        public static UserProfile GetUserProfile(string openId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("select * from xxx where openid=@openid", connection);
                    cmd.Parameters.AddWithValue("openid", openId);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    sdr.Read();
                    var result = new UserProfile();
                    result.Name = sdr["name"].ToString();
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
    }

    public class UserProfile
    {
        public string Name { get; set; }
        public string CardNum { get; set; }
        public string Bonus { get; set; }
        public string Balance { get; set; }
    }
}