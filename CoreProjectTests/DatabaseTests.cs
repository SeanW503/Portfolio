using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass()]
    public class DatabaseTests
    {
        [TestMethod()]
        public void getUserNameTest()
        {
            //Arrange
            String expectedOutput = "Bob";
            String actualOutput;
            Database.fillDatabase();
            //Act
            actualOutput = Database.getUserName(0);
            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void getUserNameTest1()
        {
            //Arrange
            String expectedOutput = "Harold";
            String actualOutput;
            Database.fillDatabase();
            //Act
            actualOutput = Database.getUserName(1);
            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void getUserNameTest2()
        {
            //Arrange
            String expectedOutput = "Cheryl";
            String actualOutput;
            Database.fillDatabase();
            //Act
            actualOutput = Database.getUserName(2);
            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void getUserNameTest3()
        {
            //Arrange
            String expectedOutput = "Francisco";
            String actualOutput;
            Database.fillDatabase();
            //Act
            actualOutput = Database.getUserName(3);
            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void getUserNameTest4()
        {
            //Arrange
            String expectedOutput = "HubertBlaineWolfeschlegelsteinhausenbergerdorff";
            String actualOutput;
            Database.fillDatabase();
            //Act
            actualOutput = Database.getUserName(4);
            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void getUserPwdTest()
        {
            //Arrange
            String expectedOutput = "Spike123";
            String actualOutput;
            Database.fillDatabase();
            //Act
            actualOutput = Database.getUserPwd(0);
            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void getUserPwdTest1()
        {
            //Arrange
            String expectedOutput = "reV456!";
            String actualOutput;
            Database.fillDatabase();
            //Act
            actualOutput = Database.getUserPwd(1);
            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void getUserPwdTest2()
        {
            //Arrange
            String expectedOutput = "tbm93rtw";
            String actualOutput;
            Database.fillDatabase();
            //Act
            actualOutput = Database.getUserPwd(2);
            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void getUserPwdTest3()
        {
            //Arrange
            String expectedOutput = "66642069";
            String actualOutput;
            Database.fillDatabase();
            //Act
            actualOutput = Database.getUserPwd(3);
            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void getUserPwdTest4()
        {
            //Arrange
            String expectedOutput = "hubert1";
            String actualOutput;
            Database.fillDatabase();
            //Act
            actualOutput = Database.getUserPwd(4);
            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void addClientTest()
        {
            //Arrange
            int expectedOutput = 1;
            int actualOutput;
            //Act
            Database.addClient("a", "a", "a", "a", "a", "a");
            actualOutput = Database.clients.Count();
            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void addClientTest1()
        {
            //Arrange
            int expectedOutput = 2;
            int actualOutput;
            //Act
            Database.addClient("b", "b", "b", "b", "b", "b");
            actualOutput = Database.clients.Count();
            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void addClientTest2()
        {
            //Arrange
            int expectedOutput = 3;
            int actualOutput;
            //Act
            Database.addClient("c", "c", "c", "c", "c", "c");
            actualOutput = Database.clients.Count();
            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void addClientTest3()
        {
            //Arrange
            int expectedOutput = 4;
            int actualOutput;
            //Act
            Database.addClient("d", "d", "d", "d", "d", "d");
            actualOutput = Database.clients.Count();
            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void addClientTest4()
        {
            //Arrange
            int expectedOutput = 5;
            int actualOutput;
            //Act
            Database.addClient("e", "e", "e", "e", "e", "e");
            actualOutput = Database.clients.Count();
            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void getClientTest()
        {
            //Arrange
            String expectedOutput = "a";
            String actualOutput;
            //Act
            Database.addClient("a", "a", "a", "a", "a", "a");
            Database.addClient("b", "b", "b", "b", "b", "b");
            Database.addClient("c", "c", "c", "c", "c", "c");
            Database.addClient("d", "d", "d", "d", "d", "d");
            Database.addClient("e", "e", "e", "e", "e", "e");
            String [] a = Database.getClient("a");
            actualOutput = a[0];
            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void getClientTest1()
        {
            //Arrange
            String expectedOutput = "b";
            String actualOutput;
            //Act
            Database.addClient("a", "a", "a", "a", "a", "a");
            Database.addClient("b", "b", "b", "b", "b", "b");
            Database.addClient("c", "c", "c", "c", "c", "c");
            Database.addClient("d", "d", "d", "d", "d", "d");
            Database.addClient("e", "e", "e", "e", "e", "e");
            String[] a = Database.getClient("b");
            actualOutput = a[1];
            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void getClientTest2()
        {
            //Arrange
            String expectedOutput = "c";
            String actualOutput;
            //Act
            Database.addClient("a", "a", "a", "a", "a", "a");
            Database.addClient("b", "b", "b", "b", "b", "b");
            Database.addClient("c", "c", "c", "c", "c", "c");
            Database.addClient("d", "d", "d", "d", "d", "d");
            Database.addClient("e", "e", "e", "e", "e", "e");
            String[] a = Database.getClient("c");
            actualOutput = a[2];
            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void getClientTest3()
        {
            //Arrange
            String expectedOutput = "d";
            String actualOutput;
            //Act
            Database.addClient("a", "a", "a", "a", "a", "a");
            Database.addClient("b", "b", "b", "b", "b", "b");
            Database.addClient("c", "c", "c", "c", "c", "c");
            Database.addClient("d", "d", "d", "d", "d", "d");
            Database.addClient("e", "e", "e", "e", "e", "e");
            String[] a = Database.getClient("d");
            actualOutput = a[3];
            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void getClientTest4()
        {
            //Arrange
            String expectedOutput = "e";
            String actualOutput;
            //Act
            Database.addClient("a", "a", "a", "a", "a", "a");
            Database.addClient("b", "b", "b", "b", "b", "b");
            Database.addClient("c", "c", "c", "c", "c", "c");
            Database.addClient("d", "d", "d", "d", "d", "d");
            Database.addClient("e", "e", "e", "e", "e", "e");
            String[] a = Database.getClient("e");
            actualOutput = a[4];
            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod()]
        public void getClientTest5()
        {
            //Arrange
            String[] expectedOutput = null;
            String[] actualOutput;
            //Act
            Database.addClient("a", "a", "a", "a", "a", "a");
            Database.addClient("b", "b", "b", "b", "b", "b");
            Database.addClient("c", "c", "c", "c", "c", "c");
            Database.addClient("d", "d", "d", "d", "d", "d");
            Database.addClient("e", "e", "e", "e", "e", "e");
            actualOutput = Database.getClient("f");
            //Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}