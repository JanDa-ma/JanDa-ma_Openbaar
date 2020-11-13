using BoekBeheerInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BoekBeheer.Data
{
    public class Genre : IGenre, INotifyPropertyChanged
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
        private Genre() { }
    
        public Genre(String genre)
        {
            GenreNaam = genre;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        private String _genrenaam;
        public String GenreNaam 
        { 
            get 
            { 
                return _genrenaam; 
            } 
            set 
            { 
                _genrenaam = value;
                OnPropertyChanged();
            } 
        }
        public override string ToString()
        {
            return $"{GenreNaam}";
        }
    }
}
