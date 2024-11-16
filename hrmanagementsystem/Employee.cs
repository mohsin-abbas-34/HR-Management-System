using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hrmanagementsystem
{
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Add_Employee ae=new Add_Employee();
            ae.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            List_of_employees lp=new List_of_employees();
            lp.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 frm=new Form3();
            frm.Show();
            this.Hide();
        }
    }
}
