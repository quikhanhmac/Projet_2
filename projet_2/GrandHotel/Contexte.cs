using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace GrandHotel
{
    class Contexte
    {
        //public static List<string> GetPaysFournisseurs()
        //{
        //    List<string> ListePaysFournisseur = new List<string>();
        //    var com = new SqlCommand();
        //    com.CommandText = @"select Id, Nom
        //    from Client 
        //    inner join Telephone  on (Idclient = Id) 
        //    where Numero is null ";
        //    using (var cnx = new SqlConnection(GCSettings.Default.Northwind2Connect))
        //    {
        //        // On affecte la connexion à la commande
        //        com.Connection = cnx;
        //        // On ouvre la connexion
        //        cnx.Open();

        //        // On exécute la commande en récupérant son résultat dans un objet SqlDataRedader
        //        using (SqlDataReader sdr = com.ExecuteReader())
        //        {
        //            // On lit les lignes de résultat une par une
        //            while (sdr.Read())
        //            {
        //                //...et pour chacune on crée un objet qu'on ajoute à la liste
        //                string pays;
        //                pays = (string)sdr["Country"];

        //                ListePaysFournisseur.Add(pays);
        //            }
        //        }
        //    }
        //    return ListePaysFournisseur;
        //}

    }
}
