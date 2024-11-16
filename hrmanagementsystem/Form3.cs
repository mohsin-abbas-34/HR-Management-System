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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            employee.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            department dept = new department();
            dept.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            job_position jb= new job_position();
            jb.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            leave lea = new leave();
            lea.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frm=new Form1();
            frm.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            attendance attendance = new attendance();
            attendance.Show();
            this.Hide();
        }
    }
}
