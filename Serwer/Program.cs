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
        public const int MAXSIZE = 10000;
    }
    class Program
    {
        static void start()
        {
            ServerClass serv = new ServerClass();
            GraphWithThreats graph = new GraphWithThreats();
            Dijkstra dijk = new Dijkstra();
            while (true)
            {
                Console.WriteLine("Waiting for data ...");
                graph = new GraphWithThreats(serv.listen());
                dijk = new Dijkstra(graph);
                dijk.alghoStart();
                serv.sendPath(dijk.showWay());
                dijk.sprawdz();
            }
        }


        static void Main(string[] args)
        {
            Console.WriteLine("SERVER\n\n");
            start();
            Console.ReadKey();
        }
    }
}
