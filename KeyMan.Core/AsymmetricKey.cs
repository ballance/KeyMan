using System;

namespace KeyMan.Core
{
    public class AsymmetricKey
    {
        public Guid Id { get; set; }

        public string PublicKeyStringEncoded { get; set; }
        public string PrivateKeyStringEncoded { get; set; }
    }
}