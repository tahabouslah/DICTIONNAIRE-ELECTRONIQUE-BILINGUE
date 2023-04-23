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
using WinForms = System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form7 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=TAHA\SQLEXPRESS;Initial Catalog=dictionnaire;Integrated Security=True");
        public Form7()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult res;
            string text = "Do you want to exit ?";
            res = MessageBox.Show(text, "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                WinForms.Application.Exit();
            }
            else
            {
                this.Show();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 ad = new Form3();
            ad.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (position == -1)
            {
                MessageBox.Show("veuillez selectionnez une ligne !!");
                return;
            }

            if (textBox1.Text == "")
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

            string t = "Are you sue ?";
            DialogResult dg = MessageBox.Show(t, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.No)
            {
                return;
            }

            conn.Open();
            string req = "Update Dic_fr_ang SET mot = @m , type = @t , traduction = @tr , exemple_fr = @fr , exemple_ang = @ang Where id = @id;";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.AddWithValue("@m", textBox1.Text);
            cmd.Parameters.AddWithValue("@t", textBox2.Text);
            cmd.Parameters.AddWithValue("@tr", textBox3.Text);
            cmd.Parameters.AddWithValue("@fr", textBox5.Text);
            cmd.Parameters.AddWithValue("@ang", textBox4.Text);
            cmd.Parameters.AddWithValue("@id", textBox6.Text);

            cmd.ExecuteNonQuery();

            conn.Close();

            MessageBox.Show("success....");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            string req = "Select * from Dic_fr_ang order by id;";
            SqlCommand cmd = new SqlCommand(req, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                this.dgv.Rows.Clear();
                while (reader.Read())
                {
                    this.dgv.Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]);
                }
            }
            else
            {
                MessageBox.Show("Erreur");
            }

            conn.Close();
            this.dgv.AllowUserToAddRows = false;
        }

        int position = -1;
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            position = this.dgv.CurrentRow.Index;
            this.textBox6.Text = this.dgv.Rows[position].Cells[0].Value.ToString();
            this.textBox1.Text = this.dgv.Rows[position].Cells[1].Value.ToString();
            this.textBox2.Text = this.dgv.Rows[position].Cells[2].Value.ToString();
            this.textBox3.Text = this.dgv.Rows[position].Cells[3].Value.ToString();
            this.textBox5.Text = this.dgv.Rows[position].Cells[4].Value.ToString();
            this.textBox4.Text = this.dgv.Rows[position].Cells[5].Value.ToString();

            this.textBox6.Enabled = false;
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            this.dgv.ReadOnly = true;
        }



        private void pnlAj_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

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
    }
}
