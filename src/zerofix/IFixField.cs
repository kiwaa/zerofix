using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zerofix
{
    public interface IFixField
    {
        public int Tag { get; }
        public Type Type { get; }

        public int ByteLength { get; }

        string AsString();
    }
}
