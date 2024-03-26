using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Diagnostics.Eventing.Reader;

namespace Gestion_de_contacts
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Contacts c = new Contacts(textBox1.Text, textBox2.Text, textBox3.Text);
            Contacts.SaveContact(c);
            RefreshListView();

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        //Mérhode qui permet d'affichier le contact ajouté dans la listView
        void RefreshListView()
        {
            listView1.Items.Clear();
            //Ici on utilise directement la class, et on n'a pas besoin d'instancier un objet,
            // car la méthode (ListContacts) est static

            Contacts.LoadFromFile();

            //Rafraichir la listView avec une boucle foreach
            foreach (Contacts item in Contacts.ListContacts)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = item.Nom;
                lvi.SubItems.Add(item.Prenom);
                lvi.SubItems.Add(item.Telephone);
                //ajouter tous ces élements dans la listView
                listView1.Items.Add(lvi);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Charger la liste au niveau du formulaire
            RefreshListView();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // faire une boucle sur la list des contacts pour vérifier la sélection
            //Ceci est une façon qui a un prblème, il ne choisit que le premier élement qui a le même nom
            /*foreach (var item in Contacts.ListContacts)
            {
                if(item.Nom == listView1.SelectedItems[0].Text)
                {
                    Contacts.ListContacts.Remove(item);
                    break;
                }
            }*/

            //Il faut utiliser une autre méthode pour supprimer un contact
            //On doit utiliser une boucle for pour parcourir la liste et suupprimer que la ligne sélectionnée
            //On ne vérifie pas le nom, on vérifie l'index de la ligne sélectionnée
            //Mais cette façon ne fonctionne pas quand la listView est triée
            /*for (int i = 0; i < Contacts.ListContacts.Count; i++)
            {
                if (i == listView1.SelectedItems[0].Index)
                {
                    Contacts.ListContacts.RemoveAt(i);
                    break;
                }
            }*/
            //!!Problème de la méthode ci-dessus, il ne supprime pas de contact
            //une autre façon de supprimer un contact
            if (listView1.SelectedItems.Count > 0)
            {
                // Obtenir l'élément sélectionné
                ListViewItem selectedItem = listView1.SelectedItems[0];

                // Supprimer l'élément sélectionné de la ListView
                listView1.Items.Remove(selectedItem);

                // Supprimer le contact correspondant de la base de données
                Contacts.ListContacts.Remove(selectedItem.Tag as Contacts); // Remplacez "Contact" par le type de vos contacts

                // Sauvegarder la liste après la suppression
                Contacts.SaveContacts();

                // Rafraîchir la ListView
                RefreshListView();
            }


            //Sauvegarder la liste après la suppression
            //Contacts.SaveContacts();
            //RefreshListView();
        }

        //Le bouton pour modifier un contact
        private void button3_Click(object sender, EventArgs e)
        {
            // tentative de récupération
            // Contacts c = new Contacts(listView1.SelectedItems[0].Text, );
            //On doit récupérer les informations du contact sélectionné
            //On doit récupérer le nom, le prénom et le téléphone et de les remettre dans les textBox
            ListViewItem lviSelected = listView1.SelectedItems[0];
            textBox1.Text = lviSelected.Text;
            textBox2.Text = lviSelected.SubItems[1].Text;
            textBox3.Text = lviSelected.SubItems[2].Text;
            //On doit pouvoir modifier les informations du contact
            //On doit supprimer le contact sélectionné
            for (int i = 0; i < Contacts.ListContacts.Count; i++)
            {
                if (i == listView1.SelectedItems[0].Index)
                {
                    Contacts.ListContacts.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
