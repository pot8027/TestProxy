using System;
using System.Runtime.Remoting.Proxies;

namespace ProxyTest
{
    public class MyAspect : ProxyAttribute
    {
        public override MarshalByRefObject CreateInstance(Type serverType)
        {
            var target = base.CreateInstance(serverType);
            var rp = new MyProxy(target, serverType);
            return rp.GetTransparentProxy() as MarshalByRefObject;
        }
    }
}
