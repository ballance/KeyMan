using System;
using KeyMan.Core;

namespace KeyMan.Runner
{
    public class Program
    {
        private static void Main()
        {
            Console.WriteLine("started up");

            var key1 = new SymmetricKey();
            using (var keyManager = new KeyManager())
            {
                key1.Id = Guid.NewGuid();
                key1.KeyStringEncoded = keyManager.GenerateSymmetricKeyEncodedString();
            }

            Console.WriteLine($"Generated key [{key1.Id} / {key1.KeyStringEncoded}]");

            Console.WriteLine($"Persisting key [{key1.Id}]");

            using (var persister = new Persister())
            {
                persister.Save(key1);
                Console.WriteLine("Persisted key");
                var key1Retrieved = persister.Retrieve(key1.Id);

                Console.WriteLine($"Retreieved key [{key1Retrieved.Id} / {key1Retrieved.KeyStringEncoded}]");
            }

            Console.WriteLine("completed run.");
            Console.ReadKey();

        }
    }
}