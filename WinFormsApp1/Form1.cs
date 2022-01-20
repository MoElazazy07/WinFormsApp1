using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-TTQ5H2M;Initial Catalog=Customer;User ID=sa;Password=12345");
        SqlDataAdapter adapt;
        SqlCommand cmd;
        int ID = 0;
        public Form1()
        {
            InitializeComponent();
            DisplayData();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {

                cmd = new SqlCommand("insert into TblStock(StockCode,LastPrice,Name) values(@StockCode,@LastPrice,@Name)", con);
                con.Open();
                //cmd.CommandText = "INSERT INTO TblStock (StockCode, LastPrice, Name) values('" + textBox1.Text.Trim() + "','" + textBox2.Text.Trim() + "','" + textBox3.Text.Trim() + "')";
                cmd.Parameters.AddWithValue("@StockCode", textBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@LastPrice", textBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@Name", textBox3.Text.Trim());
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                MessageBox.Show("Record inserted successfully!");
                DisplayData();
            }
            catch (Exception )
            {
                MessageBox.Show("Error");
            }
        }


        private void DisplayData()
        {
            try
            {
                con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from TblStock", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            }
            catch (Exception )
            {
                MessageBox.Show("Error");
            }
        }

        private void ClearData()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            ID = 0;
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                cmd = new SqlCommand("update TblStock set StockCode=@StockCode,LastPrice=@LastPrice,Name=@Name where StockId=@StockId", con);
                con.Open();
                cmd.Parameters.AddWithValue("@StockId", ID);
                cmd.Parameters.AddWithValue("@StockCode", textBox1.Text);
                cmd.Parameters.AddWithValue("@LastPrice", textBox2.Text);
                cmd.Parameters.AddWithValue("@Name", textBox3.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated Successfully");
                con.Close();
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Update");
            }
            }
            catch (Exception )
            {
                MessageBox.Show("Error");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (ID != 0)
            {
                cmd = new SqlCommand("delete TblStock where StockId=@StockId", con);
                con.Open();
                cmd.Parameters.AddWithValue("@StockId", ID);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully!");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
            }
            catch (Exception )
            {
                MessageBox.Show("Error");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dataGridView1.SelectedRows[0];
            if (row.Cells[0].Value != null && row.Cells[1].Value != null && row.Cells[2].Value != null)
            {
                ID= Convert.ToInt32(row.Cells[0].Value.ToString());
                textBox1.Text = row.Cells[1].Value.ToString();
                textBox2.Text = row.Cells[2].Value.ToString();
                textBox3.Text = row.Cells[3].Value.ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
