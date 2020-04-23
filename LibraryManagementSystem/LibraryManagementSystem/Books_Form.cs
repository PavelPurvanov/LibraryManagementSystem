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

namespace LibraryManagementSystem
{
    public partial class Books_Form : Form
    {
        SqlDataAdapter adap;
        DataSet ds;
        SqlCommandBuilder cmdbl;


        public Books_Form()
        {
            InitializeComponent();
        }


        private void button_insert_Click(object sender, EventArgs e)
        {
            if ((!(string.IsNullOrWhiteSpace(txt_name.Text) ||
                string.IsNullOrWhiteSpace(txt_country.Text) ||
                string.IsNullOrWhiteSpace(txt_publisher.Text) ||
                string.IsNullOrWhiteSpace(txt_year.Text) ||
                string.IsNullOrWhiteSpace(txt_genre.Text) ||
                string.IsNullOrWhiteSpace(txt_amount.Text)
                )) &&
                txt_year.Text.Length <= 4)
            {

                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Library;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into Books_Table(Name,Country,Author,Yearr,Genre,Amount) values ('" + txt_name.Text + "','" + txt_country.Text + "','" + txt_publisher.Text + "','" + txt_year.Text + "','" + txt_genre.Text + "','" + txt_amount.Text + "')", con);
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
                adap.Fill(ds, "Books_Table");
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
                adap = new SqlDataAdapter("select BookID, Name as 'First Name', Country as 'Country Name', Author as 'Author Name', Yearr, Genre as 'Genre Name', Amount from Books_Table ", con2);
                ds = new System.Data.DataSet();
                adap.Fill(ds, "Books_Table");
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
                adap.Update(ds, "Books_table");
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

                SqlCommand cmd = new SqlCommand("DELETE from Books_Table WHERE (BookID='" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "')", con3);
                int j = cmd.ExecuteNonQuery();
                if (j > 0)
                {
                    MessageBox.Show("Record Deleted Successfully!" + j + "");


                }
                con3.Close();

                ds = new System.Data.DataSet();
                adap.Fill(ds, "Books_Table");
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                this.Show();

            }
        }



        private void txt_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txt_country_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txt_publisher_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txt_year_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txt_genre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txt_amount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}
