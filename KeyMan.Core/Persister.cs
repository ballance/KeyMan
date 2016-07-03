using System;
using System.Linq;
using LiteDB;

namespace KeyMan.Core
{
    public class Persister : IDisposable
    {
        public SymmetricKey Retrieve(Guid id)
        {
            // Check authorization for caller
            // TODO: make DAL generic to support LiteDB + others
            using (var db = new LiteDatabase(@"Keys.db"))
            {
                try
                {
                    var key = db.GetCollection<SymmetricKey>("CryptoKeys");
                    var foundKeyCollection = key.Find(b => b.Id.Equals(id));
                    if (foundKeyCollection.Any())
                    {
                        if (foundKeyCollection != null)
                        {
                            var foundKey = foundKeyCollection.Single();
                            return foundKey;
                        }

                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"Failed to retrieve existing key [{id}]");
                }

                throw new ApplicationException($"Key not found in db for id [{id}]");
            }
        }

        public void Save(SymmetricKey symmetricKey)
        {
            // Check authorization for caller
            // TODO: make DAL generic to support LiteDB + others
            using (var db = new LiteDatabase(@"Keys.db"))
            {
                try
                {
                    var keyCollection = db.GetCollection<SymmetricKey> ("CryptoKeys");
                    keyCollection.Insert(symmetricKey);
                }
                catch (Exception)
                {
                    Console.WriteLine($"Failed to save  key [{symmetricKey.Id}]");
                }
            }
        }

        public void Dispose()
        {
            // TODO: Properly dispose here
        }
    }
}
