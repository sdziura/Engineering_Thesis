using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using serverlib;

namespace ServerEX
{
    class Program
    {
        static void start()
        {
            ServerClass serv = new ServerClass();
            GraphWithThreats graph = new GraphWithThreats();
            Dijkstra dijk = new Dijkstra();
            while (true)
            {
                Console.WriteLine("\nWaiting for data ...");
                graph = new GraphWithThreats(serv.listen());
                dijk = new Dijkstra(graph);
                dijk.alghorithmStart();
                Console.WriteLine();
                serv.sendPath(dijk.showWay());
                graph.newGraph().showGraph();
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
