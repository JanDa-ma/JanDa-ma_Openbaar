using System;

namespace Opdracht_ImmoWin
{
    interface IWoning : IComparable<object>
    {
        string _typeWoning { get; set; }
        double _prijs { get; set; }
    }
}
