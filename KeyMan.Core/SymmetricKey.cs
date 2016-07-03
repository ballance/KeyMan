using System;
using System.Collections.Generic;
using System.Linq;

namespace KeyMan.Core
{
    public interface ISymmetricKey
    {
        Guid Id { get; set; }
        string KeyStringEncoded { get; set; }
    }

    public class SymmetricKey : ISymmetricKey
    {
        public Guid Id { get; set; }

        public string KeyStringEncoded { get; set; }
    }
}
