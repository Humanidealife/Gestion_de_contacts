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
    }
}
