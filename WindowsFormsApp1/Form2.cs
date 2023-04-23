using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
          
        }

       
        SqlConnection conn = new SqlConnection("Data Source=TAHA\\SQLEXPRESS;Initial Catalog=dictionnaire;Integrated Security=True");
        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        Boolean lan = false; //si la langue choisi est fr
        private void Form2_Load(object sender, EventArgs e)
        {

            conn.Open();


            string query = "SELECT mot FROM Dic_fr_ang";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            AutoCompleteStringCollection autoCompleteCollection = new AutoCompleteStringCollection();



            while (reader.Read())
            {
                autoCompleteCollection.Add(reader[0].ToString());
            }


            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox1.AutoCompleteCustomSource = autoCompleteCollection;
            reader.Close();
            conn.Close();
            textBox1.Focus();

            textBox6.ReadOnly = true;
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

        private void button3_Click(object sender, EventArgs e)
        {

            //si label francais est visible et l'utlisateur appuyer sur le boutton changer la langue il va cacher le mot francais et montrer anglais et vice versa 
            //en cours il va recuperer les mots en francais et les mets dans la textbox du suggestion
            if (label2.Visible)
            {
                label1.Visible = false;
                label2.Visible = false;
                label6.Visible = true;
                label5.Visible = true;
                conn.Open();


                string query = "SELECT traduction FROM Dic_fr_ang";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection autoCompleteCollection = new AutoCompleteStringCollection();



                while (reader.Read())
                {
                    autoCompleteCollection.Add(reader[0].ToString());
                }


                textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox1.AutoCompleteCustomSource = autoCompleteCollection;
                reader.Close();
                conn.Close();
                textBox1.Focus();

                textBox6.ReadOnly = true;

            }
            else // le meme principe pour anglais
            {
          
                label1.Visible = true;
                label2.Visible = true;
                label6.Visible = false;
                label5.Visible = false;
                conn.Open();


                string query = "SELECT mot FROM Dic_fr_ang";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection autoCompleteCollection = new AutoCompleteStringCollection();



                while (reader.Read())
                {
                    autoCompleteCollection.Add(reader[0].ToString());
                }


                textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox1.AutoCompleteCustomSource = autoCompleteCollection;
                reader.Close();
                conn.Close();
                textBox1.Focus();

                textBox6.ReadOnly = true;
            }
            
                textBox6.ReadOnly = true;
                textBox1.ReadOnly = false;
            

            string t = textBox6.Text.Trim();
            textBox6.Clear();
            textBox1.Clear();
            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string mot = textBox1.Text;
            string type = "";

            conn.Open();

            
                string req = "select * from Dic_fr_ang where mot = @mot1 or traduction = @mot2"; //verifier l'existance du mot
                SqlCommand cmd = new SqlCommand(req, conn);
                cmd.Parameters.AddWithValue("@mot1", mot);
                cmd.Parameters.AddWithValue("@mot2", mot);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 1;
                    while (reader.Read())
                    {
                        if (label2.Visible) // si la langue est francais 
                        {
                            textBox6.Text += reader[3].ToString().Trim() + Environment.NewLine;
                            type = reader[2].ToString().Trim();
                            textBox4.Text += "Exemple " + i + "[ " + reader[3] + " ]"  + " : " + reader[4].ToString().Trim() + " ." + Environment.NewLine;//il va recuperer les exemples en francais du mot
                            textBox5.Text += "Example " + i + "[ " + reader[1] + " ]" + " : " + reader[5].ToString().Trim() + " ." + Environment.NewLine;
                            i++;
                        }
                        else
                        {
                            textBox6.Text += reader[1].ToString() + Environment.NewLine;
                            type = reader[2].ToString().Trim();
                            textBox4.Text += "Example " + i + "[ " + reader[1] +" ]" + ": " + reader[5].ToString().Trim() + " ." + Environment.NewLine;
                            textBox5.Text += "Exemple " + i + "[ " + reader[3] + " ]" + ": " +  reader[4].ToString().Trim() + " ." + Environment.NewLine;
                            i++;
                        }
                    }

                    textBox3.Text = textBox1.Text + " : type :  " + type + " .";
                }
                else
                {
                    MessageBox.Show("Mot n'existe pas dans notre base de donnee DSL!");
                }

                conn.Close();
  
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_Click_1(object sender, EventArgs e)
        {
            textBox6.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox3.Clear();
            textBox1.SelectAll();
            textBox1.Focus();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            textBox1.Clear();
            textBox6.Clear();   
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox6.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox3.Clear();
            textBox1.SelectAll();
            textBox1.Focus();
        }
    }
}
