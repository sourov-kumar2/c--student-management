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

namespace polytechnicManagementSystem
{
    public partial class Department : Form
    {
        public Department()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\souro\OneDrive\Documents\Collagedb.mdf;Integrated Security=True;Connect Timeout=30;");

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void populate()
        {
            conn.Open();
            string query = "select * from DepartmentTb";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DepartmentDGV.DataSource = ds.Tables[0];
            conn.Close();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (DepNameTbl.Text == "" || DepDescTbl.Text == "" || DepDurationTbl.Text == "")
                {
                    MessageBox.Show("Fill all the Information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    conn.Open();
                    string query = "INSERT INTO DepartmentTb (DepName, DepDesc, DepDuration) VALUES (@DepName, @DepDesc, @DepDuration)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@DepName", DepNameTbl.Text);
                    cmd.Parameters.AddWithValue("@DepDesc", DepDescTbl.Text);
                    cmd.Parameters.AddWithValue("@DepDuration", DepDurationTbl.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Department Added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    populate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went Wrong: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Department_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (DepNameTbl.Text == "")
                {
                    MessageBox.Show("Select a Department Name !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    conn.Open();
                    string query = "delete from DepartmentTb where DepName = @DepName";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@DepName", DepNameTbl.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Department Deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    populate();
                }
            }
            catch
            {
                MessageBox.Show("Something Went Wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DepartmentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DepNameTbl.Text = DepartmentDGV.SelectedRows[0].Cells[0].Value.ToString();
            DepDescTbl.Text = DepartmentDGV.SelectedRows[0].Cells[1].Value.ToString();
            DepDurationTbl.Text = DepartmentDGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (DepNameTbl.Text == "" || DepDescTbl.Text == "" || DepDurationTbl.Text == "")
                {
                    MessageBox.Show("Fill all the Information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    conn.Open();
                    string query = "UPDATE DepartmentTb SET DepDuration=@DepDuration, DepDesc=@DepDesc WHERE DepName=@DepName";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@DepName", DepNameTbl.Text);
                    cmd.Parameters.AddWithValue("@DepDesc", DepDescTbl.Text);
                    cmd.Parameters.AddWithValue("@DepDuration", DepDurationTbl.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Department Updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    populate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went Wrong: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }
    }
}
