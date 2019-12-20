using System;
using System.Drawing;

namespace ProjectCS
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test Compte
            Client client1 = new Client("cniMaximeLacourpaille256681415569Tarbes", "Maxime", "Lacourpaille");
            Client client2 = new Client("cniLucasBareilles256681415569Tarbes", "Lucas", "Bareilles");
            Compte compte1 = new Compte(client1);
            Compte compte2 = new Compte(client2);
            Compte.AfficherNbComptes();
            compte1.Crediter(100);
            compte2.Crediter(50);
            compte1.Crediter(30, compte2);
            compte1.Debiter(20, compte1);
            compte1.AfficherCompte();
            compte2.AfficherCompte();

            //Test Article
            Article article1 = new Article();
            Article article2 = new Article("GFDGDF151S3", "Jouet",25f,0.2f);
            Article article3 = new Article(article2);
            Article article4 = new Article("GGFDKSJGKNC444", "Marteau");
            article1.AfficherArticle();
            article2.AfficherArticle();
            article3.AfficherArticle();
            article4.AfficherArticle();
        }
    }

    class CompteBancaire
    {
        string titulaire;
        float solde;
        string devise;
        int nbrComptes;

        public CompteBancaire(string titulaire, string devise, int solde)
        {
            Titulaire = titulaire;
            Devise = devise;
            Solde = solde;
        }

        public string Titulaire { get => titulaire; set => titulaire = value; }
        public string Devise { get => devise; set => devise = value; }
        public float Solde { get => solde; set => solde = value; }
        public int NbrComptes { get => nbrComptes; set => nbrComptes = value; }

        public void Crediter(float montant)
        {
            Solde += montant;
        }

        public void Debiter(float montant)
        {
            Solde -= montant;
        }

        public string Decrire()
        {
            return "Ce compte appartient à "+Titulaire+", le solde est de "+Solde+" "+Devise;
        }
    }

    class Client
    {
        private object cin;

        public object GetCin()
        {
            return cin;
        }

        public void SetCin(Uri value)
        {
            cin = value;
        }

        private string nom;

        public string GetNom()
        {
            return nom;
        }

        public void SetNom(string value)
        {
            nom = value;
        }

        private string prenom;

        public string GetPrenom()
        {
            return prenom;
        }

        public void SetPrenom(string value)
        {
            prenom = value;
        }

        private string telephone;

        public string GetTelephone()
        {
            return telephone;
        }

        public void SetTelephone(string value)
        {
            telephone = value;
        }

        public Client(object cin, string nom, string prenom, string telephone)
        {
            this.cin = cin;
            this.nom = nom;
            this.prenom = prenom;
            this.telephone = telephone;
        }

        public Client(object cin, string nom, string prenom)
        {
            this.cin = cin;
            this.nom = nom;
            this.prenom = prenom;
            this.telephone = "";
        }

        public string Afficher()
        {
            string description = nom+" "+prenom+" "+ cin;
            Console.WriteLine(description);
            return description;
        }
    }

    class Compte
    {
        static int totalCompte;
        float solde;
        private int code;
        Client proprietaire;

        public Compte(Client proprietaire)
        {
            TotalCompte = TotalCompte == null ? 0 : ++TotalCompte;
            this.code = TotalCompte;
            this.Proprietaire = proprietaire;
            solde = 0;
        }

        public static int TotalCompte { get => totalCompte; set => totalCompte = value; }
        public int Code { get => code;  }
        public float Solde { get => solde; }
        public Client Proprietaire { get => proprietaire; set => proprietaire = value; }

        public void Crediter(float somme)
        {
            solde += somme;
        }

        public void Crediter(float somme, Compte compte)
        {
            this.Crediter(somme);
            compte.Debiter(somme);
        }

        public void Debiter(float somme)
        {
            solde -= somme;
        }

        public void Debiter(float somme, Compte compte)
        {
            this.Debiter(somme);
            compte.Crediter(somme);
        }

        public string AfficherCompte()
        {
            string description = Proprietaire.GetNom() + " " + Proprietaire.GetPrenom() + " possède " + Solde + " euros sur son compte numero " + code;
            Console.WriteLine(description);
            return description;
        }

        public static string AfficherNbComptes()
        {
            string description = totalCompte +"comptes ont été crée à ce jour";
            Console.WriteLine(description);
            return description;
        }
    }

    class Article
    {
        string reference;
        string designation;
        float prixHT;
        static float tauxTVA = 0.2f;

        public string Reference { get => reference; set => reference = value; }
        public string Designation { get => designation; set => designation = value; }
        public float PrixHT { get => prixHT; set => prixHT = value; }
        public float TauxTVA { get => tauxTVA; set => tauxTVA = value; }

        public Article(Article copie)
        {
            Reference = copie.Reference;
            Designation = copie.Designation;
            PrixHT = copie.PrixHT;
            TauxTVA = copie.TauxTVA;
        }

        public Article(string reference, string designation)
        {
            Reference = reference;
            Designation = designation;
            PrixHT = 0f;
        }

        public Article(string reference, string designation, float prixHT, float tauxTVA)
        {
            Reference = reference;
            Designation = designation;
            PrixHT = prixHT;
            TauxTVA = tauxTVA;
        }

        public Article()
        {
            Reference = "0000";
            Designation = "Anonyme";
            PrixHT = 0f;
        }

        public float CalculerPrixTTC()
        {
            return PrixHT + (PrixHT * TauxTVA / 100);
        }

        public string AfficherArticle()
        {
            string description = "L'article " + Designation + " associé à la référence " + Reference + " est vendu pour la somme de " + PrixHT + " euros HT, " + CalculerPrixTTC() + " TTC(Taux TVA : " + TauxTVA + ")";
            Console.WriteLine(description);
            return description;
        }
        
    }
}
