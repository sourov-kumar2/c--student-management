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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\souro\OneDrive\Documents\Collagedb.mdf;Integrated Security=True;Connect Timeout=30;");

        private void Dashboard_Load(object sender, EventArgs e)
        {
            conn.Open();

            SqlCommand cmdStudents = new SqlCommand("SELECT COUNT(*) FROM StudentTbl", conn);
            int studentCount = (int)cmdStudents.ExecuteScalar();
            stdLbl.Text = studentCount.ToString();

            SqlCommand cmdDepartments = new SqlCommand("SELECT COUNT(*) FROM DepartmentTb", conn);
            int departmentCount = (int)cmdDepartments.ExecuteScalar();
            DepLbl.Text = departmentCount.ToString();

            SqlCommand cmdTeachers = new SqlCommand("SELECT COUNT(*) FROM TeacherTbl", conn);
            int teacherCount = (int)cmdTeachers.ExecuteScalar();
            TLbl.Text = teacherCount.ToString();

            SqlCommand cmdFeesCount = new SqlCommand("SELECT COUNT(*) FROM FeesTbl", conn);
            int feesCount = (int)cmdFeesCount.ExecuteScalar();

            int totalFees = feesCount * 21000;
            FeesLbl.Text = "TK-" + totalFees.ToString();

            conn.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2GradientCircleButton1_Click(object sender, EventArgs e)
        {
            MainForm guna = new MainForm();
            guna.Show();
            this.Hide();
        }

        private void guna2GradientButton6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            Users users = new Users();
            users.Show();
            this.Hide();
        }
    }
}
