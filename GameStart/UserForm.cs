using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.OleDb;

namespace GameStart
{
    public partial class UserForm : Form
    {
        User current;
        Game[] g = new Game[100];
        int i = 0;
        int size;
        int selection = -1;
        string ogu, ogp;
        public UserForm(User x)
        {
            InitializeComponent();
            current = x;
            txtUser.Text = current.Username;
            txtPass.Text = current.Password;
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            try
            {
                // TODO: This line of code loads data into the 'gameStartDBDataSet.Customers' table. You can move, or remove it, as needed.
                this.customersTableAdapter.Fill(this.gameStartDBDataSet.Customers);
                var query = from x in this.gameStartDBDataSet.Customers
                            where x.FullName.ToLower().Contains(current.Name.ToLower()) || x.Email.ToLower().Contains(current.Email.ToLower())
                            orderby x.OrderID
                            select x;
                customersBindingSource.DataSource = query.ToList();
                // TODO: This line of code loads data into the 'gameStartDBDataSet.Games' table. You can move, or remove it, as needed.
                this.gamesTableAdapter.Fill(this.gameStartDBDataSet.Games);
                this.gamesTableAdapter.SortByID(this.gameStartDBDataSet.Games);
                string cmdText = "SELECT * FROM Games";
                OleDbConnection con = new OleDbConnection(LoginForm.constring);
                con.Open();
                OleDbCommand cmd = new OleDbCommand(cmdText, con);
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    g[i] = new Game(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetDecimal(3), dr.GetInt32(4));
                    ++i;
                }
                size = i;
                con.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm lf = new LoginForm();
            lf.Show();
            this.Close();
        }

        private void txtID_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if(e.KeyCode == Keys.Enter)
                {
                    bool flag = true;
                    for(int j = 0; j < size; ++j)
                    {
                        if(g[j].ID == Convert.ToInt32(txtID.Text))
                        {
                            txtTitle.Text = g[j].Title;
                            txtGenre.Text = g[j].Genre;
                            txtPrice.Text = g[j].Price.ToString();
                            txtRating.Text = g[j].Rating.ToString();
                            flag = false;
                            selection = j;
                        }
                    }
                    if(flag)
                    {
                        selection = -1;
                        txtTitle.Text = null;
                        txtGenre.Text = null;
                        txtPrice.Text = null;
                        txtRating.Text = null;
                        MessageBox.Show("This Game ID does not exist. Please check Collection for " +
                                "the correct Game ID.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    e.SuppressKeyPress = true;
                }
            }
            catch(Exception ex)
            {
                selection = -1;
                txtTitle.Text = null;
                txtGenre.Text = null;
                txtPrice.Text = null;
                txtRating.Text = null;
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtID.Text))
                MessageBox.Show("Please enter the Game ID.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if(selection == -1)
                MessageBox.Show("Selected Game ID is not valid.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                Customer buyer = new Customer(current.ID, current.Name, current.Email, current.Username, current.Password, g[selection]);
                CustomerForm cf = new CustomerForm(buyer);
                cf.Show();
                this.Close();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (txtPass.PasswordChar == (char)0)
                txtPass.PasswordChar = '*';
            else
                txtPass.PasswordChar = (char)0;
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            ogu = txtUser.Text;
            ogp = txtPass.Text;
            txtUser.Enabled = true;
            txtPass.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtUser.Text = ogu;
            txtPass.Text = ogp;
            txtUser.Enabled = false;
            txtPass.Enabled = false;
        }

        private void btnSurprise_Click(object sender, EventArgs e)
        {
            Unexpected u = new Unexpected();
            u.ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                current.Username = txtUser.Text;
                current.Password = txtPass.Text;
                string cmdText = "UPDATE Users SET Username = @uname, [Password] = @pword Where ID = @id";
                OleDbConnection con = new OleDbConnection(LoginForm.constring);
                con.Open();
                OleDbCommand cmd = new OleDbCommand(cmdText, con);
                cmd.Parameters.AddWithValue("@uname", current.Username);
                cmd.Parameters.AddWithValue("@pword", current.Password);
                cmd.Parameters.AddWithValue("@id", current.ID);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data updated successfully!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                con.Close();
                txtUser.Enabled = false;
                txtPass.Enabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
