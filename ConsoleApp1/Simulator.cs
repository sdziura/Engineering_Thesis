using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Simulator
    {
        Random rnd = new Random();
        public Graph simulateGraph(int graphSize, double graphDensity, int maxWeight = Constants.MAXWEIGHT)
        {
            if (1 < graphDensity) graphDensity = 1;
            int[,] graph = new int[graphSize, graphSize];
            
            for(int i = 0; i < graphSize; i++)
            {
                graph[i, i] = 0;
                for(int j = i+1; j < graphSize; j++)
                {
                    if(rnd.Next(100) < 100*graphDensity)
                    {
                        int weight = rnd.Next(maxWeight);
                        graph[i, j] = weight;
                        graph[j, i] = weight;

                    }
                    else
                    {
                        graph[i, j] = int.MaxValue;
                        graph[j, i] = int.MaxValue;
                    }
                }
            }

            Graph simGraph = new Graph(graphSize, graph, rnd.Next(graphSize), rnd.Next(graphSize));

            return simGraph;
        }

        public Threats simulateThreats(Graph graph)
        {
            Threats threats = new Threats(graph);
            threats.positionsOfThreats[rnd.Next(graph.nrOfNodes)] = true;

            return threats;
        }

        public Threats changeThreats(Graph graph, Threats threats)
        {

            for(int i = 0; i < graph.nrOfNodes; i++)
            {
                if(threats.positionsOfThreats[i])
                {
                    for(int j = i+1; j < graph.nrOfNodes; j++)
                    {
                        if (rnd.Next(Constants.MAXWEIGHT) > graph.graph[i, j])
                            threats.positionsOfThreats[j] = true;
                    }
                }
            }
            return threats;
        }
    }
}
