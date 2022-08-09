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
    public class LoginTests
    {
        [TestMethod()]
        public void checkBox1_CheckedChangedTest()
        {
            //Arrange
            Boolean expectedOutput = false;
            Boolean actualOutput;
            Boolean checkBoxChecked = true;

            //Act
            if (checkBoxChecked == false)
            {
                actualOutput = true;
            } else
            {
                actualOutput = false;
            }


            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void checkBox1_CheckedChangedTest1()
        {
            //Arrange
            Boolean expectedOutput = true;
            Boolean actualOutput;
            Boolean checkBoxChecked = false;

            //Act
            if (checkBoxChecked == false)
            {
                actualOutput = true;
            }
            else
            {
                actualOutput = false;
            }


            //Assert
            Assert.AreEqual(expectedOutput, actualOutput); ;
        }

        [TestMethod()]
        public void button1_ClickTest()
        {
            //Arrange
            String expectedOutput = "Please Fill Out All Fields";
            String actualOutput = "";
            String username = "";
            String password = "a";

            //Act
            if (username.Equals("") || password.Equals(""))
            {
                actualOutput = "Please Fill Out All Fields";
            } else
            {
                for(int i = 0; i < 5; i++)
                {
                    if (username.Equals(Database.dummyUsers[i, 0]))
                    {
                        if(password.Equals(Database.dummyUsers[i, 1]))
                        {
                            actualOutput = "Log In Completed";
                            break;
                        } else
                        {
                            actualOutput = "Invalid Password";
                            break;
                        }
                    }
                    actualOutput = "This User Does Not Exist";
                }
            }


            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void button1_ClickTest1()
        {
            //Arrange
            String expectedOutput = "Please Fill Out All Fields";
            String actualOutput = "";
            String username = "a";
            String password = "";

            //Act
            if (username.Equals("") || password.Equals(""))
            {
                actualOutput = "Please Fill Out All Fields";
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    if (username.Equals(Database.dummyUsers[i, 0]))
                    {
                        if (password.Equals(Database.dummyUsers[i, 1]))
                        {
                            actualOutput = "Log In Completed";
                            break;
                        }
                        else
                        {
                            actualOutput = "Invalid Password";
                            break;
                        }
                    }
                    actualOutput = "This User Does Not Exist";
                }
            }


            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void button1_ClickTest2()
        {
            //Arrange
            String expectedOutput = "This User Does Not Exist";
            String actualOutput = "";
            String username = "Tavish";
            String password = "Wellman9!";

            //Act
            if (username.Equals("") || password.Equals(""))
            {
                actualOutput = "Please Fill Out All Fields";
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    if (username.Equals(Database.dummyUsers[i, 0]))
                    {
                        if (password.Equals(Database.dummyUsers[i, 1]))
                        {
                            actualOutput = "Log In Completed";
                            break;
                        }
                        else
                        {
                            actualOutput = "Invalid Password";
                            break;
                        }
                    }
                    actualOutput = "This User Does Not Exist";
                }
            }


            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void button1_ClickTest3()
        {
            //Arrange
            String expectedOutput = "Invalid Password";
            String actualOutput = "";
            String username = "Harold";
            String password = "Wellman9!";
            Database.fillDatabase();
            //Act
            if (username.Equals("") || password.Equals(""))
            {
                actualOutput = "Please Fill Out All Fields";
            }
            else
            {
                actualOutput = "This User Does Not Exist";
                for (int i = 0; i < 5; i++)
                {
                    if (username.Equals(Database.dummyUsers[i, 0]))
                    {
                        if (password.Equals(Database.dummyUsers[i, 1]))
                        {
                            actualOutput = "Log In Completed";
                            return;
                        }
                        else
                        {
                            actualOutput = "Invalid Password";
                            return;
                        }
                    }
                    
                }
            }


            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void button1_ClickTest4()
        {
            //Arrange
            String expectedOutput = "Log In Completed";
            String actualOutput = "";
            String username = "Harold";
            String password = "reV456!";
            Database.fillDatabase();
            //Act
            if (username.Equals("") || password.Equals(""))
            {
                actualOutput = "Please Fill Out All Fields";
            }
            else
            {
                actualOutput = "This User Does Not Exist";
                for (int i = 0; i < 5; i++)
                {
                    if (username.Equals(Database.dummyUsers[i, 0]))
                    {
                        if (password.Equals(Database.dummyUsers[i, 1]))
                        {
                            actualOutput = "Log In Completed";
                            break;
                        }
                        else
                        {
                            actualOutput = "Invalid Password";
                            break;
                        }
                    }
                    
                }
            }


            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}