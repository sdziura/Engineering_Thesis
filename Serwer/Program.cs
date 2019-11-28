using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] heh = new int[,] { { 0, 2, 3, 4 }, { 2, 0, 1, 2 }, { 3, 1, 0, 2 }, { 4, 2, 2, 0 } };
            Graph graf = new Graph(4, heh, 1, 3);
            Dijkstra dijk = new Dijkstra(graf);
            dijk.sprawdz();
            Console.ReadKey();
        }
    }
}
