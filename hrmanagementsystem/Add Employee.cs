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
    public partial class Add_Employee : Employee
    {
        public Add_Employee()
        {
            InitializeComponent();
        }
        string connection = "server=localhost;port=3306;database=hr_management_system;user=root;password=";
        private void Add_Employee_Load(object sender, EventArgs e)
        {
            loaddept();
            loadjob();
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
                depcomboBox.DataSource = dataTable;
                depcomboBox.DisplayMember = "dp_name";
                depcomboBox.ValueMember = "id";
            }
        }
        private void loadjob()
        {
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();

                string query = "SELECT id, jb_title FROM position";
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Bind the data to the ComboBox
                jobcomboBox.DataSource = dataTable;
                jobcomboBox.DisplayMember = "jb_title";
                jobcomboBox.ValueMember = "id";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkfieldsnull() == true)
            {


                string dept = Convert.ToString(depcomboBox.SelectedValue);
                string job = Convert.ToString(jobcomboBox.SelectedValue);

                string emp = emptextBox.Text;
                string name = nametextBox.Text.Trim();
                string dob = dobdateTimePicker.Text.Trim();
                string gender = gendercomboBox.Text.Trim();

                string address = addresstextBox.Text.Trim();
                string phone = phonetextBox.Text.Trim();
                string email = gmailtextBox.Text.Trim();
                string hire = hiredateTimePicker.Text.Trim();

               
                Insert(name, gender, job, phone, email, address, dept, emp, dob,hire);

            }
        }
        private void Insert(string name, string gender, string job, string phone, string email, string address, string dept, string emp, string dob, string hire)
        {
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string query = "INSERT INTO employee(id,name,gmail,gender,dob,phone_no,address,hire_date,dp_id,jb_id) VALUES (@emp,@name,@gmail,@gender,@dob,@phone,@address,@hire,@dept,@job)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@emp", emp);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@gmail", email);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@dob", dob);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@hire", hire);
                cmd.Parameters.AddWithValue("@dept", dept);
                cmd.Parameters.AddWithValue("@job", job);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Employee Added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }




        }
    Boolean checkfieldsnull()
        {
            if(emptextBox.Text.Trim().Equals("")|| nametextBox.Text.Trim().Equals("") || phonetextBox.Text.Trim().Equals("") ||
                addresstextBox.Text.Trim().Equals("") || gmailtextBox.Text.Trim().Equals("") || hiredateTimePicker.Text.Trim().Equals("") 
                || dobdateTimePicker.Text.Trim().Equals("") || jobcomboBox.Text.Trim().Equals("") || gendercomboBox.Text.Trim().Equals("") ||
                depcomboBox.Text.Trim().Equals(""))
            {
                MessageBox.Show("All Fields Are Required");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3();
            frm.Show();
            this.Hide();
        }
    }


}