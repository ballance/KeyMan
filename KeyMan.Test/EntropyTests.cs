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
            byte[] allZeroes = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Assert.AreEqual(32, allZeroes.Length);

            var allZeroesEntropyScore = allZeroes.ComputeShannonEntropy();
            Assert.Greater(0.1, allZeroesEntropyScore);
        }

        [Test]
        public void Should_Fail_EntropyTest()
        {
            const string explicitlyPoorEntropyString = "aaaacc7ccccc7cddddd7deeee7eeeeff";
            var quickBrownFoxTruncated = explicitlyPoorEntropyString.TrimOrExpandToLength(32);
            if (quickBrownFoxTruncated.Length != 32) { Assert.Fail($"Invalid string length {quickBrownFoxTruncated.Length}"); }
            var quickBrownFoxByteArray = Encoding.ASCII.GetBytes(quickBrownFoxTruncated);
            Assert.AreEqual(32, quickBrownFoxByteArray.Length);

            var allZeroesEntropyScore = quickBrownFoxByteArray.ComputeShannonEntropy();
            Assert.Less(2, allZeroesEntropyScore);
        }
    }
}