using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hrmanagementsystem
{
    public partial class job_position : Form
    {
        public job_position()
        {
            InitializeComponent();
        }
        string connection = "server=localhost;port=3306;database=hr_management_system;user=root;password=";
        private void job_position_Load(object sender, EventArgs e)
        {
            loaddept();
        }
        private void loaddept()
        {
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();

                string query = "SELECT id, dp_name FROM department";
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Bind the data to the ComboBox
                deptcomboBox.DataSource = dataTable;
                deptcomboBox.DisplayMember = "dp_name";
                deptcomboBox.ValueMember = "id";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkfieldsnull() == true)
            {


                string selecteddep = Convert.ToString(deptcomboBox.SelectedValue);
                string title = titletextBox.Text.Trim();
                string desc = destextBox.Text.Trim();
                Insert(title, desc, selecteddep);
            }
        }
        
        private void Insert(string title, string desc, string dep)
        {
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
               
                    conn.Open();
                    string query = "INSERT INTO `position` (jb_title, job_description, dp_id) VALUES (@title, @des, @dep)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@des", desc);
                    cmd.Parameters.AddWithValue("@dep", dep);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("New Job Added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
        
        
        Boolean checkfieldsnull()
        {
            if (deptcomboBox.Text.Trim().Equals("") || titletextBox.Text.Trim().Equals("") || destextBox.Text.Trim().Equals(""))
            {
                MessageBox.Show("All Fields Are Required");
                return false;
                
            }
            else
            { return true; }    
        }

        private void label3_Click(object sender, EventArgs e)
        {

            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string query = "Select * from position";
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
                string query = "delete from position where id=@id";
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
                string query = "Select * from position";
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
                string title = Convert.ToString(selectedRow.Cells["jb_title"].Value);
                string disp = Convert.ToString(selectedRow.Cells["job_description"].Value);
                string dp_id = Convert.ToString(selectedRow.Cells["dp_id"].Value);
                updatejob(id, title,disp,dp_id);
                loaddata();
                MessageBox.Show("Data Updated");
            }
        }
        private void updatejob(string id, string title, string disp, string dp_id)
        {
            using(MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string query = "update position set jb_title=@title, job_description=@disp, dp_id=@dept where id=@id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@disp",disp );
                cmd.Parameters.AddWithValue("@dept", dp_id);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
