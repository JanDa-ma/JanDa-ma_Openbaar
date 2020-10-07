using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opdracht_ImmoWin
{
    class Adres : IAdres
    {
        public Adres(string straat, string gemeente, int postcode, int huisnummer)
        {
            _gemeente = gemeente;
            _postcode = postcode;
            _huisnummer = huisnummer;
            _straat = straat;
        }

        //toekenning variabele
        public int _postcode { get; set; }
        public int _huisnummer { get; set; }
        public string _gemeente { get; set; }
        public string _straat { get; set; }
    }
}
