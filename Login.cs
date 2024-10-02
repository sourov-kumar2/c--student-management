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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace polytechnicManagementSystem
{
    public partial class Login : Form
    {
        public Login()
        {
            // Showpass.CheckedChanged += Showpass_CheckedChanged;
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\souro\OneDrive\Documents\Collagedb.mdf;Integrated Security=True;Connect Timeout=30;");

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            conn.Open();

            string username = Uname.Text;
            string password = Upass.Text;

            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from UserTbl where UserName = '" + username + "' and Password = '" + password + "'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            conn.Close();

            if (dt.Rows[0][0].ToString() == "1")
            {
                dashboard.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong username or password!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Uname.Text = "";
                Upass.Text = "";
            }
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Showpass_CheckedChanged(object sender, EventArgs e)
        {
            if (Showpass.Checked)
            {
                Upass.PasswordChar = '\0';
            }
            else
            {
                Upass.PasswordChar = '*';
            }
        }

        private void BtnClr_Click(object sender, EventArgs e)
        {
            Uname.Text = "";
            Upass.Text = "";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
