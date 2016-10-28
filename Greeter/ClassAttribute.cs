using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Greeter
{    //贴上类的标签
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class ClassAttribute: ContextAttribute, IContributeObjectSink
    {
       public ClassAttribute() : base("Class")
       {
            
       }
        public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink)
       { 
            return new AopHandler(nextSink);
       }
    }
}
