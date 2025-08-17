using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace authentication
{
    internal class User
    {
       
        private string GetConnectionString()
        {
           
            return ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString;
        }

        public bool Login(string inputUser, string inputPass)
        {
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @user AND Password = @pass";

            using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", inputUser);
                    cmd.Parameters.AddWithValue("@pass", inputPass);

                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count == 1;
                }
            }
        }

        public bool Register(string newUser, string newPass)
        {
            string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @user";
            string insertQuery = "INSERT INTO Users (Username, Password) VALUES (@user, @pass)";

            using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
            {
                conn.Open();

                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@user", newUser);
                    int count = (int)checkCmd.ExecuteScalar();
                    if (count > 0) return false; // username exists
                }

                using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn))
                {
                    insertCmd.Parameters.AddWithValue("@user", newUser);
                    insertCmd.Parameters.AddWithValue("@pass", newPass);
                    insertCmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public bool IsStrongPassword(string password)
        {
            return password.Length >= 8 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsDigit) &&
                   password.Any(c => "!@#$%^&*()_+-=[]{}|;:',.<>?/".Contains(c));
        }

        // Individual rule checks for UI
        public bool HasUpper(string pass) => pass.Any(char.IsUpper);
        public bool HasLower(string pass) => pass.Any(char.IsLower);
        public bool HasDigit(string pass) => pass.Any(char.IsDigit);
        public bool HasSpecial(string pass) => pass.Any(c => "!@#$%^&*()_+-=[]{}|;:',.<>?/".Contains(c));
        public bool HasMinLength(string pass) => pass.Length >= 8;
    }
}
