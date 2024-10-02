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
using System.Net;
using System.Security.Cryptography;

namespace polytechnicManagementSystem
{
    public partial class Student : Form
    {
        public Student()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\souro\OneDrive\Documents\Collagedb.mdf;Integrated Security=True;Connect Timeout=30;");

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Filldepartment()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select DepName from DepartmentTb", conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("DepName", typeof(string));
            dt.Load(rdr);
            Sdepartment.ValueMember = "DepName";
            Sdepartment.DataSource = dt;
            conn.Close();
        }

        private void populate()
        {
            conn.Open();
            string query = "select * from StudentTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            StudentDGV.DataSource = ds.Tables[0];
            conn.Close();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (SId.Text == "" || Sname.Text == "" || Sgender.Text == "" || Sdob.Text == "" || Sdepartment.Text == "")
                {
                    MessageBox.Show("Fill all the Information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    conn.Open();
                    string query = "INSERT INTO StudentTbl (StdId, StdName, StdGender, StdDOB, StdDep, StdFees) VALUES (@StdId, @StdName, @StdGender, @StdDOB, @StdDep, @StdFees)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StdId", SId.Text);
                    cmd.Parameters.AddWithValue("@StdName", Sname.Text);
                    cmd.Parameters.AddWithValue("@StdGender", Sgender.Text);
                    cmd.Parameters.AddWithValue("@StdDOB", Sdob.Text);
                    cmd.Parameters.AddWithValue("@StdDep", Sdepartment.Text);
                    cmd.Parameters.AddWithValue("@StdFees", 0);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    populate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something Went Wrong: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }

        private void Student_Load(object sender, EventArgs e)
        {
            populate();
            Filldepartment();
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (SId.Text == "")
                {
                    MessageBox.Show("Select a Student Id !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    conn.Open();
                    string query = "delete from StudentTbl where StdId = @StdId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StdId", SId.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    populate();
                }
            }
            catch
            {
                MessageBox.Show("Something Went Wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StudentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SId.Text = StudentDGV.SelectedRows[0].Cells[0].Value.ToString();
            Sname.Text = StudentDGV.SelectedRows[0].Cells[1].Value.ToString();
            Sgender.Text = StudentDGV.SelectedRows[0].Cells[2].Value.ToString();
            Sdob.Text = StudentDGV.SelectedRows[0].Cells[3].Value.ToString();
            Sdepartment.Text = StudentDGV.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (SId.Text == "" || Sname.Text == "" || Sgender.Text == "" || Sdob.Text == "" || Sdepartment.Text == "")
                {
                    MessageBox.Show("Fill all the Information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    conn.Open();
                    string query = "UPDATE StudentTbl SET StdName=@StdName, StdGender=@StdGender, StdDOB=@StdDOB, StdDep=@StdDep, StdFees=@StdFees WHERE StdId=@StdId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StdId", SId.Text);
                    cmd.Parameters.AddWithValue("@StdName", Sname.Text);
                    cmd.Parameters.AddWithValue("@StdGender", Sgender.Text);
                    cmd.Parameters.AddWithValue("@StdDOB", Sdob.Text);
                    cmd.Parameters.AddWithValue("@StdDep", Sdepartment.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    populate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went Wrong: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
