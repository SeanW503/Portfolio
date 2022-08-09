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
    public partial class Apply : Form
    {
        public Apply()
        {
            InitializeComponent();
            displayClients();
        }

        string name;
        string phoneNum;
        string email;
        string EIN;
        string billingAddr;
        string shippingAddr;

        private void displayClients()
        {
            clientList.Text = "Current Clients:";
            foreach(string[] s in Database.clients)
            {
                clientList.Text = clientList.Text + "\n" + s[0];
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Homepage h = new Homepage();
            h.Show();
            this.Hide();
        }

        private void Apply_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals("") || textBox2.Text.Equals("") || textBox3.Text.Equals("") || textBox4.Text.Equals("") || textBox4.Text.Equals("") || textBox5.Text.Equals("") || textBox6.Text.Equals(""))
            {
                MessageBox.Show("Please fill out all required fields", "Error: Missing Fields", MessageBoxButtons.OK);
            }
            else
            {
                name = textBox1.Text;
                phoneNum = textBox2.Text;
                email = textBox3.Text;
                EIN = textBox4.Text;
                billingAddr = textBox5.Text;
                shippingAddr = textBox6.Text;
                Database.addClient(name, phoneNum, email, EIN, billingAddr, shippingAddr);
                displayClients();
                MessageBox.Show("Application Submitted!", "SUCCESS", MessageBoxButtons.OK);
            }
        }
    }
}
