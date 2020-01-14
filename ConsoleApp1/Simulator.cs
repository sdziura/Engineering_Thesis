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
                        int weight = rnd.Next(1,maxWeight);
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
            int start = rnd.Next(graphSize - 1);
            int end;
            while (start == (end = rnd.Next(graphSize - 1))) ;

            Graph simGraph = new Graph(graphSize, graph, rnd.Next(graphSize-1), rnd.Next(graphSize-1));

            return simGraph;
        }

        public Threats simulateThreats(Graph graph)
        {
            Threats threats = new Threats(graph);
            int temp = rnd.Next(graph.nrOfNodes - 1);
            while(temp==graph.start || temp==graph.end)
                temp = rnd.Next(graph.nrOfNodes - 1);
            threats.positionsOfThreats[rnd.Next(graph.nrOfNodes-1)] = true;

            return threats;
        }
        public Threats addThreats(Graph graph, Threats threats, int nrOfThreats)
        {
            int tempRand = 0;
            for (int i = 0; i < nrOfThreats; i++)
            {
                while (threats.positionsOfThreats[tempRand = rnd.Next(graph.nrOfNodes)]) ;
                threats.positionsOfThreats[tempRand] = true;
            }


            return threats;
        }

        public Threats changeThreats(Graph graph, Threats threats)
        {

            for(int i = 0; i < graph.nrOfNodes; i++)
            {
                if(threats.positionsOfThreats[i])
                {
                    for(int j = 0; j < graph.nrOfNodes; j++)
                    {
                        if (rnd.Next(Constants.MAXWEIGHT * 3) > (graph.graph[i, j] + 2 * Constants.MAXWEIGHT) && graph.start!=j && graph.end != j)
                        {
                            threats.positionsOfThreats[j] = true;
                            return threats;
                        }
                    }
                }
            }
            return threats;
        }
    }
}
