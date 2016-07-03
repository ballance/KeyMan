using System;
using System.Linq;
using Ballance.Kms.Core;
using NUnit.Framework;

namespace Ballance.Kms.Test
{
    [TestFixture]
    public class KeyManagementTests
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
            Assert.Greater(shannonEntropy, 4.0);
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
}
