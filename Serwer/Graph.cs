using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Graph
    {
        public int nrOfNodes { get; }
        public int[,] graph { get; }
        public int start { get; }
        public int end { get; }

        public Graph(int _nrOfNodes, int[,] _graph, int _start, int _end)
        {
            nrOfNodes = _nrOfNodes;
            graph = _graph;
            start = _start;
            end = _end;
        }
    }
}
