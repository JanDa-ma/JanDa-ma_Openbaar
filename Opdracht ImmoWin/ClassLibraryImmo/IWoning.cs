using System;

namespace ClassLibraryImmo
{
    interface IWoning : IComparable<object>
    {
        string _typeWoning { get; set; }
        double _prijs { get; set; }
    }
}
