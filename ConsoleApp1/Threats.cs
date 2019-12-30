using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Threats
    {
        // Number of Nodes in the graph
        public int nrOfNodes { get; }
        // Which Nodes are unavailable
        public bool[] positionsOfThreats { get; set; }
        public enum typeOfThreat { None = 0, Fire, Gas, Agressor };
        typeOfThreat type;

        public Threats(Graph graph)
        {
            nrOfNodes = graph.nrOfNodes;
            positionsOfThreats = new bool[nrOfNodes];
            type = typeOfThreat.None;
        }

        public int setType(typeOfThreat _type)
        {
            type = _type;
            return (int)type;
        }
    }
}
