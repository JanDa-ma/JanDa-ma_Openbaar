using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ClassLibraryVintage
{
    public class Item
    {
        public Item(int id, string name, string description, decimal price, string forSale, int ownerid, string categoryName)
        {
            item_id = id;
            item_name = name;
            item_description = description;
            item_price = price;
            item_isForSale = forSale;
            item_ownerid = ownerid;
            item_categoryName = categoryName;
        }
        public Item(int id, decimal price, string seller, string buyer, int invoice)
        {
            item_id = id;
            item_price = price;
            item_buyer = buyer;
            item_seller = seller;
            invoice_id = invoice;
        }
        public Item()
        {
        }
        public string item_seller { get; }
        public string item_buyer { get; }
        public int item_owner { get; set; }
        public byte[] item_data { get; set; }
        public int item_id { get; }
        public string item_photoName { get; set; }
        public int invoice_id { get; }
        public int? item_coverphoto { get; set; }
        public int item_ownerid { get; set; }
        public string item_name { get; set; }
        public string item_description { get; set; }
        public string item_properties { get; set; }
        public decimal item_price { get; set; }
        public string item_isForSale { get; set; }
        public string item_categoryName { get; set; }
        public bool editItem(string connString)
        {
            bool done = false;
            try
            {
                // werknemer opslaan
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    SqlCommand convertCommand = new SqlCommand("dbo.spUpdateItem", conn);
                    convertCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    convertCommand.Parameters.AddWithValue("@forSale", item_isForSale);
                    convertCommand.Parameters.AddWithValue("@id", item_id);
                    convertCommand.Parameters.AddWithValue("@name", item_name);
                    convertCommand.Parameters.AddWithValue("@price", item_price);
                    convertCommand.Parameters.AddWithValue("@properties", item_properties);
                    convertCommand.Parameters.AddWithValue("@Description", item_description);
                    convertCommand.Parameters.AddWithValue("@categoryName", item_categoryName);

                    if (item_coverphoto == null)
                    {
                        SqlParameter ip = new SqlParameter("@coverphotoId", item_coverphoto);
                        ip.Value = DBNull.Value;
                        convertCommand.Parameters.Add(ip);
                    }
                    else
                    {
                        convertCommand.Parameters.AddWithValue("@coverphotoId", item_coverphoto);
                    }
                    convertCommand.ExecuteNonQuery();
                    conn.Close();
                    done = true;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return done;
        }
    
        public bool DeleteItem( string connString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand("dbo.spDeleteItem", conn);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@itemid", item_id);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    conn.Close();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<Item> LoadItemsForSale(string connString)
        {
            //list voor forsale items aanmaken
            List<Item> itemsForSale = new List<Item>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    // open connectie
                    conn.Open();
                    // voer SQL commando uit
                    SqlCommand sqlCommand = new SqlCommand("SELECT * from vwForSaleItems", conn);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["id"]);
                        string name = Convert.ToString(reader["name"]);
                        string description = Convert.ToString(reader["description"]);
                        decimal price = Convert.ToDecimal(reader["price"]);
                        string forSale = Convert.ToString(reader["is_forsale"]);
                        string category_name = Convert.ToString(reader["category_name"]);
                        int owner_id = Convert.ToInt32(reader["owner_id"]);
                        Item item = new Item(id, name, description, price, forSale, owner_id, category_name);
                        if (Convert.ToString(reader["coverphoto_id"]) != DBNull.Value.ToString())
                        {
                            item.item_coverphoto = Convert.ToInt32(reader["coverphoto_id"]);
                        }
                        if (Convert.ToString(reader["properties"]) != DBNull.Value.ToString())
                        {
                            item.item_properties = Convert.ToString(reader["properties"]);
                        }
                        itemsForSale.Add(item);
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return itemsForSale;
        }
        public string buyItem(Item item, User user, string connString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand("dbo.spBuyItem", conn);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@itemid", item.item_id);
                    sqlCommand.Parameters.AddWithValue("@ownerid", user.person_id);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    conn.Close();
                }
                return $"Je hebt het item {item.item_name} gekocht. U ({user.person_firstname}) bent de nieuwe eigenaar!";
            }
            catch (Exception ex) { return ex.Message; }
        }
        public static List<Item> filterItem(string category, int? price, string connString)
        {
            List<Item> items = new List<Item>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    // open connectie
                    conn.Open();
                    // voer SQL commando uit
                    SqlCommand sqlCommand = new SqlCommand("SELECT * from vwForSaleItems", conn);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        int _id = Convert.ToInt32(reader["id"]);
                        string _name = Convert.ToString(reader["name"]);
                        string _description = Convert.ToString(reader["description"]);
                        decimal _price = Convert.ToDecimal(reader["price"]);
                        string _forSale = Convert.ToString(reader["is_forsale"]);
                        string _category_name = Convert.ToString(reader["category_name"]);
                        int _owner_id = Convert.ToInt32(reader["owner_id"]);
                        Item item = new Item(_id, _name, _description, _price, _forSale, _owner_id, _category_name);
                        if (Convert.ToString(reader["coverphoto_id"]) != DBNull.Value.ToString())
                        {
                            item.item_coverphoto = Convert.ToInt32(reader["coverphoto_id"]);
                        }
                        if (Convert.ToString(reader["properties"]) != DBNull.Value.ToString())
                        {
                            item.item_properties = Convert.ToString(reader["properties"]);
                        }
                        else
                        {
                            item.item_properties = "";
                        }
                        if ((category == "" || _category_name == category) && (price >= _price || price == null))
                        {
                            items.Add(item);
                        }
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message);}

            return items;
        }
        public override string ToString()
        {
            return $"{item_id}: {item_name}";
        }
        public static List<Item> LoadMyItems(User user,string connString)
        {
            List<Item> myItems = new List<Item>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand("dbo.spItemsWithId", conn);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@id", user.person_id);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["id"]);
                        string name = Convert.ToString(reader["name"]);
                        string description = Convert.ToString(reader["description"]);
                        decimal price = Convert.ToDecimal(reader["price"]);
                        string forSale = Convert.ToString(reader["is_forsale"]);
                        string category_name = Convert.ToString(reader["category_name"]);
                        int owner_id = Convert.ToInt32(reader["owner_id"]);
                        Item item = new Item(id, name, description, price, forSale, owner_id, category_name);
                        if (Convert.ToString(reader["coverphoto_id"]) != DBNull.Value.ToString())
                        {
                            item.item_coverphoto = Convert.ToInt32(reader["coverphoto_id"]);
                        }

                        if (Convert.ToString(reader["properties"]) != DBNull.Value.ToString())
                        {
                            item.item_properties = Convert.ToString(reader["properties"]);
                        }
                        else
                        {
                            item.item_properties = "";
                        }
                        myItems.Add(item);
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return myItems;
        }
        public bool CreateItem(string connString)
        {
            bool done = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    SqlCommand convertCommand = new SqlCommand("dbo.spAddItem", conn);
                    convertCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    convertCommand.Parameters.AddWithValue("@name", item_name);
                    convertCommand.Parameters.AddWithValue("@price", item_price);
                    convertCommand.Parameters.AddWithValue("@owner_id", item_ownerid);
                    convertCommand.Parameters.AddWithValue("@isforSale", item_isForSale);
                    convertCommand.Parameters.AddWithValue("@properties", item_properties);
                    convertCommand.Parameters.AddWithValue("@Description", item_description);
                    convertCommand.Parameters.AddWithValue("@category_name", item_categoryName);

                    if (item_photoName != null)
                    {
                        convertCommand.Parameters.AddWithValue("@namePhoto", item_photoName);
                        convertCommand.Parameters.AddWithValue("@data", item_data);
                    }
                    else
                    {
                        convertCommand.Parameters.AddWithValue("@namePhoto", DBNull.Value);
                        byte[] data = new byte[Convert.ToInt32(0)];
                        convertCommand.Parameters.AddWithValue("@data", data);
                    }

                    convertCommand.ExecuteNonQuery();
                    conn.Close();
                    done = true;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return done;
        }
    }
}
