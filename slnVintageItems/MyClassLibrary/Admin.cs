using System;
using System.Data.SqlClient;
namespace slnVintageItems
{
    public class Admin : Person
    {
        public Admin(int id, string login,string firstname, string lastname, string gender) : base(id, login,firstname, lastname, gender)
        {
            admin_id = id;
            admin_login = login;
            admin_firstname = firstname;
            admin_lastname = lastname;
            admin_gender = gender;
            user_roleName= "admin";
        }
        public int admin_id { get; }
        public string admin_login { get; set; }
        public string admin_firstname { get; set; }
        public string admin_lastname { get; set; }
        public string admin_gender { get; set; }
        public Person loadUsers()
        {
            Person person;
            try
            {
                using (SqlConnection conn = new SqlConnection(adminConnString))
                {
                    // open connectie
                    conn.Open();

                    // voer SQL commando uit
                    SqlCommand sqlCommand = new SqlCommand("select * from dbo.vwUsers", conn);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    lstUsers.Items.Clear();

                    // lees en verwerk resultaten
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["ID"]);
                        string login = Convert.ToString(reader["login"]).Trim();
                        string lastname = Convert.ToString(reader["lastname"]);
                        string gender = Convert.ToString(reader["gender"]);
                        string firstname = Convert.ToString(reader["firstname"]);
                        string role = Convert.ToString(reader["role_name"]);
                        if (role == "admin")
                        {
                            Admin admin = new Admin(id, login, firstname, lastname, gender);
                           
                        }
                    }
                    conn.Close();
                }
            }
            catch (SqlException ex) { System.Windows.Forms.MessageBox.Show("An error has occured: " + ex.Message); }
            return person;
        }

        public override string ToString()
        {
            return $"{admin_id}: {admin_firstname} [admin]";
        }
    }
}
