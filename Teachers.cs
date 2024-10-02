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
    public partial class Teachers : Form
    {
        public Teachers()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\souro\OneDrive\Documents\Collagedb.mdf;Integrated Security=True;Connect Timeout=30;");

        private void populate()
        {
            conn.Open();
            string query = "select * from TeacherTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TeacherDGV.DataSource = ds.Tables[0];
            conn.Close();
        }

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
            Tdepartment.ValueMember = "DepName";
            Tdepartment.DataSource = dt;
            conn.Close();
        }

        private void Teachers_Load(object sender, EventArgs e)
        {
            populate();
            Filldepartment();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tid.Text == "" || Tname.Text == "" || Tgender.Text == "" || Tdob.Text == "" || Tdepartment.Text == "" || Tphone.Text == "" || Taddress.Text == "")
                {
                    MessageBox.Show("Fill all the Information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    conn.Open();
                    string query = "INSERT INTO TeacherTbl (TeacherId, TeacherName, TeacherGender, TeacherDOB, TeacherPhone, TeacherDep, TeacherAdd) VALUES (@Tid, @Tname, @Tgender, @Tdob, @Tphone, @Tdepartment, @Taddress)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Tid", Tid.Text);
                    cmd.Parameters.AddWithValue("@Tname", Tname.Text);
                    cmd.Parameters.AddWithValue("@Tgender", Tgender.Text);
                    cmd.Parameters.AddWithValue("@Tdob", Tdob.Text);
                    cmd.Parameters.AddWithValue("@Tphone", Tphone.Text);
                    cmd.Parameters.AddWithValue("@Tdepartment", Tdepartment.Text);
                    cmd.Parameters.AddWithValue("@Taddress", Taddress.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Teacher Added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tid.Text == "")
                {
                    MessageBox.Show("Select a Teacher Id !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    conn.Open();
                    string query = "delete from TeacherTbl where TeacherId = @TeacherId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TeacherId", Tid.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Teacher Deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    populate();
                }
            }
            catch
            {
                MessageBox.Show("Something Went Wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TeacherDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Tid.Text = TeacherDGV.SelectedRows[0].Cells[0].Value.ToString();
            Tname.Text = TeacherDGV.SelectedRows[0].Cells[1].Value.ToString();
            Tgender.Text = TeacherDGV.SelectedRows[0].Cells[2].Value.ToString();
            Tdob.Text = TeacherDGV.SelectedRows[0].Cells[3].Value.ToString();
            Tphone.Text = TeacherDGV.SelectedRows[0].Cells[4].Value.ToString();
            Tdepartment.Text = TeacherDGV.SelectedRows[0].Cells[5].Value.ToString();
            Taddress.Text = TeacherDGV.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tid.Text == "" || Tname.Text == "" || Tgender.Text == "" || Tdob.Text == "" || Tdepartment.Text == "" || Tphone.Text == "" || Taddress.Text == "")
                {
                    MessageBox.Show("Fill all the Information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    conn.Open();
                    string query = "UPDATE TeacherTbl SET TeacherName=@TeacherName, TeacherGender=@TeacherGender,TeacherDOB=@TeacherDOB,TeacherPhone=@TeacherPhone,TeacherDep=@TeacherDep,TeacherAdd=@TeacherAdd WHERE TeacherId=@TeacherId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TeacherName", Tname.Text);
                    cmd.Parameters.AddWithValue("@TeacherGender", Tgender.Text);
                    cmd.Parameters.AddWithValue("@TeacherId", Tid.Text);
                    cmd.Parameters.AddWithValue("@TeacherDOB", Tdob.Text);
                    cmd.Parameters.AddWithValue("@TeacherDep", Tdepartment.Text);
                    cmd.Parameters.AddWithValue("@TeacherAdd", Taddress.Text);
                    cmd.Parameters.AddWithValue("@TeacherPhone", Tphone.Text);

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

        private void Tdob_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
