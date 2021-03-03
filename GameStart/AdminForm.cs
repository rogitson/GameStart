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
    public partial class AdminForm : Form
    {
        Admin current;
        public AdminForm(Admin x)
        {
            InitializeComponent();
            current = x;
            lblCrnt.Text = "Current User: " + current.Username;
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'gameStartDBDataSet.Customers' table. You can move, or remove it, as needed.
            this.customersTableAdapter.Fill(this.gameStartDBDataSet.Customers);
            try
            {
                // TODO: This line of code loads data into the 'gameStartDBDataSet.Games' table. You can move, or remove it, as needed.
                this.gamesTableAdapter.Fill(this.gameStartDBDataSet.Games);
                gamesBindingSource.DataSource = this.gameStartDBDataSet.Games;
                // TODO: This line of code loads data into the 'gameStartDBDataSet.Users' table. You can move, or remove it, as needed.
                this.usersTableAdapter.Fill(this.gameStartDBDataSet.Users);
                usersBindingSource.DataSource = this.gameStartDBDataSet.Users;

                this.usersTableAdapter.SortByID(this.gameStartDBDataSet.Users);
                this.gamesTableAdapter.SortByID(this.gameStartDBDataSet.Games);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(txtSearch2.Text))
                {
                    this.gamesTableAdapter.Fill(this.gameStartDBDataSet.Games);
                    gamesBindingSource.DataSource = this.gameStartDBDataSet.Games;
                    this.gamesTableAdapter.SortByID(this.gameStartDBDataSet.Games);
                }
                else
                {
                    var query = from x in this.gameStartDBDataSet.Games
                                where x.Title.ToLower().Contains(txtSearch2.Text.ToLower()) || x.Genre.ToLower().Contains(txtSearch2.Text.ToLower())
                                orderby x.ID
                                select x;
                    gamesBindingSource.DataSource = query.ToList();
                }
                e.Handled = true;
            }
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure you wish to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    gamesBindingSource.RemoveCurrent();
                    gamesBindingSource.EndEdit();
                    gamesTableAdapter.Update(this.gameStartDBDataSet.Games);
                    panelGames.Enabled = false;
                }
            }
            if (e.KeyCode == Keys.S && e.Control)
            {
                try
                {
                    gamesBindingSource.EndEdit();
                    gamesTableAdapter.Update(this.gameStartDBDataSet.Games);
                    panelGames.Enabled = false;
                    MessageBox.Show("Saved successfully!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBrowse2_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog img = new OpenFileDialog() { Filter = "Image Files|*.jpg;*.jpeg;*.png;|JPEG|*.jpg;*.jpeg|PNG|*.png", ValidateNames = true, Multiselect = false })
                {
                    if (img.ShowDialog() == DialogResult.OK)
                        pictureBox1.Image = Image.FromFile(img.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInsert2_Click(object sender, EventArgs e)
        {
            try
            {
                panelGames.Enabled = true;
                txtTitle.Focus();
                this.gameStartDBDataSet.Games.AddGamesRow(this.gameStartDBDataSet.Games.NewGamesRow());
                gamesBindingSource.MoveLast();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gamesBindingSource.ResetBindings(false);
            }
        }

        private void btnEdit2_Click(object sender, EventArgs e)
        {
            panelGames.Enabled = true;
            txtTitle.Focus();
        }

        private void btnCancel2_Click(object sender, EventArgs e)
        {
            panelGames.Enabled = false;
            gamesBindingSource.ResetBindings(false);
        }

        private void btnSave2_Click(object sender, EventArgs e)
        {
            try
            {
                gamesBindingSource.EndEdit();
                gamesTableAdapter.Update(this.gameStartDBDataSet.Games);
                panelGames.Enabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gamesBindingSource.ResetBindings(false);
            }
        }

        private void txtSearch1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(txtSearch1.Text))
                {
                    this.usersTableAdapter.Fill(this.gameStartDBDataSet.Users);
                    usersBindingSource.DataSource = this.gameStartDBDataSet.Users;
                    this.usersTableAdapter.SortByID(this.gameStartDBDataSet.Users);
                }
                else
                {
                    var query = from x in this.gameStartDBDataSet.Users
                                where x.FullName.ToLower().Contains(txtSearch1.Text.ToLower()) || x.Email.ToLower().Contains(txtSearch1.Text.ToLower()) || x.Username.ToLower().Contains(txtSearch1.Text.ToLower()) || x.Password.ToLower().Contains(txtSearch1.Text.ToLower())
                                orderby x.ID
                                select x;
                    usersBindingSource.DataSource = query.ToList();
                }
                e.Handled = true;
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure you wish to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    usersBindingSource.RemoveCurrent();
                    usersBindingSource.EndEdit();
                    usersTableAdapter.Update(this.gameStartDBDataSet.Users);
                    panelUsers.Enabled = false;
                }
            }
            if (e.KeyCode == Keys.S && e.Control)
            {
                try
                {
                    usersBindingSource.EndEdit();
                    usersTableAdapter.Update(this.gameStartDBDataSet.Users);
                    panelUsers.Enabled = false;
                    MessageBox.Show("Saved successfully!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnInsert1_Click(object sender, EventArgs e)
        {
            try
            {
                panelUsers.Enabled = true;
                txtName.Focus();
                this.gameStartDBDataSet.Users.AddUsersRow(this.gameStartDBDataSet.Users.NewUsersRow());
                usersBindingSource.MoveLast();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                usersBindingSource.ResetBindings(false);
            }
        }

        private void btnEdit1_Click(object sender, EventArgs e)
        {
            panelUsers.Enabled = true;
            txtName.Focus();
        }

        private void btnCancel1_Click(object sender, EventArgs e)
        {
            panelUsers.Enabled = false;
            usersBindingSource.ResetBindings(false);
        }

        private void btnSave1_Click(object sender, EventArgs e)
        {
            try
            {
                usersBindingSource.EndEdit();
                usersTableAdapter.Update(this.gameStartDBDataSet.Users);
                panelUsers.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                usersBindingSource.ResetBindings(false);
            }
        }

        private void pnlLogout_Click(object sender, EventArgs e)
        {
            LoginForm lf = new LoginForm();
            lf.Show();
            this.Close();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            pnlLogout_Click(sender, e);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pnlLogout_Click(sender, e);
        }

        private void dataGridView3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure you wish to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    customersBindingSource.RemoveCurrent();
                    customersBindingSource.EndEdit();
                    customersTableAdapter.Update(this.gameStartDBDataSet.Customers);
                }
            }
            if (e.KeyCode == Keys.S && e.Control)
            {
                try
                {
                    customersBindingSource.EndEdit();
                    customersTableAdapter.Update(this.gameStartDBDataSet.Customers);
                    MessageBox.Show("Saved successfully!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
