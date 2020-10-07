using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace slnVintageItems
{
    class Item
    {/*
        public Item(int id, string name, string description, decimal price, string forSale, int ownerid,string categoryName)
        {
            item_id = id;
            item_name = name;
            item_description = description;
            item_price = price;
            item_isForSale = forSale;
            item_ownerid = ownerid;
            item_categoryName = categoryName;
        }
        public Item()
        {
        }
        public int item_id { get; }
        public int? item_coverphoto { get; set; }
        public int item_ownerid { get; set; }
        public string item_name { get; set; }
        public string item_description { get; set; }
        public string item_properties { get; set; }
        public decimal item_price { get; set; }
        public string item_isForSale { get; set; }
        public string item_categoryName { get; set; }
    
        public bool deleteItem(Item item, string connString)
        {
            bool done = false;
            DialogResult result = MessageBox.Show($"Are you sure you want to delete this item: {Environment.NewLine}{Environment.NewLine}" +
                $"=============================================={Environment.NewLine}" +
                $"{item.item_name}{Environment.NewLine}" +
                $"==============================================", "Delete item", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        // open connectie
                        conn.Open();
                        SqlCommand sqlCommand = new SqlCommand("dbo.spDeleteItem", conn);
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@itemid", item.item_id);
                        sqlCommand.ExecuteNonQuery();
                        conn.Close();
                    }
                    done = true;
                }
                catch (Exception ex) {MessageBox.Show(ex.Message); }
            }

            return done;
        }
        public string buyItem(Item item, Buyer buyer, string connString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand("dbo.spBuyItem", conn);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@itemid", item.item_id);
                    sqlCommand.Parameters.AddWithValue("@ownerid", buyer.user_id);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    conn.Close();
                }
                return $"Je hebt het item {item.item_name} gekocht. U ({buyer.user_firstname}) bent de nieuwe eigenaar!";

            }
            catch (Exception ex) { return ex.Message; }
        }
        public static List<Item> filterItem(string? category,int? price, string connString)
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

                        if ((category==""||_category_name == category)&&(price<=_price||price==null))
                        {
                            items.Add(item);
                        }
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex) {}

            return items;
        }
        public Item itemWithId(int id, string connString)
        {
            Item item=new Item();
            string[] info=new string[6];
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    // open connectie
                    conn.Open();
                    // voer SQL commando uit
                    // voer SQL commando uit
                    SqlCommand sqlCommand = new SqlCommand("dbo.spItemWithId", conn);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@itemid", id);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    // lees en verwerk resultaten
                    reader.Read();
                        int _id = Convert.ToInt32(reader["id"]);
                        decimal price = Convert.ToDecimal(reader["price"]);
                        int owner_id = Convert.ToInt32(reader["owner_id"]);
                        string name = Convert.ToString(reader["name"]);
                        string prop = Convert.ToString(reader["properties"]);
                        string catname = Convert.ToString(reader["category_name"]);
                        string desc = Convert.ToString(reader["description"]);
                        string forS = Convert.ToString(reader["is_forsale"]);
                    reader.Close();
                    conn.Close();
                    item = new Item(_id,name,desc,price, forS,owner_id,catname);
                }
                
            }
            catch (Exception ex) { }
            return item;
        }
        public override string ToString()
        {
            return $"{item_id}: {item_name}";
        }*/
    }
}
 