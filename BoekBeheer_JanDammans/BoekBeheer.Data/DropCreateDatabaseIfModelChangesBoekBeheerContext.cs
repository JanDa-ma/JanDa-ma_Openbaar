using BoekBeheerInterfaces;
using System;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace BoekBeheer.Data
{
    internal class DropCreateDatabaseIfModelChangesBoekBeheerContext : DropCreateDatabaseIfModelChanges<BoekBeheerContext>
    {
        
        protected override void Seed(BoekBeheerContext context)
        {
            base.Seed(context);

            Auteur TomLanoy = new Auteur("Tom", "Lanoy");
            Genre Roman = new Genre("Roman");

            Auteur JeroenMeus = new Auteur("Jeroen", "Meus");
            Genre Koken = new Genre("Koken");

            var pad = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            Image image = Image.FromFile($"{pad}\\BoekBeheerApplication\\Afbeeldingen\\BoekCover1.jpg");
            byte[] cover1=ImageConverterDataBase.ImageToByteArray(image);

            image = Image.FromFile($"{pad}\\BoekBeheerApplication\\Afbeeldingen\\BoekCover2.jpg");
            byte[] cover2 = ImageConverterDataBase.ImageToByteArray(image);

            context.Boeken.Add(new Boek("Sprakeloos", 20, Roman,TomLanoy, "Engels", "In het boek beschrijft Lanoye zijn eigen moeder[2]. Hij heeft het vooral over de aftakeling van zijn moeder na haar beroerte. Naast deze beschrijving van zijn moeder doorspekt hij het boek ook met jeugdverhalen en -herinneringen. Ook de 'coming-out' van Lanoye speelt een kleine rol in deze ode. De lezer komt uiteindelijk te weten hoe de dood van een moeder een gezin uit elkaar kan drijven.", 2009,cover1, (decimal)29.5));
            context.Boeken.Add(new Boek("Dagelijkse Kost - koken met gezond verstand", 285 ,Koken,JeroenMeus, "Nederlands", "In ‘Dagelijkse kost. Koken met gezond verstand’ kookt Jeroen Meus met kennis van zaken, kwaliteitsvolle ingrediënten, juiste technieken én met smaak en plezier als uitgangspunt. Honderd nieuwe recepten: helder, eenvoudig, makkelijk, heerlijk, voedzaam, betaalbaar. Vanaf september 2016 een nieuw jaarlijks rendez-vous.", 2016,cover2,(decimal)29.5));
            
            context.Auteurs.Add(TomLanoy);
            context.Auteurs.Add(JeroenMeus);

            context.Genres.Add(Roman);
            context.Genres.Add(Koken);

            context.SaveChanges();
        }
    }
}
