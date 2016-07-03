using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ballance.Kms.Core
{
    public interface ICryptoKey
    {
        Guid Id { get; set; }
        string KeyText { get; set; }
    }

    public class CryptoKey : ICryptoKey
    {
        public Guid Id { get; set; }

        public string KeyText { get; set; }
    }
}
