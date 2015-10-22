using System;
using System.Collections;
using System.Linq;

namespace GraphDesigner
{
    [Serializable()]
    class ShortestWayClass
    {
        int[] d;
        int[] p;
        bool[] u;
        int sizeOfNodes;

        public int SizeOfNodes
        {
            get
            {
                return sizeOfNodes;
            }

            set
            {
                sizeOfNodes = value;
            }
        }

        public void resetParams()
        {
            d = new int[sizeOfNodes];
            p = new int[sizeOfNodes];
            u = new bool[sizeOfNodes];

            for (int i = 0; i < sizeOfNodes; ++i)
            {
                u[i] = false;
                d[i] = 999999999;
                p[i] = -1;
            }
        }

        public ArrayList findShortWay(NodeClass fromThis, NodeClass toThis, GraphClass graph)
        {
            // Dijkstra's algorithm
            // d - minimum weights of ways to other nodes
            // u - marks of visited nodes
            // p - parent of each node, its necessary for back way finding
            d[graph.findNodeIndexByNodeNumber(fromThis.NodeNumber)] = 0;


            for (int i = 0; i < sizeOfNodes; ++i)
            {
                // weights to every node is infinite from the start
                int v = -1;
                for (int j = 0; j < sizeOfNodes; ++j)
                    if (!u[j] && (v == -1 || d[j] < d[v]))
                        v = j;
                if (d[v] == 999999999)
                    break;
                u[v] = true;

                for (int j = 0; j < graph.GraphNodes[v].nodeEdges.Count(); ++j)
                {
                    // if current way have lower weight sum to the next node - set new weight and parent
                    int to = graph.findNodeIndexByNodeNumber(graph.GraphNodes[v].nodeEdges[j].NextNode.NodeNumber); 
                    int len = graph.GraphNodes[v].nodeEdges[j].Weight; 
                    if (d[v] + len < d[to])
                    {
                        d[to] = d[v] + len;
                        p[to] = v;
                    }
                }
            }
            // find a way back from end to start and reverse it
            ArrayList path = new ArrayList(); 
            // i have no idea, how index v could be less than 0, but it has happened a few times
            // programming is... magic!
            for (int v = graph.findNodeIndexByNodeNumber(toThis.NodeNumber); 
                     (v != graph.findNodeIndexByNodeNumber( fromThis.NodeNumber )) && (v >= 0) && (p[v] >= 0);
                     v = p[v])
                path.Add(v);
            path.Add(graph.findNodeIndexByNodeNumber( fromThis.NodeNumber ));
            path.Reverse();

            return path;
        }
    }
}
