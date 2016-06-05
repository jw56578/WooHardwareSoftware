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
            c = (byte)(a & b);//this takes the above bits and does an AND comparison on each "column"
            //0&&0 = 0, 1 &&1 =1, ,1 &&0 = 0....etc
            //65 decimal, 01000001
        }
        [TestMethod]
        public void LogicalOperationTests()
        {
            //lower level languages allow logic comparisons on bytes, integers..etc
            //this is like javascript falsey truthy
            //anything that is not a 0 is true
            //but you cannot do this in c# 
            //you can mimic it by doing a convert or parse
            //anything but a 0 will get converted to true
            byte a = 0x41;
            bool boolA = Convert.ToBoolean(a);
            bool notBoolA = !boolA;

            byte b = 0x00;
            bool boolB = Convert.ToBoolean(b);
            bool notBoolB = !boolA;


        }
        [TestMethod]
        public void LogicalSetsTests()
        {
            //binary literals are not possible in C#
            //use convert to mimic
            
            //each position in the byte represents a number according to position
            //7,6,5,4,3,2,1,0
            //wherever there is a 1 means the number is in the set
            //if there is a zero it means the number is not in the set
            //so this set is {0,3,5,6}
            //because in the 1 position there is a 1
            //in the 3 position there is a 1 
            //in the 5 position there is a 1
            //in the 6 position there is a 1
            
            byte a = Convert.ToByte("01101001",2); 

            //{0,2,4,6}
            byte b = Convert.ToByte("01010101", 2);

            //& represents whatetver is in both sets
            // 6 and 0
            //01000001
            var both = a & b;
            var intersection = Convert.ToString(both, 2).PadLeft(8, '0');
            Assert.AreEqual(intersection, "01000001");

            //| represents all number in both sets, one time
            //0,2,3,4,5,6
            //01111101
            var all = a | b;
            var union = Convert.ToString(all, 2).PadLeft(8, '0');
            Assert.AreEqual(union, "01111101");

            //^ represents represents the number that are only in one set, not both
            //2,3,4,5
            //00111100
            var either = a ^ b;
            var symmetric = Convert.ToString(either, 2).PadLeft(8, '0');
            Assert.AreEqual(symmetric, "00111100");

            //~ applies to just one set and is just all the bits flipped
            //if you don't cast it to byte it will be the same number but negative
            var opposite = (byte)~b;
            var compliment = Convert.ToString(opposite, 2).PadLeft(8, '0');
            Assert.AreEqual(compliment, "10101010");



        }
    }
}
