using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace GameStart
{
    public partial class LoginForm : Form
    {
        public static string constring = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + System.IO.Directory.GetCurrentDirectory() + @"\GameStartDB.accdb";
        Person[] p = new Person[100];
        int i = 0;
        int size;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = true;
                for (int j = 0; j < size; ++j)
                {
                    if (p[j].Username == txtUsername.Text && p[j].Password == txtPassword.Text)
                    {
                        if (p[j] is User)
                        {
                            UserForm uf = new UserForm(p[j] as User);
                            uf.Show();
                            this.Close();
                        }
                        else if (p[j] is Admin)
                        {
                            AdminForm af = new AdminForm(p[j] as Admin);
                            af.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Somehow, you are neither a user not an admin." +
                        "Someone screwed up somewhere and it's not me.");
                        }
                        flag = false;
                    }
                }
                if(flag)
                {
                    txtUsername.Text = null;
                    txtPassword.Text = null;
                    MessageBox.Show("Username or password is incorrect.");
                    txtUsername.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtPassword.Focus();

                e.SuppressKeyPress = true;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            string cmdText = "SELECT * FROM Users";
            OleDbConnection con = new OleDbConnection(constring);
            con.Open();
            OleDbCommand cmd = new OleDbCommand(cmdText, con);
            OleDbDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                if (dr.GetString(5) == "user")
                {
                    p[i] = new User(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4));
                }
                else if (dr.GetString(5) == "admin")
                {
                    p[i] = new Admin(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4));
                }
                else
                {
                    MessageBox.Show("Somehow, you are neither a user not an admin." +
                        "Someone screwed up somewhere and it's not me.");
                }
                ++i;
            }
            size = i;
            con.Close();
        }
    }
}
