using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ClassLibraryImmo;
namespace Opdracht_ImmoWin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<IWoning> woningen = new List<IWoning>();
        public MainWindow()
        {
            InitializeComponent();
       
            IWoning huis1 = new Huis("Driegevel", "Lipstraat", 54, 4500, "Polla", 27412);
            IWoning huis2 = new Huis("Rijhuis", "Makstraat", 1, 1500, "b", 27412);
            IWoning app1 = new Appartement("Lipsiusstraat", "Knokke",9500,45,152220,3);
            IWoning app2 = new Appartement("Konnektstraat", "Pola", 1510, 15, 2152415, 56);

            woningen.Add(huis1);
            woningen.Add(huis2);
            woningen.Add(app1);
            woningen.Add(app2);

            voegToeAanList();
        }
        void voegToeAanList()
        {
              lbItems.Items.Clear();
              foreach (object item in woningen)
              {
                  lbItems.Items.Add(item.ToString());
              }
        }
        private void btnSorteer_Click(object sender, RoutedEventArgs e)
        {
            woningen.Sort();
            lbItems.Items.Clear();
            foreach (var item in woningen)
            {
                lbItems.Items.Add(item);
            }
        }
    }
}
