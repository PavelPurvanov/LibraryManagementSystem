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
    public partial class Reference_Form : Form
    {
        SqlDataAdapter adap;
        DataSet ds;
        SqlCommandBuilder cmdbl;

        public Reference_Form()
        {
            InitializeComponent();
        }

        private void Reference_Form_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con2 = new SqlConnection("Data Source=.;Initial Catalog=Library;Integrated Security=True");
                con2.Open();
                adap = new SqlDataAdapter("select ReferenceID,BookID , ReaderID, EmployeeID, Amount, Date_of_borrowing, Date_of_returning from Reference_Table", con2);
                ds = new System.Data.DataSet();
                adap.Fill(ds, "Reference_Table");
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_insert_Click(object sender, EventArgs e)
        {
            if ((!(string.IsNullOrWhiteSpace(txt_bookid.Text) ||
                string.IsNullOrWhiteSpace(txt_readerid.Text) ||
                string.IsNullOrWhiteSpace(txt_employeeid.Text) ||
                string.IsNullOrWhiteSpace(txt_amount.Text))))
            {
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Library;Integrated Security=True");

                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into Reference_Table(BookID,ReaderID,EmployeeID,Amount,Date_of_borrowing,Date_of_returning) values ('" + txt_bookid.Text + "','" + txt_readerid.Text + "','" + txt_employeeid.Text + "','" + txt_amount.Text + "','" + dateTimePicker1.Value.Date + "','" + dateTimePicker2.Value.Date + "')", con);
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
                adap.Fill(ds, "Reference_Table");
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("Incorrectly entered data !");
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            try
            {
                cmdbl = new SqlCommandBuilder(adap);
                adap.Update(ds, "Reference_Table");
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

                SqlCommand cmd = new SqlCommand("DELETE from Reference_Table WHERE (ReferenceID='" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "')", con3);
                int j = cmd.ExecuteNonQuery();
                if (j > 0)
                {
                    MessageBox.Show("Record Deleted Successfully!" + j + "");


                }
                con3.Close();

                ds = new System.Data.DataSet();
                adap.Fill(ds, "Reference_Table");
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                this.Show();

            }
        }

        private void txt_bookid_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txt_readerid_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txt_employeeid_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
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
