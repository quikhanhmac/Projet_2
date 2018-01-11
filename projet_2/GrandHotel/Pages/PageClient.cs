using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandHotel.Pages
{
    public class PageClient : MenuPage
    {
        public PageClient() : base("Client", false)
        {
            //Choix utilisateur
            Menu.AddOption("1", "Liste des clients", AfficherClients);
            Menu.AddOption("2", "Coordonnees d'un client", CoordonneesClient);
            Menu.AddOption("3", "Création nouveau client", CreationClient);
            Menu.AddOption("4", "Ajouter un numero telephone ou un mail", AjoutTelephoneMail);
        }
        private void AfficherClients()
        {
            //Appel de la methode
            List<Client> clients = Contexte.GetClients();
            //Affiche sous forme d'un tableau
            ConsoleTable.From(clients, "Clients").Display("Clients");
        }
        private void CoordonneesClient()
        {
            //Demande de saisie utilisateur
            int idclient1 = Input.Read<int>("Veuillez donner un numero d'identifiant client: ");
            List<Coordonnees> coordonnees = Contexte.GetCoordonneesClient(idclient1);
            ConsoleTable.From(coordonnees, "Coordonnees").Display("Coordonnees");
        }

        private void CreationClient()
        {
            Output.WriteLine("Saisissez les informations du client :");

            //Creation nouveau client
            Client c1 = new Client();
            c1.Civilite = Input.Read<string>("Civilite :");
            c1.Nom = Input.Read<string>("Nom :");
            c1.Prenom = Input.Read<string>("Prenom :");

            Contexte.AjouterClient(c1);
            Output.WriteLine(ConsoleColor.Green, "Client créé avec succès");
            Output.WriteLine("");


            //Methode permettant d'aller chercher le dernier numero identifiant créé
            int num = Contexte.NumeroClient();


            // Creation nouvelle adresse
            Adresse a1 = new Adresse();

            a1.IdClient = num;
            a1.Rue = Input.Read<string>("Rue :");
            a1.CodePostal = Input.Read<string>("Code Postal :");
            a1.Ville = Input.Read<string>("Ville :");

            Contexte.AjouterAdresse(a1);
            Output.WriteLine(ConsoleColor.Green, "Adresse créée avec succès");
            Output.WriteLine("");

        }

        private void AjoutTelephoneMail()
        {

            Console.WriteLine("1-Ajouter Telephone \t 2-mail");
            string saisie = Console.ReadLine();

            switch (saisie)
            {
                case "1":
                    List<Client> clients = Contexte.GetClients();
                    int idclient3 = Input.Read<int>("Veuillez donner un numero d'identifiant client: ");
                    Telephone t = new Telephone();
                    t.IdClient = idclient3;
                    t.Numero = Input.Read<string>("Veuillez donner un numero de telephone client: ");
                    t.CodeType = Input.Read<string>("Veuillez donner le type de telephone client(M mobile, F fixe): ");
                    t.Pro = Input.Read<byte>("Sagit-il d'un numero professionnel(0/1): ");

                    Contexte.GetAjouterTelephone(t);

                    break;

                case "2":
                    List<Client> clients2 = Contexte.GetClients();
                    int idclient4 = Input.Read<int>("Veuillez donner un numero d'identifiant client: ");
                    Email e = new Email();
                    e.IdClient = idclient4;
                    e.Adresse = Input.Read<string>("Veuillez donner un numero de mail client: ");
                    e.Pro = Input.Read<byte>("Sagit-il d'un mail professionnel(0/1): ");

                    Contexte.GetAjouterMail(e);
                    break;

            }





        }


    }

}
