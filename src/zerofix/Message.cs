using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zerofix
{
    public abstract class Message
    {
        public const char Delimeter = '\x0001';
        protected Dictionary<int, string> _map = new Dictionary<int, string>();
        public char Type { get; init; }
        public Message(char type)
        {
            Type = type;
        }

        public abstract byte[] GetBytes();

        public int GetCheckSum(byte[] bytes)
        {
            var total = 0;
            foreach (var b in bytes)
                total += b;
            return total % 256;
        }
    }
}
