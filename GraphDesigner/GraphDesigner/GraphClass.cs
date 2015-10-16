using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDesigner
{
    class GraphClass
    {
        private List<NodeClass> graphNodes;

        private Color nodeColor;
        private Color edgeColor;

        public Color NodeColor
        {
            get
            {
                return nodeColor;
            }

            set
            {
                nodeColor = value;
            }
        }

        public Color EdgeColor
        {
            get
            {
                return edgeColor;
            }

            set
            {
                edgeColor = value;
            }
        }

        public GraphClass()
        {
            graphNodes = new List<NodeClass>();
            nodeColor = Color.Black;
            edgeColor = Color.Gray;
        }

        public void addNodeToList(NodeClass node)
        {
            graphNodes.Add(node);
        }

        public int numberOfNodes()
        {
            return graphNodes.Count;
        }

        public void drawGraph(Graphics graphic)
        {
            Pen pen = new Pen(edgeColor);
            pen.Width = 3;

            graphic.Clear(Color.White);
            // draw edges
            foreach (NodeClass node in graphNodes)
            {
                foreach (EdgeClass edge in node.nodeEdges)
                {
                    // draw line with arrow at the end
                    drawArrowhead(graphic, pen, node.NodePosition, edge.NextNode.NodePosition, 0.03);
                }
            }
            // draw nodes
            foreach (NodeClass node in graphNodes)
            {
                node.drawNode(graphic, nodeColor, Color.White);
            }
        }

        public NodeClass whichNodeWasClicked(Point click)
        {
            //find which node was clicked by its position
            NodeClass result = null;
            foreach(NodeClass node in graphNodes)
            {
                if (node.nodeWasClicked(click))
                    result = node;
            }
            return result;
        }

        public EdgeClass whichEdgeWasClicked(Point click)
        {
            EdgeClass result = null;

            Point A;
            Point B; // clicked dot
            Point C;

            double AB = 0;
            double AC = 0;
            double BC = 0;

            double range = 0;
            double treshhold = 0.002; // finger in the sky, just enough to work properly

            foreach (NodeClass node in graphNodes)
            {
                foreach (EdgeClass edge in node.nodeEdges)
                {
                    // find range between click and this edge
                    A = node.NodePosition;
                    C = edge.NextNode.NodePosition;
                    B = click;

                    // lenght of vectors
                    AB = Math.Sqrt((B.X - A.X) * (B.X - A.X) + (B.Y - A.Y) * (B.Y - A.Y));
                    AC = Math.Sqrt((C.X - A.X) * (C.X - A.X) + (C.Y - A.Y) * (C.Y - A.Y));
                    BC = Math.Sqrt((C.X - B.X) * (C.X - B.X) + (C.Y - B.Y) * (C.Y - B.Y));

                    // difference between length of main line and line throw click point
                    range = BC + AB - AC;

                    if (range < AC * treshhold)
                    {
                        result = edge;
                        return result;
                    }
                }
            }


            return result;
        }

        public void deleteEdge(EdgeClass deleteEdge)
        {
            foreach (NodeClass node in graphNodes)
            {
                node.nodeEdges.Remove(deleteEdge);
            }
        }

        private void drawArrowhead(Graphics gr, Pen pen,
           Point start, Point end, double length)
        {
            // find coeff for normalized direction
            double deltaY = (end.Y - start.Y);
            double deltaX = (end.X - start.X);
            deltaX *= deltaX;
            deltaY *= deltaY;

            double lenghtOfLine = Math.Sqrt(deltaY + deltaX);
            double lyambda = lenghtOfLine / 15;

            // find coordinates of dot on edge which just before node circle starts
            int x = (int)((start.X + lyambda * end.X) / (1 + lyambda));
            int y = (int)((start.Y + lyambda * end.Y) / (1 + lyambda));

            // arrow must be independent from pen's size, LineCap gives too small arrow 
            AdjustableArrowCap bigArrow = new AdjustableArrowCap(5, 5);
            pen.CustomEndCap = bigArrow;
            gr.DrawLine(pen, start.X, start.Y, x, y);
        }

        public void deleteNode(NodeClass deleteNode)
        {

            EdgeClass deleteEdge = null;
            // find edge to deleting node in other nodes
            foreach (NodeClass node in graphNodes)
            {
                foreach (EdgeClass edge in node.nodeEdges)
                {
                    if(edge.NextNode == deleteNode)
                    {
                        deleteEdge = edge;
                        break;
                    }
                }
                node.nodeEdges.Remove(deleteEdge);
            }
            // delete node by itself
            graphNodes.Remove(deleteNode);
        }


        public bool isEdgeAlreadyExist(NodeClass from, NodeClass to)
        {
            bool result = false;

            foreach (EdgeClass edge in from.nodeEdges)
            {
                if (edge.NextNode == to)
                    result = true;
            }
            return result;
        }
    }
}
