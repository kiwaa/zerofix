using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using zerofix.Messages;

namespace zerofix
{
    public class MessageParser
    {
        private const byte Delimeter = 1; 
        private const long FIX_4 = 3329336981254978872L;

        private const int Type_0 = 809317683;
        private const int Type_1 = 826094899;
        private const int Type_2 = 842872115;
        private const int Type_3 = 859649331;
        private const int Type_4 = 876426547;
        private const int Type_5 = 893203763;
        private const int Type_A = 1094530355;

        public Message Parse(ReadOnlySpan<byte> span)
        {
            int total = 0, checksum = 0, read, sum;

            string version = ReadVersion(span, out read, out sum);
            total += read; checksum += sum;

            int length = ReadBodyLength(span.Slice(total), out read);
            total += read;

            char type = ReadType(span.Slice(total), out read);
            total += read;

            switch (type)
            {
                case '0':
                    return new Heartbeat();
                case '1':
                    return new TestRequest();
                case 'A':
                    return new Logon();
                default:
                    throw new NotImplementedException();
            }
        }

        private string ReadVersion(ReadOnlySpan<byte> span, out int read, out int sum)
        {
            string version;
            var prefix = MemoryMarshal.Read<long>(span);
            read = 8;
            if (prefix == FIX_4)
            {
                // read one more byte
                char v = (char)span[read++];
                switch (v)
                {
                    case '2':
                        version = "FIX.4.2";
                        Debug.Assert(span[read] == 1);
                        read++; // delimeter
                        sum = 12; // TODO
                        return version;
                }
            }
            throw new NotImplementedException();
        }
        private int ReadBodyLength(ReadOnlySpan<byte> span, out int read)
        {
            int length;
            length = checked((int)NumberConverter.ReadInt64(span.Slice(2), out read));
            read = read + 3; // 2 + delimeter
           // sum = 12;
            return length;
        }
        private char ReadType(ReadOnlySpan<byte> span, out int read)
        {
            MemoryMarshal.Read<int>(Encoding.UTF8.GetBytes("35=0").AsSpan());

            var type = MemoryMarshal.Read<int>(span);
            read = 5;
            // todo replace with if
            switch (type)
            {
                case Type_0:
                    return '0';
                    break;
                case Type_1:
                    return '1';
                    break;
                case Type_2:
                    return '2';
                    break;
                case Type_3:
                    return '3';
                    break;
                case Type_4:
                    return '4';
                    break;
                case Type_5:
                    return '5';
                    break;
                case Type_A:
                    return 'A'; 
                    break;
            }
            throw new NotImplementedException();
        }
    }
}
