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
    public partial class List_of_employees : Employee
    {
        public List_of_employees()
        {
            InitializeComponent();
        }
        string connection = "server=localhost;port=3306;database=hr_management_system;user=root;password=";
        private void button2_Click(object sender, EventArgs e)
        {
            Add_Employee employee = new Add_Employee();
            employee.Show();
            this.Hide();
        }

        private void List_of_employees_Load(object sender, EventArgs e)
        {
            loaddata();
        }

        private void loaddata()
        {
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string query = "Select * from employee";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void button3_Click(object sender, EventArgs e)
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
                string query = "delete from employee where id=@id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                string emp = Convert.ToString(selectedRow.Cells["id"].Value);
                string name = Convert.ToString(selectedRow.Cells["name"].Value);
                string job = Convert.ToString(selectedRow.Cells["jb_id"].Value);
                string dept = Convert.ToString(selectedRow.Cells["dp_id"].Value);
                string dob = Convert.ToString(selectedRow.Cells["dob"].Value);
                string gender = Convert.ToString(selectedRow.Cells["gender"].Value);
                string address = Convert.ToString(selectedRow.Cells["address"].Value);
                string phone = Convert.ToString(selectedRow.Cells["phone_no"].Value);
                string gmail = Convert.ToString(selectedRow.Cells["gmail"].Value);
                string hdate = Convert.ToString(selectedRow.Cells["hire_date"].Value);
                updateroom(emp,name,job,dept,dob,gender,address,phone,gmail,hdate);
                loaddata();
                MessageBox.Show("Date Updated");

            }
        }
        private void updateroom(string emp,string name,string job,string dept,string dob,string gender, string address, string phone,string gmail,string hdate)
        {
         using(MySqlConnection conn = new MySqlConnection(connection)) 
            {
                conn.Open();
                string query = "update employee set name=@name,gmail=@gmail,gender=@gen,dob=@dob,phone_no=@ph,address=@address,hire_date=@hire,dp_id=@dept,jb_id=@job where id=@id";
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@id", emp);
                command.Parameters.AddWithValue ("@name", name);
                command.Parameters.AddWithValue("@gmail", gmail);
                command.Parameters.AddWithValue("@gen", gender);
                command.Parameters.AddWithValue("@dob", dob);
                command.Parameters.AddWithValue("@ph", phone);
                command.Parameters.AddWithValue("@address", address);
                command.Parameters.AddWithValue("@hire", hdate);
                command.Parameters.AddWithValue("@dept", dept);
                command.Parameters.AddWithValue("@job", job);
                command.ExecuteNonQuery();
                conn.Close();
            }   
        }
    }
}
