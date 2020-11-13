using BoekBeheer.Business;
using BoekBeheer.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup.Localizer;
using System.Windows.Media.Imaging;

namespace BoekBeheerApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //globale variabelen
        Boek geselecteerdBoek;
        RepositoryConnected repository = new RepositoryConnected();
        ObservableCollection<Boek> boekenlijst = new ObservableCollection<Boek>();
        public MainWindow()
        {
            InitializeComponent();
            //boekenlijst = repository.GetBoeken();
            boekenlijst = repository.VindBoeken();
            lstBoeken.DataContext = boekenlijst;
            //lstBoeken.DataContext= repository.VindGenre("Koken");
            //lstBoeken.DataContext = repository.GetBoekenByGenre("Koken");
        }
        private void btnMaakNieuwBoekAan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String taal = txtTaalNieuwBoek.Text;
                String titel = txtTitelNieuwBoek.Text;
                int jaar = Convert.ToInt32(txtJaarNieuwBoek.Text);
                String beschrijving = txtBeschrijvingNieuwBoek.Text;
                int bladzijden = Convert.ToInt32(txtBladzijdenNieuwBoek.Text);

                Genre genre = new Genre(txtGenreNieuwBoek.Text);
                Auteur auteur = new Auteur(txtVoornaamAuteurNieuwBoek.Text, txtAchternaamAuteurNieuwBoek.Text);

                Boek boek = new Boek(titel, bladzijden, genre, auteur, taal, beschrijving, jaar, null, (decimal)28.5);

                repository.VoegBoekToe(boek);

                boekenlijst.Add(boek);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void txtNiew_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.SelectAll();
           
        }
        private void lstBoeken_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstBoeken.SelectedItem != null)
            {
                btnBewerkBoekCover.Visibility = Visibility.Visible;
                geselecteerdBoek = (Boek)lstBoeken.SelectedItem;
            }
            else
            {
                btnBewerkBoekCover.Visibility = Visibility.Hidden;
            }
        }


        private void btnBewerkBoekCover_Click(object sender, RoutedEventArgs e)
        {
            //geselcteerd boek initialiseren
            geselecteerdBoek = (Boek)lstBoeken.SelectedItem;

            //Afbeelding kiezen 
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();     // Openfiledialog aanmaken
            openFileDialog.Filter = "Image Files (*.jpg)|*.jpg";                                                // Alleen .jpg files toelaten
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);  // Locatie opening openfiledialog

            // Afbeelding geselecteerd en bevestigd
            if (openFileDialog.ShowDialog().ToString().Equals("OK"))
            {
                // Pad foto
                string sourcePath = openFileDialog.FileName;

                // Image aanmaken van gekozen foto
                System.Drawing.Image image = System.Drawing.Image.FromFile(sourcePath);

                // Cover toewijzen aan boek (conventie van foto naar byte[] array)
                geselecteerdBoek.Cover = ImageConverterDataBase.ImageToByteArray(image);

                // Boek updaten
                repository.UpdateBoek(geselecteerdBoek);

            }
        }
        private void btnVerwijderBoek_Click(object sender, RoutedEventArgs e)
        {
            using(BoekBeheerContext context = new BoekBeheerContext())
            {
                if(geselecteerdBoek != null)
                {
                    repository.VerwijderBoek(geselecteerdBoek);
                    boekenlijst.Remove(geselecteerdBoek);
                }
            }
            lstBoeken.SelectedItem = null;
        }

        private void gegevensBoekTekst_Focus(object sender, RoutedEventArgs e)
        {
            imgUpdateBoekLoading.Visibility = Visibility.Visible;
        }

        private void gegevensBoekTekst_LostFocus(object sender, RoutedEventArgs e)
        {
            imgUpdateBoekLoading.Visibility = Visibility.Hidden;
            repository.UpdateBoek(geselecteerdBoek);
         }
    }
}
