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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ShowBooks();
        }

        public void ShowBooks() {
            this.Controls.Clear();
            Controls.Add(new BooksView(this));
            Controls[0].Dock = DockStyle.Fill;
        }

        public void ShowClients() {
            this.Controls.Clear();
            Controls.Add(new Clients(this));
            Controls[0].Dock = DockStyle.Fill;
        }
    }
}
