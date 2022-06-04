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
    public partial class Clients : UserControl
    {
        private MainForm main;
        public Clients(MainForm main)
        {
            InitializeComponent();
            this.main = main;
            dataGridView1.DataSource = SQL.GetClients();
        }

        public class Client {
            public string name { get; set; }
            public string surname { get; set; }
            public bool available { get; set; }
            public string bookName { get; set; }
            public string bookAuthor { get; set; }
            public int idKsiazki { get; set; }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SQL.AddClient(textBox2.Text, textBox3.Text, checkBox1.Checked);
            dataGridView1.DataSource = SQL.GetClients();
            dataGridView1.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SQL.RemoveClient(textBox5.Text, textBox4.Text);
            dataGridView1.DataSource = SQL.GetClients();
            dataGridView1.Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            main.ShowBooks();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQL.LinkClientToBook(textBox6.Text, textBox1.Text, textBox8.Text, textBox7.Text);
            dataGridView1.DataSource = SQL.GetClients();
            dataGridView1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SQL.UnLinkClientToBook(textBox6.Text, textBox1.Text);
            dataGridView1.DataSource = SQL.GetClients();
            dataGridView1.Refresh();
        }
    }
}
