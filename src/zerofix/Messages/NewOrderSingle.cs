using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zerofix.Messages
{
    public class NewOrderSingle : Message
    {
        public NewOrderSingle() : base('D')
        {

        }
        public override byte[] GetBytes()
        {
            throw new NotImplementedException();
        }
    }
}
