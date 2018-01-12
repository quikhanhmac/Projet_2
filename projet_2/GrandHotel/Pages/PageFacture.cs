using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace GrandHotel.Pages
{
    public class PageFacture : MenuPage
    {
        public PageFacture() : base("Facture", false)
        {

            Menu.AddOption("1", "Des factures d'un an glissant à partir d'une date donnée ", AfficherFactures);
            Menu.AddOption("2", "Lignes de facture", AfficherLigneFacture);
            Menu.AddOption("3", "Saisir une facture", CreerFacture);
            //Menu.AddOption("4", "Saisir une ligne de facture",  CreerLigneFacture);
            Menu.AddOption("5", "Mettre à jour la date et mode de paiement d'une facture", ModifierFacture);
        }

        private void AfficherFactures()
        {
            int iD = Input.Read<int>("Saisir l'Id du client");
            int date = Input.Read<int>("Saisir l'année de la facture:");
            var Listdate = Contexte.GetFactures(iD, date);
            ConsoleTable.From(Listdate).Display("Factures");
        }

        private void AfficherLigneFacture()
        {
            int id = Input.Read<int>("Saisir Id de la facture:");
            var lignes = Contexte.GetLigneFacture(id);
            ConsoleTable.From(lignes).Display("Lignes");
        }

        private void CreerFacture()
        {
            /*Saisie des valeurs par l'utilisateur*/
            Output.WriteLine("Saisissez les informations de la facture :");
            Facture fct = new Facture();
            fct.IdClient = Input.Read<int>("Id du client :");
            fct.DateFacture = Input.Read<DateTime>("Date de la facture :");
            fct.DatePaiement = Input.Read<DateTime>("Date du paiement:");
            fct.CodeModePaiement = Input.Read<string>("Code de mode du paiement:");

            /*---> Condition sur le code de mode de paiement et sur les dates de paiement et de facture. Les dates doivent être
              ---> au moins inferieur à la date d'aujourd'hui et le code de payement prend les valeurs "CB", "CHQ" et "ESP" */
            bool CondCodeModePaiement = fct.CodeModePaiement == "CB" || fct.CodeModePaiement == "CHQ" || fct.CodeModePaiement == "ESP";
            bool CondDate = fct.DateFacture <= DateTime.Today && fct.DatePaiement <= DateTime.Today;

            if (CondCodeModePaiement && CondDate)
            {
                Contexte.AjouterFacture(fct);
                Output.WriteLine(ConsoleColor.Green, "Facture créée avec succès");
            }
            else
                Output.WriteLine(ConsoleColor.Red,
                    "Le code de mode de paiement ou les dates ne sont pas validé");
        }

        private void ModifierFacture()
        {

            // Récupère le produit dont l'id a été saisi
            int id = Input.Read<int>("Id  de la facture à modifier :");
            var facture = Contexte.GetFacture(id);
            Facture fact = new Facture();
            if (facture != null)
            {
                // Ddemande les nouvelles valeurs des infos du produit, en proposant les valeurs actuelles par défaut
                Output.WriteLine("Modifiez chaque information du produit ou appuyez sur Entrée pour conserver la valeur actuelle :");
                fact.DateFacture = Input.Read<DateTime>("la date du facture:", facture.DateFacture);
                fact.CodeModePaiement = Input.Read<String>("Id de la catégorie :", facture.CodeModePaiement);
                // Enregistrement dans la base
                Contexte.ModifierFacture(fact);
                Output.WriteLine(ConsoleColor.Green, "Facture modifié avec succès");
            }
            else
                Output.WriteLine(ConsoleColor.Red, "L'id de la facture n'existe pas");

        }

    }




}


