using BoekBeheerInterfaces;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BoekBeheer.Data
{
    [Table("Boeken")]
    public class Boek : IBoek, INotifyPropertyChanged
    {

        #region Properties

        //integers
        private int _id;
        private int _jaar;
        private int _bladzijden;

        //Strings
        private String _taal = "Onbekend";
        private String _titel;
        private String _beschrijving;

        //decimals
        private decimal _prijs;

        //bytes[]
        byte[] _cover;

        //Genre
        private Genre _genre;

        //Auteurs
        private Auteur _auteur;

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] String propertyName = null)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }


        #region constructors
        private Boek() { }

        public Boek(String titel, int bladzijde, Genre genre, Auteur auteur, String taal, String beschrijving, int jaar, byte[] cover, decimal prijs)
            : this(0, titel, bladzijde, genre, auteur, taal, beschrijving, jaar, cover, prijs)
        {
          
        }
        internal Boek(int id, String titel, int bladzijde, Genre genre, Auteur auteur, String taal, String beschrijving, int jaar, byte[] cover, decimal prijs)
        {
            _id = id;
            _titel = titel;
            _bladzijden = bladzijde;
            _genre = genre;
            _auteur = auteur;
            _jaar = jaar;
            _taal = taal;
            _beschrijving = beschrijving;
            _cover = cover;
            _prijs = Prijs;
        }
        #endregion

        #region Fields & Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }
        public String Titel
        {
            get
            {
                return _titel;
            }
            set
            {
                _titel = value;
                OnPropertyChanged();
            }
        }
        public String Beschrijving
        {
            get
            {
                return _beschrijving;
            }
            set
            {
                _beschrijving = value;
                OnPropertyChanged();
            }
        }
        public String Taal
        {
            get
            {
                return _taal;
            }
            set
            {
                _taal = value;
                OnPropertyChanged();
            }
        }

        [Required]
        public Auteur Auteur
        {
            get
            {
                return _auteur;
            }
            set
            {
                _auteur = value;
                OnPropertyChanged();
            }
        }

        [Required]
        public Genre Genre
        {
            get
            {
                return _genre;
            }
            set
            {
                _genre = value;
                OnPropertyChanged();
            }
        }

        public int Bladzijden
        {
            get
            {
                return _bladzijden;
            }
            set
            {
                _bladzijden = value;
                OnPropertyChanged();
            }
        }
        public int Jaar
        {
            get
            {
                return _jaar;
            }
            set
            {
                _jaar = value;
                OnPropertyChanged();
            }
        }
        public decimal Prijs
        {
            get
            {
                return _prijs;
            }
            set
            {
                _prijs = value;
            }
        }
        public byte[] Cover
        {
            get
            {
                return _cover;
            }
            set
            {
                _cover = value;
                OnPropertyChanged();
            }

        }
        
        #endregion
        public override string ToString()
        {
            return $"{Titel}: {Auteur} ";
        }
    }
}
