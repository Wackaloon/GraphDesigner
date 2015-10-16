using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace GraphDesigner
{
    class GraphClass
    {
        private List<NodeClass> graphNodes;
        private List<NodeClass> shortPath;

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

        internal List<NodeClass> GraphNodes
        {
            get
            {
                return graphNodes;
            }

            set
            {
                graphNodes = value;
            }
        }

        public GraphClass()
        {
            GraphNodes = new List<NodeClass>();
            nodeColor = Color.Black;
            edgeColor = Color.Gray;
        }

        public void addNodeToList(NodeClass node)
        {
            GraphNodes.Add(node);
        }

        public int numberOfNodes()
        {
            return GraphNodes.Count;
        }

        public NodeClass whichNodeWasClicked(Point click)
        {
            //find which node was clicked by its position
            NodeClass result = null;
            foreach(NodeClass node in GraphNodes)
            {
                if (node.nodeWasClicked(click))
                    result = node;
            }
            return result;
        }

        public EdgeClass whichEdgeWasClicked(Point click)
        {
            EdgeClass result = null;

            Point A;// clicked point
            Point B; 
            Point C;

            double range = 0;

            foreach (NodeClass node in GraphNodes)
            {
                foreach (EdgeClass edge in node.nodeEdges)
                {
                    // find range between click and this edge
                    A = click;
                    C = edge.NextNode.NodePosition;
                    B = node.NodePosition;

                    range = Math.Abs( (B.Y - C.Y) * A.X + (C.X - B.X)*A.Y + (B.X * C.Y - C.X * B.Y) ) 
                          / Math.Sqrt( (B.Y-C.Y) * (B.Y - C.Y) + (C.X - B.X) * (C.X - B.X) );

                    if (range < 8)
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
            foreach (NodeClass node in GraphNodes)
            {
                node.nodeEdges.Remove(deleteEdge);
            }
        }



        public void deleteNode(NodeClass deleteNode)
        {

            EdgeClass deleteEdge = null;
            // find edge to deleting node in other nodes
            foreach (NodeClass node in GraphNodes)
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
            GraphNodes.Remove(deleteNode);
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

        public void drawGraph(Graphics graphic)
        {
            Pen pen = new Pen(edgeColor);
            pen.Width = 3;

            graphic.Clear(Color.White);
            // draw edges
            foreach (NodeClass node in GraphNodes)
            {
                foreach (EdgeClass edge in node.nodeEdges)
                {
                    // draw line with arrow at the end
                    drawArrowhead(graphic, pen, node.NodePosition, edge.NextNode.NodePosition, 0.03);
                }
            }
            // draw nodes
            foreach (NodeClass node in GraphNodes)
            {
                node.drawNode(graphic, nodeColor, Color.White);
            }
        }

        public void drawShortPath(ArrayList path, Graphics graphic, Color color)
        {
            int pos = 0;
            shortPath = new List<NodeClass>();
            Pen pen = new Pen(color);
            pen.Width = 3;
            
            for (int k = 0; k < path.Count; ++k)
            {
                pos = (int)path[k];
                shortPath.Add(graphNodes[pos]);
            }

           for (int i = 0; i < shortPath.Count - 1; ++i)
            {
                
                foreach(EdgeClass edge in shortPath[i].nodeEdges)
                {
                    if (edge.NextNode == shortPath[i + 1])
                    {
                        // draw line with arrow at the end
                        drawArrowhead(graphic, pen, shortPath[i].NodePosition, edge.NextNode.NodePosition, 0.03);
                    }
                }
                shortPath[i].drawNode(graphic, Color.Red, Color.White);
            }
            shortPath[shortPath.Count - 1].drawNode(graphic, Color.Red, Color.White);
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
    }
}
