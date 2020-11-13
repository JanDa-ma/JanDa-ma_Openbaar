using BoekBeheer.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BoekBeheer.Business
{
    public class RepositoryConnected
    { 
        //BoekbeheerContext aanmaken
        public BoekBeheerContext Context { get; private set; }
        
        //Repository koppelen aan BoekbeheerContext
        public RepositoryConnected()
        {
            Context = new BoekBeheerContext();
        }

        /// <summary>
        /// Geeft de boeken terug, gekoppeld met tabel auteur en genre in de vorm van een lijst
        /// </summary>
        /// <returns>lijstobservable</returns>
        public ObservableCollection<Boek> VindBoeken()
        {
            List<Boek> lijst = Context.Boeken.Include(x => x.Auteur).Include(x=>x.Genre).ToList();

            ObservableCollection<Boek> lijstobservable = new ObservableCollection<Boek>(lijst);
            return  lijstobservable;
        }
        
       public ObservableCollection<Auteur> VindAuteurs(string auteur)
        {
            //List<Auteur> lijst1 = Context.Auteurs.Include(x => x.geschrevenBoeken.Select(x => x.Auteur).Where(f=> f.Voornaam==auteur)).ToList();
            ObservableCollection<Auteur> lijst = new ObservableCollection<Auteur>(Context.Auteurs.ToList<Auteur>());
            ObservableCollection<Auteur> obStrings = new ObservableCollection<Auteur>(lijst);
            
            return obStrings;
        }
        public ObservableCollection<Boek> GetBoekenByGenre(string genre)
        {
            //ist<Auteur> lijst = Context.Auteurs.Include(x => x.geschrevenBoeken.Select(y => y.Genre)).ToList();

            List<Boek> lijst3 = Context.Boeken.Include(c => c.Genre).Where(f => f.Genre.GenreNaam == genre).ToList();
            //ObservableCollection<Auteur> lijst = new ObservableCollection<Auteur>(Context.Auteurs.ToList<Auteur>());
            ObservableCollection<Boek> obStrings = new ObservableCollection<Boek>(lijst3);

            return obStrings;
        }
        public Boek VindBoekId(int id)
        {
            return Context.Boeken.FirstOrDefault(p => p.Id == id);
        }

        public Boek VindBoekTitel(string titel)
        {
            return Context.Boeken.FirstOrDefault(p => p.Titel == titel);
        }

        public Boek VoegBoekToe(Boek boek)
        {
            Context.Boeken.Add(boek);
            //entity state
            Context.SaveChanges();
            return boek;
        }
        public void VerwijderBoek(Boek boek)
        {
            Context.Boeken.Remove(boek);
            Context.SaveChanges();
        }

        public Boek UpdateBoek(Boek boek)
        {
            Context.SaveChanges();
            return boek;
        }
        public List<Boek> VindGenre(string genre)
        {
            List<Boek> list =Context.Boeken.Include(p => p.Genre).Where(p => p.Genre.GenreNaam == genre).ToList();
            return list;
        }
    }
}
