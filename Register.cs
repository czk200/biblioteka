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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            string login = LoginTextBox.Text.ToString();
            string password = PasswordTextBox.Text.ToString();

            if (SQL.Register(login, password))
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.error.Visible = true;
            }
        }
    }
}
