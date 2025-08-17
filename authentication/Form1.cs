using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace authentication
{
    public partial class Form1 : Form
    {
        TextBox txtUsername;
        TextBox txtPassword;

        public Form1()
        {
            InitializeComponent();
            CreateLoginForm();
        }

        private void CreateLoginForm()
        {
            // Username TextBox
            txtUsername = new TextBox();
            txtUsername.Location = new Point(100, 30);
            txtUsername.ForeColor = Color.Gray;
            txtUsername.Text = "Username";
            txtUsername.GotFocus += RemoveUsernamePlaceholder;
            txtUsername.LostFocus += SetUsernamePlaceholder;
            this.Controls.Add(txtUsername);

            // Password TextBox
            txtPassword = new TextBox();
            txtPassword.Location = new Point(100, 70);
            txtPassword.ForeColor = Color.Gray;
            txtPassword.Text = "Password";
            txtPassword.GotFocus += RemovePasswordPlaceholder;
            txtPassword.LostFocus += SetPasswordPlaceholder;
            this.Controls.Add(txtPassword);

            // Login Button
            Button btnLogin = new Button();
            btnLogin.Location = new Point(100, 110);
            btnLogin.Size = new Size(100, 30);
            btnLogin.Text = "Login";
            btnLogin.Click += BtnLogin_Click;
            this.Controls.Add(btnLogin);

            // Form Settings
            this.Text = "Login Form";
            this.Size = new Size(350, 200);
        }

        private void RemoveUsernamePlaceholder(object sender, EventArgs e)
        {
            if (txtUsername.Text == "Username")
            {
                txtUsername.Text = "";
                txtUsername.ForeColor = Color.Black;
            }
        }

        private void SetUsernamePlaceholder(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                txtUsername.Text = "Username";
                txtUsername.ForeColor = Color.Gray;
            }
        }

        private void RemovePasswordPlaceholder(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Password")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void SetPasswordPlaceholder(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.Text = "Password";
                txtPassword.ForeColor = Color.Gray;
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text == "Username" ? "" : txtUsername.Text;
            string password = txtPassword.Text == "Password" ? "" : txtPassword.Text;

            if (username == "admin" && password == "1234")
            {
                MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Invalid login.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
