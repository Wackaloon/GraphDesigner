using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace GraphDesigner
{
    [Serializable()]
    class GraphClass
    {
        private List<NodeClass> graphNodes;
        private List<NodeClass> shortPath;

        private Color nodeColor;
        private Color edgeColor;
        private Color shortPathEdgeColor;
        private Color shortPathNodeColor;
        private Color nodeSelectedColor;

        private ShortestWayClass shortWay;

        private int nodeNumberCounter;

        [field: NonSerialized()]
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

        public Color ShortPathEdgeColor
        {
            get
            {
                return shortPathEdgeColor;
            }

            set
            {
                shortPathEdgeColor = value;
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

        public Color ShortPathNodeColor
        {
            get
            {
                return shortPathNodeColor;
            }

            set
            {
                shortPathNodeColor = value;
            }
        }

        public Color NodeSelectedColor
        {
            get
            {
                return nodeSelectedColor;
            }

            set
            {
                nodeSelectedColor = value;
            }
        }

        public GraphClass()
        {
            GraphNodes = new List<NodeClass>();
            shortWay = new ShortestWayClass();
            nodeColor = Color.Black;
            edgeColor = Color.Gray;
            shortPathEdgeColor = Color.LawnGreen;
            shortPathNodeColor = Color.Red;
            nodeNumberCounter = 0;

        }
        /* =================== Functions for editing graph =================== */
        public void addEdge(NodeClass nodeClickedFirst, NodeClass nodeClickedSecond)
        {
            // prevent clones of existing edges
            if (!isEdgeAlreadyExist(nodeClickedFirst, nodeClickedSecond))
            {
                nodeClickedFirst.addEdge(nodeClickedSecond);
            }
            drawGraph();
        }

        public bool deleteEdge(Point position)
        {
            // delete if some edge was clicked
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
            // add new node if click place was empty
            NodeClass newNode = whichNodeWasClicked(position, true);
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

        public void addNode(Point position, int NodeId)
        {
            // add node by it's id (used in load function)
            NodeClass newNode = new NodeClass(position, NodeId);
            addNodeToList(newNode);
            drawGraph();
                
        }

        public bool deleteNode(Point position)
        {

            NodeClass deleteNode = whichNodeWasClicked(position);
            if (deleteNode != null)
            {
                EdgeClass deleteEdge = null;
                // find edge to this node in other nodes
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
        public NodeClass whichNodeWasClicked(Point click, bool withFreedomRadius = false)
        {
            //find which node was clicked by its position
            foreach (NodeClass node in GraphNodes)
            {
                if (node.nodeWasClicked(click, withFreedomRadius))
                    return node;
            }
            return null;
        }

        private EdgeClass whichEdgeWasClicked(Point click)
        {
            Point A;// clicked point
            Point B;
            Point C;

            double range = 0;

            // find first suitable
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
                        return edge;
                }
            }
            return null;
        }

        private bool isEdgeAlreadyExist(NodeClass from, NodeClass to)
        {
            foreach (EdgeClass edge in from.nodeEdges)
            {
                if (edge.NextNode == to)
                    return true;
            }
            return false;
        }
        /* **************** END Functions for click position determinating  **************************/


        /* ++++++++++++++++ Functions for inner math  ++++++++++++++++++++++++++++++++*/
        public bool shortPathCalculation(NodeClass nodeClickedFirst, NodeClass nodeClickedSecond)
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
            if (path.Count == 1)
                return false;
            return true;
        }

        public int findNodeIndexByNodeNumber(int nodeNumber)
        {
            for (int i = 0; i < graphNodes.Count; ++i)
            {
                if (graphNodes[i].NodeNumber == nodeNumber)
                {
                    return i;
                }
            }
            return -1;
        }

        public NodeClass findNodeByNodeNumber(int nodeNumber)
        {
            for (int i = 0; i < graphNodes.Count; ++i)
            {
                if (graphNodes[i].NodeNumber == nodeNumber)
                {
                    return graphNodes[i];
                }
            }
            return null;
        }

        public void clearGraph()
        {
            graphNodes.Clear();
            drawGraph();
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
            shortPath = new List<NodeClass>();
            Pen pen = new Pen(shortPathEdgeColor);
            pen.Width = 3;

            // make list of nodes in short path
            for (int k = 0; k < path.Count; ++k)
            {
                shortPath.Add( graphNodes[ (int)path[k] ] );
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
                shortPath[i].drawNode(graphic, shortPathNodeColor, Color.White);
            }
            shortPath[shortPath.Count - 1].drawNode(graphic, shortPathNodeColor, Color.White);
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
