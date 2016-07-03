using System;
using System.Text;
using KeyMan.Common;
using KeyMan.Core;
using NUnit.Framework;

namespace KeyMan.Test
{
    public class EntropyTests
    {
        [Test]
        public void Should_Fail_EntropyTest_All_Zeroes()
        {
            var allZeroes = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Assert.AreEqual(32, allZeroes.Length);

            var allZeroesEntropyScore = allZeroes.ComputeShannonEntropy();
            Assert.Greater(0.1, allZeroesEntropyScore);
        }

        [Test]
        public void Should_Fail_EntropyTest_String()
        {
            const string explicitlyPoorEntropyString = "aaaacc7ccccc7cddddd7deeee7eeeeff";
            var quickBrownFoxTruncated = explicitlyPoorEntropyString.TrimOrExpandToLength(32);
            if (quickBrownFoxTruncated.Length != 32) { Assert.Fail($"Invalid string length {quickBrownFoxTruncated.Length}"); }
            var quickBrownFoxByteArray = Encoding.ASCII.GetBytes(quickBrownFoxTruncated);
            Assert.AreEqual(32, quickBrownFoxByteArray.Length);

            var allZeroesEntropyScore = quickBrownFoxByteArray.ComputeShannonEntropy();
            Assert.Less(2, allZeroesEntropyScore);
        }

        [Test]
        public void Should_Fail_EntropyTest_Digits()
        {
            var lowEntropyByteArray = new byte[] { 1, 2, 3, 4, 6, 56, 1, 126, 56, 1, 126, 56, 1, 126, 6, 6, 1, 56, 0, 1, 126, 56, 1, 56, 56, 1, 2, 126, 1, 2, 126, 1 };

            Assert.AreEqual(32, lowEntropyByteArray.Length);

            var allZeroesEntropyScore = lowEntropyByteArray.ComputeShannonEntropy();
            Assert.Less(2.5, allZeroesEntropyScore);
        }

        [Test]
        public void Should_Pass_EntropyTest_Random()
        {
            var rand = new Random();

            var lowEntropyByteArray = new byte[32];
            for (int i = 0; i < 32; i++)
            {
                lowEntropyByteArray[i] = Convert.ToByte(rand.Next(59, 62));
            }
            
            Assert.AreEqual(32, lowEntropyByteArray.Length);

            var allZeroesEntropyScore = lowEntropyByteArray.ComputeShannonEntropy();
            Assert.Less(allZeroesEntropyScore, 1.59);
        }
    }
}