using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace hrmanagementsystem
{
    public partial class department : Form
    {
        public department()
        {
            InitializeComponent();
        }
        string connection = "server=localhost;port=3306;database=hr_management_system;user=root;password=";

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkfieldsnull() == true)
            {
                string id = idtextBox.Text.Trim();
                string name = nametextBox.Text.Trim();
                using (MySqlConnection conn = new MySqlConnection(connection))
                {
                    conn.Open();
                    string query = "insert into department (id, dp_name) values (@id,@name)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", name);

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Department Added Successfully");
                    }
                }

            }
        }
        Boolean checkfieldsnull()
        {
            if (idtextBox.Text.Trim().Equals("") || nametextBox.Text.Trim().Equals(""))
            {
                MessageBox.Show("All Fields Are Required");
                return false;
            }
            return true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string query = "Select * from department";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                    string id = Convert.ToString(selectedRow.Cells["id"].Value);
                    deletedata(id);
                    loaddata();
                }

            }
        }
        private void deletedata(string id)
        {
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string query = "delete from department where id=@id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        private void loaddata()
        {
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string query = "Select * from department";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3();
            frm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                string id = Convert.ToString(selectedRow.Cells["id"].Value);
                string dept = Convert.ToString(selectedRow.Cells["dp_name"].Value);
                updatedept(id, dept);
                loaddata();
                MessageBox.Show("Data Updated");
            }
        }
        private void updatedept(string id, string dept)
        {
            using(MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string query = "update department set dp_name=@dept where id=@id";
                MySqlCommand cmd = new MySqlCommand(query,conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@dept", dept);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
