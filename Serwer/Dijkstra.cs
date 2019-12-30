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
        int[] predecessor { get; set; }
        // Furthest node we got to 
        int current; 

        public Dijkstra(Graph _graph)
        {
            graph = _graph;
            onPath = new bool[_graph.nrOfNodes];
            shortestDist = new int[_graph.nrOfNodes];
            predecessor = new int[_graph.nrOfNodes];
            current = _graph.start;
            for (int i = 0; i < _graph.nrOfNodes; ++i)
            {
                onPath[i] = false;
                shortestDist[i] = int.MaxValue;
                predecessor[i] = -1;
            }
            onPath[graph.start] = true;   // Set starting node as visited
            shortestDist[graph.start] = 0;  // 0 weight to starting node
        }
  
        private int findShortestWay()
        {
            int shortest = int.MaxValue;
            int pathCandidate = -1;

            // Go to every Node that is not on path
            for (int j = 0; j < graph.nrOfNodes; ++j)
            {
                if (!onPath[j])
                {
                    // Update shortest paths to Nodes, including path by current Node  
                    if (shortestDist[current] + graph.graph[current, j] < shortestDist[j])
                    {
                        shortestDist[j] = shortestDist[current] + graph.graph[current, j];
                        predecessor[j] = current;
                    }
                    // Find a Node, not included in path, that is closest from start
                    if (shortestDist[j] < shortest)
                    {
                        shortest = shortestDist[j];
                        pathCandidate = j;
                    }
                }
            }

            // Change current Node and add it to Path
            current = pathCandidate;
            onPath[pathCandidate] = true;
            return pathCandidate;
        }

        public void alghoStart()
        {
            while(current!=graph.end)
                findShortestWay();   
        }

        // Return a list of  with shortest path
        public List<int> showWay()
        {
            int temp = graph.end;
            List<int> way = new List<int>();
            way.Add(temp);
            while(temp != graph.start)
                 way.Add(temp = predecessor[temp]);

            way.Reverse();
            return way;
        }

        public void sprawdz()
        {
            for(int i = 0; i < graph.nrOfNodes; ++i)
            {
                Console.WriteLine(onPath[i] + "\t" + shortestDist[i] + "\t" + predecessor[i] + "\n");
            }
        }
    }
}
