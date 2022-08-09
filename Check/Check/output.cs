using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Check
{
    public partial class output : Form
    {
        public string name { get; set; }
        public string amount { get; set; }
        public string memo { get; set; }

        public int counter { get; set; }
        public int checkNumber { get; set; }
        public  double totalAmount { get; set; }
        public output()
        {
            InitializeComponent();
        }

        private void output_Load(object sender, EventArgs e)
        {
            textBox3.Text = name;
            textBox4.Text = amount;
            textBox6.Text = memo;

            textBox1.Text = Convert.ToString(checkNumber);
            //
            //string currentAmount = amount;
            //
            //tally sendAmount  = new tally(currentAmount);
            //totalAmount = sendAmount.getTotal();
            //textBox4.Text = totalAmount;
            //Processor send = new Processor(totalAmount);
            //textBox5.Text = send.getWord();

            Processor send = new Processor(amount);
            textBox5.Text = send.getWord();

            textBox2.Text = System.DateTime.Now.ToShortDateString();

            textBox8.Text = Convert.ToString(counter);

            textBox9.Text = Convert.ToString(totalAmount);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
