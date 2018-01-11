using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using GrandHotel.Properties;

namespace GrandHotel
{

    public class Contexte
    {
        public static List<Client> GetClients()
        {
            var list = new List<Client>();
            var cmd = new SqlCommand();
            // Demande de la liste des clients
            cmd.CommandText = @"select S.Id, S.Civilite, S.Nom, S.Prenom
							from Client S
							order by 1";


            using (var conn = new SqlConnection(Settings1.Default.GrandHotelConnect))
            {
                //Connection à la base de données
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new Client();
                        item.Id = (int)reader["Id"];
                        item.Civilite = (string)reader["Civilite"];
                        item.Nom = (string)reader["Nom"];
                        item.Prenom = (string)reader["Prenom"];

                        list.Add(item);
                    }
                }
            }

            return list;
        }

        public static List<Coordonnees> GetCoordonneesClient(int idclient1)
        {
            var list = new List<Coordonnees>();
            var cmd = new SqlCommand();
            cmd.CommandText = @"select C.Nom, C.Prenom, T.Numero, E.Adresse, A.Rue, A.CodePostal, A.Ville
                            from Client C
                            inner join Telephone T on C.Id=T.IdClient
                            inner join Email E on C.Id= E.IdClient
                            inner join Adresse A on C.Id= A.IdClient
                            Where C.Id=@ideclient1
                            order by 1, 2";

            cmd.Parameters.Add(new SqlParameter
            {
                SqlDbType = SqlDbType.Int,
                ParameterName = "@ideclient1",
                Value = idclient1
            });

            using (var conn = new SqlConnection(Settings1.Default.GrandHotelConnect))
            {
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new Coordonnees();
                        item.Nom = (string)reader["Nom"];
                        item.Prenom = (string)reader["Prenom"];
                        item.Numero = (string)reader["Numero"];
                        item.Adresse = (string)reader["Adresse"];
                        item.Rue = (string)reader["Rue"];
                        item.CodePostal = (string)reader["CodePostal"];
                        item.Ville = (string)reader["Ville"];


                        list.Add(item);
                    }
                }
            }

            return list;
        }

        public static void AjouterClient(Client c1)
        {
            var cmd = new SqlCommand();


            cmd.CommandText = @"insert Client (Civilite, Nom, Prenom)
									values (@Civilite, @Nom, @Prenom)";


            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.NVarChar, ParameterName = "@Civilite", Value = c1.Civilite });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.NVarChar, ParameterName = "@Nom", Value = c1.Nom });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.NVarChar, ParameterName = "@Prenom", Value = c1.Prenom });

            using (var cnx = new SqlConnection(Settings1.Default.GrandHotelConnect))
            {
                cmd.Connection = cnx;
                cnx.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static int NumeroClient()
        {
            var cmd = new SqlCommand();


            cmd.CommandText = @"select top (1) Id
							    from Client
                                order by 1 desc";


            using (var cnx = new SqlConnection(Settings1.Default.GrandHotelConnect))
            {
                cmd.Connection = cnx;
                cnx.Open();
                return (int)cmd.ExecuteScalar();
            }
        }


        public static void AjouterAdresse(Adresse a1)
        {
            var cmd = new SqlCommand();


            cmd.CommandText = @"insert Adresse (IdClient, Rue, CodePostal, Ville)
									values (@IdClient, @Rue, @CodePostal, @Ville)";

            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Int, ParameterName = "@IdClient", Value = a1.IdClient });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.NVarChar, ParameterName = "@Rue", Value = a1.Rue });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.NVarChar, ParameterName = "@CodePostal", Value = a1.CodePostal });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.NVarChar, ParameterName = "@Ville", Value = a1.Ville });

            using (var cnx = new SqlConnection(Settings1.Default.GrandHotelConnect))
            {
                cmd.Connection = cnx;
                cnx.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void GetAjouterTelephone(Telephone t)
        {
            var cmd = new SqlCommand();


            cmd.CommandText = @"insert Telephone (IdClient, Numero, CodeType, Pro)
									values (@IdClient, @Numero, @CodeType, @Pro)";

            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Int, ParameterName = "@IdClient", Value = t.IdClient });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.NVarChar, ParameterName = "@Numero", Value = t.Numero });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.NVarChar, ParameterName = "@CodeType", Value = t.CodeType });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Bit, ParameterName = "@Pro", Value = t.Pro });

            using (var cnx = new SqlConnection(Settings1.Default.GrandHotelConnect))
            {
                cmd.Connection = cnx;
                cnx.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void GetAjouterMail(Email e)
        {
            var cmd = new SqlCommand();


            cmd.CommandText = @"insert Email (IdClient, Adresse, Pro)
									values (@IdClient, @Adresse, @Pro)";

            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Int, ParameterName = "@IdClient", Value = e.IdClient });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.NVarChar, ParameterName = "@Adresse", Value = e.Adresse });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Bit, ParameterName = "@Pro", Value = e.Pro });

            using (var cnx = new SqlConnection(Settings1.Default.GrandHotelConnect))
            {
                cmd.Connection = cnx;
                cnx.Open();
                cmd.ExecuteNonQuery();
            }
        }


        /*==============================================Lydia Methodes===============================================*/

        //public class Contexte
        //{
        //    /*Récupération de la liste des clients sans numéro de téléphone*/
        //    public static List<Client> GetClientSansNumero()
        //    {
        //        List<Client> ListClients = new List<Client>();
        //        On créé une commande et on définit le code sql à exécuter
        //        var com = new SqlCommand();
        //        com.CommandText = @"select Id, Nom
        //        from Client 
        //        inner join Telephone  on (Idclient = Id) 
        //        where Numero is null ";

        //        On crée une connexion à partir de la chaîne de connexion stockée
        //         dans les paramètres de l'appli
        //        using (var cnx = new SqlConnection(Settings1.Default.GrandHotelConnect))
        //        {
        //            On affecte la connexion à la commande
        //            com.Connection = cnx;
        //            On ouvre la connexion
        //            cnx.Open();

        //            On exécute la commande en récupérant son résultat dans un objet SqlDataRedader
        //            using (SqlDataReader sdr = com.ExecuteReader())
        //            {
        //                On lit les lignes de résultat une par une
        //                while (sdr.Read())
        //                {
        //                    ...et pour chacune on crée un objet qu'on ajoute à la liste
        //                    var client = new Client();
        //                    client.Id = (int)sdr["Id"];
        //                    client.Nom = (string)sdr["Nom"];

        //                    ListClients.Add(client);
        //                }
        //            }
        //        }
        //        Le fait d'avoir créé la connexion dans une instruction using
        //         permet de fermer cette connexion automatiquement à la fin du bloc using

        //        return ListClients;
        //    }

        //}

        ///*Récupération de la liste des clients sans numéro de téléphone*/
        //public static int GetTauxMoyen()
        //{
        //    List<Client> ListClients = new List<Client>();
        //    On créé une commande et on définit le code sql à exécuter
        //    var com = new SqlCommand();
        //    com.CommandText = @"select Id, Nom
        //        from Client 
        //        inner join Telephone  on (Idclient = Id) 
        //        where Numero is null ";

        //    On crée une connexion à partir de la chaîne de connexion stockée
        //     dans les paramètres de l'appli
        //    using (var cnx = new SqlConnection(Settings1.Default.GrandHotelConnect))
        //    {
        //        On affecte la connexion à la commande
        //        com.Connection = cnx;
        //        On ouvre la connexion
        //        cnx.Open();

        //        On exécute la commande en récupérant son résultat dans un objet SqlDataRedader
        //        using (SqlDataReader sdr = com.ExecuteReader())
        //        {
        //            On lit les lignes de résultat une par une
        //            while (sdr.Read())
        //            {
        //                ...et pour chacune on crée un objet qu'on ajoute à la liste
        //                var client = new Client();
        //                client.Id = (int)sdr["Id"];
        //                client.Nom = (string)sdr["Nom"];

        //                ListClients.Add(client);
        //            }
        //        }
        //    }
        //    Le fait d'avoir créé la connexion dans une instruction using
        //     permet de fermer cette connexion automatiquement à la fin du bloc using

        //    return 0;
        //}

        //public static int GetChiffreAffaire()
        //{
        //    On créé une commande et on définit le code sql à exécuter
        //    var cmd = new SqlCommand();
        //    cmd.CommandText = @"select   DATEPART(quarter,F.DatePaiement) as Trimestre,   sum(LF.Quantite * LF.MontantHT* (1 - TauxTVA)) as Ventes 
        //                        from Facture F
        //                        inner join LigneFacture LF on LF.IdFacture = F.Id
        //                        where year(F.DatePaiement) = 1997 
        //                        group by  DATEPART(quarter,F.DatePaiement)
        //                        order by  Trimestre ";

        //    using (var cnx = new SqlConnection(Settings1.Default.GrandHotelConnect))
        //    {
        //        cmd.Connection = cnx;
        //        cnx.Open();

        //        return (int)cmd.ExecuteScalar();
        //    }

        //}


        /*==============================================Lydia Methodes===============================================*/
    }

}


