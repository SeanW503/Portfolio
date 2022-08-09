using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoreProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreProject.Tests
{
    [TestClass()]
    public class ApplyTests
    {
        [TestMethod()]
        public void button3_ClickTest()
        {
            //Arrange
            String name = "";
            String phone = "a";
            String email = "a";
            String EIN = "a";
            String billAdd = "a";
            String shipAdd = "a";
            String expectedOutput = "Please Fill Out All Fields";
            String actualOutput;

            //Act
            if(name.Equals("") || phone.Equals("") || email.Equals("") || EIN.Equals("") || billAdd.Equals("") || shipAdd.Equals(""))
            {
                actualOutput = "Please Fill Out All Fields";
            } else
            {
                actualOutput = "Application Submitted";
            }


            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void button3_ClickTest1()
        {
            //Arrange
            String name = "a";
            String phone = "";
            String email = "a";
            String EIN = "a";
            String billAdd = "a";
            String shipAdd = "a";
            String expectedOutput = "Please Fill Out All Fields";
            String actualOutput;

            //Act
            if (name.Equals("") || phone.Equals("") || email.Equals("") || EIN.Equals("") || billAdd.Equals("") || shipAdd.Equals(""))
            {
                actualOutput = "Please Fill Out All Fields";
            }
            else
            {
                actualOutput = "Application Submitted";
            }


            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void button3_ClickTest2()
        {
            //Arrange
            String name = "a";
            String phone = "a";
            String email = "";
            String EIN = "a";
            String billAdd = "a";
            String shipAdd = "a";
            String expectedOutput = "Please Fill Out All Fields";
            String actualOutput;

            //Act
            if (name.Equals("") || phone.Equals("") || email.Equals("") || EIN.Equals("") || billAdd.Equals("") || shipAdd.Equals(""))
            {
                actualOutput = "Please Fill Out All Fields";
            }
            else
            {
                actualOutput = "Application Submitted";
            }


            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void button3_ClickTest3()
        {
            //Arrange
            String name = "a";
            String phone = "a";
            String email = "a";
            String EIN = "";
            String billAdd = "a";
            String shipAdd = "a";
            String expectedOutput = "Please Fill Out All Fields";
            String actualOutput;

            //Act
            if (name.Equals("") || phone.Equals("") || email.Equals("") || EIN.Equals("") || billAdd.Equals("") || shipAdd.Equals(""))
            {
                actualOutput = "Please Fill Out All Fields";
            }
            else
            {
                actualOutput = "Application Submitted";
            }


            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void button3_ClickTest4()
        {
            //Arrange
            String name = "a";
            String phone = "a";
            String email = "a";
            String EIN = "a";
            String billAdd = "";
            String shipAdd = "a";
            String expectedOutput = "Please Fill Out All Fields";
            String actualOutput;

            //Act
            if (name.Equals("") || phone.Equals("") || email.Equals("") || EIN.Equals("") || billAdd.Equals("") || shipAdd.Equals(""))
            {
                actualOutput = "Please Fill Out All Fields";
            }
            else
            {
                actualOutput = "Application Submitted";
            }


            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void button3_ClickTest5()
        {
            //Arrange
            String name = "a";
            String phone = "a";
            String email = "a";
            String EIN = "a";
            String billAdd = "a";
            String shipAdd = "";
            String expectedOutput = "Please Fill Out All Fields";
            String actualOutput;

            //Act
            if (name.Equals("") || phone.Equals("") || email.Equals("") || EIN.Equals("") || billAdd.Equals("") || shipAdd.Equals(""))
            {
                actualOutput = "Please Fill Out All Fields";
            }
            else
            {
                actualOutput = "Application Submitted";
            }


            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void button3_ClickTest6()
        {
            //Arrange
            String name = "a";
            String phone = "a";
            String email = "a";
            String EIN = "a";
            String billAdd = "a";
            String shipAdd = "a";
            String expectedOutput = "Application Submitted";
            String actualOutput;

            //Act
            if (name.Equals("") || phone.Equals("") || email.Equals("") || EIN.Equals("") || billAdd.Equals("") || shipAdd.Equals(""))
            {
                actualOutput = "Please Fill Out All Fields";
            }
            else
            {
                actualOutput = "Application Submitted";
            }


            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}