using AddingData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataСorrectnessTest
{
    /// <summary>
    /// Сводное описание для CorrecthnessTest
    /// </summary>
    [TestClass]
    public class CorrecthnessTest
    {
        [TestMethod]
        public void TestMethodPositive()
        {
            string Name = "Диван";
            int Cost = 30000;
            int Quantity = 15;
            int TypeOfFurniture = 2;
            byte[] Image = System.Text.Encoding.Default.GetBytes("divan.jpeg");

            bool result = DataCorrectness.Correctness(Name, Cost, Quantity, TypeOfFurniture, Image);

            Assert.AreEqual(result, true);
        }
        [TestMethod]
        public void TestMethodNegative()
        {
            string Name = "Кровать";
            int Cost = -200000;
            int Quantity = 40;
            int TypeOfFurniture = 0;
            byte[] Image = System.Text.Encoding.Default.GetBytes("bed.jpeg");

            bool result = DataCorrectness.Correctness(Name, Cost, Quantity, TypeOfFurniture, Image);

            Assert.AreEqual(result, false);
        }
    }
}
