using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Data Source=TAHA\\SQLEXPRESS;Initial Catalog=dictionnaire;Integrated Security=True");


        private void button4_Click(object sender, EventArgs e)
        {

            if(textBox1.Text == "" )
            {
                panel16.Visible = true;
                textBox1.Focus();
                return;
            }
            if (textBox2.Text == "")
            {
                panel3.Visible = true;
                textBox2.Focus();
                return;
            }
            if (textBox3.Text == "")
            {
                panel6.Visible = true;
                textBox3.Focus();
                return;
            }
            if (textBox4.Text == "")
            {
                panel9.Visible = true;
                textBox4.Focus();
                return;
            }
            if (textBox5.Text == "")
            {
                panel12.Visible = true;
                textBox5.Focus();
                return;
            }
            conn.Open();
                string req = "INSERT INTO Dic_fr_ang (ID, mot, type, traduction, exemple_fr, exemple_ang) VALUES (NEXT VALUE FOR Dic_fr_ang_seq, @mot, @type, @traduction, @ex_fr, @ex_ang);";
                string req2 = "SELECT * from Dic_fr_ang where mot = @mot and traduction = @trad;";
                SqlCommand cmd1 = new SqlCommand(req2, conn);
                cmd1.Parameters.AddWithValue("@mot", textBox1.Text);
                cmd1.Parameters.AddWithValue("@trad", textBox2.Text);
                SqlDataReader reader = cmd1.ExecuteReader();
                if (reader.HasRows)
                {
                  MessageBox.Show("existant...");
                  textBox1.Clear();
                  textBox2.Clear();
                  textBox3.Clear();
                  textBox4.Clear();
                  textBox5.Clear();
                  textBox1.Focus();
                  return;
                 }

                conn.Close();
                conn.Open();
                SqlCommand cmd = new SqlCommand(req, conn);
                cmd.Parameters.AddWithValue("@mot", textBox1.Text);
                cmd.Parameters.AddWithValue("@type", textBox3.Text);
                cmd.Parameters.AddWithValue("@traduction", textBox2.Text);
                cmd.Parameters.AddWithValue("@ex_fr", textBox4.Text);
                cmd.Parameters.AddWithValue("@ex_ang", textBox5.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Success...");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox1.Focus();
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           try
            {
                if (textBox1.Text == " ")
                {
                    textBox1.Text = "";
                    return;
                }
                panel16.Visible = false;
            }
            catch { }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text == " ")
                {
                    textBox2.Text = "";
                    return;
                }
                panel3.Visible = false;
            }
            catch { }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox3.Text == " ")
                {
                    textBox3.Text = "";
                    return;
                }
                panel6.Visible = false;
            }
            catch { }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox4.Text == " ")
                {
                    textBox4.Text = "";
                    return;
                }
                panel9.Visible = false;
            }
            catch { }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox5.Text == " ")
                {
                    textBox5.Text = "";
                    return;
                }
                panel12.Visible = false;
            }
            catch { }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 ad = new Form3();
            ad.ShowDialog();
            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
          
        }
    }
}
