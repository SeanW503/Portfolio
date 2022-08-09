using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Check
{
    public class FileIO
    {
        public FileIO()
        {

        }

        public void newEntry(string name, string amount, string memo, string type, int checkCount, int numcheck, int entryCount, double newBalance)
        {

            string date = System.DateTime.Now.ToShortDateString();


            if (type.Equals("CHECK"))
            {
                FileStream fs = new FileStream(name + ".cbk", FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(entryCount + "," + checkCount + "," + newBalance);
                sw.Close();
                fs.Close();
                FileStream fs2 = new FileStream(name + ".cbk", FileMode.Append);
                StreamWriter mw = new StreamWriter(fs2);
                mw.WriteLine(date + "," + numcheck + "," + amount + "," + name + "," + memo + "," + newBalance);
                mw.Close();
                fs2.Close();
            }
            else if (type.Equals("CASH"))
            {
                FileStream fs = new FileStream(name + ".cbk", FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(entryCount + "," + checkCount + "," + newBalance);
                sw.Close();
                fs.Close();
                FileStream fs2 = new FileStream(name + ".cbk", FileMode.Append);
                StreamWriter mw = new StreamWriter(fs2);
                mw.WriteLine(date + "," + "CASH" + "," + amount + "," + "CASH" + "," + memo + "," + newBalance);
                mw.Close();
                fs2.Close();
            }
            else if (type.Equals("DEPOSIT"))
            {
                FileStream fs = new FileStream(name + ".cbk", FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(entryCount + "," + checkCount + "," + newBalance);
                sw.Close();
                fs.Close();
                FileStream fs2 = new FileStream(name + ".cbk", FileMode.Append);
                StreamWriter mw = new StreamWriter(fs2);
                mw.WriteLine(date + "," + "DEPOSIT" + "," + amount + "," + "DEPOSIT" + "," + memo + "," + newBalance);
                mw.Close();
                fs2.Close();
            }



        }
        public string[] readOutFile(string name)
        {
            using (StreamReader sr = new StreamReader(name + ".cbk"))
            {
                string line = sr.ReadLine();
                string[] value = line.Split(',');

                sr.Close();

                return value;
            }
        }

        public Boolean open(string name)
        {
            try
            {
                FileStream fs = new FileStream(name + ".cbk", FileMode.Open);
                fs.Close();
                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }

    }
}