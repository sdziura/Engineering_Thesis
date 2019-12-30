using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Server
{
    public static class Constants
    {
        public const int MAXWEIGHT = 100;
        public const int MAXSIZE = 1000;
    }
    class Program
    {
      
        static void Main(string[] args)
        {
            Console.WriteLine("SERVER\n\n");
            ServerClass serv = new ServerClass();
            DataToSend dat = serv.listen();
            Console.WriteLine(dat.graph.start);
            Dijkstra dijk = new Dijkstra(dat.graph);
            dijk.sprawdz();
            dijk.alghoStart();
            foreach (int i in dijk.showWay())
            {
                Console.Write("{0}\t", i.ToString());
            }
            dijk.sprawdz();
            Console.ReadKey();
        }
    }
}
