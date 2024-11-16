using MySql.Data.MySqlClient;
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
    public partial class attendance : Form
    {
        public attendance()
        {
            InitializeComponent();
        }
        string connection = "server=localhost;port=3306;database=hr_management_system;user=root;password=";
        private void label3_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connection))
                {
                    conn.Open();
                    string query = "SELECT * FROM `attendance`";
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

        private void attendance_Load(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkfieldsnull() == true)
            {


                string emp = Convert.ToString(empcomboBox.SelectedValue);
                string date = datePicker.Text.Trim();
                string chkin = inTimePicker.Text.Trim();
                string chkout = outTimePicker.Text.Trim();
                Insert(emp, date, chkin, chkout);
            }
        }
        private void Insert(string emp, string date, string chkin, string chkout)
        {
            using (MySqlConnection conn = new MySqlConnection(connection))
            {

                conn.Open();
                string query = "INSERT INTO `attendance` (date,chk_in_time,chk_out_time,emp_id) VALUES (@date, @chkin, @chkout,@emp)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@chkin", chkin);
                cmd.Parameters.AddWithValue("@chkout", chkout);
                cmd.Parameters.AddWithValue("@emp", emp);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        Boolean checkfieldsnull()
        {
            if (empcomboBox.Text.Trim().Equals("") || datePicker.Text.Trim().Equals("") || inTimePicker.Text.Trim().Equals("")
                || outTimePicker.Text.Trim().Equals(""))
            {
                MessageBox.Show("All Fields Are Required");
                return false;

            }
            else
            { return true; }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3();
            frm.Show();
            this.Hide();
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
                string query = "delete from attendance where id=@id";
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
                string query = "Select * from attendance";
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
                string date = Convert.ToString(selectedRow.Cells["date"].Value);
                string chkin = Convert.ToString(selectedRow.Cells["chk_in_time"].Value);
                string chkout = Convert.ToString(selectedRow.Cells["chk_out_time"].Value);
                string emp = Convert.ToString(selectedRow.Cells["emp_id"].Value);
                updateatd(id,date,chkin,chkout,emp);
                loaddata();
                MessageBox.Show("Data Updated");
            }
        }
        private void updateatd(string id,string date,string chkin,string chkout,string emp)
        {
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string query = " update attendance set date=@date,chk_in_time=@chkin,chk_out_time=@chkout,emp_id=@emp where id=@id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@chkin", chkin);
                cmd.Parameters.AddWithValue("@chkout", chkout);
                cmd.Parameters.AddWithValue("@emp", emp);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        } 
    }

}
