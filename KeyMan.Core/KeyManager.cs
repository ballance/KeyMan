using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ballance.Kms.Core
{
    public class KeyManager : IDisposable, IKeyManager
    {
        public string CreateKeyEncodedString()
        {
            var createdPassword = String.Concat(Environment.MachineName, Environment.UserDomainName, Environment.TickCount);
            var binaryKey = CreateKey(createdPassword, 32);
            return Convert.ToBase64String(binaryKey);
        }
        private static byte[] CreateKey(string password, int keyBytes = 32)
        {
            const int Iterations = 300;
            var keyGenerator = new Rfc2898DeriveBytes(password, GetSalt(32), Iterations);
            return keyGenerator.GetBytes(keyBytes);
        }

        private static byte[] GetSalt(int maximumSaltLength)
        {
            var salt = new byte[maximumSaltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }

        public void Dispose()
        {
            // TODO: Enforce cleanup of everything
        }

        public static double ComputeShannonEntropy(byte[] candidateBytes)
        {
            var map = new Dictionary<byte, int>();
            foreach (var foundByte in candidateBytes)
            {
                if (!map.ContainsKey(foundByte))
                    map.Add(foundByte, 1);
                else
                    map[foundByte] += 1;
            }

            var result = 0.0;
            int len = candidateBytes.Length;
            foreach (var item in map)
            {
                var frequency = (double)item.Value / len;
                result -= frequency * (Math.Log(frequency) / Math.Log(2));
            }

            return result;
        }
    }
}
