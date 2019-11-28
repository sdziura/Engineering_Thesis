using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{

    class Dijkstra
    {
        Graph graph { get; set; }
        // Table informing if a node is already included in path
        bool[] onPath { get; set; } 
        // Current shortest known distance from start node to node of index
        int[] shortestDist { get; set; }
        // From which node we got to the considered node
        int[] previous { get; set; }

        public Dijkstra(Graph _graph)
        {
            graph = _graph;
            onPath = new bool[graph.nrOfNodes];
            shortestDist = new int[graph.nrOfNodes];
            previous = new int[graph.nrOfNodes];
            for (int i = 0; i < graph.nrOfNodes; ++i)
            {
                onPath[i] = false;
                shortestDist[i] = int.MaxValue;
                previous[i] = 0;
            }
            onPath[graph.start] = true;
        }

        public void sprawdz()
        {
            for(int i = 0; i < graph.nrOfNodes; ++i)
            {
                Console.WriteLine(onPath[i] + "\t" + shortestDist[i] + "\t" + previous[i] + "\n");
            }
        }
    }
}
