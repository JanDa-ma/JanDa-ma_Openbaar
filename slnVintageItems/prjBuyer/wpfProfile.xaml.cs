using ClassLibraryVintage;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace prjBuyer
{
    /// <summary>
    /// Interaction logic for wpfProfile.xaml
    /// </summary>
    public partial class wpfProfile : Window
    {
        public wpfProfile(User user)
        {
            InitializeComponent();
            txtLogin.Text = user.person_login;
            txtRoleName.Text = user.person_roleName;
            txtfirstname.Text = user.person_firstname;
            txtUserId.Text = user.person_id.ToString();

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("images/image" + user.person_gender + ".png", UriKind.Relative);
            bitmap.EndInit();
            imgGender.Source = bitmap;

        }
    }
}
