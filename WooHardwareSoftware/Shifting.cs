using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WooHardwareSoftware
{
    [TestClass]
    public class Shifting
    {
        [TestMethod]
        public void ShiftToLeftTest()
        {
            byte a = Convert.ToByte("00000110", 2);// = 6
            var shifted = a << 3; // should shift everythingg over to create 00110000
            var strShifted = Convert.ToString(shifted,2).PadLeft(8, '0');
            Assert.AreEqual(strShifted, "00110000"); // = 48
            //the formula is however many bits you are moving over you do 2 to the x power and then multiply it by the orignal value
            //in this example its 2 to the 3rd power which is 8, multiple 6 by 8 = 48
        }

        [TestMethod]
        public void ShiftToRightTest()
        {
            byte a = Convert.ToByte("00000110", 2);// = 6
            var shifted = a >> 2; // should shift everythingg over to create 00000001
            var strShifted = Convert.ToString(shifted, 2).PadLeft(8, '0');
            Assert.AreEqual(strShifted, "00000001"); // = 1
            //the formula is however many bits you are moving over you do 2 to the x power and then divide by the original value
            //in this example its 2 to the 2 power which is 4, divide 6 by 4 = 1.5 , but you can't have fractions at this point so its just 1

        }
        /// <summary>
        /// what is the point of shifting
        /// you can do multiplication and division by the power of 2's, so what
        /// you can also do conditional logic
        /// </summary>
        [TestMethod]
        public void ShiftLogicTests()
        {
            int valueItShouldBe = 3;
            bool shouldReturn = false;
            int x  =1,y = 3 ,z = 4,a = 0;
            if (x == 1)
                a =  y;
            else
                a = z;
            DoShiftIfStatement:
            var asbits = Convert.ToString(x, 2).PadLeft(32, '0');
            x = x << 31;
            asbits = Convert.ToString(x, 2).PadLeft(32, '0');
            x = x >> 31;
            //you can see here that when you shift the bit back to the right, it fills in the other bits with whatever the shifted bit's value is, 1 or 0
            asbits = Convert.ToString(x, 2).PadLeft(32, '0');
            Assert.IsTrue(asbits == "11111111111111111111111111111111" || asbits == "00000000000000000000000000000000");
            //what this accomplishes is making the bits all 0's or all 1's depending on what the bit in the 1's place was
            //========irrelvant to this case, but you can see x is now -1 because all 1's in 2's compliment is -1 the highest order bit is the negative number + the sum of all the other bits

            //what is the point of having all 1's or 0's
            //you can do a bitwise & on Y which will then return the value of Y or 0 depending on whether x was true or not
            int valueOfY = x & y; // in this example x was true or 1 (same thing) so the value should be y (3) as the if statement describes

            //but we still have to account for the other else statement
            //well if x is false, y will be 0 so we can just add that  to the other statement because it won't affect anything

            //just do the exact same thing but negate x so it accounts for the false
            //c# doesn't allow negating int so convert
            bool xAsBool = Convert.ToBoolean(x);
            xAsBool = !xAsBool;
            x = Convert.ToInt32(xAsBool);
            x = x << 31;
            x = x >> 31;

            int valueOfZ = x & z;

            //just add the values together because the outcome of all this stuff is that one value will always be zero and the other value will be its original value
            // zero + whatever is just whatever
            a = valueOfY + valueOfZ;
            Assert.AreEqual(a, valueItShouldBe);
            x = 0;
            valueItShouldBe = 4;
            if (shouldReturn)
                return;
            shouldReturn = true;
            goto DoShiftIfStatement;

        }



        }
}
