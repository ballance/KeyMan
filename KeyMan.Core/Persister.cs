using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Ballance.Kms.Core
{
    public class Persister : IDisposable
    {
        public CryptoKey Retrieve(Guid id)
        {
            // Check authorization for caller
            // TODO: make DAL generic to support LiteDB + others
            using (var db = new LiteDatabase(@"Keys.db"))
            {
                try
                {
                    var key = db.GetCollection<CryptoKey>("CryptoKeys");
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

        public void Save(CryptoKey cryptoKey)
        {
            // Check authorization for caller
            // TODO: make DAL generic to support LiteDB + others
            using (var db = new LiteDatabase(@"Keys.db"))
            {
                try
                {
                    var keyCollection = db.GetCollection<CryptoKey> ("CryptoKeys");
                    keyCollection.Insert(cryptoKey);
                }
                catch (Exception)
                {
                    Console.WriteLine($"Failed to save  key [{cryptoKey.Id}]");
                }
            }
        }

        public void Dispose()
        {
            // TODO: Properly dispose here
        }
    }
}
