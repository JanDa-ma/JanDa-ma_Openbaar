using ClassLibraryVintage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Button = System.Windows.Controls.Button;
using MessageBox = System.Windows.Forms.MessageBox;
using Orientation = System.Windows.Controls.Orientation;
using TextBox = System.Windows.Controls.TextBox;

namespace prjAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class wpfAdmin : Window
    {
        User user;
        string adminConnString = ConfigurationManager.AppSettings["adminConnString"];
        public wpfAdmin(User us)
        {
            InitializeComponent();
            user = us;
            loadUsers();
            loadHistory();
        }
        void loadUsers()
        {
            pnlUsers.Children.Clear();
            foreach (Person person in Person.persons)
            {
                Image img = new Image();
                img.Height = 70;
                img.Margin = new Thickness(5);
                if (person.person_roleName == "admin")
                {
                    img.Source = new BitmapImage(new Uri("images/admin.png", UriKind.Relative));
                }
                else if (person.person_roleName == "seller")
                {
                    img.Source = new BitmapImage(new Uri("images/seller.png", UriKind.Relative));
                }
                else if (person.person_roleName == "buyer")
                {
                    img.Source = new BitmapImage(new Uri("images/buyer.png", UriKind.Relative));
                }

                TextBox textBlock = new TextBox();
                textBlock.Text = $"{person.person_id}: {person.person_login}"; //indien een user 2 of meerdere rollen heeft
                textBlock.BorderBrush = Brushes.Transparent;
                textBlock.Height = 30;
                textBlock.IsReadOnly = true;
                textBlock.Padding = new Thickness(5);
                textBlock.Background = Brushes.Transparent;
                textBlock.TextWrapping = TextWrapping.Wrap;

                Button btn = new Button();

                btn.Tag = person.person_id + " " + person.person_roleName;
                btn.Content = "More ...";
                btn.Margin = new Thickness(5);
                btn.Background = Brushes.White;
                btn.Click += new RoutedEventHandler(UserClick);

                StackPanel stackPanel = new StackPanel();
                stackPanel.Width = 120;
                stackPanel.Height = 150;
                stackPanel.Margin = new Thickness(5);
                stackPanel.Background = Brushes.LightGray;
                stackPanel.Orientation = Orientation.Vertical;

                stackPanel.Children.Add(img);
                stackPanel.Children.Add(textBlock);
                stackPanel.Children.Add(btn);

                pnlUsers.Children.Add(stackPanel);

            }
        }

        private void UserClick(object sender, RoutedEventArgs e)
        {
            Button btn = new Button();
            btn = (Button)sender;

            //info splitten om user met juiste role te kunnen weergeven
            string tag = btn.Tag.ToString();
            string[] info = tag.Split(' ');
            int personid = Convert.ToInt32(info[0]);
            string personrole = info[1];


            foreach (Person person in Person.persons)
            {
                if (person.person_id == personid && personrole == person.person_roleName)
                {
                    txtId.Text = person.person_id.ToString();
                    txtEmail.Text = person.person_email;
                    cmbRoles.Text = person.person_roleName;
                    txtpaswoord.Text = person.person_password;
                    txtLastname.Text = person.person_lastname;
                    txtLogin.Text = person.person_login;
                    txtFirstname.Text = person.person_firstname;
                    txtTelephone.Text = person.person_telephone;
                    txtBirthdate.Text = person.person_birthdate.ToString();
                    btnEdit.IsEnabled = true;
                    btnDelete.IsEnabled = true;
                }
            }
        }

        //Menu
        private void logOut_Click(object sender, RoutedEventArgs e)
        {
            DialogResult result = MessageBox.Show($"You are about to log out. All changes will not be saved.{Environment.NewLine}{Environment.NewLine}" +
                 $"Are you sure you want to exit?", "Log out", MessageBoxButtons.YesNo);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            wpfAbout wpfAbout = new wpfAbout();
            wpfAbout.Show();
        }
        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            wpfProfile wpf = new wpfProfile(user);
            wpf.Show();
        }
        //================
        //Edit users
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            foreach (Person person in Person.persons)
            {
                if (person.person_id == Convert.ToInt32(txtId.Text)) //if id of person is same as user to change
                {
                    if (user.person_id == person.person_id && user.person_roleName == person.person_roleName && cmbRoles.Text != "admin") //check if you change your own role from admin to another (yes => you will lose your adminrights)
                    {
                        DialogResult result = MessageBox.Show($"You are going to change your role. " +
                        $"If you change your role, you will be logged out because you no longer have the correct rights. {Environment.NewLine}{Environment.NewLine}" +
                        $"=============================================={Environment.NewLine}" +
                        $"Are your sure you want to change your role to '{cmbRoles.Text}' and no longer own the admin rights?{Environment.NewLine}" +
                        $"==============================================", "Delete user", MessageBoxButtons.YesNo);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            person.person_login = txtLogin.Text;
                            person.person_email = txtEmail.Text;
                            person.person_oldRoleName = person.person_roleName;
                            person.person_roleName = cmbRoles.Text;
                            person.person_password = txtpaswoord.Text;
                            person.person_lastname = txtLastname.Text;
                            person.person_firstname = txtFirstname.Text;
                            person.person_telephone = txtTelephone.Text;
                            person.person_birthdate = Convert.ToDateTime(txtBirthdate.Text);

                            person.updateUser();
                            loadUsers();
                            clearUserTextboxes();
                            this.Close();
                            break;
                        }
                    }
                    else
                    {
                        person.person_login = txtLogin.Text;
                        person.person_email = txtEmail.Text;
                        person.person_oldRoleName = person.person_roleName.ToString();
                        
                        if (cmbRoles.SelectedIndex == 0)
                        {
                            person.person_roleName = person.person_roleName;
                        }
                        else
                        {
                            person.person_roleName = cmbRoles.Text;
                        }
                        person.person_password = txtpaswoord.Text;
                        person.person_lastname = txtLastname.Text;
                        person.person_firstname = txtFirstname.Text;
                        person.person_telephone = txtTelephone.Text;
                        person.person_birthdate = Convert.ToDateTime(txtBirthdate.Text);
                        person.updateUser();

                        loadUsers();
                        clearUserTextboxes();
                        break;
                    }
                }
            }
        }
        //================
        //Create users
        void btnAanmaken_Click(object sender, RoutedEventArgs e)
        {
            if (validInfo())
            {
                Person person = new Person();

                string gender = "O";
                if (rbF.IsChecked == true) { gender = "F"; }
                else if (rbM.IsChecked == true) { gender = "M"; }
                person.person_gender = gender;

                if (txtEmailNewUser.Text != "")
                {
                    person.person_email = txtEmailNewUser.Text;
                }
                if (txtTelephoneNewUser.Text != "")
                {
                    person.person_telephone = txtTelephoneNewUser.Text;
                }
                person.person_login = txtLoginNewUser.Text;
                person.person_email = txtEmailNewUser.Text;
                person.person_roleName = cmbRolesNewUser.Text;
                person.person_password = txtpaswoordNewUser.Text;
                person.person_lastname = txtLastnameNewUser.Text;
                person.person_firstname = txtFirstnameNewUser.Text;
                person.person_telephone = txtTelephoneNewUser.Text;
                person.person_birthdate = Convert.ToDateTime(txtBirthdateNewUser.Text);
                person.CreateUser();
                loadUsers();
                clearNewUserTextboxes();
            }
        }
        bool validInfo()
        {
            bool isValid = false;
            //als alles is ingevuld
            if (txtLoginNewUser.Text != "" && txtpaswoordNewUser.Text != "" && txtPasswordConfirmNewUser.Password != "" && txtFirstnameNewUser.Text != "" && txtLastnameNewUser.Text != "" && cmbRolesNewUser.Text != "" &&
               txtBirthdateNewUser.Text != "")//kijken of datum goed is)
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
        void clearNewUserTextboxes()
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
        void clearUserTextboxes()
        {
            txtId.Text = "";
            cmbRoles.Text = "";
            txtLogin.Text = "";
            txtEmail.Text = "";
            txtLastname.Text = "";
            txtpaswoord.Text = "";
            txtBirthdate.Text = "";
            txtFirstname.Text = "";
            txtTelephone.Text = "";

            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            clearUserTextboxes();
        }
        //================
        //Delete Users
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            foreach (Person person in Person.persons)
            {
                if (person.person_id == Convert.ToInt32(txtId.Text))
                {
                    DialogResult result = MessageBox.Show($"Are your sure you want to delete this user: {Environment.NewLine}{Environment.NewLine}" +
                 $"=============================================={Environment.NewLine}" +
                 $"{person.person_firstname}{Environment.NewLine}" +
                 $"==============================================", "Delete user", MessageBoxButtons.YesNo);

                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        person.DeleteUser();
                        loadUsers();
                        clearUserTextboxes();
                    }
                }
            }
        }
        //================
        //History
        private void btnRefreshHistory_Click(object sender, RoutedEventArgs e)
        {
            loadHistory();
        }
        private void loadHistory()
        {
            pnlHistory.Children.Clear();
            foreach (Invoice invoice in Invoice.invoices)
            {
                TextBox textBlock = new TextBox();
                textBlock.Text = $"Invoice id: {invoice.invoice_id} {Environment.NewLine}" +
                    $"item id: {invoice.invoice_item} {Environment.NewLine}" +
                    $"buyer: {invoice.invoice_buyer}{Environment.NewLine}" +
                    $"seller: {invoice.invoice_seller}";

                textBlock.Width = 200;
                textBlock.Height = 100;
                textBlock.IsReadOnly = true;
                textBlock.Padding = new Thickness(5);
                textBlock.Margin = new Thickness(20);
                textBlock.Background = Brushes.LightGray;
                textBlock.TextWrapping = TextWrapping.Wrap;
                textBlock.BorderBrush = Brushes.Transparent;
                pnlHistory.Children.Add(textBlock);
            }
        }
    }
}
