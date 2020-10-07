using ClassLibraryVintage;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Button = System.Windows.Controls.Button;
using MessageBox = System.Windows.Forms.MessageBox;
using Orientation = System.Windows.Controls.Orientation;
using TextBox = System.Windows.Controls.TextBox;

namespace prjSeller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class wpfSeller : Window
    {
        User user;
        string sellerConnString;
        public wpfSeller(User us)
        {
            InitializeComponent();
            user = us;
            sellerConnString = ConfigurationManager.AppSettings["sellerConnString"];
            LoadMyItems();
        }
        //================
        //Menu
        //================
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
            wpfAbout wpf = new wpfAbout();
            wpf.Show();
        }
        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            wpfProfile wpf = new wpfProfile(user);
            wpf.Show();
        }
        //================

        /// <summary>
        /// Load items in lstItems
        /// </summary>
        void LoadMyItems()
        {
            pnlMyitems.Children.Clear();
            foreach (Item item in Item.LoadMyItems(user, sellerConnString))
            {
                Image image1 =new Image();
                image1.Height = 100;
                image1.Source = new BitmapImage(new Uri("images/noImage.png", UriKind.Relative));
                if(item.item_coverphoto!=null)
                {
                    using (SqlConnection conn = new SqlConnection(sellerConnString))
                    {
                        SqlCommand convertCommand = new SqlCommand("dbo.spPhotos", conn);
                        convertCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        convertCommand.Parameters.AddWithValue("@id", item.item_coverphoto);
                        conn.Open();
                        SqlDataReader reader = convertCommand.ExecuteReader();
                        reader.Read();
                        BitmapImage image = new BitmapImage();
                        try
                        {
                            image.BeginInit();
                            image.CacheOption = BitmapCacheOption.OnLoad;
                            image.StreamSource = new MemoryStream((byte[])reader["data"]);
                            image.EndInit();
                            image1.Source = image;
                        }
                        catch (Exception)
                        {
                            image1.Source= new BitmapImage(new Uri("images/error.png", UriKind.Relative));
                        }
                        
                        reader.Close();
                    }
                }
                TextBox textBlock = new TextBox();
                textBlock.Text = item.item_name;
                textBlock.TextWrapping = TextWrapping.Wrap;
                textBlock.Height = 70;
                textBlock.IsReadOnly = true;
                textBlock.BorderBrush = Brushes.Transparent;
                textBlock.Background = Brushes.Transparent;
                textBlock.Padding = new Thickness(5);

                Button btn = new Button();
                btn.Tag = item.item_id;
                btn.Content = "More ...";
                btn.Margin = new Thickness(5);
                btn.Background = Brushes.White;
                btn.Click += new RoutedEventHandler(MyItem_Click);
                btn.BorderBrush = Brushes.Transparent;

                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Vertical;
                stackPanel.Width = 120;
                stackPanel.Height = 220;
                stackPanel.Margin = new Thickness(5);
                stackPanel.Background = Brushes.LightGray;

                stackPanel.Children.Add(image1);
                stackPanel.Children.Add(textBlock);
                stackPanel.Children.Add(btn);

                pnlMyitems.Children.Add(stackPanel);
            }
        }
        private void MyItem_Click(object sender, RoutedEventArgs e)
        {
            btnEdit.IsEnabled = true;
            btnDelete.IsEnabled = true;
            Button button = (Button)sender;
            foreach (Item item in Item.LoadMyItems(user, sellerConnString))
            {
                if (item.item_id == Convert.ToInt32(button.Tag))
                {
                    ;
                    txtName.Text = item.item_name;
                    txtItemId.Text = Convert.ToString(item.item_id);
                    txtProperties.Text = item.item_properties;
                    cmbCategoryName.Text = item.item_categoryName;
                    txtPrice.Text = item.item_price.ToString();
                    txtDescription.Text = item.item_description;
                    txtCoverphotoId.Text = item.item_coverphoto.ToString();
                    if (item.item_isForSale == "Y")
                    {
                        rbYes.IsChecked = true;
                    }
                    else
                    {
                        rbNo.IsChecked = true;
                    }
                }
            }
        }
        /// <summary>
        /// Delete item
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Item item = new Item();
            foreach (Item itm in Item.LoadMyItems(user, sellerConnString))
            {
                if (Convert.ToInt32(txtItemId.Text) == itm.item_id)
                {
                    item = itm;
                }
            };
            item.DeleteItem(sellerConnString);
            updateLstItems();
            lblStatus.Text = $"You deleted {item.item_name}";
        }
        /// <summary>
        /// Make new item
        /// </summary>
        private void btnAanmaken_Click(object sender, RoutedEventArgs e)
        {
            if (validInfo())
            {
                Item item = new Item();
                item.item_name = txtNameNewItem.Text;
                item.item_description = txtDescriptionNewItem.Text;
                item.item_properties = txtPropertiesNewItem.Text;
                item.item_price = Convert.ToDecimal(txtPriceNewItem.Text);
                item.item_isForSale = rbNoNewItem.IsChecked == true ? "N" : "Y";
                item.item_categoryName = cmbRolesNewUser.Text;

                if (txtPhotoPath.Text != "")
                {
                    FileInfo fi = new FileInfo(txtPhotoPath.Text);
                    byte[] imageData = File.ReadAllBytes(txtPhotoPath.Text);
                    item.item_photoName = fi.Name;
                    item.item_data = imageData;
                }
                item.item_ownerid=user.person_id;
                item.CreateItem(sellerConnString);

                LoadMyItems();
                lblStatus.Text = $"{txtNameNewItem.Text} has been created.";
                clearNewUser();
            }
        }

        /// <summary>
        /// clear new user field
        /// </summary>
        void clearNewUser()
        {
            //clear
            txtPhotoPath.Text = "";
            txtNameNewItem.Text = "";
            cmbRolesNewUser.Text = "";
            txtPriceNewItem.Text = "";
            txtPropertiesNewItem.Text = "";
            txtDescriptionNewItem.Text = "";

            rbNoNewItem.IsChecked = false;
            rbYesNewItem.IsChecked = false;
        }
        /// <summary>
        /// Valid if all fields are filled in
        /// </summary>
        /// <returns>If valid</returns>
        bool validInfo()
        {
            bool isValid = false;
            //als alles is ingevuld
            if (txtNameNewItem.Text != "" && txtDescriptionNewItem.Text != "" && txtPriceNewItem.Text != "" && cmbRolesNewUser.Text != "" &&
                (rbNoNewItem.IsChecked == true || rbYesNewItem.IsChecked == true))
            {
                isValid = true;
            }
            else
            {
                lblStatus.Text = "Please fill in all necessary information. These fields are marked with '*'.";
            }
            return isValid;
        }

        /// <summary>
        /// chose photo
        /// </summary>
        private void btnChoosePhotos_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtPhotoPath.Text = openFileDialog.FileName;
            }
        }

        void updateLstItems()
        {
            pnlMyitems.Children.Clear();
            LoadMyItems();

            txtName.Text = "";
            txtPrice.Text = "";
            txtItemId.Text = "";
            txtProperties.Text = "";
            txtDescription.Text = "";
            cmbCategoryName.Text = "";
            txtCoverphotoId.Text = "";

            rbNo.IsChecked = false;
            rbYes.IsChecked = false;

            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Item item = new Item(); ;
            foreach (Item itm in Item.LoadMyItems(user, sellerConnString))
            {
                if (itm.item_id == Convert.ToInt32(txtItemId.Text))
                {
                    item = itm;
                }
            }
            string forSale;
            if (rbYes.IsChecked == true) { forSale = "Y"; }
            else if (rbNo.IsChecked == true) { forSale = "N"; }
            else { forSale = ""; }
            if (txtName.Text != "" && txtDescription.Text != "" && txtPrice.Text != "" && forSale != "" && cmbCategoryName.Text != "")
            {
                item.item_isForSale = forSale;
                item.item_name = txtName.Text;
                item.item_price = Convert.ToDecimal(txtPrice.Text);
                item.item_properties = txtProperties.Text;
                item.item_description = txtDescription.Text;
                item.item_categoryName = cmbCategoryName.Text;
                if(txtCoverphotoId.Text!="")
                {
                    item.item_coverphoto = Convert.ToInt32(txtCoverphotoId.Text);
                }


                item.editItem(sellerConnString);
            }
            updateLstItems();
            lblStatus.Text = $"The item {txtName.Text} has been updated";
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

