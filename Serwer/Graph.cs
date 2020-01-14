using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Graph
    {
        public int nrOfNodes { get; set; }
        public int[,] graph { get; set; }
        public int start { get; set; }
        public int end { get; set; }

        public Graph(int _nrOfNodes, int[,] _graph, int _start, int _end)
        {
            nrOfNodes = _nrOfNodes;
            graph = _graph;
            start = _start;
            end = _end;
        }
        public Graph(Graph _graph): this(_graph.nrOfNodes, _graph.graph, _graph.start, _graph.end)
        {
        }
        public Graph()
        {
        }

        public void showGraph()
        {
            Console.WriteLine();
            for(int i = 0; i<nrOfNodes; i++)
                Console.Write("\t"+i);
            for (int i = 0; i < nrOfNodes; i++)
            {
                Console.Write("\n"+i);
                for (int j = 0; j < nrOfNodes; j++)
                {
                    if(Math.Abs(graph[i,j]+10)<nrOfNodes*Constants.MAXWEIGHT)
                        Console.Write("\t" + graph[i,j]);
                    else
                        Console.Write("\tTHREAT");
                }
            }    
        }
    }
}
