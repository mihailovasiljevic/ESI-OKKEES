using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Common;
using System.Threading;
using Common.Model;

namespace BufferClient
{
     public class Program
    {
         private static double pMin = -1;
         private static double pMax = -1;
         private static int deadband = -1;
         private static bool threadWorks = true;

         public static void Main(string[] args)
         {
             NetTcpBinding binding = new NetTcpBinding();

             string address = "net.tcp://localhost:40000/BufferService";
             //string address = EnterIpAddressAndPort();

             try
             {
                 IClientFactory clientFactory = new RealFactory(binding, address);
                 using (Client proxy = new Client(binding, address, clientFactory))
                 {
                     EnterDeadBandandValues(proxy);
                     
                     Console.Clear();

                     Thread oThread = new Thread(() => MeasureDumpingValue(proxy));
                     oThread.Start();

                     ClientMenu(proxy);
                     oThread.Join();
                 }
             }
             catch (Exception e)
             {
                 Console.WriteLine(e);
                 return;
             }
         }

         public static void ClientMenu(Client proxy)
         {
             int izbor = -1;
             bool a = true;
             do
             {
                 Console.WriteLine("Izaberite opciju:\n");
                 Console.WriteLine("1.Procitaj trenutnu konfiguraciju sistema");
                 Console.WriteLine("2.Procitaj sve vrednosti");
                 Console.WriteLine("3.Statistika rada bufera");
                 Console.WriteLine("4.Promena stanja servisa");
                 Console.WriteLine("5.Povezivanje sa historical-om");
                 Console.WriteLine("6.Izlaz");
                 try
                 {
                     izbor = Int32.Parse(Console.ReadLine());
                     a = true;
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine("Izabrali ste nepostojecu opciju!\n" + ex.Message);
                     a = false;
                 }
             }
             while (izbor < 1 || izbor > 6 || !a);

             if (izbor == 1)
             {
                 SystemConfiguration sc = proxy.GetSystemConfiguration();
                 ShowSystemConfiguration(sc);
                 
                 ClientMenu(proxy);
             }
             else if (izbor == 2)
             {
                 Dictionary<string, Dictionary<string, double>> dict = new Dictionary<string, Dictionary<string, double>>();
                 dict = proxy.GetData(DateTime.MinValue, DateTime.MaxValue);
                 Console.Clear();
                 Console.WriteLine("************Dumping Values************");
                 ShowAllDumpingValue(dict);
                 Console.WriteLine("**************************************\n");
                 ClientMenu(proxy);
             }
             else if (izbor == 3)
             {
                 Dictionary<string, Dictionary<string, double>> dict = new Dictionary<string, Dictionary<string, double>>();
                 DateTime[] dates = new DateTime[2];
                 dates = BufferStatisticsMenu();

                 dict = proxy.GetData(dates[0], dates[1]);
                 Console.Clear();
                 Console.WriteLine("***********Buffer Statistics***********");
                 ShowAllDumpingValue(dict);
                 Console.WriteLine("***************************************\n");
                 ClientMenu(proxy);
             }
             else if (izbor == 4)
             {
                 Console.Clear();
                 Console.WriteLine("************Change state************");
                 ChangeStateMenu(proxy);
                 ClientMenu(proxy);
             }
             else if (izbor == 5)
             {
                 Console.Clear();
                 string endpointAddress = EnterIpAddressAndPort();
                 proxy.ConnectToHistorical(endpointAddress);
                 ClientMenu(proxy);
             }

             else if (izbor == 6)
             {
                 threadWorks = false;
                 return;
             }
             else
             {
                 ClientMenu(proxy);
             }
         }

