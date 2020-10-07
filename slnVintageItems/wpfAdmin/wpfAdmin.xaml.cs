using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpfAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class wpfAdmin : Window
    {
        
        string adminConnString = ConfigurationManager.AppSettings["adminConnString"];
        public wpfAdmin() => InitializeComponent();

        //================
        //Load Users
        public void loadUsers()
        {
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
                        string password = Convert.ToString(reader["password"]);
                        string lastname = Convert.ToString(reader["lastname"]);
                        string login = Convert.ToString(reader["login"]).Trim();
                        string firstname = Convert.ToString(reader["firstname"]);

                        ListBoxItem item = new ListBoxItem();
                        item.Content = ($"{id}: {firstname} {lastname} | login: {login.Trim()}").Trim();
                        item.Tag = id;
                    }
                    conn.Close();
                }
            }
            catch (SqlException ex) { System.Windows.Forms.MessageBox.Show("An error has occured: " + ex.Message); }
        }
        //================
        //Menu
        private void logOut_Click(object sender, RoutedEventArgs e)
        {
            DialogResult result = System.Windows.Forms.MessageBox.Show($"You are about to log out. All changes will not be saved.{Environment.NewLine}{Environment.NewLine}" +
                 $"Are you sure you want to exit?", "Log out", MessageBoxButtons.YesNo);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
              //  wpfLogin wpflogin = new wpfLogin();
                //wpflogin.Show();
                //this.Close();
            }
        }
        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
        //    wpfAbout wpf = new wpfAbout();
          //  wpf.Show();
        }
        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            //wpfProfile wpf = new wpfProfile(admin);
            //wpf.Show();
        }
        //================
        //Users list
        private void lstUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ListBoxItem item = (ListBoxItem)lstUsers.SelectedItem;
            if (item == null)
            {
                btnDelete.IsEnabled = false;
                btnEdit.IsEnabled = false;
                return;
            }

            btnEdit.IsEnabled = true;
            btnDelete.IsEnabled = true;

            int userid = Convert.ToInt32(item.Tag);

            try
            {
                using (SqlConnection conn = new SqlConnection(adminConnString))
                {
                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand("dbo.spUserWithId", conn);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@id", userid);
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    reader.Read();
                    txtId.Text = Convert.ToString(reader["id"]);
                    txtEmail.Text = Convert.ToString(reader["email"]);
                    cmbRoles.Text = Convert.ToString(reader["role_name"]);
                    txtpaswoord.Text = Convert.ToString(reader["password"]);
                    txtLastname.Text = Convert.ToString(reader["lastname"]);
                    txtLogin.Text = Convert.ToString(reader["login"]).Trim();
                    txtFirstname.Text = Convert.ToString(reader["firstname"]);
                    txtTelephone.Text = Convert.ToString(reader["telephone"]);
                    txtBirthdate.Text = Convert.ToDateTime(reader["birthdate"]).ToShortDateString();
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        //================
        //Edit users
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text == Convert.ToString(admin.user_id) && cmbRoles.Text != "admin")
            {
                DialogResult result = System.Windows.Forms.MessageBox.Show($"You are going to change your role. " +
                    $"If you change your role, you will be logged out because you no longer have the correct rights. {Environment.NewLine}{Environment.NewLine}" +
               $"=============================================={Environment.NewLine}" +
               $"Are your sure you want to change your role to '{cmbRoles.Text}' and no longer own the admin rights?{Environment.NewLine}" +
               $"==============================================", "Delete user", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    wpfLogin wpf = new wpfLogin();
                    wpf.Show();
                    UpdateUser();
                    this.Close();
                }
            }
            else
            {
                UpdateUser();
            }
        }
        void UpdateUser()
        {
            try
            {
                // werknemer opslaan
                using (SqlConnection conn = new SqlConnection(adminConnString))
                {
                    conn.Open();
                    SqlCommand convertCommand = new SqlCommand("dbo.spUpdateUser", conn);
                    convertCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    convertCommand.Parameters.AddWithValue("@id", txtId.Text);
                    convertCommand.Parameters.AddWithValue("@login", txtLogin.Text);
                    convertCommand.Parameters.AddWithValue("@email", txtEmail.Text);
                    convertCommand.Parameters.AddWithValue("@rolename", cmbRoles.Text);
                    convertCommand.Parameters.AddWithValue("@password", txtpaswoord.Text);
                    convertCommand.Parameters.AddWithValue("@lastname", txtLastname.Text);
                    convertCommand.Parameters.AddWithValue("@firstname", txtFirstname.Text);
                    convertCommand.Parameters.AddWithValue("@telephone", txtTelephone.Text);
                    convertCommand.Parameters.AddWithValue("@birthdate", Convert.ToDateTime(txtBirthdate.Text));

                    convertCommand.ExecuteNonQuery();
                    conn.Close();
                    loadUsers();
                    lblStatus.Text = $"{txtFirstname.Text} {txtLastname.Text} ({txtLogin.Text}) has been updated.";

                    txtId.Text = "";
                    txtpaswoord.Text = "";
                    txtFirstname.Text = "";
                    txtLastname.Text = "";
                    txtBirthdate.Text = "";
                    txtEmail.Text = "";
                    txtTelephone.Text = "";
                    txtLogin.Text = "";
                    cmbRoles.Text = "";
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        //================
        //Create users
        void btnAanmaken_Click(object sender, RoutedEventArgs e)
        {
            if (validInfo())
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(adminConnString))
                    {
                        conn.Open();
                        SqlCommand convertCommand = new SqlCommand("dbo.spAddUser", conn);
                        convertCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        convertCommand.Parameters.AddWithValue("@login", txtLoginNewUser.Text);
                        convertCommand.Parameters.AddWithValue("@email", txtEmailNewUser.Text);
                        convertCommand.Parameters.AddWithValue("@rolename", cmbRolesNewUser.Text);
                        convertCommand.Parameters.AddWithValue("@lastname", txtLastnameNewUser.Text);
                        convertCommand.Parameters.AddWithValue("@password", txtpaswoordNewUser.Text);
                        convertCommand.Parameters.AddWithValue("@firstname", txtFirstnameNewUser.Text);
                        convertCommand.Parameters.AddWithValue("@telephone", txtTelephoneNewUser.Text);

                        string gender = "U";
                        if (rbM.IsChecked == true) { gender = "M"; }
                        else if (rbF.IsChecked == true) { gender = "F"; }

                        string date = "";
                        if (txtBirthdateNewUser.Text != "")
                        {
                            date = txtBirthdateNewUser.SelectedDate.Value.ToString();
                        }
                        convertCommand.Parameters.AddWithValue("@gender", gender);
                        convertCommand.Parameters.AddWithValue("@birthdate", date);
                        convertCommand.ExecuteNonQuery();
                        conn.Close();

                        loadUsers();
                        lblStatus.Text = $"{txtFirstnameNewUser.Text} {txtLastnameNewUser.Text} with login {txtLoginNewUser.Text} and password {txtpaswoordNewUser.Text} has been created.";
                        clearUserTextboxes();
                    }
                }
                catch (Exception ee) { lblStatus.Text = ee.Message; }
            }
        }
        bool validInfo()
        {
            bool isValid = false;
            //als alles is ingevuld
            if (txtLoginNewUser.Text != "" && txtpaswoordNewUser.Text != "" && txtPasswordConfirmNewUser.Password != "" && txtFirstnameNewUser.Text != "" && txtLastnameNewUser.Text != "" && cmbRolesNewUser.Text != "")
            {
                if (txtpaswoordNewUser.Text == txtPasswordConfirmNewUser.Password)
                {
                    isValid = true;
                }
                else
                {
                    lblStatus.Text = "Passwords do not match. Please enter it again.";
                    txtPasswordConfirmNewUser.Password = "";
                    txtpaswoordNewUser.Text = "";
                }
            }
            else
            {
                lblStatus.Text = "Please fill in all necessary information. These fields are marked with '*'.";
                isValid = false;
            }
            return isValid;
        }
        //================
        //Clear textboxes
        void clearUserTextboxes()
        {
            rbU.IsChecked = true;
            rbF.IsChecked = false;
            rbM.IsChecked = false;
            txtLoginNewUser.Text = "";
            cmbRolesNewUser.Text = "";
            txtEmailNewUser.Text = "";
            txtpaswoordNewUser.Text = "";
            txtLastnameNewUser.Text = "";
            txtFirstnameNewUser.Text = "";
            txtBirthdateNewUser.Text = "";
            txtTelephoneNewUser.Text = "";
            txtPasswordConfirmNewUser.Password = "";
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            clearUserTextboxes();
        }
        //================
        //Delete Users
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)lstUsers.SelectedItem;
            DialogResult result = System.Windows.Forms.MessageBox.Show($"Are your sure you want to delete this user: {Environment.NewLine}{Environment.NewLine}" +
                $"=============================================={Environment.NewLine}" +
                $"{item.Content}{Environment.NewLine}" +
                $"==============================================", "Delete user", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(adminConnString))
                    {
                        // open connectie
                        conn.Open();
                        SqlCommand sqlCommand = new SqlCommand("dbo.spDeleteUser", conn);
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@id", item.Tag);
                        sqlCommand.ExecuteNonQuery();
                        conn.Close();
                    }
                    lstUsers.Items.Clear();
                    loadUsers();
                    txtId.Text = "";
                    txtLogin.Text = "";
                    txtEmail.Text = "";
                    txtLastname.Text = "";
                    txtpaswoord.Text = "";
                    txtBirthdate.Text = "";
                    txtFirstname.Text = "";
                    txtTelephone.Text = "";
                }
                catch (Exception ex)
                {
                   MessageBox.Show(ex.Message);
                }
            }
        }
        //================
        //History
        private void btnRefreshHistory_Click(object sender, RoutedEventArgs e)
        {
            loadHistory();
        }
        void loadHistory()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(adminConnString))
                {
                    // open connectie
                    conn.Open();

                    // voer SQL commando uit
                    SqlCommand sqlCommand = new SqlCommand("select * from dbo.vwHistory", conn);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    lstHistory.Items.Clear();
                    // lees en verwerk resultaten
                    while (reader.Read())
                    {
                        string price = Convert.ToString(reader["price"]);
                        string itemid = Convert.ToString(reader["item_id"]);
                        int factuurid = Convert.ToInt32(reader["factuur_id"]);
                        string buyer = Convert.ToString(reader["Bfirst"]) + " " + Convert.ToString(reader["Blast"]);
                        string seller = Convert.ToString(reader["Sfirst"]) + " " + Convert.ToString(reader["Slast"]);
                        if (itemid == "Null")
                        {
                            itemid = "Verwijderd";
                        }

                        ListBoxItem item = new ListBoxItem();
                        item.Content = $"Invoice: {factuurid} | Buyer: {buyer} | Seller: {seller} | Price: {price} | Item: {itemid}";

                        lstHistory.Items.Add(item);
                    }
                    conn.Close();
                }
            }
            catch (SqlException ex) { System.Windows.MessageBox.Show("Er is een fout opgetreden: " + ex.Message); }
        }
        //================
    }*/
}
