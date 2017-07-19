using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Playground.AI
{
    public class Percept
    {
        private IDictionary<object, object> attributes = new Dictionary<object, object>();

        public void AddAttribute(object key, object value)
        {
            attributes[key] = value;
        }

        public object GetAttribute(object key)
        {
            return attributes[key];
        }
    }
}
