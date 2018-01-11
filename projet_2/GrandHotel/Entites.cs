using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandHotel
{
    public class Client
    {
        public int Id { get; set; }
        public string Civilite { get; set; }
        public string Nom { get; set; }
        public string Preonom { get; set; }
        public byte CarteFidelite { get; set; }
    }
    public class Reservation
    {
        public Int16 NumChambre { get; set; }
        public DateTime Jour { get; set; }
        public int IdClient { get; set; }
        public Int16 NbPersonne { get; set; }
        public Int16 HeureArrivee { get; set; }
    }
    public class Chambre
    {
        public Int16 Numero { get; set; }
        public Int16 Etage { get; set; }
        public byte Bain { get; set; }
        public byte Douche { get; set; }
        public byte WC { get; set; }
        public Int16 NbLits { get; set; }
    }
    public class Adresse
    {
        public int IdClient { get; set; }
        public string Rue { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
    }
    public class Facture
    {
        public int Id { get; set; }
        public int IdClient { get; set; }
        public DateTime DateFacture { get; set; }
        public DateTime DatePaiement { get; set; }
        public string CodeModePaiement { get; set; }     
    }
    public class LigneFacture
    {
        public int IdFacture { get; set; }
        public int NumLigne { get; set; }
        public Int16 Quantite { get; set; }
        public decimal MontantHT { get; set; }
        public decimal TauxTVA { get; set; }
        public decimal TauxReduction { get; set; }
    }
    public class ModePaiement
    {
        public string Code { get; set; }
        public string Libelle { get; set; }
    }
    public class Calendrier
    {
        public DateTime Jour { get; set; }

    }
    public class Telephone
    {
        public string Numero { get; set; }
        public int IdClient { get; set; }
        public string CodeType { get; set; }
        public Int16 Pro { get; set; }     
    }
    public class Email
    {
        public string Adresse { get; set; }
        public int IdClient { get; set; }
        public Int16 Pro { get; set; }
    }
    public class TarifChambre
    {        
        public Int16 NumChambre { get; set; }
        public string CodeTarif { get; set; }
        public class Tarif
    {
        public string Code { get; set; }
        public DateTime DateDebut { get; set; }
        public decimal Prix { get; set; }
    }

}
