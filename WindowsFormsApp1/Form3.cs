using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult res;
            string text = "Do you want to exit ?";
            res = MessageBox.Show(text, "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                this.Show();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void pnlH_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            this.Hide();
            Form4 ad = new Form4();
            ad.ShowDialog();
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DialogResult res;
            string text = "Do you want to exit ?";
            res = MessageBox.Show(text, "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                this.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form5 ad = new Form5();
            ad.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form7 ad = new Form7();
            ad.ShowDialog();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(@"Data Source=TAHA\SQLEXPRESS;Initial Catalog=dictionnaire;Integrated Security=True"))
            {
                connection.Open();

                using (var reader = new StreamReader("C:\\Users\\bousl\\source\\repos\\WindowsFormsApp1\\dictionnaire.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        string mot = "";
                        string type = "";
                        string traduction = "";
                        string ex_fr = "";
                        string ex_ang = "";
                        int positionEspace = values[0].IndexOf(" ");
                        if (positionEspace != -1) //souschaine 
                        {
                             mot += values[0].Substring(positionEspace + 1); // +1 pour exclure l'espace
 
                        }
                        int positionEspace1 = values[1].IndexOf(" ");
                        if (positionEspace1 != -1)
                        {
                             type += values[1].Substring(positionEspace1 + 1); // +1 pour exclure l'espace

                        }
                        int positionEspace2 = values[2].IndexOf(" ");
                        if (positionEspace2 != -1)
                        {
                            traduction += values[2].Substring(positionEspace2 + 1); // +1 pour exclure l'espace

                        }
                        int positionEspace3 = values[3].IndexOf(" ");
                        if (positionEspace3 != -1)
                        {
                             ex_fr += values[3].Substring(positionEspace3 + 1); // +1 pour exclure l'espace

                        }
                        int positionEspace4 = values[4].IndexOf(" ");
                        if (positionEspace4 != -1)
                        {
                             ex_ang += values[4].Substring(positionEspace4 + 1); // +1 pour exclure l'espace

                        }

                        string query = "INSERT INTO Dic_fr_ang (ID, mot, type, traduction, exemple_fr, exemple_ang) VALUES (NEXT VALUE FOR Dic_fr_ang_seq, @mot, @type, @traduction, @ex_fr, @ex_ang);";

                        using (var command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@mot", mot);
                            command.Parameters.AddWithValue("@type", type);
                            command.Parameters.AddWithValue("@traduction", traduction);
                            command.Parameters.AddWithValue("@ex_fr", ex_fr);
                            command.Parameters.AddWithValue("@ex_ang", ex_ang);
                            command.ExecuteNonQuery();
                            
                        }
                        
                    }
                    MessageBox.Show("successs...");
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<MyObject> objects = new List<MyObject>();

            using (SqlConnection connection = new SqlConnection(@"Data Source=TAHA\SQLEXPRESS;Initial Catalog=dictionnaire;Integrated Security=True")) 
            {
                connection.Open();

                // Écrire une requête SQL pour extraire les données
                string query = "SELECT  mot,type,traduction,exemple_fr,exemple_ang FROM Dic_fr_ang";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Parcourir les données et les transformer en objets
                        while (reader.Read())
                        {
                            MyObject obj = new MyObject
                            {
                                Property1 = reader[0].ToString(),
                                Property2 = reader[1].ToString(),
                                Property3 = reader[2].ToString(),
                                Property4 = reader[3].ToString(),
                                Property5 = reader[4].ToString()
                            };
                            objects.Add(obj);
                        }

                        reader.Close();
                    }

                    connection.Close();
                    MessageBox.Show("sucessss...");

                        // Écrire les objets dans un nouveau fichier
                        using (StreamWriter writer = new StreamWriter("C:\\Users\\bousl\\source\\repos\\WindowsFormsApp1\\dictionnaire_exporte.txt"))
                    {
                            foreach(MyObject obj in objects)
                        {
                            writer.WriteLine("Mot: " + obj.Property1 + "," + "Type: " + obj.Property2 + "," + "Traduction: " +obj.Property3 +
                                "," + "Exemple_fr: " + obj.Property4 + ","+ "Exemple_ang: " + obj.Property5);
                        }
                        }
                    }
                }
            }

        }
    }

    internal class MyObject
    {
        public string Property1 { get; set; }
        public string Property2 { get; set; }
        public string Property3 { get; set; }
        public string Property4 { get; set; }
        public string Property5 { get; set; }
    }

