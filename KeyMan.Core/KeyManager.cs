using System;
using System.Collections.Generic;
using KeyMan.Common;

namespace KeyMan.Core
{
    public class KeyManager : IDisposable, IKeyManager
    {
        public string GenerateSymmetricKeyEncodedString()
        {
            var binaryKey = GenerateSymmetricKey();
            return Convert.ToBase64String(binaryKey);
        }

        public byte[] GenerateSymmetricKey()
        {
            var createdPassword = string.Concat(Environment.MachineName, Environment.UserDomainName, Environment.TickCount);
            var binaryKey = new byte[32];

            var keyComputationTimeout = 1000;

            while (keyComputationTimeout > 0 && binaryKey.ComputeShannonEntropy() < 4.0)
            {
                binaryKey = CryptoManager.CreateSymmetricKey(createdPassword, 32);
                keyComputationTimeout -= 100;
            }

            if (binaryKey.ComputeShannonEntropy() < 4.0)
                throw new ApplicationException("Not enough entropy in key after multiple tries...");

            return binaryKey;
        }

        public CryptoKeyPair CreateAsymmetricKeyPair()
        {
            return CryptoManager.GenerateKeyPair();
        }

        public void Dispose()
        {
            // TODO: Enforce cleanup of everything
        }
    }
}
