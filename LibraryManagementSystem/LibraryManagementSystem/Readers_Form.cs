using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class Readers_Form : Form
    {
        SqlDataAdapter adap;
        DataSet ds;
        SqlCommandBuilder cmdbl;



        public Readers_Form()
        {
            InitializeComponent();
        }


        private void button_insert_Click(object sender, EventArgs e)
        {
            if ((!(string.IsNullOrWhiteSpace(txt_first_name.Text) ||
                string.IsNullOrWhiteSpace(txt_last_name.Text) ||
                string.IsNullOrWhiteSpace(txt_egn.Text) ||
                string.IsNullOrWhiteSpace(txt_address.Text) ||
                string.IsNullOrWhiteSpace(txt_phone.Text))) &&
                txt_egn.Text.Length == 10 &&
                txt_phone.Text.Length == 10)
            {
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Library;Integrated Security=True");

                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into Readers_Table(First_Name,Last_Name,EGN,Address,Phone) values ('" + txt_first_name.Text + "','" + txt_last_name.Text + "','" + txt_egn.Text + "','" + txt_address.Text + "','" + txt_phone.Text + "')", con);
                int i = cmd.ExecuteNonQuery();
                if (i != 0)
                {
                    MessageBox.Show("Saved");

                }
                else
                {
                    MessageBox.Show("Error");
                }
                con.Close();

                ds = new System.Data.DataSet();
                adap.Fill(ds, "Readers_Table");
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
               MessageBox.Show("Incorrectly entered data !");
            }
        }

        private void Books_Form_Load(object sender, EventArgs e)
        {

            try
            {
                SqlConnection con2 = new SqlConnection("Data Source=.;Initial Catalog=Library;Integrated Security=True");
                con2.Open();
                adap = new SqlDataAdapter("select ReaderID,First_Name as 'First Name', Last_Name as 'Last Name', EGN as 'EGN', Address as 'Address', Phone as 'Phone' from Readers_Table", con2);
                ds = new System.Data.DataSet();
                adap.Fill(ds, "Readers_Table");
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            try
            {
                cmdbl = new SqlCommandBuilder(adap);
                adap.Update(ds, "Readers_Table");
                MessageBox.Show("Information Updated", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            SqlConnection con3 = new SqlConnection("Data Source=.;Initial Catalog=Library;Integrated Security=True");
            DialogResult del = MessageBox.Show("Are you Sure you want to Delete" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "Record", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (del == DialogResult.Yes)
            {
                con3.Open();

                SqlCommand cmd = new SqlCommand("DELETE from Readers_Table WHERE (ReaderID='" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "')", con3);
                int j = cmd.ExecuteNonQuery();
                if (j > 0)
                {
                    MessageBox.Show("Record Deleted Successfully!" + j + "");


                }
                con3.Close();

                ds = new System.Data.DataSet();
                adap.Fill(ds, "Readers_Table");
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                this.Show();

            }
        }



        private void txt_first_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txt_last_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txt_egn_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txt_phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
       
}