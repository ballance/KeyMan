using System;
using System.Collections.Generic;
using System.Linq;

namespace KeyMan.Common
{
    public static class ByteArrayExtentions
    {
        public static double ComputeShannonEntropy(this byte[] byteArray)
        {
            var map = new Dictionary<byte, int>();
            foreach (var foundByte in byteArray)
            {
                if (!map.ContainsKey(foundByte))
                    map.Add(foundByte, 1);
                else
                    map[foundByte] += 1;
            }

            return map.Select(item => (double)item.Value / byteArray.Length)
                .Aggregate(0.0, (current, frequency) => current - frequency * (Math.Log(frequency) / Math.Log(2)));
        }
    }
}