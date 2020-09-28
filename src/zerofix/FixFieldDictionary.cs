using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zerofix
{
    public class FixFieldDictionary : IEnumerable<IFixField>
    {
        private Dictionary<int, IFixField> _map = new Dictionary<int, IFixField>();

        public IFixField this[int tag] => _map[tag];

        public void Add(int key, string value)
        {
            _map[key] = new FixField<string>(key, value);
        }
        public void Add(int key, int value)
        {
            _map[key] = new FixField<int>(key, value);
        }
        public void Add(IFixField field)
        {
            _map[field.Tag] = field;
        }

        public int GetBodyLength()
        {
            // naive
            var body = 0;
            foreach (var pair in _map)
            {
                if (pair.Key == 8 || pair.Key == 9)
                    continue;
                body += pair.Value.ByteLength;
            }
            return body;
        }

        public IEnumerator<IFixField> GetEnumerator()
        {
            return _map.Values.GetEnumerator();
        }

        public int GetSum()
        {
            // naive
            var total = 0;
            var sb = new StringBuilder();
            ToString(sb, '\u0001');
            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return bytes.Sum(x => (int)x);
        }

        public void ToString(StringBuilder sb, char delimeter)
        {
            foreach (var pair in _map)
            {
                sb.Append(pair.Key)
                    .Append('=')
                    .Append(pair.Value.AsString())
                    .Append(delimeter);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
