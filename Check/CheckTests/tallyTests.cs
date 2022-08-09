using Microsoft.VisualStudio.TestTools.UnitTesting;
using Check;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Check.Tests
{
    [TestClass()]
    public class tallyTests
    {
        [TestMethod()]
        public void getEntryCountTest()
        {
            //Arrange
            tally test = new tally();
            int expectedResult = 0;
            int actualResult;

            //Act
            actualResult = test.getEntryCount();

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void getEntryCountTest1()
        {
            //Arrange
            tally test = new tally(3, 2, 1502.45);
            int expectedResult = 6;
            int actualResult;
            //Act

            for(int i = 0; i < 3; i++)
            {
                test.deposit(500.00);
            }
            actualResult = test.getEntryCount();

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void getEntryCountTest2()
        {
            //Arrange
            tally test = new tally(0, 0, 1502.45);
            int expectedResult = 9;
            int actualResult;
            //Act

            for (int i = 0; i < 9; i++)
            {
                test.deposit(500.00);
            }
            actualResult = test.getEntryCount();

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void getEntryCountTest3()
        {
            //Arrange
            tally test = new tally(1, 1, 1502.45);
            int expectedResult = 20;
            int actualResult;
            //Act

            for (int i = 0; i < 19; i++)
            {
                test.deposit(500.00);
            }
            actualResult = test.getEntryCount();

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void getEntryCountTest4()
        {
            //Arrange
            tally test = new tally(3, 2, 1502.45);
            int expectedResult = 3;
            int actualResult;
            //Act
            test.deposit(-500.00);
            
            actualResult = test.getEntryCount();

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }//bad input

        //-----------------------------------------------------------------------
        
                [TestMethod()]
                public void getCheckNumberTest()
                {
                    //Arrange
                    tally test = new tally();
                    int expectedResult = 2000;
                    int actualResult;

                    //Act
                    actualResult = test.getCheckNumber();


                    //Assert
                    Assert.AreEqual(expectedResult, actualResult);
                }

                [TestMethod()]
                public void getCheckNumberTest1()
                {
                    //Arrange
                    tally test = new tally(3, 2, 1502.45);
                    int expectedResult = 2005;
                    int actualResult;

                    //Act
                    for(int i = 0; i < 3; i++)
                    {
                        test.addCheck(25.00);
                    }
                    actualResult = test.getCheckNumber();

                    //Assert
                    Assert.AreEqual(expectedResult, actualResult);
                }
                [TestMethod()]
                public void getCheckNumberTest2()
                {
                   //Arrange
                    tally test = new tally(10, 0, 1502.45);
                    int expectedResult = 2022;
                    int actualResult;

                    //Act
                    for(int i = 0; i < 22; i++)
                    {
                        test.addCheck(5.00);
                    }
                    actualResult = test.getCheckNumber();


                    //Assert
                    Assert.AreEqual(expectedResult, actualResult);
                }
                
                [TestMethod()]
                public void getCheckNumberTest3()
                {
                    //Arrange
                    tally test = new tally(0, 0, 1502.45);
                    int expectedResult = 2001;
                    int actualResult;

                    //Act
                    for(int i = 0; i < 3; i++)
                    {
                        test.deposit(50.00);
                    }
                    test.addCheck(30.00);

                    actualResult = test.getCheckNumber();

                    //Assert
                    Assert.AreEqual(expectedResult, actualResult);
                }
                
                [TestMethod()]
                public void getCheckNumberTest4()
                {
                   //Arrange
                    tally test = new tally(0, 17, 1502.45);
                    int expectedResult = 2017;
                    int actualResult;

                    //Act
                    test.addCheck(-50.00);
                    actualResult = test.getCheckNumber();

                    //Assert
                    Assert.AreEqual(expectedResult, actualResult);
                
                }//bad input

        //-----------------------------------------------------------------------

                [TestMethod()]
                public void getCheckCountTest()
                {
                    //Arrange
                    tally test = new tally();
                    int expectedResult = 0;
                    int actualResult;

                    //Act
                    actualResult = test.getCheckCount();

                    //Assert
                    Assert.AreEqual(expectedResult, actualResult);
                }

                [TestMethod()]
                public void getCheckCountTest1()
                {
                   //Arrange
                    tally test = new tally(3, 2, 1502.45);
                    int expectedResult = 5;
                    int actualResult;

                    //Act
                    for(int i = 0; i < 3; i++)
                    {
                        test.addCheck(25.00);
                    }
                    actualResult = test.getCheckCount();

                    //Assert
                    Assert.AreEqual(expectedResult, actualResult);
                }

                [TestMethod()]
                public void getCheckCountTest2()
                {
                    //Arrange
                    tally test = new tally(10, 0, 1502.45);
                    int expectedResult = 22;
                    int actualResult;

                    //Act
                    for(int i = 0; i < 22; i++)
                    {
                        test.addCheck(5.00);
                    }
                    actualResult = test.getCheckCount();

                    //Assert
                    Assert.AreEqual(expectedResult, actualResult);
                }

                [TestMethod()]
                public void getCheckCountTest3()
                {
                    //Arrange
                   tally test = new tally(0, 0, 1502.45);
                    int expectedResult = 1;
                    int actualResult;

                    //Act
                    for(int i = 0; i < 3; i++)
                    {
                        test.deposit(50.00);
                    }
                    test.addCheck(30.00);

                    actualResult = test.getCheckCount();


                    //Assert
                    Assert.AreEqual(expectedResult, actualResult);
                }

                [TestMethod()]
                public void getCheckCountTest4()
                {
                    //Arrange
                    tally test = new tally(0, 0, 1502.45);
                    int expectedResult = 0;
                    int actualResult;

                    //Act
                    test.addCheck(-50.00);
                    actualResult = test.getCheckCount();

                    //Assert
                    Assert.AreEqual(expectedResult, actualResult);
                }//bad input

        //-----------------------------------------------------------------------

                [TestMethod()]
                public void getCheckTotalTest()
                {
                   //Arrange
                    tally test = new tally();
                    double expectedResult = 0;
                    double actualResult;

                    //Act
                    actualResult = test.getCheckTotal();

                    //Assert
                    Assert.AreEqual(expectedResult, actualResult);
                }

                [TestMethod()]
                public void getCheckTotalTest1()
                {
                   //Arrange
                    tally test = new tally(0, 0, 50.50);
                    double expectedResult = 101.00;
                    double actualResult;

                    //Act
                    test.deposit(50.50);
                    actualResult = test.getCheckTotal();

                    //Assert
                    Assert.AreEqual(expectedResult, actualResult);
                }

                [TestMethod()]
                public void getCheckTotalTest2()
                {
                   //Arrange
                    tally test = new tally(0, 0, 50.50);
                    double expectedResult = 0.50;
                    double actualResult;

                    //Act
                    test.addCheck(45.00);
                    test.cash(5.00);
                    actualResult = test.getCheckTotal();

                    //Assert
                    Assert.AreEqual(expectedResult, actualResult);
                }
        
                [TestMethod()]
                public void getCheckTotalTest3()
                {
                   //Arrange
                    tally test = new tally(5, 3, 100.00);
                    double expectedResult = 110.00;
                    double actualResult;

                    //Act
                    for(int i = 0; i < 10; i++)
                    {
                        test.deposit(3.00);
                    }
                    for(int i = 0; i < 40; i++)
                    {
                        test.cash(0.50);
                    }
                    actualResult = test.getCheckTotal();

                    //Assert
                    Assert.AreEqual(expectedResult, actualResult);
                }
        
                [TestMethod()]
                public void getCheckTotalTest4()
                {
                   //Arrange
                    tally test = new tally(5, 3, 100.00);
                    double expectedResult = 100.00;
                    double actualResult;

                    //Act
                    for(int i = 0; i < 10; i++)
                    {
                        test.deposit(-3.00);
                    }
                    actualResult = test.getCheckTotal();

                    //Assert
                    Assert.AreEqual(expectedResult, actualResult);
                }//bad input
    }

}
