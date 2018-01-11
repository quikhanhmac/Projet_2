using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandHotel.Pages
{
    class PageResultatHotel : MenuPage
    {
        public PageResultatHotel(): base("ResultatHotel", false)
        {
            Menu.AddOption("1", "Afficher la liste des clients sans numéro", AfficherClientSansNumero);
        }

        private void AfficherClientSansNumero()
        {
            var list = Contexte.GetClientSansNumero();
            ConsoleTable.From(list, "Clients").Display("Liste des clients sans numéro");
        }
    }
}
