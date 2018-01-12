using GrandHotel.Pages;
using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GrandHotel.Pages.PageAcceuil;

namespace GrandHotel
{
    class Program
    {
        static void Main(string[] args)
        {
            GrandHotelApp app = GrandHotelApp.Instance;
            app.Title = "GrandHotel";

            // Ajout des pages
            Page accueil = new PageAccueil();
            app.AddPage(accueil);
            app.AddPage(new PageClient());
            app.AddPage(new PageFacture());
            app.AddPage(new PageResultatHotel());

            // Affichage de la page d'accueil
            app.NavigateTo(accueil);

            // Lancement de l'appligfgdf
            // 
            app.Run();
        }
    }
}
