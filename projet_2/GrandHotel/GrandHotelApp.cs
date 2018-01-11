using GrandHotel.Pages;
using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandHotel
{
    
    public class GrandHotelApp : ConsoleApplication
    {
        private static GrandHotelApp _instance;
        
        public static GrandHotelApp Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GrandHotelApp();

                return _instance;
            }
        }


        // Constructeur
        public GrandHotelApp()
        {
            // Définition des options de menu à ajouter dans tous les menus de pages
            MenuPage.DefaultOptions.Add(
                new Option("a", "Accueil", () => _instance.NavigateHome()));

        }
    }
}
