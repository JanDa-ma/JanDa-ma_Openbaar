using BoekBeheerInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoekBeheer.Data
{
    [Table("Uitgevers")]
    public class Uitgever: IUitgever
    {
        public Uitgever()
        {

        }
        [Key]
        public int Id { get; set; }
        public String Bedrijfsnaam{ get; set; }
    }
}
