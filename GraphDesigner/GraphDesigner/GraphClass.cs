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
        private Color shortPathColor;
        private Color selectedNodeColor;

        private ShortestWayClass shortWay;

        private int nodeNumberCounter;

        Graphics graphic;

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

        public Color ShortPathColor
        {
            get
            {
                return shortPathColor;
            }

            set
            {
                shortPathColor = value;
            }
        }

        public Graphics Graphic
        {
            get
            {
                return graphic;
            }

            set
            {
                graphic = value;
            }
        }

        public Color SelectedNodeColor
        {
            get
            {
                return selectedNodeColor;
            }

            set
            {
                selectedNodeColor = value;
            }
        }

        public GraphClass()
        {
            GraphNodes = new List<NodeClass>();
            shortWay = new ShortestWayClass();
            nodeColor = Color.Black;
            edgeColor = Color.Gray;
            shortPathColor = Color.LawnGreen;
            selectedNodeColor = Color.Red;
            nodeNumberCounter = 0;

        }
        /* =================== Functions for editing graph =================== */
        public void addEdge(NodeClass nodeClickedFirst, NodeClass nodeClickedSecond)
        {
            if (!isEdgeAlreadyExist(nodeClickedFirst, nodeClickedSecond))
            {
                nodeClickedFirst.addEdge(nodeClickedSecond);
            }
            drawGraph();
        }

        public bool deleteEdge(Point position)
        {
            EdgeClass deleteEdge = whichEdgeWasClicked(position);
            if (deleteEdge != null)
            {  
                foreach (NodeClass node in GraphNodes)
                {
                    node.nodeEdges.Remove(deleteEdge);
                }
                drawGraph();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool addNode(Point position)
        {
            NodeClass newNode = whichNodeWasClicked(position);
            if (newNode == null)
            {
                newNode = new NodeClass(position, nodeNumberCounter++);
                addNodeToList(newNode);
                drawGraph();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool deleteNode(Point position)
        {

            NodeClass deleteNode = whichNodeWasClicked(position);
            if (deleteNode != null)
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
                drawGraph();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void addNodeToList(NodeClass node)
        {
            GraphNodes.Add(node);
        }

        private int numberOfNodes()
        {
            return GraphNodes.Count;
        }
        /* =================== END Functions for editing graph  =================== */

        /* **************** Functions for click position determinating  **************************/
        public NodeClass whichNodeWasClicked(Point click)
        {
            //find which node was clicked by its position
            NodeClass result = null;
            foreach (NodeClass node in GraphNodes)
            {
                if (node.nodeWasClicked(click))
                    result = node;
            }
            return result;
        }

        private EdgeClass whichEdgeWasClicked(Point click)
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

                    range = Math.Abs((B.Y - C.Y) * A.X + (C.X - B.X) * A.Y + (B.X * C.Y - C.X * B.Y))
                          / Math.Sqrt((B.Y - C.Y) * (B.Y - C.Y) + (C.X - B.X) * (C.X - B.X));

                    if (range < 8)
                    {
                        result = edge;
                        return result;
                    }
                }
            }
            return result;
        }

        private bool isEdgeAlreadyExist(NodeClass from, NodeClass to)
        {
            bool result = false;

            foreach (EdgeClass edge in from.nodeEdges)
            {
                if (edge.NextNode == to)
                    result = true;
            }
            return result;
        }
        /* **************** END Functions for click position determinating  **************************/


        /* ++++++++++++++++ Functions for inner math  ++++++++++++++++++++++++++++++++*/
        public void shortPathCalculation(NodeClass nodeClickedFirst, NodeClass nodeClickedSecond)
        {
            shortWay.SizeOfNodes = numberOfNodes();
            shortWay.resetParams();
            ArrayList path = shortWay.findShortWay(nodeClickedFirst, nodeClickedSecond, this);
            if (path.Count > 0)
            {
                // show path on paintBox
                drawGraph();
                drawShortPath(path);
            }
        }

        public int findNodeIndexByNodeNumber(int nodeNumber)
        {
            int currentNode = -1;

            for (int i = 0; i < graphNodes.Count; ++i)
            {
                if (graphNodes[i].NodeNumber == nodeNumber)
                    currentNode = i;
            }
            return currentNode;
        }
        /* ++++++++++++++++END Functions for inner math  +++++++++++++++++++++++++++++++*/


        /* **************** Functions for graphic operations  **************************/
        public void drawGraph()
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

        private void drawShortPath(ArrayList path)
        {
            int pos = 0;
            shortPath = new List<NodeClass>();
            Pen pen = new Pen(shortPathColor);
            pen.Width = 3;

            // make list of nodes in short path
            for (int k = 0; k < path.Count; ++k)
            {
                pos = (int)path[k];
                shortPath.Add(graphNodes[pos]);
            }

            // draw that list
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
                shortPath[i].drawNode(graphic, selectedNodeColor, Color.White);
            }
            shortPath[shortPath.Count - 1].drawNode(graphic, selectedNodeColor, Color.White);
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

        /* ****************END  Functions for graphic operations  **************************/
    }
}
