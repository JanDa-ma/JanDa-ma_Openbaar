using BoekBeheerInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace BoekBeheer.Data
{
    [Table("Auteurs")]
    public class Auteur:IAuteur, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] String propertyName = null)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
   
        private int _id;
        private String _voornaam;
        private String _achternaam;

       
        private Auteur() { }
        public Auteur(String voornaam,String achternaam)
        {
            _voornaam = voornaam;
            _achternaam = achternaam;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get { return _id;}
            set { _id = value; }
        }
        public String Voornaam
        {
            get { return _voornaam; }
            set 
            { 
                _voornaam = value;
                OnPropertyChanged();
            }
        }
        public String Achternaam
        {
            get { return _achternaam; }
            set 
            { 
                _achternaam = value;
                OnPropertyChanged();
            }
        }
        public List<Boek> geschrevenBoeken { get ; set; } = new List<Boek>();
        public override string ToString()
        {
            return $"{Voornaam} {Achternaam}";
        }
    }
}
