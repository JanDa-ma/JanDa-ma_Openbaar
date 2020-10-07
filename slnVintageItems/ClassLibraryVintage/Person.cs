using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace ClassLibraryVintage
{
    public class  Person
    {
        static string adminConnString = ConfigurationManager.AppSettings["adminConnString"];
        public Person(int id, string login, string firstname, string lastname, string gender,string password,DateTime dateTime,string email,string telephone)
        {
            person_id = id;
            person_login = login;
            person_email = email;
            person_gender = gender;
            person_password = password;
            person_lastname = lastname;
            person_birthdate = dateTime;
            person_telephone = telephone;
            person_firstname = firstname;
        }
        public Person(string login, string firstname, string lastname, string gender, string password,DateTime birthdate,string role)
        {
            person_login = login;
            person_gender = gender;
            person_password = password;
            person_lastname = lastname;
            person_firstname = firstname;
            person_birthdate = birthdate;
            person_roleName = role;
        }
        public Person()
        {
        
        }

        public int person_id { get;}
        public string person_login { get; set; }
        public string person_password { get; set; }
        public DateTime person_birthdate { get; set; }
        public string person_roleName { get; set;}
        public string person_oldRoleName { get; set; }
        public string person_email { get; set; } = DBNull.Value.ToString();
        public string person_telephone { get; set; } = DBNull.Value.ToString();
        public string person_firstname { get; set; }
        public string person_lastname { get; set; }
        public string person_gender { get; set; } = "U";
        public override string ToString()
        {
            return $"{person_id}: {person_firstname} [{person_roleName}]";
        }
        public void DeleteUser()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(adminConnString))
                {
                    // open connectie
                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand("dbo.spDeleteUser", conn);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@id", person_id);
                    sqlCommand.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void CreateUser()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(adminConnString))
                {
                    conn.Open();
                    SqlCommand convertCommand = new SqlCommand("dbo.spAddUser", conn);
                    convertCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    convertCommand.Parameters.AddWithValue("@login", person_login);
                    convertCommand.Parameters.AddWithValue("@email", person_email);
                    convertCommand.Parameters.AddWithValue("@gender", person_gender);
                    convertCommand.Parameters.AddWithValue("@rolename", person_roleName);
                    convertCommand.Parameters.AddWithValue("@lastname", person_lastname);
                    convertCommand.Parameters.AddWithValue("@password", person_password);
                    convertCommand.Parameters.AddWithValue("@firstname", person_firstname);
                    convertCommand.Parameters.AddWithValue("@telephone", person_telephone);
                    convertCommand.Parameters.AddWithValue("@birthdate", person_birthdate);
                    convertCommand.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ee) { MessageBox.Show(ee.Message); }
        }
        public void updateUser()
        {
            try
            {
                // werknemer opslaan
                using (SqlConnection conn = new SqlConnection(adminConnString))
                {
                    conn.Open();
                    SqlCommand convertCommand = new SqlCommand("dbo.spUpdateUser", conn);
                    convertCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    convertCommand.Parameters.AddWithValue("@id", person_id);
                    convertCommand.Parameters.AddWithValue("@login", person_login);
                    convertCommand.Parameters.AddWithValue("@email", person_email);
                    convertCommand.Parameters.AddWithValue("@gender", person_gender);
                    convertCommand.Parameters.AddWithValue("@rolename", person_roleName);
                    convertCommand.Parameters.AddWithValue("@password", person_password);
                    convertCommand.Parameters.AddWithValue("@lastname", person_lastname);
                    convertCommand.Parameters.AddWithValue("@firstname", person_firstname);
                    convertCommand.Parameters.AddWithValue("@telephone", person_telephone);
                    convertCommand.Parameters.AddWithValue("@birthdate", person_birthdate);
                    convertCommand.Parameters.AddWithValue("@oldRole_name", person_oldRoleName);

                    convertCommand.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static List<Person> persons
        {
            get
            {
                List<Person> personsList = new List<Person>();
                try
                {
                    using (SqlConnection conn = new SqlConnection(adminConnString))
                    {
                        // open connectie
                        conn.Open();

                        // voer SQL commando uit
                        SqlCommand sqlCommand = new SqlCommand("select * from dbo.vwUsers", conn);
                        SqlDataReader reader = sqlCommand.ExecuteReader();

                        // lees en verwerk resultaten
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["ID"]);
                            string login = Convert.ToString(reader["login"]).Trim();
                            string lastname = Convert.ToString(reader["lastname"]);
                            string gender = Convert.ToString(reader["gender"]);
                            string firstname = Convert.ToString(reader["firstname"]);
                            string password = Convert.ToString(reader["password"]);
                            DateTime birthdate = Convert.ToDateTime(reader["birthdate"]);
                            string email = Convert.ToString(reader["email"]);
                            string telephone = Convert.ToString(reader["telephone"]);
                            string role = Convert.ToString(reader["role_name"]);
                            if (role == "admin")
                            {
                                Person person = new Admin(id, login, firstname, lastname, gender, password, birthdate, email, telephone);
                                personsList.Add(person);
                            }
                            else if (role == "seller")
                            {
                                Person person = new Seller(id, login, firstname, lastname, gender, password, birthdate, email, telephone);
                                personsList.Add(person);
                            }
                            else if (role == "buyer")
                            {
                                Person person = new Buyer(id, login, firstname, lastname, gender, password, birthdate, email, telephone);
                                personsList.Add(person);
                            }
                        }
                        conn.Close();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("An error has occured: " + ex.Message);
                }
                return personsList;
            }
        }
    }
}
