using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WooHardwareSoftware
{
    /// <summary>
    /// how would you represent one card from a deck in memory
    /// there are 52 cards with a number value and a suit
    /// you don't just have to store it, you have to operate on it
    /// the most memory efficient way is not necessarily the best
    /// </summary>
    [TestClass]
    public class Encoding
    {
        long bytesUsedOneHot = 428;
        long bytesUsedTwoHot = 520;
        long bytesUsedBinaryEncoding = 64; //less memory but cannot do anything with the information
        long bytesUsedBinaryEncoding2 = 156; // best solution
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
        /// just us the actual number representation of the card
        /// 1- 52
        /// you only need 6 bits to represent up to 52, so you just need 1 byte
        /// this saves lots of memory but is still impossible to compare cards
        /// you would have a ton of if statements
        /// </summary>
        [TestMethod]
        public void BinaryEncoding()
        {
            var mem = GC.GetTotalMemory(true);

            byte[] deck = new byte[52];
            for (int i = 0; i < 52; i++)
            {
                deck[i] = (byte)i;
            }

            var memafter = GC.GetTotalMemory(true);
            var memadded = memafter - mem;
            Assert.AreEqual(memadded, bytesUsedBinaryEncoding);
            var length = deck.Length;
            Console.Write(length);
        }
        /// <summary>
        /// so you encode a thing in binary but seperate out the information.  suits and value
        /// 2 bits for the suit, 4 bits for the value, 6 bits total, so you still only need one byte of space
        /// suits are 1-4
        /// there are 4 suits, you only need 2 bits to represent up to the number 4: 00,01,10,11
        /// there are 13 values, you only need 4 bits to represnt up to the number 13: 00,01,10,11,100,101,110,111, 1000,1001,...etc
        /// this saves memory and makes it way easier to compare things
        /// because you can have an if statement for the suit
        /// then you can take the last 4 bits compare it as a number
        /// </summary>
        [TestMethod]
        public void BinaryEncoding2()
        {
            var mem = GC.GetTotalMemory(true);

            byte[] deck = new byte[52];
            string[] suits = new string[] { "00", "01", "10", "11" };
            string[] values = new string[] { "0000", "0001", "0010", "0011",
            "0100", "0101", "0110", "0111","1000","1001","1010","1011","1100"};

            int counter = 0;
            foreach (var suit in suits)
            {
                foreach (var val in values)
                {
                    byte a = Convert.ToByte("00" + suit + val, 2);
                    deck[counter] = a;
                    counter++;
                }
            }

            var memafter = GC.GetTotalMemory(true);
            var memadded = memafter - mem;
            Assert.AreEqual(memadded, bytesUsedBinaryEncoding2);
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
            string[] suits = new string[] { "0001", "0010", "0100", "1000" };
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
        [TestMethod]
        public void CanCompareCards()
        {
            byte[] hand = new byte[5];
            //ace of diamonds: first 00 is nothing, 01 means diamonds, 0000 means ace
            hand[0] = Convert.ToByte("00010000", 2);
            //2 of diamonds
            hand[1] = Convert.ToByte("00010001", 2);

            //2 of clubs
            hand[2] = Convert.ToByte("00100001", 2);

            byte card1 = hand[0], card2 = hand[1];
            Assert.AreEqual(CompareCardSuit(card1, card2), true);

            card1 = hand[0];
            card2 = hand[2];
            Assert.AreEqual(CompareCardSuit(card1, card2), false);

        }
        bool CompareCardSuit(byte card1, byte card2)
        { 
            //IMPORTANT CONCEPT TO UNDERSTAND HERE
            //if  you endocde information in certain bits of a byte and you want to compare those certain bits
            //use a bit wise and operator to zero out all of the other bits you dont' care about
            //use a "mask" where you have 1's for the bits you care about and 0's for bits you don't care about
            //then you can compare the entire byte because the bits you don't care about are all the same (0's) so are irrelevant in the comparison


            byte suitMask = 0x30; //0011 0000
            // you are doing a biwise and on 00010000 & 00110000
            //  00010000
            //  00110000
            //==00010000, oh i see, because it will turn every bit into a 0 except for the suit part
            //if there is any 1 value for the suit bits, then it will stay 1 because you are ANDING to another 1
            //if its a 0 it will stay zero, all other bits will definately turn to 0 because you are ANDING to 0 
            //so the suit part stays the same, all other bit change to zero
            var card1Mask = (byte)(card1 & suitMask);
            var card2Mask = (byte)(card2 & suitMask);
            //so at ths point you have 2 bytes that represent 2 cards, that only have bits for the suit location, every other bit will be 0
            //this will allow you to compare only the suit part
            var compare = (byte)(card1Mask ^ card2Mask); // now do an XOR, exclusive or
            //doing an XOR is how you compare if they are the same thing, if they are the same then XOR will produce 0
            //because XOR only produces a 1 if they are not the same
            
            //at this point we have a boolean that is the opposite of what we want
            //then reverse the result to get a 1 (true) if they are the same
            bool suitsAreTheSame = Convert.ToBoolean(compare);
            suitsAreTheSame = !suitsAreTheSame;

            return suitsAreTheSame;
        }
    }
}
