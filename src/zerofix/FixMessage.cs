using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace zerofix
{
    public class FixMessage : FixFieldDictionary
    {
        public const char Delimeter = '\x0001';
        private HashSet<int> _headerTags = new HashSet<int>(new[]
        {
            8, 9, 35, 49, 52, 56
        });

        public FixFieldDictionary Header { get; init; }
        public FixFieldDictionary Trailer { get; init; }

        public FixMessage(SessionIdentity sessionId, char type)
        {
            Header = new FixFieldDictionary();
            Trailer = new FixFieldDictionary();

            Header.Add(Tags.BeginString, sessionId.BeginString);
            Header.Add(Tags.BodyLength, string.Empty);
            Header.Add(Tags.MsgType, type.ToString());
            Header.Add(Tags.MsgSeqNum, string.Empty);
            Header.Add(Tags.SenderCompID, sessionId.SenderCompID);
            Header.Add(Tags.TargetCompID, sessionId.TargetCompID);
        }

        public FixMessage(FixFieldDictionary dictionary)
        {
            Header = new FixFieldDictionary();
            Trailer = new FixFieldDictionary();

            foreach (var field in dictionary)
            {
                if (IsHeaderTag(field.Tag))
                {
                    Header.Add(field);
                }
                else
                {
                    Add(field);
                }
            }
        }

        private bool IsHeaderTag(int tag)
        {
            return _headerTags.Contains(tag);
        }

        // todo: too naive, do it better
        public byte[] ToByteArray()
        {
            return Encoding.UTF8.GetBytes(ToString());
        }

        public override string ToString()
        {
            Header.Add(Tags.BodyLength, Header.CalculateBodyLength() + CalculateBodyLength());
            var sum = Header.GetSum() + GetSum();
            Trailer.Add(Tags.CheckSum, (sum % 256).ToString("000"));

            var sb = new StringBuilder();
            Header.ToString(sb, Delimeter);
            ToString(sb, Delimeter);
            Trailer.ToString(sb, Delimeter);
            return sb.ToString();
        }

        public int GetCheckSum(byte[] bytes)
        {
            var total = 0;
            foreach (var b in bytes)
                total += b;
            return total % 256;
        }
    }
}
