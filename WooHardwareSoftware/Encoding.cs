using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WooHardwareSoftware
{
    /// <summary>
    /// how would you represent one card from a deck in memory
    /// there are 52 cards with a number value and a suit
    /// </summary>
    [TestClass]
    public class Encoding
    {
        long bytesUsedOneHot = 428;
        long bytesUsedTwoHot = 520;
        /// <summary>
        /// with this technique you would just use a 64 bit address space
        /// you would use 52 bits of that space to represent each card
        /// so there are 52 cards so you need 52 * 64 spaces of memory
        /// each block of memory will have 1 in a different location to represent
        ///     which of the 52 cards it is
        /// ace of diamonds = 00000000000...0001
        /// 2 of diamonds =   00000000000...0010
        /// ....etc
        /// king of spades =  10000000000...0000
        /// this is a waste of memory
        /// also it would be very hard to compare values and suits
        /// you would have to make an if statement for every single card
        /// 
        /// </summary>
        [TestMethod]
        public void OneHotEnodingTest()
        {
            var mem = GC.GetTotalMemory(true);

            Int64[] deck = new long[52];
            for (int i = 0; i < 52; i++)
            {
                deck[i] = i;
            }
            var memafter = GC.GetTotalMemory(true);
            var memadded = memafter - mem;
            Assert.AreEqual(memadded, bytesUsedOneHot);
            var length = deck.Length;
            Console.Write(length);
            
        }

        /// <summary>
        /// use 17 bits of 64 bits
        /// 4 bits for the suit, 13 bits for the value
        /// this wastes the same amount of memory
        /// all it would do is make comparing things easier
        /// 
        /// </summary>
        [TestMethod]
        public void TwoHotEnodingTest()
        {
            var mem = GC.GetTotalMemory(true);

            Int64[] deck = new long[52];
            string[] suits = new string[] {"0001","0010","0100","1000" };
            string[] values = new string[] { "0000000000001", "0000000000010", "0000000000100", "0000000001000",
            "0000000010000", "0000000100000", "0000001000000", "0000010000000",
            "0000100000000", "0001000000000", "0010000000000", "0100000000000",
            "1000000000000"};

            int counter = 0;
            foreach (var suit in suits)
            {
                foreach (var val in values)
                {
                    long a = Convert.ToInt64("00000000000000000000000000000000000000000000000" + suit + val, 2);
                    deck[counter] = a;
                    counter++;
                }
            }
            suits = null;
            values = null;
            var memafter = GC.GetTotalMemory(true);
            var memadded = memafter - mem;
            Assert.AreEqual(memadded, bytesUsedTwoHot);
            var length = deck.Length;
            Console.Write(length);
        }
    }
}
