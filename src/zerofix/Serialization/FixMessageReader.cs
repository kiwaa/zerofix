using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace zerofix
{
    public class FixMessageReader
    {
        private const byte Delimeter = 1; 
        private const long FIX_4_x = 3329336981254978872L;

        private const int Type_0 = 809317683;
        private const int Type_1 = 826094899;
        private const int Type_2 = 842872115;
        private const int Type_3 = 859649331;
        private const int Type_4 = 876426547;
        private const int Type_5 = 893203763;
        private const int Type_A = 1094530355;
        private const int Type_D = 1144862003;
        private const int Type_j = 1782396211;

        public FixMessage Parse(ReadOnlySpan<byte> span)
        {
            int total = 0, body = 0, read, sum;

            var dict = new FixFieldDictionary();

            // 1st field; tag 8
            var version = ReadVersion(span, out read) as FixField<string>;
            total += read;
            dict.Add(version);

            // 2nd field; tag 9
            var length = ReadBodyLength(span.Slice(total), out read) as FixField<int>;
            total += read;
            dict.Add(length);

            // 3rd field; tag 35
            var type = ReadType(span.Slice(total), out read) as FixField<char>;
            total += read; body += read;
            dict.Add(type);

            while (body < length.Value)
            {
                var field = ReadField(span.Slice(total), out read);
                total += read; body += read;
                dict.Add(field);
            }

            return new FixMessage(dict);
        }

        private IFixField ReadVersion(ReadOnlySpan<byte> span, out int read)
        {
            string version;
            var prefix = MemoryMarshal.Read<long>(span);
            read = 8;
            if (prefix == FIX_4_x)
            {
                // read one more byte
                char v = (char)span[read++];
                switch (v)
                {
                    case '2':
                        version = "FIX.4.2";
                        Debug.Assert(span[read] == 1);
                        read++; // delimeter
                        return new FixField<string>(8, version);
                }
            }
            throw new NotImplementedException();
        }
        private IFixField ReadBodyLength(ReadOnlySpan<byte> span, out int read)
        {
            int length;
            length = checked((int)NumberConverter.ReadInt64(span.Slice(2), out read));
            read = read + 3; // 2 + delimeter
            // sum = 12;
            return new FixField<int>(9, length);
        }
        private IFixField ReadType(ReadOnlySpan<byte> span, out int read)
        {
            var type = MemoryMarshal.Read<int>(span);
            read = 5;
            // todo replace with if
            switch (type)
            {
                case Type_0:
                    return new FixField<char>(35, '0');
                case Type_1:
                    return new FixField<char>(35, '1');
                case Type_2:
                    return new FixField<char>(35, '2');
                case Type_3:
                    return new FixField<char>(35, '3');
                case Type_4:
                    return new FixField<char>(35, '4');
                case Type_5:
                    return new FixField<char>(35, '5');
                case Type_A:
                    return new FixField<char>(35, 'A');
                case Type_D:
                    return new FixField<char>(35, 'D');
                case Type_j:
                    return new FixField<char>(35, 'j');
                    
            }
            throw new NotImplementedException();
        }
        private IFixField ReadField(ReadOnlySpan<byte> span, out int read)
        {
            var tag = checked((int)NumberConverter.ReadInt64(span, out int tagRead));
            var value = ReadString(span.Slice(tagRead + 1), out int valueRead);
            read = tagRead + valueRead + 2;
            return new FixField<string>(tag, value);
        }

        private string ReadString(ReadOnlySpan<byte> span, out int read)
        {
            // naive
            for (int i =0; i < span.Length; i++)
            {
                if (span[i] == Delimeter)
                {
                    read = i;
                    return Encoding.UTF8.GetString(span.Slice(0, i));
                }
            }
            throw new NotImplementedException();
        }
    }
}
