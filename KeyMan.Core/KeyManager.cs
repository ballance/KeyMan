using System;
using System.Collections.Generic;

namespace KeyMan.Core
{
    public class KeyManager : IDisposable, IKeyManager
    {
        public string GenerateSymmetricKeyEncodedString()
        {
            var createdPassword = string.Concat(Environment.MachineName, Environment.UserDomainName, Environment.TickCount);
            var binaryKey = CryptoManager.CreateSymmetricKey(createdPassword, 32);
            return Convert.ToBase64String(binaryKey);
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

    public class CryptoKeyPair
    {
        public string PublicKeyEncodedString { get; set; }

        public string PrivateKeyEncodedString { get; set; }
    }
}
