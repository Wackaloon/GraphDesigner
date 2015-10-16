using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDesigner
{
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

        public ShortestWayClass()
        {
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

            d[fromThis.NodeNumber] = 0;


            for (int i = 0; i < sizeOfNodes; ++i)
            {
                int v = -1;
                for (int j = 0; j < sizeOfNodes; ++j)
                    if (!u[j] && (v == -1 || d[j] < d[v]))
                        v = j;
                if (d[v] == 999999999)
                    break;
                u[v] = true;

                for (int j = 0; j < graph.GraphNodes[v].nodeEdges.Count(); ++j)
                {
                    int to = graph.GraphNodes[v].nodeEdges[j].NextNode.NodeNumber; 
                    int len = graph.GraphNodes[v].nodeEdges[j].Weight; 
                    if (d[v] + len < d[to])
                    {
                        d[to] = d[v] + len;
                        p[to] = v;
                    }
                }
            }

            ArrayList path = new ArrayList(); ;
            for (int v = toThis.NodeNumber; v != fromThis.NodeNumber; v = p[v])
                path.Add(v);
            path.Add(fromThis.NodeNumber);
            path.Reverse();


            return path;
        }
    }

    struct ways
    {
        public int nodeNumber;
        public int weight;
        public ways(int node, int mweight)
        {
            nodeNumber = node;
            weight = mweight;

        }
    }
    struct backWay
    {
        public int nodeNumber;
        public int previousNode;

        public backWay(int node, int backNode)
        {
            nodeNumber = node;
            previousNode = backNode;
        }
    }
}
