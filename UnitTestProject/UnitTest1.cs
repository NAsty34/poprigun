using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WSUniversalLib;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetQuantityForProduct_NonExistentProductType_pervoe()
        {
            Assert.AreEqual(new Calculation().GetQuantityForProduct(15,23,14,6,12), -1);

        }
        [TestMethod]
        public void GetQuantityForProduct_NonExistentProductType_vtoroe()
        {
            Assert.AreEqual(new Calculation().GetQuantityForProduct(-5, -3, -1, -6, -1), -1);

        }

        [TestMethod]
        public void GetQuantityForProduct_NonExistentProductType_null()
        {
            Assert.AreEqual(new Calculation().GetQuantityForProduct(0, 1, 1, 6, 1), -1);

        }
        [TestMethod]
        public void GetQuantityForProduct_NonExistentProductType_par()
        {
            Assert.AreEqual(new Calculation().GetQuantityForProduct(1, 1, 15, 20, 30), 9929);

        }
    }
}
