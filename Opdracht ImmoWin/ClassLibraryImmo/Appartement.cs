using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryImmo
{
    class Appartement : Woning, IAppartement
    {
        public Appartement(string straat, string gemeente, int postcode, int huisnummer, double prijs, int verdieping) : base(prijs, straat, gemeente, postcode, huisnummer)
        {
            _verdieping = verdieping;
        }
        public int _verdieping { get; set; }
        public override string ToString()
        {
            return $"Appartement: verdieping: {_verdieping}, {adres._straat} nr: {adres._huisnummer}, {adres._postcode} {adres._postcode} | {_prijs}{Environment.NewLine}";
        }
    }
}
