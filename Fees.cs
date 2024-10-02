using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace polytechnicManagementSystem
{
    public partial class Fees : Form
    {
        public Fees()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\souro\OneDrive\Documents\Collagedb.mdf;Integrated Security=True;Connect Timeout=30;");

        private void populate()
        {
            conn.Open();
            string query = "select * from FeesTbl ";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            FeesDGV.DataSource = ds.Tables[0];
            conn.Close();
        }

        private void FillIDt()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select StdId from StudentTbl", conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("StdId", typeof(string));
            dt.Load(rdr);
            Sidp.ValueMember = "StdId";
            Sidp.DataSource = dt;
            conn.Close();
        }

        private void Fees_Load(object sender, EventArgs e)
        {
            FillIDt();
            populate();
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }

        private void updateFees()
        {
            conn.Open();
            string query = "UPDATE StudentTbl SET StdFees=@StdFees WHERE StdId=@StdId";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@StdId", Sidp.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@StdFees", Samountp.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void Sidp_SelectionChangeCommitted(object sender, EventArgs e)
        {
            conn.Open();
            string query = "select * from StudentTbl where StdId=" + Sidp.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, conn);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Snamep.Text = dr["StdName"].ToString();
            }
            conn.Close();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Sidp.Text == "" || Snamep.Text == "" || SiNo.Text == "" || Speriod.Text == "" || Samountp.Text == "")
                {
                    MessageBox.Show("Fill all the Information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    conn.Open();
                    string query = "INSERT INTO FeesTbl (FeesNum, StdId, StdName, period, Amount) VALUES (@FeesNum, @StdId, @StdName, @period, @Amount)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FeesNum", SiNo.Text);
                    cmd.Parameters.AddWithValue("@StdName", Snamep.Text);
                    cmd.Parameters.AddWithValue("@StdId", Sidp.Text);
                    cmd.Parameters.AddWithValue("@period", Speriod.Text);
                    cmd.Parameters.AddWithValue("@Amount", Samountp.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Payment Added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    populate();
                    updateFees();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something Went Wrong: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (SiNo.Text == "")
                {
                    MessageBox.Show("Select a Serial Number !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    conn.Open();
                    string query = "delete from FeesTbl where FeesNum = @FeesNum";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FeesNum", SiNo.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Payment Deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    populate();
                }
            }
            catch
            {
                MessageBox.Show("Something Went Wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FeesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SiNo.Text = FeesDGV.SelectedRows[0].Cells[0].Value.ToString();
            Sidp.Text = FeesDGV.SelectedRows[0].Cells[1].Value.ToString();
            Snamep.Text = FeesDGV.SelectedRows[0].Cells[2].Value.ToString();
            Speriod.Text = FeesDGV.SelectedRows[0].Cells[3].Value.ToString();
            Samountp.Text = FeesDGV.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
