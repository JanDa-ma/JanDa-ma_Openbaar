using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibraryVintage
{
    public class Invoice
    {

        static string adminConnString = ConfigurationManager.AppSettings["adminConnString"];
        public Invoice(int invoiceid, string seller, string buyer, decimal price, int item)
        {
            invoice_id = invoiceid;
            invoice_buyer = buyer;
            invoice_seller = seller;
            invoice_price = price;
            invoice_item = item;
        }
        public int invoice_id { get; }
        public string invoice_buyer { get; }
        public string invoice_seller { get; }
        public decimal invoice_price { get; }
        public int invoice_item { get; }
        public static List<Invoice> invoices
        {
            get
            {
                List<Invoice> history = new List<Invoice>();
                try
                {
                    using (SqlConnection conn = new SqlConnection(adminConnString))
                    {
                        // open connectie
                        conn.Open();

                        // voer SQL commando uit
                        SqlCommand sqlCommand = new SqlCommand("select * from dbo.vwHistory", conn);
                        SqlDataReader reader = sqlCommand.ExecuteReader();
                        // lees en verwerk resultaten
                        while (reader.Read())
                        {
                            decimal price = Convert.ToDecimal(reader["price"]);
                            int itemid = Convert.ToInt32(reader["item_id"]);
                            int factuurid = Convert.ToInt32(reader["factuur_id"]);
                            string buyer = Convert.ToString(reader["Bfirst"]) + " " + Convert.ToString(reader["Blast"]);
                            string seller = Convert.ToString(reader["Sfirst"]) + " " + Convert.ToString(reader["Slast"]);

                            Invoice invoice = new Invoice(factuurid, seller, buyer, price, itemid);
                            history.Add(invoice);
                        }
                        conn.Close();
                    }
                }
                catch (SqlException ex) { MessageBox.Show("Er is een fout opgetreden: " + ex.Message); }

                return history;
            }
        }
    }
}

