using System;
using System.Collections.Generic;
using System.Linq;
using Ballance.Kms.Core;
using NUnit.Framework;

namespace Ballance.Kms.Test
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void Should_Generate_AES_Key()
        {
            var keyIdToCreate = Guid.NewGuid();
            var key = new CryptoKey();
            using (var keyManager = new KeyManager())
            {
                key.Id = keyIdToCreate;
                key.KeyText = keyManager.CreateKeyEncodedString();
            }

            Assert.IsNotNull(key);

            Assert.IsNotEmpty(key.KeyText);

            var decodedKeyBytes = Convert.FromBase64String(key.KeyText);
            Assert.AreEqual(32, decodedKeyBytes.Length);

            // Ensure sufficient entropy in key
            Assert.Greater(decodedKeyBytes.Distinct().Count(), 16);

            var shannonEntropy = KeyManager.ComputeShannonEntropy(decodedKeyBytes);
            Assert.Greater(shannonEntropy, 2.0);
        }

        [Test]
        public void Should_Fail_EntropyTest_All_Zeroes()
        {
            byte[] allZeroes = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Assert.AreEqual(32, allZeroes.Length);

            var allZeroesEntropyScore = KeyManager.ComputeShannonEntropy(allZeroes);
            Assert.Greater(0.1, allZeroesEntropyScore);
        }

        [Test]
        public void Should_Fail_EntropyTest_Quick_Brown_Fox()
        {
            var quickBrownFox = "aaaaaaaaaaaabbbbbbbbbbccccccccdddddddeeeeeeeefffffff";
            var quickBrownFoxTruncated = quickBrownFox.TrimOrExpandToLength(32);
            if (quickBrownFoxTruncated.Length != 32) { Assert.Fail($"Invalid string length {quickBrownFoxTruncated.Length}");}
            var quickBrownFoxByteArray = System.Text.Encoding.ASCII.GetBytes(quickBrownFoxTruncated);
            Assert.AreEqual(32, quickBrownFoxByteArray.Length);

            var allZeroesEntropyScore = KeyManager.ComputeShannonEntropy(quickBrownFoxByteArray);
            Assert.Greater(2, allZeroesEntropyScore);
        }
        
        [Test]
        public void Should_Store_And_Retrieve_Key()
        {
            var key1 = new CryptoKey();
            using (var keyManager = new KeyManager())
            {
                key1.Id = Guid.NewGuid();
                key1.KeyText = keyManager.CreateKeyEncodedString();
            }

            using (var persister = new Persister())
            {
                persister.Save(key1);
                var key1Retrieved = (CryptoKey)persister.Retrieve(key1.Id);
                Assert.AreEqual(key1.Id, key1Retrieved.Id);
                Assert.AreEqual(key1.KeyText, key1Retrieved.KeyText);
            }
        }
    }

    public static class StringExtention
    {
        public static string TrimOrExpandToLength(this string stringToTrim, int desiredLength = 32)
        {
            var quickBrownFoxTruncated = string.Empty;
            if (stringToTrim.Length < desiredLength)
            {   
                quickBrownFoxTruncated = string.Concat(stringToTrim, new string(' ', stringToTrim.Length - desiredLength));
            }
            if (stringToTrim.Length > desiredLength)
            {
                quickBrownFoxTruncated = stringToTrim.Substring(0, desiredLength);
            }
            return quickBrownFoxTruncated;
        }
    }
}
