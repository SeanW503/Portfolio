using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreProject
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            Database.fillDatabase();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        public void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.UseSystemPasswordChar = false;
            } else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        public void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals("") || textBox2.Text.Equals(""))
            {
                MessageBox.Show("Please Fill Out All Fields", "ERROR", MessageBoxButtons.OK);
            } else
            {
                for (int i = 0; i < 5; i++)
                {
                    if (textBox1.Text.Equals(Database.dummyUsers[i, 0]))
                    {
                        if (textBox2.Text.Equals(Database.dummyUsers[i, 1]))
                        {
                            MessageBox.Show("Log In Complete!", "SUCCESS", MessageBoxButtons.OK);
                            Homepage h = new Homepage();
                            h.Show();
                            this.Hide();
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Invalid Password", "ERROR", MessageBoxButtons.OK);
                            return;
                        }
                    }
                }
                MessageBox.Show("This User Does Not Exist", "ERROR", MessageBoxButtons.OK);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Homepage h = new Homepage();
            h.Show();
            this.Hide();
        }
    }
}
