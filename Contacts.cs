using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Ajout des esapces de noms pour la sérialisation des objets
//pour la maniplulation des fichiers
using System.IO;
// Pour avois accès à la sérialisation et désérialisation des fichiers Json
using System.Text.Json;

namespace Gestion_de_contacts
{
    internal class Contacts
    {
        //Déclaration des propriétés de la classe Contacts
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Telephone { get; set; }
        //On doit sérialiser cette liste pour la sauvegarder dans un fichier Json
        public static List<Contacts> ListContacts = new List<Contacts>();

        //Génération du constructeur par l'outil tournevis à gauche semi-automatiquement 
        public Contacts(string nom, string prenom, string telephone)
        {
            Nom = nom;
            Prenom = prenom;
            Telephone = telephone;
        }

        //Méthode pour ajouter un contact spécifique
        public static void SaveContact(Contacts contact)
        {
            ListContacts.Add(contact);
            string jsonString = JsonSerializer.Serialize(ListContacts);
            File.WriteAllText("contacts.json", jsonString);

        }

        //Sérialisation de ces objets dans un fichier Json

        //Donner la possibilité aux utilisteurs d'enregistrer les contacts
        //Création d'une méthode qui permet de sauvegrader les contects
        //Cette méthode est en static pour que cela soit accessible sans instancier l'objet contects
        public static void SaveContacts()
        {
            string jsonString = JsonSerializer.Serialize(ListContacts);
            //Pour l'enregistrement des contacts dans la globalité du projet c'est-à-dire
            //ce qui est dans la liste ListContacts
            File.WriteAllText("contacts.json", jsonString);
        }

        //Méthode pour le chargement des contacts
        //Cette LoadFromFile doit désérialiser la liste dans le fichier Json
        public static void LoadFromFile()
        {
            if (File.Exists("contacts.json"))
            {
                string jsonString = File.ReadAllText("contacts.json");
                //Supprimer le contenu (anciens contacts) dans la ListContacts
                ListContacts.Clear();
                ListContacts = JsonSerializer.Deserialize<List<Contacts>>(jsonString);
            }
        }


    }
}
