using System;
using Ballance.Kms.Core;

namespace Ballance.Kms.Runner
{
    public class Program
    {
        private static void Main()
        {
            Console.WriteLine("started up");

            CryptoKey key1 = new CryptoKey();
            using (var keyManager = new KeyManager())
            {
                key1.Id = Guid.NewGuid();
                key1.KeyText = keyManager.CreateKeyEncodedString();
            }

            Console.WriteLine($"Generated key [{key1.Id} / {key1.KeyText}]");

            Console.WriteLine($"Persisting key [{key1.Id}]");

            using (var persister = new Persister())
            {
                persister.Save(key1);
                Console.WriteLine("Persisted key");
                var key1Retrieved = (CryptoKey)persister.Retrieve(key1.Id);

                Console.WriteLine($"Retreieved key [{key1Retrieved.Id} / {key1Retrieved.KeyText}]");
            }

            Console.WriteLine("completed run.");
            Console.ReadKey();

        }
    }
}