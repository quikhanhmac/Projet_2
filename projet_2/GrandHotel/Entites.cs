using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace GrandHotel
{
    public class Client
    {
        [XmlAttribute]
        public int Id { get; set; }
        [XmlAttribute]
        public string Civilite { get; set; }
        [XmlAttribute]
        public string Nom { get; set; }
        [XmlAttribute]
        public string Prenom { get; set; }
        [XmlAttribute]
        public byte CarteFidelite { get; set; }

        [XmlIgnore]
        [Display(ShortName= "None")]
        public Telephone Telephone { get; set; }
        [XmlIgnore]
        [Display(ShortName = "None")]
        public Email Email { get; set; }

        [Display(ShortName = "None")]
        public Adresse Adresses { get; set; }

        [Display(ShortName = "None")]
        public List<Telephone> Telephones { get; set; }

        [Display(ShortName = "None")]
        public List<Email> Emails { get; set; }
    }
    public class Reservation
    {
        public int NumChambre { get; set; }
        public DateTime Jour { get; set; }
        public int IdClient { get; set; }
        public Int16 NbPersonne { get; set; }
        public Int16 HeureArrivee { get; set; }
    }
    public class Chambre
    {
        public int Numero { get; set; }
        public Int16 Etage { get; set; }
        public byte Bain { get; set; }
        public byte Douche { get; set; }
        public byte WC { get; set; }
        public Int16 NbLits { get; set; }
    }

    public class Adresse
    {
        [XmlIgnore]
        public int IdClient { get; set; }
        [XmlAttribute]
        public string Rue { get; set; }
        [XmlAttribute]
        public string CodePostal { get; set; }
        [XmlAttribute]
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
        public int Quantite { get; set; }
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
        [XmlAttribute]
        public string Numero { get; set; }
        [XmlIgnore]
        public int IdClient { get; set; }
        [XmlAttribute]
        public string CodeType { get; set; }
        [XmlAttribute]
        public int Pro { get; set; }
    }
    public class Email
    {
        [XmlAttribute]
        public string Adresse { get; set; }
        [XmlIgnore]
        public int IdClient { get; set; }
        [XmlAttribute]
        public int Pro { get; set; }
    }
    public class TarifChambre
    {
        public int NumChambre { get; set; }
        public string CodeTarif { get; set; }
       
    }
    public class Tarif
    {
        public string Code { get; set; }
        public DateTime DateDebut { get; set; }
        public decimal Prix { get; set; }
    }
    public class Coordonnees
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Numero { get; set; }
        public string Adresse { get; set; }
        public string Rue { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
    }

    //public class NouveauClient
    //{
    //    public int Id { get; set; }
    //    public string Civilite { get; set; }
    //    public string Nom { get; set; }
    //    public string Prenom { get; set; }
    //    public string Rue { get; set; }
    //    public string CodePostal { get; set; }
    //    public string Ville { get; set; }
    //}
}
