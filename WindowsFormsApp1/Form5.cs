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
using static System.Net.Mime.MediaTypeNames;
using WinForms = System.Windows.Forms;
using MediaType = System.Net.Mime.MediaTypeNames;



namespace WindowsFormsApp1
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=TAHA\SQLEXPRESS;Initial Catalog=dictionnaire;Integrated Security=True");

        private void pictureBox4_Click(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            string req = "Select * from Dic_fr_ang;";
            SqlCommand cmd = new SqlCommand(req, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if(reader.HasRows)
            {
                this.dgv.Rows.Clear();
                while (reader.Read())
                {
                    this.dgv.Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4], reader[5] ); 
                }
            }else
            {
                MessageBox.Show("Erreur");
            }

            conn.Close();

            this.dgv.AllowUserToAddRows = false;


        }

        string idc = "-1";
        int i = -1;
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             i = this.dgv.CurrentRow.Index;
             idc = this.dgv.Rows[i].Cells[0].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(i == -1)
            {
                MessageBox.Show("veuillez selectionnez une ligne !!");
                return;
            }

            string t = "Are you sure ?";
            DialogResult dg = MessageBox.Show(t, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dg == DialogResult.No) {
                return;
            }


            conn.Open();
            string req = "Delete from Dic_fr_ang where id = @id;";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.AddWithValue("@id", idc);
            cmd.ExecuteNonQuery();
            this.dgv.Rows.RemoveAt(i);
            MessageBox.Show("Action faite avec succes !");
            conn.Close();

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 ad = new Form3();
            ad.ShowDialog();
            this.Close();
        }
    }
}
