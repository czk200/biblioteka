using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string login = LoginTextBox.Text.ToString();
            string password = PasswordTextBox.Text.ToString();

            if (SQL.CheckLogin(login, password))
            {
                this.Visible = false;
                new MainForm().ShowDialog();
                this.Visible = true;
            }
            else
            {
                error.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Register().ShowDialog();
        }
    }
}
