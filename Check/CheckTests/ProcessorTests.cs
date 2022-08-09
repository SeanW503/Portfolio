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
    public class ProcessorTests
    {
        [TestMethod()]
        public void getWordTest()
        {
            // Arrange
            Processor myProcessor = new Processor("12345.67");
            String expectedResult = "Twelve Thousand Three Hundred and Fourty-Five and 67/100";
            String actualResult;

            // Act
            actualResult = myProcessor.getWord();

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void getWordTest1()
        {
            Processor myProcessor = new Processor("0.34");
            String expectedResult = "Zero and 34/100";
            String actualResult;

            // Act
            actualResult = myProcessor.getWord();

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void getWordTest2()
        {
            Processor myProcessor = new Processor("1563274.46");
            String expectedResult = "One Million Five Hundred and Sixty-Three Thousand Two Hundred and Seventy-Four and 46/100";
            String actualResult;

            // Act
            actualResult = myProcessor.getWord();

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void getWordTest3()
        {
            Processor myProcessor = new Processor("45.93");
            String expectedResult = "Fourty-Five and 93/100";
            String actualResult;

            // Act
            actualResult = myProcessor.getWord();

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void getWordTest5()
        {
            Processor myProcessor = new Processor("1.342");
            String expectedResult = "Invalid Input";
            String actualResult;

            // Act
            actualResult = myProcessor.getWord();

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}