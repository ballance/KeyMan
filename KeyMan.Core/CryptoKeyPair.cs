using System;

namespace KeyMan.Core
{
    public class CryptoKeyPair
    {
        public Guid Id { get; set; }
        public string PublicKeyEncodedString { get; set; }

        public string PrivateKeyEncodedString { get; set; }

        public void SetFullKey(CryptoKeyPair generatedKey)
        {
            PublicKeyEncodedString = generatedKey.PublicKeyEncodedString;
            PrivateKeyEncodedString = generatedKey.PrivateKeyEncodedString;
        }
    }
}