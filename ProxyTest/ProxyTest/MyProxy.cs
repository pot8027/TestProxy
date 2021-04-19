using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace ProxyTest
{
    public class MyProxy : RealProxy
    {
        private MarshalByRefObject _target;

        public MyProxy(MarshalByRefObject target, Type t) : base(t)
        {
            this._target = target;
        }

        public override IMessage Invoke(IMessage msg)
        {
            var call = (IMethodCallMessage)msg;
            var ctor = call as IConstructionCallMessage;
            
            IMethodReturnMessage res;

            var logAttr = call.MethodBase.GetCustomAttributes(true).FirstOrDefault(r => r.GetType() == typeof(TraceLogAttribute)) as TraceLogAttribute;

            if (ctor != null)
            {
                //以下、コンストラクタを実行する処理

                var rp = RemotingServices.GetRealProxy(this._target);
                res = rp.InitializeServerObject(ctor);
                var tp = this.GetTransparentProxy() as MarshalByRefObject;
                res = EnterpriseServicesHelper.CreateConstructionReturnMessage(ctor, tp);
            }
            else
            {
                //以下、コンストラクタ以外のメソッドを実行する処理

                if (logAttr != null)
                {
                    //メソッド前処理
                    Console.WriteLine("[{0}]{1} : 実行開始",
                        DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), logAttr.Title + "開始");
                }

                //メソッド実行
                res = RemotingServices.ExecuteMessage(this._target, call);
                if (res.Exception != null)
                {
                    throw res.Exception;
                }

                if (logAttr != null)
                {
                    //メソッド後処理
                    Console.WriteLine("[{0}]{1} : 実行終了",
                        DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), logAttr.Title + "終了");
                }
            }

            return res;
        }
    }
}
