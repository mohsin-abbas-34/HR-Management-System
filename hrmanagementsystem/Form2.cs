using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace hrmanagementsystem
{
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }
        string connection = "server=localhost;port=3306;database=hr_management_system;user=root;password=";
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkfieldsnull() == true)
            {
                string name = nametextBox.Text.Trim();
                string username = usertextBox.Text.Trim();
                string password = passtextBox.Text.Trim();

                using (MySqlConnection conn = new MySqlConnection(connection))
                {
                    conn.Open();

                    string query = "insert into login (name, username, password) values (@name, @username, @pass)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@pass", password);

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Account Created");
                    }

                    conn.Close();
                    Form1 frm = new Form1();
                    frm.Show();
                    this.Hide();
                }
            }
        }
        Boolean checkfieldsnull()
        {
            if (nametextBox.Text.Trim().Equals("") || usertextBox.Text.Trim().Equals("") || passtextBox.Text.Trim().Equals(""))
            {
                MessageBox.Show("All Fields Are Required");
                return false;
            }
            return true;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
