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
using Guna.UI2.WinForms;

namespace polytechnicManagementSystem
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\souro\OneDrive\Documents\Collagedb.mdf;Integrated Security=True;Connect Timeout=30;");

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Users_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void populate()
        {
            conn.Open();
            string query = "select * from UserTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UserDGV.DataSource = ds.Tables[0];
            conn.Close();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (UidTb.Text == "" || UnameTb.Text == "" || UserPassTb.Text == "")
                {
                    MessageBox.Show("Fill all the Information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into UserTbl values(" + UidTb.Text + ",'" + UnameTb.Text + "','" + UserPassTb.Text + "')", conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    populate();
                }
            }
            catch
            {
                MessageBox.Show("Something Went Wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UidTb.Text = UserDGV.SelectedRows[0].Cells[0].Value.ToString();
            UnameTb.Text = UserDGV.SelectedRows[0].Cells[1].Value.ToString();
            UserPassTb.Text = UserDGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (UidTb.Text == "")
                {
                    MessageBox.Show("Select a User Id !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    conn.Open();
                    string query = "delete from UserTbl where UserId = @UserId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserId", UidTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    populate();
                }
            }
            catch
            {
                MessageBox.Show("Something Went Wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (UidTb.Text == "" || UnameTb.Text == "" || UserPassTb.Text == "")
                {
                    MessageBox.Show("Fill all the Information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    conn.Open();
                    string query = "UPDATE UserTbl SET UserName=@UserName, Password=@Password WHERE UserId=@UserId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserName", UnameTb.Text);
                    cmd.Parameters.AddWithValue("@Password", UserPassTb.Text);
                    cmd.Parameters.AddWithValue("@UserId", UidTb.Text);
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

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }
    }
}