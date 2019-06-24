using ContactLog.ContactClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactLog
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


        }
        // Creating a contacts object

        Contacts contacts = new Contacts();

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

          private void btnAdd_Click(object sender, EventArgs e)
          {
              contacts.FirstName = txtFirstName.Text;
              contacts.LastName = txtLastName.Text;
              contacts.Email = txtEmail.Text;
              contacts.Address = txtAddress.Text;
              bool result;
              result=contacts.Insert(contacts);

              if(result)
              {
                  MessageBox.Show("Contact is Added successfuly");
              }
              else { MessageBox.Show("Could not add a new contact due to the Error"); }

              //Loading data to the GridView
              DataTable dt = contacts.DisplayAllData();
              dataGridView1.DataSource = dt;
              clearAll();
          }

          private void Form1_Load(object sender, EventArgs e)
          {
              DataTable dt = contacts.DisplayAllData();
              dataGridView1.DataSource = dt;
          }

        public void clearAll()
          {
              txtContactId.Text = "";
              txtFirstName.Text = "";
              txtLastName.Text="";
              txtEmail.Text = " ";
              txtAddress.Text = "";
          }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtContactId.Text = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dataGridView1.Rows[rowIndex].Cells[3].Value.ToString();
            txtAddress.Text = dataGridView1.Rows[rowIndex].Cells[4].Value.ToString();
           
       
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            contacts.ContactID = int.Parse(txtContactId.Text);
            contacts.FirstName = txtFirstName.Text;
            contacts.LastName = txtLastName.Text;
            contacts.Email = txtEmail.Text;
            contacts.Address = txtAddress.Text;
          
            bool rows=contacts.Update(contacts);

            if (rows==true)
            {
                MessageBox.Show("Contact is Successfully Updated");
            }
            else { MessageBox.Show("Error occured while trying to Update the Contact"); }

            DataTable dt = contacts.DisplayAllData();
            dataGridView1.DataSource = dt;
            clearAll();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            contacts.ContactID = int.Parse(txtContactId.Text);

            bool isSuccess=contacts.Delete(contacts);
            if(isSuccess)
            { MessageBox.Show("Data is Deleted Successfuly From the DataBase"); }
            else { MessageBox.Show("Error occured while deleting a data from the database"); }
            clearAll();
            DataTable dt = contacts.DisplayAllData();
            dataGridView1.DataSource = dt;
        }

        static string connectionString = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;

            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "SELECT * FROM all_contacts WHERE FirstName Like '%" + keyword + "%' OR LastName LIKE '%" + keyword + "%'";
            
            SqlDataAdapter adapter = new SqlDataAdapter(sql,connection);
            connection.Open();
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        
      }
    
}
