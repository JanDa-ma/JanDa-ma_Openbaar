using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoekBeheerInterfaces
{
    public interface IBoek
    {
        public int Id { get; set; }
        public int Bladzijden { get; set; }

        public decimal Prijs { get; set; }

        public String Taal { get; set; }
        public String Titel { get; set; }
        public byte[] Cover { get; set; }
        public String Beschrijving { get; set; }

        public int Jaar { get; set; }


    }
}
