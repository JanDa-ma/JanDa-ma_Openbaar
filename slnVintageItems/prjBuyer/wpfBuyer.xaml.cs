using ClassLibraryVintage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Button = System.Windows.Controls.Button;
using Label = System.Windows.Controls.Label;
using MessageBox = System.Windows.Forms.MessageBox;
using Orientation = System.Windows.Controls.Orientation;
using TextBox = System.Windows.Controls.TextBox;

namespace prjBuyer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class wpfBuyer : Window
    {
        User user;
        string buyerConnString = ConfigurationManager.AppSettings["buyerConnString"];
        public wpfBuyer(User us)
        {
            InitializeComponent();
            user = us;
            LoadMyItems();
            LoadItemsForSale(Item.LoadItemsForSale(buyerConnString));
        }
            
        //================
        //Load items
        void LoadMyItems()
        {
            pnlMyitems.Children.Clear();
            foreach (Item item in Item.LoadMyItems(user, buyerConnString))
            {
                Image image1 = new Image();
                image1.Height = 100;
                image1.Source = new BitmapImage(new Uri("images/noImage.png", UriKind.Relative));
                if (item.item_coverphoto != null)
                {
                    using (SqlConnection conn = new SqlConnection(buyerConnString))
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
                            image1.Source = new BitmapImage(new Uri("images/error.png", UriKind.Relative));
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
            btnBijwerken.IsEnabled = true;
            btnDelete.IsEnabled = true;
            Button button = (Button)sender;
            foreach (Item item in Item.LoadMyItems(user,buyerConnString))
            {
                if (item.item_id == Convert.ToInt32(button.Tag))
                {;
                    txtName.Text = item.item_name;
                    txtItemId.Text = Convert.ToString(item.item_id);
                    txtProperties.Text = item.item_properties;
                    cmbCategoryName.Text = item.item_categoryName;
                    txtPrice.Text = item.item_price.ToString();
                    txtDescription.Text = item.item_description;
                    txtCoverphotoId.Text = item.item_coverphoto.ToString();
                    if(item.item_isForSale=="Y")
                    {
                        rbYes.IsChecked = true;
                    }
                    else
                    {
                        rbNo.IsChecked = true;
                    }
                    btnBuy.IsEnabled = true;
                    imgBuy.Opacity = 1;
                }
            }
        }
        //================
        void LoadItemsForSale(List<Item> items)
        {
            foreach (Item item in items)
            {
                Image image1 = new Image();
                image1.Height = 100;
                image1.Source = new BitmapImage(new Uri("images/noImage.png", UriKind.Relative));
                if (item.item_coverphoto != null)
                {
                    using (SqlConnection conn = new SqlConnection(buyerConnString))
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
                            image1.Source = new BitmapImage(new Uri("images/error.png", UriKind.Relative));
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
                btn.Click += new RoutedEventHandler(ItemForSaleClick);
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

                pnlItemsForSale.Children.Add(stackPanel);
            }
        }
        //================
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
              wpfAbout wpf = new wpfAbout();
              wpf.Show();

        }
        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
              wpfProfile wpf = new wpfProfile(user);
              wpf.Show();
        }
        private void btnBijwerken_Click(object sender, RoutedEventArgs e)
        {
            Item item = new Item(); ;
            foreach (Item itm in Item.LoadMyItems(user,buyerConnString))
            {
                if(itm.item_id==Convert.ToInt32(txtItemId.Text))
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
                if (txtCoverphotoId.Text != "")
                {
                    item.item_coverphoto = Convert.ToInt32(txtCoverphotoId.Text);
                }

                item.editItem(buyerConnString);
            }
            updateLstItems();
            lblStatus.Text = $"The item {item.item_name} has been updated";
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
        }
        void updateLstItemsForSale()
        {
            pnlItemsForSale.Children.Clear();
            LoadItemsForSale(Item.LoadItemsForSale(buyerConnString));

            txtNameForSale.Text = "";
            txtPriceForSale.Text = "";
            txtItemIdForSale.Text = "";
            txtOwnerIdForSale.Text = "";
            txtCategoryForSale.Text = "";
            txtPropertiesForSale.Text = "";
            txtDescriptionForSale.Text = "";
            btnBuy.IsEnabled = false;
            imgBuy.Opacity = 0.3;
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Item item = new Item();
            foreach (Item itm in Item.LoadMyItems(user,buyerConnString))
            {
                if(Convert.ToInt32(txtItemId.Text)==itm.item_id)
                {
                    item = itm;
                }
            };
            item.DeleteItem(buyerConnString);
            updateLstItems();
        }
        //================
        //Buy items
        Item selectedItemForSale = new Item();
        private void ItemForSaleClick(object sender, RoutedEventArgs e)
        {
            btnBijwerken.IsEnabled = true;
            btnDelete.IsEnabled = true;
            Button button = (Button)sender;
            foreach (Item item in Item.LoadItemsForSale(buyerConnString))
            {
                if (item.item_id == Convert.ToInt32(button.Tag))
                {
                    //set text of item in textboxes
                    selectedItemForSale = item;
                    txtNameForSale.Text = item.item_name;
                    txtItemIdForSale.Text = item.item_id.ToString();
                    txtPropertiesForSale.Text = item.item_properties;
                    txtCategoryForSale.Text = item.item_categoryName;
                    txtPriceForSale.Text = item.item_price.ToString();
                    txtDescriptionForSale.Text = item.item_description;
                    txtOwnerIdForSale.Text = item.item_ownerid.ToString();

                    btnBuy.IsEnabled = true;
                    imgBuy.Opacity = 1;
                }
            }
        }
        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            lblStatus.Text = selectedItemForSale.buyItem(selectedItemForSale, user, buyerConnString);
            btnBuy.IsEnabled = false;
            imgBuy.Opacity = 0.3;
            updateLstItemsForSale();
            updateLstItems();
        }
        //================
        //Filter
        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            updateLstItemsForSale();
            int? price = null;
            string categoryName = "";

            if (txtPriceFilter.Text != "")
            {
                price = Convert.ToInt32(txtPriceFilter.Text);
            }
            if (cmbCategoryFilter.SelectedIndex != 0)
            {
                categoryName = cmbCategoryFilter.Text;
            }

            List<Item> items = Item.filterItem(categoryName, price, buyerConnString);
            pnlItemsForSale.Children.Clear();
            foreach (Item item in items)
            {
                Image image1 = new Image();
                image1.Height = 100;
                image1.Source = new BitmapImage(new Uri("images/noImage.png", UriKind.Relative));
                if (item.item_coverphoto != null)
                {
                    using (SqlConnection conn = new SqlConnection(buyerConnString))
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
                            image1.Source = new BitmapImage(new Uri("images/error.png", UriKind.Relative));
                        }

                        reader.Close();
                    }
                }
                TextBox textBlock = new TextBox();
                textBlock.Height = 70;
                textBlock.IsReadOnly = true;
                textBlock.Text = item.item_name;
                textBlock.Padding = new Thickness(5);
                textBlock.TextWrapping = TextWrapping.Wrap;
                textBlock.Background = Brushes.Transparent;
                textBlock.BorderBrush = Brushes.Transparent;

                Button btn = new Button();
                btn.Tag = item.item_id;
                btn.Content = "More ...";
                btn.Margin = new Thickness(5);
                btn.Background = Brushes.White;
                btn.BorderBrush = Brushes.Transparent;
                btn.Click += new RoutedEventHandler(ItemForSaleClick);

                StackPanel stackPanel = new StackPanel();
                stackPanel.Width = 120;
                stackPanel.Height = 220;
                stackPanel.Margin = new Thickness(5);
                stackPanel.Background = Brushes.LightGray;
                stackPanel.Orientation = Orientation.Vertical;

                stackPanel.Children.Add(image1);
                stackPanel.Children.Add(textBlock);
                stackPanel.Children.Add(btn);

                pnlItemsForSale.Children.Add(stackPanel);
            }
        }
    }
}
