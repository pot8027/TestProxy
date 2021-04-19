using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyTest
{
    //[MyAspect]
    public class Sample : ContextBoundObject
    {
        public Sample()
        {

        }

        [TraceLog("すぎの")]
        public void SampleMethod1()
        {
            throw new Exception("てすと");
        }

        public void SampleMethod2()
        {

        }
    }
}
