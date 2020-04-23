using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Books_Form b1 = new Books_Form();
            b1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Readers_Form r1 = new Readers_Form();
            r1.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Reference_Form r1 = new Reference_Form();
            r1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Employess_Form e1 = new Employess_Form();
            e1.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
