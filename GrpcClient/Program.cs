using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Greeter;

namespace GrpcClient
{
    class Program
    {
        const int Port = 50051;
        const int sslPort = 50052;
        static void Main(string[] args)
        {
            //var cacert = File.ReadAllText(@"C:\GrpcService\key\ca.crt");
            //var clientcert = File.ReadAllText(@"C:\GrpcService\key\client.crt");
            //var clientkey = File.ReadAllText(@"C:\GrpcService\key\client.key");
            //var ssl = new SslCredentials(cacert, new KeyCertificatePair(clientcert, clientkey));
            //var channel = new Channel("wannian-PC", sslPort, ssl);

            var channel = new Channel("localhost",Port, ChannelCredentials.Insecure);
           
            var client = new IMService.IMServiceClient(channel);
            //String user = "you";
            Metadata headers =new Metadata();
            headers.Add("content-type", "application/json");
            headers.Add("Authorization", "Bearer 76c66a99749e4b8082d687959c39dac3");
            try
            {
                var reply = client.Create(new CreateGroup(), headers);
                Console.WriteLine("Greeting: " + reply);
            }
            catch (RpcException rpc)
            {
                Console.WriteLine("Status:" + rpc.Status);
            }
           

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
