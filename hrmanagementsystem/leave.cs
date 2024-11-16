using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hrmanagementsystem
{
    public partial class leave : Form
    {
        public leave()
        {
            InitializeComponent();
        }
        string connection = "server=localhost;port=3306;database=hr_management_system;user=root;password=";
        private void button3_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3();
            frm.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connection))
                {
                    conn.Open();
                    string query = "SELECT * FROM `leave`";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (checkfieldsnull() == true)
            {


                string emp = Convert.ToString(empcomboBox.SelectedValue);
                string start = startdateTimePicker.Text.Trim();
                string end = enddateTimePicker.Text.Trim();
                string status = statuscomboBox.Text.Trim();
                Insert(emp, start, end, status);
            }

        }
        private void Insert(string emp, string start, string end, string status)
        {
            using (MySqlConnection conn = new MySqlConnection(connection))
            {

                conn.Open();
                string query = "INSERT INTO `leave` (start,end,status,emp_id) VALUES (@start, @end, @status,@emp)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@emp", emp);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        Boolean checkfieldsnull()
        {
            if (empcomboBox.Text.Trim().Equals("") || startdateTimePicker.Text.Trim().Equals("") || enddateTimePicker.Text.Trim().Equals("")
                || statuscomboBox.Text.Trim().Equals(""))
            {
                MessageBox.Show("All Fields Are Required");
                return false;

            }
            else
            { return true; }
        }
        private void leave_Load(object sender, EventArgs e)
        {
            loademp();
        }
        private void loademp()
        {
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();

                string query = "SELECT * FROM employee";
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Bind the data to the ComboBox
                empcomboBox.DataSource = dataTable;
                empcomboBox.DisplayMember = "id";
                empcomboBox.ValueMember = "id";
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
                string query = "delete from leave where id=@id";
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
                string query = "Select * from leave";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                string id = Convert.ToString(selectedRow.Cells["id"].Value);
                string start = Convert.ToString(selectedRow.Cells["start"].Value);
                string end = Convert.ToString(selectedRow.Cells["end"].Value);
                string status = Convert.ToString(selectedRow.Cells["status"].Value);
                string emp = Convert.ToString(selectedRow.Cells["emp_id"].Value);
                updateleave(id, start, end, status, emp);
                loaddata();
                MessageBox.Show("Data Updated");
            }
        }
        private void updateleave(string id, string start, string end, string status, string emp)
        {

            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string query = "UPDATE leave SET start=@start, end=@end, status=@status, emp_id=@emp WHERE id=@id";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@emp", emp);
                cmd.ExecuteNonQuery();
                conn.Close();

            }

        }
    }
    
}
