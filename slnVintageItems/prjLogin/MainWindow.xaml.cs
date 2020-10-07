using ClassLibraryVintage;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace prjLogin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User user;
        string adminConnString = ConfigurationManager.AppSettings["adminConnString"];
        public MainWindow()
        {
            InitializeComponent();
        }
        int id = 0;
        string role = "";
        string login = "";
        string email = "";
        string gender = "";
        string lastname = "";
        string password = "";
        string firstname = "";
        string telephone = "";
        bool matchedLogin = false;

        DateTime birthdate = DateTime.MinValue;
        void LogIn()
        {
            switch (user.person_roleName)
            {
                case "admin":
                    Person personA = new Admin(id, login, firstname, lastname, gender, password, birthdate, email, telephone);
                    prjAdmin.wpfAdmin windowA = new prjAdmin.wpfAdmin(user);
                    windowA.Show();
                    this.Close();
                    return;
                case "seller":
                    Person personS = new Seller(id, login, firstname, lastname, gender, password, birthdate, email, telephone);
                    prjSeller.wpfSeller windowS = new prjSeller.wpfSeller(user);
                    windowS.Show();
                    this.Close();
                    return;
                case "buyer":
                    Person personB = new Buyer(id, login, firstname, lastname, gender, password, birthdate, email, telephone);
                    prjBuyer.wpfBuyer windowB = new prjBuyer.wpfBuyer(user);
                    windowB.Show();
                    this.Close();
                    return;
            }
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string _login = txtLogin.Text.Trim();
            string _password = txtPassword.Password.Trim();

            if (_password != "" && _login != "")
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(adminConnString))
                    {
                        // open connectie
                        conn.Open();

                        // voer SQL commando uit
                        SqlCommand sqlCommand = new SqlCommand("select * from dbo.vwLoginPassword", conn);
                        SqlDataReader reader = sqlCommand.ExecuteReader();

                        int aantal = 0;
                        while (reader.Read())
                        {
                            id = Convert.ToInt32(reader["ID"]);
                            email = Convert.ToString(reader["email"]);
                            gender = Convert.ToString(reader["gender"]);
                            role = Convert.ToString(reader["role_name"]);
                            lastname = Convert.ToString(reader["lastname"]);
                            password = Convert.ToString(reader["password"]);
                            login = Convert.ToString(reader["login"]).Trim();
                            firstname = Convert.ToString(reader["firstname"]);
                            telephone = Convert.ToString(reader["telephone"]);
                            birthdate = Convert.ToDateTime(reader["birthdate"]);

                            if (login == _login && password == _password)
                            {
                                matchedLogin = true;
                                aantal++;
                                user = new User(id, login, firstname, lastname, gender, password, birthdate, email, telephone);
                                user.person_roleName = role;
                                ComboBoxItem comboBoxItem = new ComboBoxItem();
                                comboBoxItem.Content = role;
                                comboBoxItem.Tag = role;
                                cmbType.Items.Add(comboBoxItem);

                                if (aantal > 1)
                                {
                                    stackLoginAs.Visibility = Visibility.Visible;
                                }
                            }
                        }
                        if(matchedLogin)
                        {
                            if(aantal == 1)
                            {

                                LogIn();
                            }
                            else
                            {
                                lblStatus.Text = "Chose youre login type on the right of the screen.";
                            }
                        }
                        else
                        {
                            lblStatus.Text = "Wrong username or password.";
                        }
                    }
                }
                catch (SqlException ex) { MessageBox.Show("An error has occurred: " + ex.Message); }
            }
            else
            {
                lblStatus.Text = "Please fill in all fields.";
            }
        }
        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            user.person_roleName = ((ComboBoxItem)cmbType.SelectedItem).Tag.ToString();
            LogIn();
        }
    }
}
