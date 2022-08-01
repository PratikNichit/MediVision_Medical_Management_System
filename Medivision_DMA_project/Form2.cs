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
using System.Configuration;

namespace Medivision_DMA_project
{
    public partial class Main_form : Form
    {
        // this is the primary key used for delete/ update operations 
        int id = 0;
        int sale_id = 0;
        int required_id = 0; 
        private MySqlConnection connection;
        public Main_form()
        {
            InitializeComponent();
        }
        // clear function
        public void clear()
        {
            Txt_product_name.Text = Txt_batch_no.Text = Txt_purchase_rate.Text = Txt_Description.Text = Txt_mrp.Text = "";
            Nud_quantity.Value = 0;
            dt_exp_date.ResetText();
            dt_Pack_date.ResetText();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to log out ", "message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Login_form login_Form = new Login_form();
                login_Form.Show();
                this.Close();
            }
            else
            {

            }

        }

        private void bt_save_Click(object sender, EventArgs e)
        {
                try
                {
                    string connectionString;
                    connectionString = "Server=localhost;Uid=root;password=pratik@123;Database=medivision_app;Port=3306;";
                    connection = new MySqlConnection(connectionString);
                    connection.Open();

                    if (Txt_product_name.Text != "" && Txt_batch_no.Text != "" && Nud_quantity.Text != "" && dt_Pack_date.Text != "" && dt_exp_date.Text != "" && Txt_purchase_rate.Text != "" && Txt_mrp.Text != "")
                    {
                        var query = "INSERT INTO medivision_app.purchase (product_name, Batch_no,quantity,Pack_date,Exp_date,Purchase_rate,MRP,Description)" +
                                    "VALUES('" + this.Txt_product_name.Text + "','" + this.Txt_batch_no.Text + "','" + this.Nud_quantity.Text + "','" + this.dt_Pack_date.Text + "','" + this.dt_exp_date.Text + "','" + this.Txt_purchase_rate.Text + "','" + this.Txt_mrp.Text + "','" + this.Txt_Description.Text + "'); ";
                        MySqlCommand cmd = new MySqlCommand(query, connection);

                        //Execute command
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show("Data entered successfully 👍", "message", MessageBoxButtons.OK);
                        clear();

                    }
                    else
                    {
                        MessageBox.Show("Please enter valid input ", "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

        }

        private void data_Grid_purchase_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void Main_form_Load(object sender, EventArgs e)
        {
            // purchase panel code 
            // bt_Pupdate hide code 
            bt_Pupdate.Visible = false;
            bt_Pdelete.Visible = false;
            // salse panel code 
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            connection = new MySqlConnection(connectionString);
            connection.Open();
            DataTable dt = new DataTable();
            string query = "SELECT product_name FROM medivision_app.purchase;";
            connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataAdapter mySqlData = new MySqlDataAdapter(query, connection);
            mySqlData.Fill(dt);
            cb_product_name.DataSource = dt;
            cb_product_name.DisplayMember = "product_name";
            connection.Close();
            data_grid_quantity.Visible = false;

            // bt_Supdate hide code
            bt_Supdate.Visible = false;
            bt_Delete_sales.Visible = false;

            // stock data grid population Code 
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            connection = new MySqlConnection(connectionString);
            connection.Open();
             dt = new DataTable();
             query = "SELECT product_id, product_name, Batch_no, Quantity, Pack_date, Exp_date,Purchase_rate, MRP ,Description FROM medivision_app.purchase;";
            connection = new MySqlConnection(connectionString);
            connection.Open();
             cmd = new MySqlCommand(query, connection);
             mySqlData = new MySqlDataAdapter(query, connection);
            mySqlData.Fill(dt);
            data_grid_stock.DataSource = dt;
            data_grid_stock.Columns[0].Visible = false;
            connection.Close();

            // code for data grid required  population code 
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            connection = new MySqlConnection(connectionString);
            connection.Open();
            dt = new DataTable();
            query = "SELECT * FROM medivision_app.requirements;";
            connection = new MySqlConnection(connectionString);
            connection.Open();
            cmd = new MySqlCommand(query, connection);
            mySqlData = new MySqlDataAdapter(query, connection);
            mySqlData.Fill(dt);
            data_grid_required.DataSource = dt;
            data_grid_required.Columns[0].Visible = false;
            connection.Close();
            // bt_update_req and bt_delete_req hide code
            bt_update_req.Visible = false;
            bt_delete_req.Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            connection = new MySqlConnection(connectionString);
            connection.Open();
            DataTable dt = new DataTable();
            string query = "SELECT product_id, product_name, Batch_no, Quantity, Pack_date, Exp_date,Purchase_rate, MRP ,Description FROM medivision_app.purchase Order by product_name ;";
            connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataAdapter mySqlData = new MySqlDataAdapter(query, connection);
            mySqlData.Fill(dt);
            data_Grid_purchase.DataSource = dt;
            data_Grid_purchase.Columns[0].Visible = false;
            connection.Close();
            Txt_product_name.Text = Txt_batch_no.Text = Txt_purchase_rate.Text = Txt_Description.Text = Txt_mrp.Text = "";
            bt_Pupdate.Visible = false;
            bt_save.Visible = Enabled;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            clear();

        }

        private void dt_sales_Click(object sender, EventArgs e)
        {
            panel_purchase.Visible = false;
            panel_sales.Visible = Enabled;
            panel1_stock.Visible = false;
        }

        private void bt_purchase_Click(object sender, EventArgs e)
        {
            panel_purchase.Visible = Enabled;
            panel_sales.Visible = false;
            panel1_stock.Visible = false;
            panel_Requirement.Visible = false;
        }

        private void bt_stock_Click(object sender, EventArgs e)
        {
            panel_purchase.Visible = false;
            panel_sales.Visible = false;
            panel1_stock.Visible = Enabled;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {

        }

        private void data_Grid_purchase_DoubleClick(object sender, EventArgs e)
        {
            if (data_Grid_purchase.CurrentRow.Index != -1)
            {
                Txt_product_name.Text = data_Grid_purchase.CurrentRow.Cells[1].Value.ToString();
                Txt_batch_no.Text = data_Grid_purchase.CurrentRow.Cells[2].Value.ToString();
                Nud_quantity.Text = data_Grid_purchase.CurrentRow.Cells[3].Value.ToString();
                dt_Pack_date.Text = data_Grid_purchase.CurrentRow.Cells[4].Value.ToString();
                dt_exp_date.Text = data_Grid_purchase.CurrentRow.Cells[5].Value.ToString();
                Txt_purchase_rate.Text = data_Grid_purchase.CurrentRow.Cells[6].Value.ToString();
                Txt_mrp.Text = data_Grid_purchase.CurrentRow.Cells[7].Value.ToString();
                Txt_Description.Text = data_Grid_purchase.CurrentRow.Cells[8].Value.ToString();
                id = Convert.ToInt32(data_Grid_purchase.CurrentRow.Cells[0].Value.ToString());
                bt_Pupdate.Visible = true;
                bt_Pdelete.Visible = true;
                bt_save.Visible = false;

            }
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            string connectionString;
            connectionString = "Server=localhost;Uid=root;password=pratik@123;Database=medivision_app;Port=3306;";
            connection = new MySqlConnection(connectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand("Update_procedure", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_Product_id", id);
                cmd.Parameters.AddWithValue("_Product_name", Txt_product_name.Text);
                cmd.Parameters.AddWithValue("_Batch_no", Txt_batch_no.Text);
                cmd.Parameters.AddWithValue("_Quantity", Nud_quantity.Text);
                cmd.Parameters.AddWithValue("_pack_date", dt_Pack_date.Text);
                cmd.Parameters.AddWithValue("_Exp_date", dt_exp_date.Text);
                cmd.Parameters.AddWithValue("_Purchase_rate", Txt_purchase_rate.Text);
                cmd.Parameters.AddWithValue("_Mrp", Txt_mrp.Text);
                cmd.Parameters.AddWithValue("_Description", Txt_Description.Text);

                //Execute command
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Data Update successfully 👍", "message", MessageBoxButtons.OK);
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want Delete the Record ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    string connectionString;
                    connectionString = "Server=localhost;Uid=root;password=pratik@123;Database=medivision_app;Port=3306;";
                    connection = new MySqlConnection(connectionString);
                    connection.Open();

                    var query = "Delete from medivision_app.purchase Where product_id = '" + id + "' ";
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //Execute command
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Record Delete successfully ");
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }

            }
            else
            { 
               
            }
            
        }

        private void bt_add_sales_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString;
                connectionString = "Server=localhost;Uid=root;password=pratik@123;Database=medivision_app;Port=3306;";
                connection = new MySqlConnection(connectionString);
                connection.Open();
                // new code for quantity 
                DataTable dt = new DataTable();
                var query = "SELECT Quantity FROM purchase  WHERE product_name = '"+ cb_product_name.Text + "';";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataAdapter mySqlData = new MySqlDataAdapter(query, connection);
                mySqlData.Fill(dt);
                data_grid_quantity.DataSource = dt;
                int Quantity = Convert.ToInt32(data_grid_quantity.CurrentRow.Cells[0].Value.ToString());
                int req_quantity = Convert.ToInt32(nud_Squantity.Text);
                if (Quantity < req_quantity)
                {
                    MessageBox.Show("Out of stock ");
                }
                // code end 
                query = "INSERT INTO  Sales VALUES(DEFAULT,(SELECT product_ID FROM purchase WHERE product_name = '" + this.cb_product_name.Text + "'),'" + this.cb_product_name.Text + "','" + this.nud_Squantity.Text + "','" + this.dt_sold_date.Text + "');";

                cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                connection.Close();
                MessageBox.Show("Data entered successfully 👍", "message", MessageBoxButtons.OK);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

            // update table purchase quantity 
            try
            {
                string connectionString;
                connectionString = "Server=localhost;Uid=root;password=pratik@123;Database=medivision_app;Port=3306;";
                connection = new MySqlConnection(connectionString);

                connection.Open();
                var query = "UPDATE purchase  SET  quantity = quantity - '" + this.nud_Squantity.Text + "' WHERE product_id = ( SELECT product_ID FROM sales WHERE sale_product_name = '" + this.cb_product_name.Text + "' );";
                //query = "(SELECT product_ID FROM purchase WHERE product_name = '" + cb_product_name + "')";

                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                connection.Close();
                clear();
            }
            catch (Exception error1)
            {
                MessageBox.Show(error1.Message);
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            connection = new MySqlConnection(connectionString);
            connection.Open();
            DataTable dt = new DataTable();
            string query = "SELECT * FROM sales;";
            connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataAdapter mySqlData = new MySqlDataAdapter(query, connection);
            mySqlData.Fill(dt);// copied table into dt 
            data_grid_sales.DataSource = dt;
            //data_grid_sales.Columns[0].Visible = false;
            connection.Close();
            Txt_product_name.Text = Txt_batch_no.Text = Txt_purchase_rate.Text = Txt_Description.Text = Txt_mrp.Text = "";
            // code for hideing buttons 
            bt_Supdate.Visible = false;
            bt_Delete_sales.Visible = false;
        }

        private void cb_product_name_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            connection = new MySqlConnection(connectionString);
            connection.Open();
            DataTable dt = new DataTable();
            string query = "SELECT product_name FROM medivision_app.purchase;";
            connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataAdapter mySqlData = new MySqlDataAdapter(query, connection);
            mySqlData.Fill(dt);
            cb_product_name.DataSource = dt;
            cb_product_name.DisplayMember = "product_name";
            connection.Close();
            bt_Supdate.Visible = true;
            cb_product_name.Text = "";
            nud_Squantity.Text = "0";
        }

        private void button7_Click(object sender, EventArgs e)
        {

            try
            {
                string connectionString;
                connectionString = "Server=localhost;Uid=root;password=pratik@123;Database=medivision_app;Port=3306;";
                connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("sales_quanity_proceduares", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_quantity", this.nud_Squantity.Text);
                cmd.Parameters.AddWithValue("_product_name", this.cb_product_name.Text);
                cmd.Parameters.AddWithValue("_Sold_date", this.dt_sold_date.Text);

                //Execute command
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Data Update successfully 👍", "message", MessageBoxButtons.OK);
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                string connectionString;
                connectionString = "Server=localhost;Uid=root;password=pratik@123;Database=medivision_app;Port=3306;";
                connection = new MySqlConnection(connectionString);

                connection.Open();
                var query = "UPDATE purchase  SET  quantity = quantity - '" + this.nud_Squantity.Text + "' WHERE product_id = ( SELECT product_ID FROM sales WHERE sale_product_name = '" + this.cb_product_name.Text + "' );";
                //query = "(SELECT product_ID FROM purchase WHERE product_name = '" + cb_product_name + "')";

                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                connection.Close();
                clear();
            }
            catch (Exception error1)
            {
                MessageBox.Show(error1.Message);
            }

        }

        private void data_grid_sales_DoubleClick(object sender, EventArgs e)
        {
            cb_product_name.Text = data_grid_sales.CurrentRow.Cells[2].Value.ToString();
            nud_Squantity.Text = data_grid_sales.CurrentRow.Cells[3].Value.ToString();
            dt_sold_date.Text = data_grid_sales.CurrentRow.Cells[4].Value.ToString();
            sale_id = Convert.ToInt32(data_grid_sales.CurrentRow.Cells[0].Value.ToString());
            bt_Supdate.Visible = true;
            bt_Delete_sales.Visible = true;

           
        }

        private void button1_Click_4(object sender, EventArgs e)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            connection = new MySqlConnection(connectionString);
            connection.Open();
            DataTable dt = new DataTable();
            string query = "SELECT product_id, product_name, Batch_no, Quantity, Pack_date, Exp_date,Purchase_rate, MRP ,Description FROM medivision_app.purchase;";
            connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataAdapter mySqlData = new MySqlDataAdapter(query, connection);
            mySqlData.Fill(dt);
            data_grid_stock.DataSource = dt;
            data_grid_stock.Columns[0].Visible = false;
            connection.Close();
        }

        private void Txt_search_TextChanged(object sender, EventArgs e)
        {
            string connectionString;
            connectionString = "Server=localhost;Uid=root;password=pratik@123;Database=medivision_app;Port=3306;";
            connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlDataAdapter mySqlData = new MySqlDataAdapter("Search_procedure", connection);
            mySqlData.SelectCommand.CommandType = CommandType.StoredProcedure;
            mySqlData.SelectCommand.Parameters.AddWithValue("_search", this.Txt_search.Text);
            DataTable dt = new DataTable();
            mySqlData.Fill(dt);
            data_grid_stock.DataSource = dt;
            data_grid_stock.Columns[0].Visible = false;

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string connectionString;
            connectionString = "Server=localhost;Uid=root;password=pratik@123;Database=medivision_app;Port=3306;";
            connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlDataAdapter mySqlData = new MySqlDataAdapter("Search_procedure", connection);
            mySqlData.SelectCommand.CommandType= CommandType.StoredProcedure;
            mySqlData.SelectCommand.Parameters.AddWithValue("_search", this.Txt_search.Text);
            DataTable dt = new DataTable();
            mySqlData.Fill(dt);
            data_grid_stock.DataSource = dt;
            data_grid_stock.Columns[0].Visible = false;
        }

        private void bt_requirement_Click(object sender, EventArgs e)
        {
            panel_purchase.Visible = false;
            panel_sales.Visible = false;
            panel1_stock.Visible = false;
            panel_Requirement.Visible = Enabled;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want Delete the Record ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    string connectionString;
                    connectionString = "Server=localhost;Uid=root;password=pratik@123;Database=medivision_app;Port=3306;";
                    connection = new MySqlConnection(connectionString);
                    connection.Open();

                    var query = "Delete from medivision_app.sales Where sale_id = '" + sale_id + "' ";
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //Execute command
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Record Delete successfully ");
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }

            }
            else
            {

            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want Delete the Record ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    string connectionString;
                    connectionString = "Server=localhost;Uid=root;password=pratik@123;Database=medivision_app;Port=3306;";
                    connection = new MySqlConnection(connectionString);
                    connection.Open();

                    var query = "Delete from medivision_app.requirements Where Requriment_id = '" + required_id + "' ";
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //Execute command
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Record Delete successfully ");
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }

            }
            else
            {

            }
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            try
            {
                string connectionString;
                connectionString = "Server=localhost;Uid=root;password=pratik@123;Database=medivision_app;Port=3306;";
                connection = new MySqlConnection(connectionString);
                connection.Open();

                    var query = "INSERT INTO requirements (Product_id,Product_name,in_stock_quantity,purchase_rate,req_MRP) SELECT product_id , product_name , Quantity,Purchase_rate,MRP FROM purchase WHERE Quantity <= 1;";
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //Execute command
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Data entered successfully 👍", "message", MessageBoxButtons.OK);
                    clear();

            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString;
                connectionString = "Server=localhost;Uid=root;password=pratik@123;Database=medivision_app;Port=3306;";
                connection = new MySqlConnection(connectionString);
                connection.Open();

                if (Txt_product_name_req.Text != "" && nud_qty_req.Text != "" && date_req_date.Text != "" && Txt_purchase_rate_req.Text != "" && Txt_mrp_req.Text != "" && Txt_description_req.Text != "" )
                {
                    var var_product_name = this.Txt_product_name_req.Text;
                    var var_date = this.date_req_date.Text;
                    int quantity_req = Convert.ToInt32(this.nud_qty_req.Text);
                    int purchase_rate = Convert.ToInt32(this.Txt_purchase_rate_req.Text);
                    int mrp_req = Convert.ToInt32(this.Txt_mrp_req.Text);
                    var description_req = this.Txt_description_req.Text;
                    var query = "INSERT INTO requirements(product_id, product_name, req_date, Required_quantity, purchase_rate, req_MRP, req_description)" +
"                   VALUES((SELECT Product_id FROM purchase WHERE Product_name = '" + var_product_name + "'),'" + var_product_name + "','" + var_date + "', '" + quantity_req + "','" + purchase_rate + "','" + mrp_req + "','" + description_req + "');";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
        
                    //Execute command
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Data entered successfully 👍", "message", MessageBoxButtons.OK);
                    clear();

                }
                else
                {
                    MessageBox.Show("Please enter valid input ", "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Txt_product_name_req_TextChanged(object sender, EventArgs e)
        {

        }

        private void bt_refresh_req_Click(object sender, EventArgs e)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            connection = new MySqlConnection(connectionString);
            connection.Open();
            DataTable dt = new DataTable();
            string query = "SELECT * FROM medivision_app.requirements;";
            connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataAdapter mySqlData = new MySqlDataAdapter(query, connection);
            mySqlData.Fill(dt);
            data_grid_required.DataSource = dt;
            data_grid_required.Columns[0].Visible = true;
            connection.Close();
            // code to hide buttons 
            bt_update_req.Visible = false;
            bt_delete_req.Visible = false;
        }

        private void data_grid_required_DoubleClick(object sender, EventArgs e)
        {

            Txt_product_name_req.Text = data_grid_required.CurrentRow.Cells[2].Value.ToString();
            date_req_date.Text = data_grid_required.CurrentRow.Cells[8].Value.ToString();
            nud_qty_req.Text = data_grid_required.CurrentRow.Cells[4].Value.ToString();
            Txt_purchase_rate_req.Text = data_grid_required.CurrentRow.Cells[5].Value.ToString();
            Txt_mrp_req.Text = data_grid_required.CurrentRow.Cells[6].Value.ToString();
            Txt_description_req.Text = data_grid_required.CurrentRow.Cells[7].Value.ToString();
            bt_update_req.Visible = true;
            bt_delete_req.Visible = true;
            required_id = Convert.ToInt32(data_grid_required.CurrentRow.Cells[0].Value.ToString());

        }

        private void data_grid_required_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bt_update_req_Click(object sender, EventArgs e)
        {
            try
            {
                // copying data from txt_box to variables 
                var var_product_name = this.Txt_product_name_req.Text;
                var var_date = this.date_req_date.Text;
                int quantity_req = Convert.ToInt32(this.nud_qty_req.Text);
                int purchase_rate = Convert.ToInt32(this.Txt_purchase_rate_req.Text);
                int mrp_req = Convert.ToInt32(this.Txt_mrp_req.Text);
                var description_req = this.Txt_description_req.Text;

                // conect with database 
                string connectionString;
                connectionString = "Server=localhost;Uid=root;password=pratik@123;Database=medivision_app;Port=3306;";
                connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlDataAdapter mySqlData = new MySqlDataAdapter("req_update_procedure", connection);
                mySqlData.SelectCommand.CommandType = CommandType.StoredProcedure;
                mySqlData.SelectCommand.Parameters.AddWithValue("_product_name", var_product_name);
                mySqlData.SelectCommand.Parameters.AddWithValue("_required_qty", quantity_req);
                mySqlData.SelectCommand.Parameters.AddWithValue("_req_date", var_date);
                mySqlData.SelectCommand.Parameters.AddWithValue("_purchase_rate", purchase_rate);
                mySqlData.SelectCommand.Parameters.AddWithValue("_Mpr_req", mrp_req);
                mySqlData.SelectCommand.Parameters.AddWithValue("_description_req", description_req);
                mySqlData.SelectCommand.Parameters.AddWithValue("_id", required_id);
                DataTable dt = new DataTable();
                mySqlData.Fill(dt);
                data_grid_required.DataSource = dt;
                MessageBox.Show("Data updated Successfully 👍 ");
               
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);

            }

        }

        private void button5_Click_2(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Are you sure you want Delete the Record ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
                    connection = new MySqlConnection(connectionString);
                    connection.Open();
                    string query = "TRUNCATE TABLE  medivision_app.requirements;";
                    connection = new MySqlConnection(connectionString);
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception Ep)
                {
                    MessageBox.Show(Ep.Message);
                }

            }
            else
            {

            }
        }
    }
}  
