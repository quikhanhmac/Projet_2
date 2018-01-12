using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GrandHotel.Properties;

namespace GrandHotel
{
    public enum Operation { adresse, telephone, email }
    public class Contexte
    {
        #region GESTION DES CLIENTS
        /*---> Renvoie les liste des clients de GRANDHOTEL*/
        public static List<Client> GetClients()
        {
            var list = new List<Client>();
            var cmd = new SqlCommand();
            // Demande de la liste des clients
            cmd.CommandText = @"select S.Id, S.Civilite, S.Nom, S.Prenom
							from Client S
							order by 1";

            using (var conn = new SqlConnection(Settings1.Default.GrandHotelConnexion))
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

        /*--> Renvoie la liste de coordonnees dans client donné*/
        public static List<Coordonnees> GetCoordonneesClient(int idclient1)
        {
            var list = new List<Coordonnees>();
            var cmd = new SqlCommand();
            // Demande de la liste des clients avec adresse
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

            using (var conn = new SqlConnection(Settings1.Default.GrandHotelConnexion))
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

        /*--> Ajoute un client*/
        public static void AjouterClient(Client c1)
        {
            var cmd = new SqlCommand();

            // methode de creation d'un nouveau client
            cmd.CommandText = @"insert Client (Civilite, Nom, Prenom)
									values (@Civilite, @Nom, @Prenom)";


            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.NVarChar, ParameterName = "@Civilite", Value = c1.Civilite });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.NVarChar, ParameterName = "@Nom", Value = c1.Nom });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.NVarChar, ParameterName = "@Prenom", Value = c1.Prenom });

            using (var cnx = new SqlConnection(Settings1.Default.GrandHotelConnexion))
            {
                cmd.Connection = cnx;
                cnx.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /*--> Renvoie le plus Id client utilise pour ajouter une adresse à un client*/
        public static int NumeroClient()
        {
            var cmd = new SqlCommand();

            // Determination du plus grand id client
            cmd.CommandText = @"select top (1) Id
							    from Client
                                order by 1 desc";

            using (var cnx = new SqlConnection(Settings1.Default.GrandHotelConnexion))
            {
                cmd.Connection = cnx;
                cnx.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        /*--> Ajouter une adresse à l'entité Adresse*/
        public static void AjouterAdresse(Adresse a1)
        {
            var cmd = new SqlCommand();

            cmd.CommandText = @"insert Adresse (IdClient, Rue, CodePostal, Ville)
									values (@IdClient, @Rue, @CodePostal, @Ville)";

            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Int, ParameterName = "@IdClient", Value = a1.IdClient });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.NVarChar, ParameterName = "@Rue", Value = a1.Rue });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.NVarChar, ParameterName = "@CodePostal", Value = a1.CodePostal });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.NVarChar, ParameterName = "@Ville", Value = a1.Ville });

            using (var cnx = new SqlConnection(Settings1.Default.GrandHotelConnexion))
            {
                cmd.Connection = cnx;
                cnx.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /*--> Ajouter un numero de téléphone */
        public static void AjouterTelephone(Telephone t)
        {
            var cmd = new SqlCommand();
            
            cmd.CommandText = @"insert Telephone (IdClient, Numero, CodeType, Pro)
									values (@IdClient, @Numero, @CodeType, @Pro)";

            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Int, ParameterName = "@IdClient", Value = t.IdClient });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.NVarChar, ParameterName = "@Numero", Value = t.Numero });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.NVarChar, ParameterName = "@CodeType", Value = t.CodeType });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Bit, ParameterName = "@Pro", Value = t.Pro });

            using (var cnx = new SqlConnection(Settings1.Default.GrandHotelConnexion))
            {
                cmd.Connection = cnx;
                cnx.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static List<int> GetIdClient()
        {
            var list = new List<int>();
            var cmd = new SqlCommand();
            // Demande de la liste des clients
            cmd.CommandText = @"select Id from Client";
            using (var conn = new SqlConnection(Settings1.Default.GrandHotelConnexion))
            {
                //Connection à la base de données
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add((int)reader["Id"]);
                    }
                }
            }
            return list;
        }

        public static void GetAjouterMail(Email e)
        {
            var cmd = new SqlCommand();


            cmd.CommandText = @"insert Email (IdClient, Adresse, Pro)
									values (@IdClient, @Adresse, @Pro)";

            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Int, ParameterName = "@IdClient", Value = e.IdClient });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.NVarChar, ParameterName = "@Adresse", Value = e.Adresse });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Bit, ParameterName = "@Pro", Value = e.Pro });

            using (var cnx = new SqlConnection(Settings1.Default.GrandHotelConnexion))
            {
                cmd.Connection = cnx;
                cnx.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Requête delete - suppression d'un client , son adresse, son numero de téléphone et son email placer dans une transaction
        // Si le produit est référencé par une commande, la requête lève une
        // SqlException avec le N°547, qu'on intercepte dans le code appelant
        public static void SupprimerClient(int id)
        {
            // Préparation des commandes
            var com1 = new SqlCommand();
            com1.CommandText = @"delete from Client where Id = @id";
            com1.Parameters.Add(new SqlParameter
            {
                SqlDbType = SqlDbType.Int,
                ParameterName = "@id",
                Value = id
            });

            var com2 = new SqlCommand();
            com2.CommandText = @"delete from Adresse where IdClient = @id";
            com2.Parameters.Add(new SqlParameter
            {
                SqlDbType = SqlDbType.Int,
                ParameterName = "@id",
                Value = id
            });
            var com3 = new SqlCommand();
            com3.CommandText = @"delete from Telephone where IdClient = @id";
            com3.Parameters.Add(new SqlParameter
            {
                SqlDbType = SqlDbType.Int,
                ParameterName = "@id",
                Value = id
            });
            var com4 = new SqlCommand();
            com4.CommandText = @"delete from Email where IdClient = @id";
            com4.Parameters.Add(new SqlParameter
            {
                SqlDbType = SqlDbType.Int,
                ParameterName = "@id",
                Value = id
            });
            using (var cnx = new SqlConnection(Settings1.Default.GrandHotelConnexion))
            {
                // Ouverture de la connexion, et affectation aux commandes
                cnx.Open();
                com1.Connection = cnx;
                com2.Connection = cnx;
                com3.Connection = cnx;
                com4.Connection = cnx;
                // Transaction
                using (SqlTransaction tran = cnx.BeginTransaction())
                {
                    // Exécution des commandes, en leur affectant la transaction
                    com1.Transaction = tran;
                    com1.ExecuteNonQuery();

                    com2.Transaction = tran;
                    com2.ExecuteNonQuery();

                    com3.Transaction = tran;
                    com3.ExecuteNonQuery();

                    com4.Transaction = tran;
                    com4.ExecuteNonQuery();

                    // Validation de la transaction
                    tran.Commit();
                }
            }
        }
        
        public static void ExporterXml(List<Client> listCol)
        {
            // On crée un sérialiseur, en spécifiant le type de l'objet à sérialiser
            // et le nom de l'élément xml racine
            XmlSerializer serializer = new XmlSerializer(typeof(List<Client>),
                                       new XmlRootAttribute("ClientsBD"));

            using (var sw = new StreamWriter(@"..\..\ListeClients.xml"))
            {
                serializer.Serialize(sw, listCol);
            }
        }

        public static List<Client> GetClientsCoordonnees()
        {
            var list = new List<Client>();
            var cmd = new SqlCommand();
            // Demande de la liste des clients
            cmd.CommandText = @"select C.Id, C.Civilite, C.Nom, C.Prenom, C.CarteFidelite, 
                                A.Rue, A.CodePostal, A.Ville, T.Numero, E.Adresse
                                from Client C
                                inner join Adresse A on A.IdClient = C.Id
                                inner join Telephone T on T.IdClient = C.Id
                                inner join Email E on E.IdClient = C.Id
                                 order by C.Id";


            using (var conn = new SqlConnection(Settings1.Default.GrandHotelConnexion))
            {
                //Connection à la base de données
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // int Id = (int)reader["Id"];
                        // Client cli = null;

                        var item = new Client();
                        item.Id = (int)reader["Id"];
                        item.Adresses = new Adresse();
                        item.Telephones = new List<Telephone>();
                        item.Emails = new List<Email>();
                        item.Civilite = (string)reader["Civilite"];
                        item.Nom = (string)reader["Nom"];
                        item.Prenom = (string)reader["Prenom"];
                        item.Adresses.Rue = (string)reader["Rue"];
                        item.Adresses.CodePostal = (string)reader["CodePostal"];
                        item.Adresses.Ville = (string)reader["Ville"];

                        item.Telephones = GetListNumero(item.Id);

                        item.Emails = GetListEmail(item.Id);

                        list.Add(item);
                    }
                }
            }

            return list;
        }

        private static List<Telephone> GetListNumero(int idCli)
        {
            var list = new List<Telephone>();
            // Demande de la liste des clients
            var cmd = new SqlCommand();
            cmd.CommandText = @"select  T.Numero
                                from Client C
                                inner join Adresse A on A.IdClient = C.Id
                                inner join Telephone T on T.IdClient = C.Id
                                where C.Id= @IdClient";
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Int, ParameterName = "@IdClient", Value = idCli });
            using (var conn = new SqlConnection(Settings1.Default.GrandHotelConnexion))
            {
                //Connection à la base de données
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        var item = new Telephone();
                        item.Numero = (string)reader["Numero"];

                        list.Add(item);
                    }
                }
            }
            return list;
        }

        private static List<Email> GetListEmail(int idCli)
        {
            var list = new List<Email>();
            // Demande de la liste des clients
            var cmd = new SqlCommand();
            cmd.CommandText = @"select E.Adresse
                                from Client C
                                inner join Adresse A on A.IdClient = C.Id
                                inner join Email E on E.IdClient = C.Id
                                where C.Id= @IdClient";
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Int, ParameterName = "@IdClient", Value = idCli });
            using (var conn = new SqlConnection(Settings1.Default.GrandHotelConnexion))
            {
                //Connection à la base de données
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        var item = new Email();
                        item.Adresse = (string)reader["Adresse"];

                        list.Add(item);
                    }
                }
            }
            return list;
        }
        #endregion

        #region GESTION DES FACTURES
        /*Cette methode prend en parametre l'identifaint d'un client et renvoie la liste */
        public static List<Facture> GetFactures(int idClient, int date)
        {
            var listeFacture = new List<Facture>();
            // Création d'une commande et definition de code sql à exécuter
            var cmd = new SqlCommand();
            cmd.CommandText = @" SELECT f.IdClient,f.Id,f.DateFacture,f.DatePaiement,f.CodeModePaiement
                                FROM Facture f
                                INNER JOIN Client c ON c.Id = f.IdClient
                                WHERE c.Id =@idClient and year(f.DateFacture)=@date
								order by f.Id";
            // Création des paramètres 
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Int, ParameterName = "@date", Value = date, });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Int, ParameterName = "@idClient", Value = idClient, });
            using (var conn = new SqlConnection(Settings1.Default.GrandHotelConnexion))
            {
                // Affection d'une connexion à la commande
                cmd.Connection = conn;
                // Ouverture d'une connexion
                conn.Open();
                // Execution d'une commande en récupérant son résultat dans un objet SqlDataRedader
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Lecture les lignes de résultat une par une
                    while (reader.Read())
                    {
                        // Création pour chacun un objet qu'on ajoute à la liste
                        var item = new Facture();
                        item.Id = (int)reader["Id"];
                        item.IdClient = (int)reader["IdClient"];
                        item.DateFacture = (DateTime)reader["DateFacture"];
                        item.DatePaiement = (DateTime)reader["DatePaiement"];
                        item.CodeModePaiement = (string)reader["CodeModePaiement"];
                        listeFacture.Add(item);
                    }
                }
            }// Fermeture de la connexion par "using"

            return listeFacture;
        }
        public static List<LigneFacture> GetLigneFacture(int id)
        {
            var listeLigne = new List<LigneFacture>();
            // Création d'une commande commande et definition de code sql à exécuter
            var cmd = new SqlCommand();
            cmd.CommandText = @"select NumLigne,IdFacture,Quantite, TauxTVA,TauxReduction,MontantHT
                                from LigneFacture
                                where IdFacture=2
                                order by NumLigne";
            // Création d'un paramètre
            cmd.Parameters.Add(new SqlParameter
            {
                SqlDbType = SqlDbType.Int,
                ParameterName = "@id",
                Value = id
            });

            using (var conn = new SqlConnection(Settings1.Default.GrandHotelConnexion))
            {
                // Affection d'une connexion à la commande
                cmd.Connection = conn;
                // Ouverture d'une connexion
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Lecture les lignes de résultat une par une
                    while (reader.Read())
                    {
                        // Création pour chacun un objet qu'on ajoute à la liste
                        var item = new LigneFacture();
                        item.IdFacture = (int)reader["IdFacture"];
                        item.NumLigne = (int)reader["NumLigne"];
                        item.Quantite = (Int16)reader["Quantite"];
                        item.MontantHT = (decimal)reader["MontantHT"];
                        item.TauxTVA = (decimal)reader["TauxTVA"];
                        item.TauxReduction = (decimal)reader["TauxReduction"];
                        listeLigne.Add(item);
                    }
                }
            }// Fermeture de la connexion par "using"

            return listeLigne;
        }

        /*Cette methode prend en parametre une facture et l'ajoute à l'entité Facture de la BD GRANDHOTEL*/
        public static void AjouterFacture(Facture f)
        {
            // Création d'une commande et definition de code sql à exécuter
            var cmd = new SqlCommand();
            cmd.CommandText = @"insert Facture  (IdClient,DateFacture, DatePaiement, CodeModePaiement)
            values (@IdClient,@DateFacture, @DatePaiement, @CodeModePaiement)";
            // Création des paramètres
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Int, ParameterName = "@IdClient", Value = f.IdClient });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Date, ParameterName = "@DateFacture", Value = f.DateFacture });

            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Date, ParameterName = "@DatePaiement", Value = f.DatePaiement });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.VarChar, ParameterName = "@CodeModePaiement", Value = f.CodeModePaiement });

            using (var cnx = new SqlConnection(Settings1.Default.GrandHotelConnexion))
            {
                // Affection d'une connexion à la commande
                cmd.Connection = cnx;
                //  Ouverture d'une connexion
                cnx.Open();
                cmd.ExecuteNonQuery();
            }// Fermeture de la connexion par "using"
        }

        public static void AjouterLigneFacture(LigneFacture lf)
        {
            //Création d'une commande et definition de code sql à exécuter

            var cmd = new SqlCommand();
            cmd.CommandText = @"insert LigneFacture(IdFacture, NumLigne, Quantite, MontantHT,TauxTVA,TauxReduction)
        values (@IdFacture, @NumLigne, @Quantite, @MontantHT,@TauxTVA,@TauxReduction)";
            // Création des paramètres
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Int, ParameterName = "@IdFacture", Value = lf.IdFacture });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Int, ParameterName = "@NumLigne", Value = lf.NumLigne });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.SmallInt, ParameterName = "@Quantite", Value = lf.Quantite });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Decimal, ParameterName = "@MontantHT", Value = lf.MontantHT });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Decimal, ParameterName = "@TauxTVA", Value = lf.TauxTVA });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Decimal, ParameterName = "@TauxReduction", Value = lf.TauxReduction });

            using (var cnx = new SqlConnection(Settings1.Default.GrandHotelConnexion))
            {
                // Affection d'une connexion à la commande
                cmd.Connection = cnx;
                // Ouverture d'une la connexion
                cnx.Open();

                cmd.ExecuteNonQuery();
            }// Fermeture de la connexion par "using"
        }
        
        public static void ModifierFacture(Facture fact)
        {
            var cmd = new SqlCommand();


            cmd.CommandText = @"update Facture set DateFacture = @dateFacture, CodeModePaiement= @CodeP			
								where Id =@Id";
            // Création des paramètres
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Int, ParameterName = "@Id", Value = fact.Id });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Date, ParameterName = "@dateFacture", Value = fact.DateFacture });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.NVarChar, ParameterName = "@CodeP", Value = fact.CodeModePaiement }); ;

            using (var cnx = new SqlConnection(Settings1.Default.GrandHotelConnexion))
            {
                cmd.Connection = cnx;
                cnx.Open();
                cmd.ExecuteNonQuery();
            }
        }
        
        public static Facture GetFacture(int id)
        {
            var item = new Facture();
            var cmd = new SqlCommand();
            cmd.CommandText = @"select DateFacture, CodeModePaiement from Facture where id =@Id";
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Int, ParameterName = "@Id", Value = id });
            using (var conn = new SqlConnection(Settings1.Default.GrandHotelConnexion))
            {
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        item.DateFacture = (DateTime)reader["DateFacture"];
                        item.CodeModePaiement = (string)reader["CodeModePaiement"];

                    }
                }
            }

            return item;
        }

        #endregion

        #region RESULTAT de l'Hotel: Les requetes sql de cette partir sont à revoir

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

        #endregion


    }
}



