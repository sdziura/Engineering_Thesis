using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Client
{
    public static class Constants
    {
        public const int MAXWEIGHT = 100;
        public const int MAXSIZE = 10000;
    }
    class Program
    {
        static async Task startAsync()
        {
            Simulator sim = new Simulator();
            Graph graf = sim.simulateGraph(20, 1);
            Threats threat = sim.simulateThreats(graf);
            ClientClass clnt = new ClientClass();

            Console.WriteLine("Trying to send");
            while (true)
            {
                clnt.sendGraph(graf, threat);
                var path = clnt.receivePath();
                Console.WriteLine("PATH :");
                foreach (int i in path)
                {
                    Console.Write("{0}\t", i.ToString());
                }
                Console.WriteLine();
                threat = sim.changeThreats(graf, threat);
                Console.WriteLine("Waiting ... ");
                await Task.Delay(5000);
            }


        }
        static void Main(string[] args)
        {
            Console.WriteLine("CLIENT\n\n");

            startAsync();
            Console.ReadKey();
        }
    }
}
