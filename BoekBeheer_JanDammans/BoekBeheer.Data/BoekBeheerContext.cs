using BoekBeheerInterfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoekBeheer.Data
{
    public class BoekBeheerContext:DbContext
    {
        #region Fields & Properties

        public DbSet<Boek> Boeken{ get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Auteur> Auteurs { get; set; }
        public DbSet<Uitgever> Uitgevers { get; set; }

        #endregion
        #region
        public BoekBeheerContext() : base("BoekBeheer")
        {
            Database.SetInitializer<BoekBeheerContext>(new DropCreateDatabaseIfModelChangesBoekBeheerContext());
        }
        #endregion
    }
}
