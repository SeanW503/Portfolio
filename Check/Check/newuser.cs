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
    public partial class newuser : Form
    {
        public newuser()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            FileIO io = new FileIO();
            if (textBox1.Text.Equals("") || textBox2.Text.Equals("")) {
                MessageBox.Show("Please Fill Out All Fields", "Sorry...", MessageBoxButtons.OK);
            } else
            {
                if (io.open(textBox1.Text) == false)
                {
                    if (canConvert(textBox2.Text))
                    {
                        if (Convert.ToDouble(textBox2.Text) < 0)
                        {
                            MessageBox.Show("Please enter a value greater than 0.00","Invalid", MessageBoxButtons.OK);
                            return;
                        }
                        tally neg = new tally();
                        if ((neg.getCheckTotal() + Convert.ToDouble(textBox2.Text)) < 0)
                        {
                            DialogResult d = MessageBox.Show("Do you still wish to proceed?", "This action will create a negative balance within your account!", MessageBoxButtons.YesNo);
                            if (d == DialogResult.Yes)
                            {
                                double temp = Convert.ToDouble(textBox2.Text);
                                string amount = Math.Round(temp, 2).ToString();
                                io.newEntry(textBox1.Text, amount, "Account Creation", "DEPOSIT", 0, 0, 1, Convert.ToDouble(amount));
                                MessageBox.Show("Account Successfully Created!", "Congratulations!", MessageBoxButtons.OK);
                                this.Close();
                            }
                        } else
                        {
                            double temp = Convert.ToDouble(textBox2.Text);
                            string amount = Math.Round(temp, 2).ToString();
                            io.newEntry(textBox1.Text, amount, "Account Creation", "DEPOSIT", 0, 0, 1, Convert.ToDouble(amount));
                            MessageBox.Show("Account Successfully Created!", "Congratulations!", MessageBoxButtons.OK);
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Input", "Sorry...", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Account Already Exists.", "Sorry...", MessageBoxButtons.OK);
                }
            }
            
        }

        private Boolean canConvert(string amount)
        {
            try
            {
                Convert.ToDouble(amount);
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }
        private void newuser_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
