using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using GrandHotel.Properties;

namespace GrandHotel
{



    /*==============================================Lydia Methodes===============================================*/

    public class Contexte
    {
        /*Récupération de la liste des clients sans numéro de téléphone*/
        public static List<Client> GetClientSansNumero()
        {
            List<Client> ListClients = new List<Client>();
            // On créé une commande et on définit le code sql à exécuter
            var com = new SqlCommand();
            com.CommandText = @"select Id, Nom
            from Client 
            inner join Telephone  on (Idclient = Id) 
            where Numero is null ";

            // On crée une connexion à partir de la chaîne de connexion stockée
            // dans les paramètres de l'appli
            using (var cnx = new SqlConnection(Settings1.Default.GrandHotelConnect))
            {
                // On affecte la connexion à la commande
                com.Connection = cnx;
                // On ouvre la connexion
                cnx.Open();

                // On exécute la commande en récupérant son résultat dans un objet SqlDataRedader
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    // On lit les lignes de résultat une par une
                    while (sdr.Read())
                    {
                        //...et pour chacune on crée un objet qu'on ajoute à la liste
                        var client = new Client();
                        client.Id = (int)sdr["Id"];
                        client.Nom = (string)sdr["Nom"];

                        ListClients.Add(client);
                    }
                }
            }
            // Le fait d'avoir créé la connexion dans une instruction using
            // permet de fermer cette connexion automatiquement à la fin du bloc using

            return ListClients;
        }

    }

    /*==============================================Lydia Methodes===============================================*/
}
