using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zerofix.Messages
{
    public class Heartbeat : Message
    {
        public Heartbeat() : base('0')
        {

        }

        public override byte[] GetBytes()
        {
            throw new NotImplementedException();
        }
    }
}
