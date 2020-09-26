using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zerofix.Messages
{
    public class Logon : Message
    {

        public Logon() : base('A')
        {
            //_map.Add(8, "FIX.4.2");
            _map.Add(35, "A");
            _map.Add(34, "1");
            _map.Add(49, "CLIENT1");
            _map.Add(56, "EXECUTOR");
            _map.Add(52, DateTime.UtcNow.ToString("yyyyMMdd-hh:mm:ss"));
            _map.Add(98, "0");
            _map.Add(108, "30");
            //9=33
            //10=108
        }

        public override byte[] GetBytes()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var pair in _map)
            {
                sb.Append(pair.Key)
                    .Append('=')
                    .Append(pair.Value)
                    .Append(Delimeter);
            }
            var length = sb.Length;
            sb.Insert(0, "8=FIX.4.2" + Delimeter + "9=" + length + Delimeter);
            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            var checksum = GetCheckSum(bytes);

            sb.Append("10=")
                .Append(checksum)
                .Append(Delimeter);
            return Encoding.UTF8.GetBytes(sb.ToString());
        }
    }
}
