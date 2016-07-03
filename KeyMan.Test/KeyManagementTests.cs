using System;
using System.Linq;
using KeyMan.Common;
using KeyMan.Core;
using NUnit.Framework;

namespace KeyMan.Test
{
    [TestFixture]
    public class KeyManagementTests
    {
        [Test]
        public void Should_Generate_AES_Key()
        {
            var keyIdToCreate = Guid.NewGuid();
            var key = new SymmetricKey();
            using (var keyManager = new KeyManager())
            {
                key.Id = keyIdToCreate;
                key.KeyStringEncoded = keyManager.GenerateSymmetricKeyEncodedString();
            }

            Assert.IsNotNull(key);

            Assert.IsNotEmpty(key.KeyStringEncoded);

            var decodedKeyBytes = Convert.FromBase64String(key.KeyStringEncoded);
            Assert.AreEqual(32, decodedKeyBytes.Length);

            // Basic check to ensure sufficient entropy in key
            Assert.Greater(decodedKeyBytes.Distinct().Count(), 16);

            // Shannon Entropy check to ensure sufficient entropy in key
            var shannonEntropy = decodedKeyBytes.ComputeShannonEntropy();
            Assert.Greater(shannonEntropy, 4.0);
        }

        [Test]
        public void Should_Store_And_Retrieve_Key()
        {
            var key1 = new SymmetricKey();
            using (var keyManager = new KeyManager())
            {
                key1.Id = Guid.NewGuid();
                key1.KeyStringEncoded = keyManager.GenerateSymmetricKeyEncodedString();
            }

            using (var persister = new Persister())
            {
                persister.Save(key1);
                var key1Retrieved = (SymmetricKey)persister.Retrieve(key1.Id);
                Assert.AreEqual(key1.Id, key1Retrieved.Id);
                Assert.AreEqual(key1.KeyStringEncoded, key1Retrieved.KeyStringEncoded);
            }
        }
    }
}
