using System;  
using System.Collections.Generic;  
using System.ComponentModel;  
using System.Data;  
using System.Drawing;  
using System.Linq;  
using System.Text;  
using System.Windows.Forms;  
using System.Data.Sql;  
using System.Data.OleDb;  
using System.Data.SqlClient;  
  
namespace LibraryManagementSystem
{  
    public partial class Login_Form : Form  
    {  
        SqlConnection con = new SqlConnection();  
        public Login_Form()  
        {  
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=.;Initial Catalog=Library;Integrated Security=True";  
  
            InitializeComponent();  
        }  
  
        private void Login_Form_Load(object sender, EventArgs e)  
        {  
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Library;Integrated Security=True");  
            con.Open();  
  
            {  
            }  
        }  
  
        private void button_login_Click(object sender, EventArgs e)  
        {  
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=.;Initial Catalog=Library;Integrated Security=True";  
            con.Open();  
            string userid = txt_username.Text;  
            string password = txt_password.Text;  
            SqlCommand cmd = new SqlCommand("select UserID,Password from Login_Table where UserID='" + txt_username.Text + "'and Password='" + txt_password.Text + "'", con);  
            SqlDataAdapter da = new SqlDataAdapter(cmd);  
            DataTable dt = new DataTable();  
            da.Fill(dt);  
            if (dt.Rows.Count > 0)  
            {  
                MessageBox.Show("Login sucess !");
                this.Hide();
                Form1 b1 = new Form1();
                b1.ShowDialog();
                this.Close();
            }  
            else  
            {  
                MessageBox.Show("Invalid Login please check username and password");  
            }  
            con.Close();  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }  
  
    }  
}  