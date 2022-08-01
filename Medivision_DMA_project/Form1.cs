using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Medivision_DMA_project
{
    public partial class Login_form : Form
    {
        private MySqlConnection connection;
        //private string server;
        //private string database;
        //private string uid;
        //private string password;
        public Login_form()
        {
            
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void bt_signin_Click(object sender, EventArgs e)
        {
            Signin_panel.Visible = Enabled;
            Login_panel.Visible = false;
        }

        private void bt_login_Click(object sender, EventArgs e)
        {
            Signin_panel.Visible = false;
            Login_panel.Visible = Enabled;
        }

        private void Signin_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string connectionString;
            connectionString = "Server=localhost;Uid=root;password=pratik@123;Database=medivision_app;Port=3306;";
            connection = new MySqlConnection(connectionString);
            connection.Open();
            if ( Txt_name.Text != "" && Txt_susername.Text != "" && Txt_emaid_ID.Text != "" && Txt_Mobile_No.Text != "" && Txt_cpassword.Text != ""  )
            {
                try
                {
                    var query = "INSERT INTO medivision_app.customers (Name, Username, Email_id,Mobile_no,Password) VALUES('" + this.Txt_name.Text + "','" + this.Txt_susername.Text + "','" + this.Txt_emaid_ID.Text + "','" + this.Txt_Mobile_No.Text + "','" + this.Txt_cpassword.Text + "');";
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //Execute command
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("signed in successfully");
                    Signin_panel.Visible = false;
                    Login_panel.Visible = Enabled;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("please enter the data correclty", "please enter valid data ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString;
            connectionString = "Server=localhost;Uid=root;password=pratik@123;Database=medivision_app;Port=3306;";
            connection = new MySqlConnection(connectionString);
            connection.Open();

            var query = "select COUNT(*) from customers  WHERE  Username = '"+ this.Txt_Lusername.Text+ "' AND Password = '" + this.Txt_Lpassword.Text+ "'; ";
            var cmd = new MySqlCommand(query, connection);

            MySqlDataAdapter mySqlData = new MySqlDataAdapter(query, connection);
            DataTable data = new DataTable();
            mySqlData.Fill(data);

            if (data.Rows[0][0].ToString()=="1")
            {
                new Main_form().Show();
                this.Hide();
                connection.Close();
            }
            else
            {
                MessageBox.Show("Invalid password or username","Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void Login_form_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to quit the Appliction ", "message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                
            }
            else if (result == DialogResult.No)
            {
                
                Close();

            }
        }
    }
}
