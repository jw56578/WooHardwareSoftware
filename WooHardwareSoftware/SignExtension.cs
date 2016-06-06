using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WooHardwareSoftware
{
    [TestClass]
    public class SignExtension
    {
        [TestMethod]
        public void CanCopy32BitInto64Bit()
        {
            //if you take a 32 bit number
            int a = int.MaxValue;
            string strA  = Convert.ToString(a, 2).PadLeft(32, '0');//01111111111111111111111111111111

            //and put it into a 64 bit number, what happens?
            long b = (long)a;
            string strB = Convert.ToString(b, 2).PadLeft(64, '0');//0000000000000000000000000000000001111111111111111111111111111111

            //all that happens is that you take the most significant bit, the one farthers to the left,
            //and just fill the rest out with the bit all the way to the left
            //                                01111111111111111111111111111111 <--- the left most bit is a 0, because this is a positive number
            //0000000000000000000000000000000001111111111111111111111111111111 <--- so now it just fills out the rest of the bits with 0

            a = int.MinValue;
            strA = Convert.ToString(a, 2).PadLeft(32, '0');//10000000000000000000000000000000


            b = (long)a;
            strB = Convert.ToString(b, 2).PadLeft(64, '0');//1111111111111111111111111111111110000000000000000000000000000000

            //                                10000000000000000000000000000000 <--- the left most bit is a 1, because this is a negative number
            //1111111111111111111111111111111110000000000000000000000000000000 <--- so now it just fills out the rest of the bits with 1


        }
    }
}
