using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoekBeheerInterfaces
{
    public interface IAuteur
    {
        public int Id { get; set ; }
        public String Voornaam { get; set; }
        public String Achternaam { get; set; }
    }
}
