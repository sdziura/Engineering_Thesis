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
    }
}
