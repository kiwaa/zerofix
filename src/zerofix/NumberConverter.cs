using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zerofix
{
    internal class NumberConverter
    {
        public static long ReadInt64(ReadOnlySpan<byte> bytes, out int readCount)
        {
            var indx = 0;
            var value = 0L;
            var sign = 1;

            if (bytes[indx] == '-')
            {
                sign = -1;
            }

            for (int i = ((sign == -1) ? indx + 1 : indx); i < bytes.Length; i++)
            {
                if (!IsNumber(bytes[i]))
                {
                    readCount = i - indx;
                    goto END;
                }

                // long.MinValue causes overflow so use unchecked.
                value = unchecked(value * 10 + (bytes[i] - '0'));
            }
            readCount = bytes.Length - indx;

            END:
            return unchecked(value * sign);
        }
        public static bool IsNumber(byte c)
        {
            return (byte)'0' <= c && c <= (byte)'9';
        }
    }
}
