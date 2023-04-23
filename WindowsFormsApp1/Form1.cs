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
using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //faire la connection avec la BD 
        // on utilise la constructeur parametre
        SqlConnection conn = new SqlConnection(@"Data Source=TAHA\SQLEXPRESS;Initial Catalog=dictionnaire;Integrated Security=True");


        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //controle saisie
            try
            {
                if(textBox1.Text == "")
                {
                    textBox1.Text = "Enter mail";
                    return;
                }
                //si textbox n'est pas vide il va changer la couleur du texte et rendre le panel qui porte l'erreur invisible
                textBox1.ForeColor = Color.White;
                panel5.Visible = false;
            }
            catch { }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //controle saisie le meme principe pour TextBox1
            try
            {
                if (textBox2.Text == "")
                {
                    textBox2.Text = "Enter password ";
                    return;
                }
                textBox2.ForeColor = Color.White;
                textBox2.PasswordChar = '*';
                panel7.Visible = false;

            }
            catch { }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.SelectAll();
        }

      

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Black;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //controle saisie 
            //verification si les textBoxs restent vides 

            if (textBox1.Text == "Enter mail" || (textBox1.Text == " ")) {
                //si true alors le panel qui porte l'erruer s'affiche et le focus sur le textBox
                panel5.Visible = true;
                textBox1.Focus();
                return;
            }
            if (textBox2.Text == "Enter password " || (textBox2.Text == " "))
            {
                panel7.Visible = true;
                textBox2.Focus();
                return;
            }


            string adrs = textBox1.Text;
            string pw = textBox2.Text;

            try
            {
                conn.Open();
                //on va verifie si l'adresse et le mot de passe sont correcte
                string query = "Select * from users where Adresse = @adrs and mdp = @pw;";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@adrs", adrs);
                cmd.Parameters.AddWithValue("@pw", pw);
                SqlDataReader dr = cmd.ExecuteReader();

                if(dr.HasRows)
                {

                   if(adrs == "admin" &&  pw == "admin123") {
                        //on va afficher le form de l'administration
                        this.Hide();
                        Form3 ad = new Form3();
                        ad.ShowDialog();
                        this.Close();
                    } else
                    {
                        this.Hide();
                        Form2 ad = new Form2();
                        ad.ShowDialog();
                        this.Close();
                    }
                    
                }else
                {
                    //sinon on reste afficher l'erruer
                    panel5.Visible = true;
                    panel7.Visible = true;
                    return; 
                }
            }
            catch {
                
            }
             conn.Close(); 

            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "Enter mail" || (textBox4.Text == " ") || (!ValidateEmail(textBox4.Text)) )
            {
                panel16.Visible = true;
                textBox4.Focus();
                return;
            }
            if (textBox3.Text == "Enter password " || (textBox3.Text == " "))
            {
                textBox3.Focus();
                return;
            }
            if(textBox5.Text == "Re-Enter password " || (textBox5.Text == " "))
            {
                textBox5.Focus();
                return;
            }
            if(textBox3.Text != textBox5.Text)
            {
                panel20.Visible = true;
                panel12.Visible = true;
                textBox3.Focus();
                return;
            }

            try {
                //on va creer un compte 
                //avant l'ajouter on va verifie si existe ou non
                conn.Open();
                string req = "INSERT INTO users VALUES(@Adresse, @mdp);";
                string req2 = "SELECT * from users where Adresse = @adrs and mdp = @pw;";
                SqlCommand cmd1 = new SqlCommand(req2, conn);
                cmd1.Parameters.AddWithValue("@adrs", textBox4.Text);
                cmd1.Parameters.AddWithValue("@pw", textBox3.Text);
                SqlDataReader reader = cmd1.ExecuteReader();

                if (reader.HasRows)
                {
                    MessageBox.Show("existant...");
                    return;
                }
                conn.Close();

                conn.Open();

                SqlCommand cmd = new SqlCommand(req, conn);
                cmd.Parameters.AddWithValue("@Adresse", textBox4.Text);
                cmd.Parameters.AddWithValue("@mdp", textBox3.Text);
                cmd.ExecuteNonQuery();
                conn.Close();

                //apres la creation on va afficher la form du dictionnaire et cacher le form du login
                this.Hide();
                Form2 formDic = new Form2();
                formDic.ShowDialog();
                this.Close();
            }
            catch { }

        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            button4.ForeColor = Color.Black;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.ForeColor = Color.White;
        }

        
        public bool ValidateEmail(string email)
        {
            // Définir l'expression régulière pour une adresse e-mail valide
            string regexPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Vérifier si l'adresse e-mail correspond à l'expression régulière
            bool isMatch = Regex.IsMatch(email, regexPattern);

            // Retourner true si l'adresse e-mail est valide, false sinon
            return isMatch;
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox4.Text == "")
                {
                    textBox4.Text = "Enter mail";
                    return;
                }
                textBox4.ForeColor = Color.White;
                panel16.Visible = false;
            }
            catch { }
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            textBox4.SelectAll();
        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (textBox3.Text == "")
                {
                    textBox3.Text = "Enter password ";
                    return;
                }
                textBox3.ForeColor = Color.White;
                panel12.Visible = false;
                textBox3.PasswordChar = '*';
            }
            catch { }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox5.Text == "")
                {
                    textBox5.Text = "Re-Enter password ";
                    return;
                }
                textBox5.ForeColor = Color.White;
                panel20.Visible = false;
                textBox5.PasswordChar = '*';
            }
            catch { }
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            textBox3.SelectAll();
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            textBox5.SelectAll();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //le boutton exit
            DialogResult res;
            string text = "Do you want to exit ?";
            res = MessageBox.Show(text, "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(res == DialogResult.Yes)
            {
                Application.Exit();
            }else
            {
                this.Show();
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlLogin.Visible = true;
            pnlLogin.Dock = DockStyle.Fill;
            pnlSu.Visible = false;
            pnlLogo.Dock = DockStyle.Left;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //pour aller a l'interface de creer un compte 
            pnlLogin.Visible = false;
            pnlSu.Visible = true;
            pnlLogo.Dock = DockStyle.Right;
            pnlSu.Dock = DockStyle.Fill;
          
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            //le boutton guest si l'utilisateur veut utliliser l'app sans avoir un compte
            this.Hide();
            Form2 f = new Form2();
            f.ShowDialog();
            this.Close();
        }

        private void panel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlLogo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlLogin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void pnlSu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel16_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void panel17_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel18_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void panel19_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel20_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void panel21_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel22_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
