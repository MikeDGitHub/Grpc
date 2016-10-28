using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Greeter;
namespace GrpcService
{
    class Program
    {
        const int Port = 50051;
        const int sslPort = 50052;
        public static void Main(string[] args)
        {
            //var cacert = File.ReadAllText(@"C:\GrpcService\key\ca.crt");
            //var servercert = File.ReadAllText(@"C:\GrpcService\key\server.crt");
            //var serverkey = File.ReadAllText(@"C:\GrpcService\key\server.key");
            //var keypair = new KeyCertificatePair(servercert, serverkey);

            //var sslCredentials = new SslServerCredentials(new List<KeyCertificatePair>() { keypair }, cacert, false);

            Server server = new Server
            {
                Services = { IMService.BindService(new GreeterImpl()) },
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
                //Ports = { new ServerPort("wannian-PC", sslPort, sslCredentials) }
            };
            server.Start();

            Console.WriteLine("Greeter server listening on port " + Port);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}
