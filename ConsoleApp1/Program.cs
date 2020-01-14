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
        public const int MAXSIZE = 500000000;
    }
    class Program
    {
        static void startAsync(int graphSize)
        {
            Simulator sim = new Simulator();
            Graph graf = sim.simulateGraph(graphSize, 1);
            Threats threat = sim.simulateThreats(graf);
            ClientClass clnt = new ClientClass();

            clnt.sendGraph(graf);
            var path = clnt.receivePath();
            Console.WriteLine("PATH :");
            foreach (int i in path)
            {
                Console.Write("{0}\t", i.ToString());
            }
            Console.WriteLine("Trying to send");
            while (true)
            {
               // await Task.Delay(1000);
                clnt.sendGraph(graf, threat);
                Console.WriteLine("dbg : sent");
                Console.ReadKey();
                path = clnt.receivePath();
                Console.WriteLine("PATH :");
                foreach (int i in path)
                {
                    Console.Write("{0}\t", i.ToString());
                }
                Console.WriteLine();
                threat = sim.changeThreats(graf, threat);
                Console.WriteLine("Waiting ... ");
                Console.ReadKey();
                //await Task.Delay(5000);
            }


        }
        static void Main(string[] args)
        {
            Console.WriteLine("CLIENT\n\n");

            //Test test = new Test();
            startAsync(10);
            //test.timeOfWorkWithThreats(20, 1, 1);
            Console.ReadKey();
        }
    }
}
