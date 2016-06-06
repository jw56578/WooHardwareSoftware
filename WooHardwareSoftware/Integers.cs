using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WooHardwareSoftware
{
    /// <summary>
    /// remember how binary works for numbers
    /// going from right to left it starts at the 1 columns
    /// 1, 2, 4, 8, 16, 32, 64, 128
    /// whatever column has a 1 in it, you take the column's number and add them all up
    /// 11111111 = 255
    /// 10000000 = 128
    /// 01000000 = 64
    /// 00100000 = 32
    /// 00010000 = 16
    /// 00001000= 8
    /// 00000100 = 4
    /// 00000010 = 2
    /// 00000001 = 1
    /// </summary>
    /// 

    [TestClass]
    public class Integers
    {
        
        [TestMethod]
        public void AddingNumbers()
        {
            //adding binary numbers is exactly the same as decial numbers
            //you add the columns and carry the 1
        }
        [TestMethod]
        public void SubtractNumbers()
        {
            //subtracting is a little more confusing because you borrow instead of subtract
            //i'm not quite sure what this means
        }
        /// <summary>
        /// signed numbers are not used
        /// </summary>
        [TestMethod]
        public void SignedNumbers()
        {
            //this is how you represent possible positive or negative numbers
            //using a byte of 8 bits, you use the higher order bit, the one most to the left to be the "sign bit'
            //if the sign bit is a 0 then its positive else a 1 its negative
            //this removes half of the available numbers because now you only have 7 bits available for the number part
            //01111111 = 127
            //11111111 = -127
            //we can no longer get to 255

            //this also produces a weird scenario of having a positive 0 and a negative 0
            //that is why this technique is not used anymore
            
        }
        /// <summary>
        /// this is the technique used by computers for negative numbers
        /// </summary>
        [TestMethod]
        public void TwoCompliment()
        {
            /*
            this does not use the most significant bit to tell whether number number is negative or positive
            this says that when you get to a certain bit column, make it the negative value of that column
            
            so for 1 byte you would have 1,2,4,8,16,23,64,128
            but if you use 2 compliment you now have 
            1,2,4,-8
            1,2,4,8,-16
            1,2,4,8,16,-32
            1,2,4,8,16,32,-64,
            1,2,4,8,16,32,64,-128
            whatever is the last bit now becomes the same value it was, but just negative

            1000 = -8 because that is just how its defined
            1001 = -7 because you have a negative 8 and a positive 1 which add to -7
            1111 = -1  = -8 + 7
            10000 = -16
            10001 = -15
            10010 = -14

            */

            int negativeNumber = Convert.ToInt32("1000", 2);
        }
        [TestMethod]
        public void IntegerTests()
        {
            /*
            the integer max values for c# are the same as in any language such as normal C
            in c# we are familiar with using int for numbers by default, its not commmon to use uint
            because uint is not CLS compliant
            int is reprsented by signed int32
            but there is also uint for unsigned int32
            why would we not just always use uint as i don't ever use negative numbers, not ClS compliant but there is no reason to not use it
            */


            //9223372036854775807
            var max = Int64.MaxValue;

            //18446744073709551615
            var umax = UInt64.MaxValue;

            //9223372036854775807
            var diff = (long)(umax / 2); // if you don't cast to long then diff will be a UInt64 and won't compare to a Int64

            Assert.AreEqual(max, diff);

            long cannotholdunsigned = (long)umax;
            Assert.AreEqual(cannotholdunsigned, -1);

            var zero = 0;
            uint uzero = 0;
            Assert.AreNotEqual(zero, uzero);//these are not equal because they are different types
            Assert.IsTrue(zero == uzero); //this is different than AreEqual because IsEqual is used and 2 different types can never be equal
                                          //even if they contain the same value

          
            int less = -1;
            uint uless = 0;
            unchecked
            {
                //var test = Convert.ToUInt64(less);
            }
            Assert.IsTrue(less < uless); //this would be false in C but i'm not sure what C# is doing, C would convert the negative 1 to a positive number

        }
    }
}
