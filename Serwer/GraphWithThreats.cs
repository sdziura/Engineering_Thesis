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
            graphWithThreats = graph;
            for(int i = 0; i < nrOfNodes; i++)
            {
                if(threats.positionsOfThreats[i])
                {
                    for (int j = 0; j < nrOfNodes; j++)
                    {
                        for(int k = 0; k < nrOfNodes; k++)
                        {
                            graphWithThreats[k, j] += Constants.MAXWEIGHT / (1 + graph[i, j]);
                            graphWithThreats[j, k] += Constants.MAXWEIGHT / (1 + graph[i, j]);
                        }
                        graphWithThreats[i, j] = int.MaxValue;
                        graphWithThreats[j, i] = int.MaxValue;

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
