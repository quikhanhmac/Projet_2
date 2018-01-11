using GrandHotel.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandHotel
{
    public class Contexte
    {
       
//        public static List<Facture> GetFactures(DateTime date)
//        {
//            var liste = new List<Facture>();
//            var cmd = new SqlCommand();
//            cmd.CommandText = @"select Id,IdClient,DateFacture,CodeModePaiement
//from Facture
//where DateFacture='@date'";

//            cmd.Parameters.Add(new SqlParameter
//            {
//                SqlDbType = SqlDbType.Date,
//                ParameterName = "@date",
//                Value = date
//            });

//            using (var conn = new SqlConnection(Settings1.Default.GrandHotelConnexion))
//            {
//                cmd.Connection = conn;
//                conn.Open();

//                using (SqlDataReader reader = cmd.ExecuteReader())
//                {
//                    while (reader.Read())
//                    {
//                        var item = new Facture();
//                        item.Id = (int)reader["Id"];
//                        item.IdClient = (int)reader["IdClient"];
//                        item.DateFacture = (DateTime)reader["DateFacture"];
//                        item.CodeModePaiement = (string)reader["DateFacture"];
//                        liste.Add(item);
//                    }
//                }
//            }

//            return liste;
//        }
    }
}

