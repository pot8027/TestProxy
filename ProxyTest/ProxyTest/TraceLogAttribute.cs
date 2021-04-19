using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyTest
{
    public class TraceLogAttribute : Attribute
    {
        private string _executeTitle = string.Empty;

        public string Title { get { return _executeTitle; } }

        public TraceLogAttribute(string executeTitle)
        {
            _executeTitle = executeTitle;
        }
    }
}
