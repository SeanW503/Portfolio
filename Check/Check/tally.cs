using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Check
{

    
    public class tally
    {
        private const int baseCounter = 2000;
        public int counter;
        public int entryCount;
        public double checkTotalNum;

        public tally()
        {
            counter = 0;
            entryCount = 0;
            checkTotalNum = 0;
        }
        public tally(int entryCount, int counter, double checkTotalNum)
        {
            this.entryCount = entryCount;
            this.counter = counter;
            this.checkTotalNum = checkTotalNum;
        }

        public void addCheck(double amount)
        {
            if(amount > 0.00) {
                entryCount++;
                counter++;
                checkTotalNum -= amount;
            }
        }
        public void cash(double amount)
        {

            if(amount > 0.00) { 
                entryCount++;
                checkTotalNum -= amount;
            }

        }

        public void deposit(double amount)
        {
            if(amount > 0.00) {
                entryCount++;
                checkTotalNum += amount;
            }        
        }

        public int getEntryCount()
        {
            return entryCount;
        }
        public int getCheckNumber()
        {
            return counter + baseCounter;
        }

        public int getCheckCount()
        {
            return counter;
        }

        public double getCheckTotal()
        {
            return checkTotalNum;
        }
    }
}