using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoekBeheerInterfaces
{
    public interface IGenre
    {
        public int Id { get; set; }
        public String GenreNaam{ get; set; }
    }
}
