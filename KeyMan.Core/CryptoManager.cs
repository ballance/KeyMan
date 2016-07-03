using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace KeyMan.Core
{
    public class CryptoManager
    {
        public static CryptoKeyPair GenerateKeyPair(int keySize = 1024)
        {
            var cryptoKeyPair = new CryptoKeyPair();

            var rsaProvider = new RSACryptoServiceProvider(keySize, new CspParameters(1));

            cryptoKeyPair.PublicKeyEncodedString = Convert.ToBase64String(rsaProvider.ExportCspBlob(false));
            cryptoKeyPair.PrivateKeyEncodedString = Convert.ToBase64String(rsaProvider.ExportCspBlob(true));

            return cryptoKeyPair;
        }

        public static byte[] GenerateSalt(int saltLength)
        {
            var salt = new byte[saltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }

        public static byte[] CreateSymmetricKey(string password, int keyBytes = 32, int iterations = 300)
        {
            var keyGenerator = new Rfc2898DeriveBytes(password, CryptoManager.GenerateSalt(32), iterations);
            return keyGenerator.GetBytes(keyBytes);
        }

   }
}