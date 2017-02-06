using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BufferService;
using BufferService.Contracts;
using BufferService.Duplex;
using Common.Interfaces;

namespace BufferServiceSelfHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //HistoricalDuplexClient historicalDuplexClient = null;

            Host clientHost = new Host("net.tcp://localhost:40000/BufferService", typeof(IBufferServiceClientContract), typeof(BufferServiceClientContract));
            ////Host historicalHost = new Host("net.tcp://localhost:40001/BufferService", typeof(IBufferServiceHistoricalContract), typeof(BufferServiceHistoricalContract));

            clientHost.Start();
            ////historicalHost.Start();

            //BufferServiceClientContract b = new BufferServiceClientContract();
            //ServiceHost host = new ServiceHost(b);
            //host.AddServiceEndpoint(typeof(IBufferServiceClientContract), new NetTcpBinding(), "net.tcp://localhost:40000/BufferService");

            //host.Open();
            //try
            //{
            //    using (historicalDuplexClient =
            //        new HistoricalDuplexClient(new InstanceContext(new CallbackHandler(b.GetBufferModel)),
            //            new NetTcpBinding(), "net.tcp://10.1.212.121:9999/HistoricalService"))
            //    {
            //        b.GetHistoricalDuplexClient = historicalDuplexClient;
            //        string message = string.Format("The WCF service {0} is ready.", host.Description.Name);
            //        Console.WriteLine(message);

            //        message = "Endpoint on address " + "net.tcp://localhost:40000/BufferService" + "has been created. \nPress enter to exit...";
            //        Console.WriteLine(message);

            //        foreach (Uri uri in host.BaseAddresses)
            //        {
            //            Console.WriteLine(uri);
            //        }

            //        b.GetHistoricalDuplexClient.Connect("nesto", b.GetSystemConfiguration().State.ToString());

            //        Console.WriteLine("\n");
            //        Console.ReadLine();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            
            


        }
    }
}
