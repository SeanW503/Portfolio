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
    public partial class start : Form
    {
    
        
        public start()
        {
            InitializeComponent();
        }

        private void start_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (comboBox1.SelectedIndex == 0)
            {
                newuser nu = new newuser();
                nu.ShowDialog();
            } else if(comboBox1.SelectedIndex == 1)
            {
                Application.Exit();
            }


          
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            memoTextBox.Enabled = true;
            checkBox1.Enabled = true;
            checkBox1.Checked = true;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            output og = new output();
            FileIO io = new FileIO();
            string[] array;
            int entryCount;
            int counter;
            double total;
            tally info;
            double oldBal;

            
            if (io.open(nameTextBox.Text) == true)
            {
                if (canConvert(amountTextBox.Text))
                {
                    if (Convert.ToDouble(amountTextBox.Text) < 0)
                    {
                        MessageBox.Show("Please enter a value greater than 0.00", "Invalid", MessageBoxButtons.OK);
                        return;
                    }

                    double temp = Convert.ToDouble(amountTextBox.Text);
                    string amount = Math.Round(temp, 2).ToString();

                    og.name = nameTextBox.Text;
                    og.amount = amount;
                    og.memo = memoTextBox.Text;


                    array = io.readOutFile(nameTextBox.Text);
                    entryCount = Convert.ToInt32(array[0]);
                    counter = Convert.ToInt32(array[1]);
                    total = Convert.ToDouble(array[2]);
                    info = new tally(entryCount, counter, total);
                    oldBal = Math.Round(info.getCheckTotal(), 2);




                    if (radioButton1.Checked == true)
                    {
                        if (oldBal - Convert.ToDouble(amountTextBox.Text) < 0)
                        {
                            DialogResult d = MessageBox.Show("Do you still wish to proceed?", "This action will create a negative balance within your account!", MessageBoxButtons.YesNo);
                            if (d == DialogResult.Yes)
                            {
                                info.addCheck(Convert.ToDouble(amount));
                                io.newEntry(nameTextBox.Text, amount, memoTextBox.Text, "CHECK", info.getCheckCount(), info.getCheckNumber(), info.getEntryCount(), info.getCheckTotal());
                                og.counter = info.getCheckCount();
                                og.checkNumber = info.getCheckNumber();
                                og.totalAmount = info.getCheckTotal();
                            } else
                            {
                                return;
                            }
                        }
                        else
                        {
                            info.addCheck(Convert.ToDouble(amount));
                            io.newEntry(nameTextBox.Text, amount, memoTextBox.Text, "CHECK", info.getCheckCount(), info.getCheckNumber(), info.getEntryCount(), info.getCheckTotal());
                            og.counter = info.getCheckCount();
                            og.checkNumber = info.getCheckNumber();
                            og.totalAmount = info.getCheckTotal();
                        }
                        
                    }

                    else if (radioButton2.Checked == true)
                    {
                        if (oldBal - Convert.ToDouble(amountTextBox.Text) < 0)
                        {
                            DialogResult d = MessageBox.Show("Do you still wish to proceed?", "This action will create a negative balance within your account!", MessageBoxButtons.YesNo);
                            if (d == DialogResult.Yes)
                            {
                                info.cash(Convert.ToDouble(amount));
                                io.newEntry(nameTextBox.Text, amount, memoTextBox.Text, "CASH", info.getCheckCount(), info.getCheckNumber(), info.getEntryCount(), info.getCheckTotal());
                            } else
                            {
                                return;
                            }
                        }
                        else
                        {
                            info.cash(Convert.ToDouble(amount));
                            io.newEntry(nameTextBox.Text, amount, memoTextBox.Text, "CASH", info.getCheckCount(), info.getCheckNumber(), info.getEntryCount(), info.getCheckTotal());
                        }
                        
                    }
                    else if (radioButton3.Checked == true)
                    {
                        if (oldBal + Convert.ToDouble(amountTextBox.Text) < 0)
                        {
                            DialogResult d = MessageBox.Show("Do you still wish to proceed?", "This action will create a negative balance within your account!", MessageBoxButtons.YesNo);
                            if (d == DialogResult.Yes)
                            {
                                info.deposit(Convert.ToDouble(amount));
                                io.newEntry(nameTextBox.Text, amount, memoTextBox.Text, "DEPOSIT", info.getCheckCount(), info.getCheckNumber(), info.getEntryCount(), info.getCheckTotal());
                            } else
                            {
                                return;
                            }
                        }
                        else
                        {
                            info.deposit(Convert.ToDouble(amount));
                            io.newEntry(nameTextBox.Text, amount, memoTextBox.Text, "DEPOSIT", info.getCheckCount(), info.getCheckNumber(), info.getEntryCount(), info.getCheckTotal());
                        }
                        
                    }




                    if (radioButton1.Checked == false && radioButton2.Checked == false && radioButton3.Checked == false)
                    {
                        MessageBox.Show("Please select Check, Cash, or Deposit before clicking submit!", " ", MessageBoxButtons.OK);
                    }

                    if (nameTextBox.Equals("") || amountTextBox.Text.Equals("") || memoTextBox.Text.Equals(""))
                    {

                        MessageBox.Show("Please fill out all required fields", " ", MessageBoxButtons.OK);

                    }
                    else
                    {
                        if (radioButton1.Checked == true)
                        {
                            if (canConvert(amount) == true)
                            {
                                if (checkBox1.Checked == true)
                                {
                                    og.ShowDialog();
                                }
                                else
                                {
                                    MessageBox.Show("Old Balance: $" + oldBal + "\nNew Balance: $" + info.getCheckTotal(), "Hello " + og.name + "!", MessageBoxButtons.OK);
                                }


                            }

                            else
                            {
                                MessageBox.Show("Invalid Input", "", MessageBoxButtons.OK);
                            }
                        }

                        else
                        {

                            if (canConvert(amount) == true)
                            {

                                MessageBox.Show("Old Balance: $" + oldBal + "\nNew Balance: $" + info.getCheckTotal(), "Hello " + og.name + "!", MessageBoxButtons.OK);

                            }

                            else
                            {
                                MessageBox.Show("Invalid Input", "", MessageBoxButtons.OK);
                            }

                        }

                    }
                }
                else
                {
                    MessageBox.Show("Invalid Input", "", MessageBoxButtons.OK);
                }
            } else
            {
               MessageBox.Show("Please Create New Account First", "ACCOUNT DOES NOT EXIST", MessageBoxButtons.OK); 
            }

}

        private Boolean canConvert(string amount) {
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




        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

                
                checkBox1.Enabled = false;
                checkBox1.Checked = false;

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            
                
                checkBox1.Enabled = false;
                checkBox1.Checked = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void amountTextBox_TextChanged(object sender, EventArgs e)
        {
            
            
        }
    }
}
