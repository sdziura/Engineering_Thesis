using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class GraphWithThreats: Graph
    {
        Threats threats;
        public int[,] graphWithThreats { get; }
        public GraphWithThreats(Graph _graph, Threats _threats): base(_graph)
        {
            threats = _threats;

            graphWithThreats = new int[nrOfNodes,nrOfNodes];
            for(int i=0; i<nrOfNodes; i++)
            {
                for (int j = i; j < nrOfNodes; j++)
                {
                    graphWithThreats[i, j] = graph[i, j];
                    graphWithThreats[j, i] = graph[i, j];
                }
            }
            for(int i = 0; i < nrOfNodes; i++)
            {
                if(threats.positionsOfThreats[i])
                {
                    for (int j = 0; j < nrOfNodes; j++)
                    {
                        for(int k = j; k < nrOfNodes; k++)
                        {
                            if (graphWithThreats[k, j] != 0)
                            {
                                graphWithThreats[k, j] += Constants.MAXWEIGHT / (1 + graph[i, j]);
                                graphWithThreats[j, k] += Constants.MAXWEIGHT / (1 + graph[i, j]);
                            }
                        }
                        graphWithThreats[i, j] = 0;
                        graphWithThreats[j, i] = 0;

                    }
                }
            }
        }
        public GraphWithThreats(DataToSend _data) : this(_data.graph, _data.threats)
        { }

        public GraphWithThreats()
        { }

        public Graph newGraph()
        {
            Graph _newgraph = new Graph(nrOfNodes, graphWithThreats, start, end);
            return _newgraph;
        }

    }
}
