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
        public const int MAXSIZE = 1000;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CLIENT\n\n");
            int[,] heh = new int[,] { { 0, 2, 3, 4 }, { 2, 0, 1, 2 }, { 3, 1, 0, 2 }, { 4, 2, 2, 0 } };
            Graph graf = new Graph(4, heh, 1, 3);

            ClientClass clnt = new ClientClass();
            Console.WriteLine("Traying to send");
            clnt.sendGraph(graf, new Threats(graf));
            Console.ReadKey();
        }
    }
}
