using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameStart
{
    public partial class CustomerForm : Form
    {
        Customer current;
        
        public CustomerForm(Customer x)
        {
            InitializeComponent();
            current = x;
            txtName.Text = current.Name;
            txtEmail.Text = current.Email;
            txtGame.Text = current.Game;
            txtDue.Text = current.Due.ToString();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            string cmdText = "SELECT * FROM Customers WHERE FullName = @fn AND Email = @e";
            OleDbConnection con = new OleDbConnection(LoginForm.constring);
            con.Open();
            OleDbCommand cmd = new OleDbCommand(cmdText, con);
            cmd.Parameters.AddWithValue("@fn", current.Name);
            cmd.Parameters.AddWithValue("@e", current.Email);
            OleDbDataReader dr = cmd.ExecuteReader();
            if(dr.Read())
            {
                bool loop = true;
                while (loop)
                {
                    string ad = dr.GetString(3);
                    string ph = dr.GetString(4);
                    loop = dr.Read();
                    if(!loop)
                    {
                        txtAddress.Text = ad;
                        txtPhone.Text = ph;
                    }
                }
            }
            con.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtAddress.Text) || String.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBox.Show("Please ensure all fields are filled out!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                current.Address = txtAddress.Text;
                current.Phone = txtPhone.Text;
                string cmdText = "INSERT INTO Customers (FullName, Email, Address, PhoneNo, [Order], Due)VALUES (@fn, @e, @a, @p, @o, @d)";
                OleDbConnection con = new OleDbConnection(LoginForm.constring);
                con.Open();
                OleDbCommand cmd = new OleDbCommand(cmdText, con);
                cmd.Parameters.AddWithValue("@fn", current.Name);
                cmd.Parameters.AddWithValue("@e", current.Email);
                cmd.Parameters.AddWithValue("@a", current.Address);
                cmd.Parameters.AddWithValue("@p", current.Phone);
                cmd.Parameters.AddWithValue("@o", current.Game);
                cmd.Parameters.AddWithValue("@d", current.Due);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Order made successfully!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                con.Close();
                UserForm uf = new UserForm(current);
                uf.Show();
                this.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            UserForm uf = new UserForm(current);
            uf.Show();
            this.Close();
        }
    }
}
