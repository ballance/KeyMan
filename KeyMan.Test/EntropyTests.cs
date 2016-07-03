using Ballance.Kms.Common;
using Ballance.Kms.Core;
using NUnit.Framework;

namespace Ballance.Kms.Test
{
    public class EntropyTests
    {
        [Test]
        public void Should_Fail_EntropyTest_All_Zeroes()
        {
            byte[] allZeroes = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Assert.AreEqual(32, allZeroes.Length);

            var allZeroesEntropyScore = KeyManager.ComputeShannonEntropy(allZeroes);
            Assert.Greater(0.1, allZeroesEntropyScore);
        }

        [Test]
        public void Should_Fail_EntropyTest()
        {
            var explicitlyPoorEntropyString = "aaaacc7ccccc7cddddd7deeee7eeeeff";
            var quickBrownFoxTruncated = explicitlyPoorEntropyString.TrimOrExpandToLength(32);
            if (quickBrownFoxTruncated.Length != 32) { Assert.Fail($"Invalid string length {quickBrownFoxTruncated.Length}"); }
            var quickBrownFoxByteArray = System.Text.Encoding.ASCII.GetBytes(quickBrownFoxTruncated);
            Assert.AreEqual(32, quickBrownFoxByteArray.Length);

            var allZeroesEntropyScore = KeyManager.ComputeShannonEntropy(quickBrownFoxByteArray);
            Assert.Less(2, allZeroesEntropyScore);
        }
    }
}