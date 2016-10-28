using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;

namespace Greeter
{
    //AOP方法处理类,实现了IMessageSink接口,以便返回给IContributeObjectSink接口的GetObjectSink方法
    public sealed class AopHandler : IMessageSink
    {
        //下一个接收器
        private IMessageSink nextSink;

        public AopHandler(IMessageSink nextSink)
        {
            this.nextSink = nextSink;
        }

        public IMessageSink NextSink
        {
            get { return nextSink; }
        }

        //同步处理方法
        public IMessage SyncProcessMessage(IMessage msg)
        {

            IMessage message = null;
            ServerCallContext context = null;
            //方法调用接口
            IMethodCallMessage callMessage = msg as IMethodCallMessage;
            for (int i = 0; i < callMessage.InArgCount; i++)
            {
                var item = callMessage.GetArg(i);
                if (item.GetType() == typeof(ServerCallContext))
                {
                    context = item as ServerCallContext;
                    break;
                }
            }
            if (Attribute.GetCustomAttribute(callMessage.MethodBase, typeof(MethodAttribute)) != null)
            {
                var headers = context.RequestHeaders;
                var accessToken = "";
                foreach (var item in headers.Where(item => item.Key == "authorization"))
                {
                    accessToken = item.Value.Split(' ')[1];
                    break;
                }
                var checkToken = !string.IsNullOrEmpty(accessToken);
                if (checkToken)
                {

                    message = nextSink.SyncProcessMessage(msg);
                }
                else
                {
                    RpcException e = new RpcException(new Status(StatusCode.Unauthenticated, "Unauthenticated"));
                    message = new ReturnMessage(e, (IMethodCallMessage)msg);
                }
            }
            else
            {
                message = nextSink.SyncProcessMessage(msg);
            }
            //如果被调用的方法没打MyCalculatorMethodAttribute标签
            //if (callMessage == null || (Attribute.GetCustomAttribute(callMessage.MethodBase, typeof(MethodAttribute))) == null)
            //{
            //    message = nextSink.SyncProcessMessage(msg);
            //}
            //else
            //{
            //    PreProceed(msg);
            //    message = nextSink.SyncProcessMessage(msg);
            //    PostProceed(message);
            //}

            return message;
        }
        //异步处理方法
        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            Console.WriteLine("异步处理方法...");
            return null;
        }

        //方法执行前
        public void PreProceed(IMessage msg)
        {
            IMethodMessage message = (IMethodMessage)msg;
            Console.WriteLine("New Method Start...");
            Console.WriteLine("This Method Is {0}", message.MethodName);
            Console.WriteLine("This Method A Total of {0} Parameters Including:", message.ArgCount);
            for (int i = 0; i < message.ArgCount; i++)
            {
                Console.WriteLine("Parameter{0}：The Args Is {1}.", i + 1, message.Args[i]);
            }
        }

        //方法执行后
        public void PostProceed(IMessage msg)
        {
            IMethodReturnMessage message = (IMethodReturnMessage)msg;

            Console.WriteLine("The Return Value Of This Method Is {0}", message.ReturnValue);
            Console.WriteLine("Method End\n");
        }
    }
}
