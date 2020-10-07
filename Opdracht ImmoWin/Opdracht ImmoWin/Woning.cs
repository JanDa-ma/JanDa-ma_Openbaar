namespace Opdracht_ImmoWin
{
    class Woning : IWoning
    {
        public Woning(double prijs, string straat, string gemeente, int postcode, int huisnummer)
        {
            _prijs = prijs;
            adres = new Adres(straat, gemeente, postcode, huisnummer);
        }
        public Adres adres { get; set; }
        public string _typeWoning { get; set; }
        public double _prijs { get; set; }
        public int CompareTo(object other)
        {
            Woning a = other as Woning;

            int result;

            if ((result = this._prijs.CompareTo(a._prijs)) == 0)
            {
                if ((result = this.adres._huisnummer.CompareTo(a.adres._huisnummer)) == 0)
                {
                    result = a.adres._gemeente.CompareTo(a.adres._gemeente);
                }
            }
            return result;
        }
    }
}
