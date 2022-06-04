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
    public partial class BooksView : UserControl
    {
        private MainForm main;
        public BooksView(MainForm main)
        {
            InitializeComponent();
            this.main = main;
            this.dataGridView1.DataSource = SQL.GetBooks();
        }

        public class Book {
            public string name { get; set; }
            public string author { get; set; }
            public bool available { get; set; } 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = SQL.GetBooks().Where(x=>textBox1.Text.Equals(x.name) || textBox1.Text.Equals(x.author)).ToList();
            this.dataGridView1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = SQL.GetBooks();
            this.dataGridView1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SQL.AddBook(textBox2.Text, textBox3.Text, checkBox1.Checked);
            this.dataGridView1.DataSource = SQL.GetBooks();
            this.dataGridView1.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SQL.RemoveBook(textBox5.Text, textBox4.Text);
            this.dataGridView1.DataSource = SQL.GetBooks();
            this.dataGridView1.Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            main.ShowClients();
        }
    }
}
