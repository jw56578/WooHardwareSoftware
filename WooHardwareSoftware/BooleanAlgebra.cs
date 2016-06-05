using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WooHardwareSoftware
{
    [TestClass]
    public class BooleanAlgebra
    {
        /// <summary>
        /// these operations are about flipping the bits contained within memory
        /// not about producing a logical comparison
        /// </summary>
        [TestMethod]
        public void BitLevelOperationsTests()
        {
            byte a, b, c; // 8 bits initialized at 00000000
            a = 0x41; //65 decimal, 01000001
            b = (byte)~a;//this just flips all bits to opposite, so 10111110, 190 decimal
            a = 0;//back to 0000000
            b = (byte)~a; //flipped to 11111111, 256 decimal
            a = (byte)0x69; //01101001, 105 decimal
            b = (byte)0x55;// 01010101, 85 decimal
            c = (byte)(a & b);//this takes the above bits and does an AND on each "column"
            //0&&0 = 0, 1 &&1 =1, ,1 &&0 = 0....etc
            //65 decimal, 01000001
        }
        [TestMethod]
        public void LogicalOperrationTests()
        {

        }
   }
}
