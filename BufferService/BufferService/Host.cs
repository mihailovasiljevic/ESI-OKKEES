using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BufferService
{
    public class Host : IDisposable
    {
        private string address = string.Empty;
        private NetTcpBinding binding;
        private Type endPointType;
        private Type serviceType;
        private ServiceHost host;

        public Host(string address, Type endPointType, Type serviceType)
        {
            this.address = address;
            this.binding = new NetTcpBinding();
            this.endPointType = endPointType;
            this.serviceType = serviceType;

            InitializeHost();
        }

        public void Start()
        {

            if (host == null)
            {
                throw new Exception("Buffer service nije startovan na odgovorajacu nacin. Host objekat je null!");
            }

            host.Open();

            string message = string.Format("The WCF service {0} is ready.", host.Description.Name);
            Console.WriteLine(message);

            message = "Endpoint on address " + address + "has been created. \nPress enter to exit...";
            Console.WriteLine(message);

            foreach (Uri uri in host.BaseAddresses)
            {
                Console.WriteLine(uri);
            }

            Console.WriteLine("\n");
            Console.ReadLine();
        }

        public void Dispose()
        {
            CloseHosts();
            GC.SuppressFinalize(this);
        }

        ~Host()
        {
            CloseHosts();
        }

        private void InitializeHost()
        {

            host = new ServiceHost(serviceType);
            host.AddServiceEndpoint(endPointType, binding, address);
        }

        private void CloseHosts()
        {

            if (host == null)
            {
                throw new Exception("Host objekat je null!");
            }

            host.Close();
        }

    }
}
