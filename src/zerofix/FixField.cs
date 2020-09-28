using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zerofix
{
    public class FixField<T> : IFixField
    {
        public int Tag { get; init; }
        public T Value { get; init; }

        public Type Type { get; init; }

        public int ByteLength => 2 + Tag.ToString().Length + Value.ToString().Length;

        public FixField(int tag, T value)
        {
            Type = typeof(T);
            Tag = tag;
            Value = value;
        }

        public string AsString()
        {
            return Value.ToString();
        }

        public override string ToString()
        {
            return $"{Tag}={Value}";
        }
    }
}
