using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zerofix.Messages
{
    public class TestRequest : Message
    {
        public TestRequest() : base('1')
        {

        }
        public override byte[] GetBytes()
        {
            throw new NotImplementedException();
        }
    }
}