         public static void ChangeStateMenu(Client proxy)
         {
             int izbor = -1;
             bool a = true;
             do
             {
                 Console.WriteLine(" 1.Local State");
                 Console.WriteLine(" 2.Remote State");
                 Console.WriteLine("************************************\n");
                 try
                 {
                     izbor = Int32.Parse(Console.ReadLine());
                     a = true;
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine("Izabrali ste nepostojecu opciju!\n" + ex.Message);
                     a = false;
                 }
             } 
             while (izbor < 1 || izbor > 2 || !a);

             if (izbor == 1)
             {
                 proxy.ChangeState(true);
                 Console.WriteLine("\nStanje servisa je promenjeno na Local.\n");
             }
             else if (izbor == 2)
             {
                 proxy.ChangeState(false);
                 Console.WriteLine("\nStanje servisa je promenjeno na Remote.\n");
             }
             else
             {
                 Console.WriteLine("\nGreska prilikom promene stanja servisa!\n");
             }
         }
         public static DateTime[] BufferStatisticsMenu()
         {
             Console.Clear();
             DateTime[] dates = new DateTime[2];
             DateTime startDate = new DateTime();
             DateTime endDate = new DateTime();
             string[] splitsStartDate = new string[3];
             string[] splitsEndDate = new string[3];
             bool parseStartDate = true, parseEndDate = true;
             int startDay = -1, startMonth = -1, startYear = -1;
             int endDay = -1, endMonth = -1, endYear = -1;
             string startYearString = string.Empty, startMonthString = string.Empty, startDayString = string.Empty;
             string endYearString = string.Empty, endMonthString = string.Empty, endDayString = string.Empty;

             do
             {
                 Console.WriteLine("Unesite pocetni datum u obliku yyyy-mm-dd: ");
                 string startDateString = Console.ReadLine();
                 try
                 {
                     splitsStartDate = startDateString.Split('-');
                     startYearString = splitsStartDate[0];
                     startMonthString = splitsStartDate[1];
                     startDayString = splitsStartDate[2]; 
                     parseStartDate = true;
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine("\nNeispravan format pocetnog datuma!" + ex.Message);
                     parseStartDate = false;
                 }

                 try
                 {
                     startYear = Convert.ToInt32(startYearString);
                     startMonth = Convert.ToInt32(startMonthString);
                     startDay = Convert.ToInt32(startDayString);
                     parseStartDate = true;
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine("\nNeispravan format pocetnog datuma!" + ex.Message);
                     parseStartDate = false;
                 }
             } 
             while (!parseStartDate || !Util.CheckDate(startDay, startMonth, startYear));
             startDate = new DateTime(startYear, startMonth, startDay);

             do
             {
                 Console.WriteLine("Unesite krajnji datum u obliku yyyy-mm-dd: ");
                 string endDateString = Console.ReadLine();
                 try
                 {
                     splitsEndDate = endDateString.Split('-');
                     endYearString = splitsEndDate[0];
                     endMonthString = splitsEndDate[1];
                     endDayString = splitsEndDate[2];
                     parseEndDate = true;
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine("\nNeispravan format krajnjeg datuma!" + ex.Message);
                     parseEndDate = false;
                 }

                 try
                 {
                     endYear = Convert.ToInt32(endYearString);
                     endMonth = Convert.ToInt32(endMonthString);
                     endDay = Convert.ToInt32(endDayString);
                     parseEndDate = true;
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine("\nNeispravan format krajnjeg datuma!" + ex.Message);
                     parseEndDate = false;
                 }
             } 
             while (!parseEndDate || !Util.CheckDate(endDay, endMonth, endYear));
             endDate = new DateTime(endYear, endMonth, endDay);

             dates[0] = startDate;
             dates[1] = endDate;
             return dates;
         }

         public static void ShowAllDumpingValue(Dictionary<string, Dictionary<string, double>> dict)
         {
             foreach (KeyValuePair<string, Dictionary<string, double>> entry in dict)
             {
                 foreach (KeyValuePair<string, double> kvp in entry.Value)
                 {
                     Console.WriteLine(" " + kvp.Key + ": " + kvp.Value + "\t" + entry.Key);
                 }
             }
         }

         public static void ShowSystemConfiguration(SystemConfiguration sc)
         {
             Console.Clear();
             Console.WriteLine("*********System Configuration*********");
             Console.WriteLine(" " + "Deadband " + ": " + sc.Deadband);
             Console.WriteLine(" " + "State " + ": " + sc.State.ToString());
             Console.WriteLine(" " + "PMin " + ": " + sc.Pmin);
             Console.WriteLine(" " + "PMax " + ": " + sc.Pmax);
             Console.WriteLine("**************************************\n");
         }

         public static string EnterIpAddressAndPort()
         {
             string ipAddress = string.Empty;
             string port = string.Empty;
             string endpointAddress = string.Empty;


             Console.WriteLine("Unesite ip adresu: ");
             ipAddress = Console.ReadLine();


             Console.WriteLine("\nUnesite port:");
             port = Console.ReadLine();

             endpointAddress = "net.tcp://" + ipAddress + ":" + port + "/BufferService";
             Console.WriteLine("\nPodesena je ip adresa i port historical-a.");

             return endpointAddress;
         }

        public static void EnterDeadBandandValues(Client proxy)
        {
            bool a = true;
            bool b = true;
            bool c = true;
            Console.Clear();
            do 
            {
                Console.WriteLine("Unesite deadband:");
                try
                {
                    deadband = Int32.Parse(Console.ReadLine());
                    a = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Mozete uneti samo broj za vrednost deadband-a!" + ex.Message);
                    a = false;
                }
            } 
            while (!a);
                
            
            do
            {
                Console.WriteLine("Unesite pMin vrednost:");
                try
                {
                    pMin = Double.Parse(Console.ReadLine());
                    b = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Mozete uneti samo broj za vrednost pMin!" + ex.Message);
                    b = false;
                }
            }
            while (!b);

            do
            {
                Console.WriteLine("Unesite pMax vrednost:");
                try
                {
                    pMax = Double.Parse(Console.ReadLine());
                    if (pMax > pMin)
                    {
                        c = true;
                    }
                    else
                    {
                        c = false;
                        Console.WriteLine("Pmax ne sme da bude manji od pMin-a!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Mozete uneti samo broj za vrednost pMax!" + ex.Message);
                    c = false;
                }
            } 
            while (!c);

            proxy.SetSystemConfiguration(pMin, pMax, deadband); 
        }

        public static void MeasureDumpingValue(Client proxy)
        {
            int brojac = 0;
            while (threadWorks)
            {
                Dictionary<Codes, double> dict = new Dictionary<Codes, double>();
                //get random code value from enum
                Array values = Enum.GetValues(typeof(Codes));
                Random random = new Random();
                Codes randomCode = (Codes)values.GetValue(random.Next(values.Length));

                //generate random dump.value from pmin to pmax
                Random rnd = new Random();
                double dumpingValue = rnd.NextDouble() * (pMax - pMin) + pMin;

                dict.Add(randomCode, dumpingValue);
                if (++brojac == 10)
                {
                    //Console.WriteLine("Napunio dictionary");
                    proxy.MeasurementOfDumpingValues(dict);
                    brojac = 0;
                    Thread.Sleep(2000);
                }
            }
        }
    }
}
