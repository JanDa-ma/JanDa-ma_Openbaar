using System;

namespace ClassLibraryImmo
{
    class Huis : Woning, IHuis, IComparable<Huis>
    {
        public Huis(string SoortWoning, string Straat, int Huisnummer, int postcode, string Gemeente, double prijs) : base(prijs, Straat, Gemeente, postcode, Huisnummer)
        {
            _soortWoning = SoortWoning;
        }

        public string _soortWoning { get; set; }

        public int CompareTo(Huis other)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"{_soortWoning}: {adres._straat} nr: {adres._huisnummer}, {adres._postcode} {adres._postcode} | {_prijs}{Environment.NewLine}";
        }
    }
}
