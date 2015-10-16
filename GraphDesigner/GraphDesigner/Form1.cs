using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GraphDesigner
{
    enum stateEnum{ stateNodeAdding, stateEdgeAdding, stateNodeDeleting, stateEdgeDeleting, stageShortWayFinding}
    public partial class Form1 : Form
    {


        List<Point> list = new List<Point>();
        stateEnum stateOfForm = stateEnum.stateNodeAdding;
        GraphClass graph = new GraphClass();
        ShortestWayClass shortWay = new ShortestWayClass();
        Graphics paintBox = null;
        NodeClass nodeClickedFirst = null;
        NodeClass nodeClickedSecond = null;
        int nodeNumberCounter;

        public Form1()
        {
            InitializeComponent();
            paintBox = pictureBoxGraph.CreateGraphics();
            nodeNumberCounter = 0;
        }

        private void buttonAddNode_Click(object sender, EventArgs e)
        {
            stateOfForm = stateEnum.stateNodeAdding;
        }

        private void buttonAddEdge_Click(object sender, EventArgs e)
        {
            stateOfForm = stateEnum.stateEdgeAdding;
        }

        private void buttonDeleteNode_Click(object sender, EventArgs e)
        {
            stateOfForm = stateEnum.stateNodeDeleting;
        }

        private void buttonDeleteEdge_Click(object sender, EventArgs e)
        {
            stateOfForm = stateEnum.stateEdgeDeleting;
        }

        private void buttonShortWay_Click(object sender, EventArgs e)
        {
            stateOfForm = stateEnum.stageShortWayFinding;
        }

        private void pictureBoxGraph_MouseClick(object sender, MouseEventArgs e)
        {
            switch (stateOfForm)
            {
                case stateEnum.stateNodeAdding:
                    addNode(e);
                    break;
                case stateEnum.stateEdgeAdding:
                    addEdge(e);
                    break;
                case stateEnum.stateEdgeDeleting:
                    deleteEdge(e);
                    break;
                case stateEnum.stateNodeDeleting:
                    deleteNode(e);
                    break;
                case stateEnum.stageShortWayFinding:
                    shortWayFind(e);
                    break;
            }
            
        }

        private void addNode(MouseEventArgs e)
        {
            Point position = new Point(e.X, e.Y);
            NodeClass newNode = graph.whichNodeWasClicked(new Point(e.X, e.Y));
            if (newNode == null) { 
                newNode = new NodeClass(position, nodeNumberCounter++);
                graph.addNodeToList(newNode);
                graph.drawGraph(paintBox);
            }
            else
            {
                // show message
            }
        }

        private void deleteNode(MouseEventArgs e)
        {
            Point position = new Point(e.X, e.Y);
            NodeClass deleteNode = graph.whichNodeWasClicked(position);
            if (deleteNode != null)
            {
                graph.deleteNode(deleteNode);
                graph.drawGraph(paintBox);
            }
            else
            {
                // show message
            }
        }

        private void addEdge(MouseEventArgs e)
        {
            if (nodeClickedFirst == null)
            {
                nodeClickedFirst = graph.whichNodeWasClicked(new Point(e.X, e.Y));
                if (nodeClickedFirst != null)
                    nodeClickedFirst.drawNode(paintBox, Color.Black, Color.Green);
            }

            else
            {
                if (nodeClickedSecond == null)
                {
                    nodeClickedSecond = graph.whichNodeWasClicked(new Point(e.X, e.Y));
                    if (nodeClickedSecond != null)
                        nodeClickedSecond.drawNode(paintBox, Color.Black, Color.Green);
                }
            }


            if (nodeClickedFirst != null && nodeClickedSecond != null)
            {

                if(!graph.isEdgeAlreadyExist(nodeClickedFirst, nodeClickedSecond))
                {
                    nodeClickedFirst.addEdge(nodeClickedSecond);
                }
                nodeClickedFirst = null;
                nodeClickedSecond = null;
                graph.drawGraph(paintBox);

            }



        }

        private void deleteEdge(MouseEventArgs e)
        {
            EdgeClass deleteEdge = graph.whichEdgeWasClicked(new Point(e.X, e.Y));
            if (deleteEdge != null) { 
                graph.deleteEdge(deleteEdge);
                graph.drawGraph(paintBox);
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            graph.drawGraph(paintBox);
        }


        private void shortWayFind(MouseEventArgs e)
        {
            //find which nodes was clicked
            if (nodeClickedFirst == null)
            {
                nodeClickedFirst = graph.whichNodeWasClicked(new Point(e.X, e.Y));
                if (nodeClickedFirst != null)
                    nodeClickedFirst.drawNode(paintBox, Color.Black, Color.Green);
            }

            else
            {
                if (nodeClickedSecond == null)
                {
                    nodeClickedSecond = graph.whichNodeWasClicked(new Point(e.X, e.Y));
                    if (nodeClickedSecond != null)
                        nodeClickedSecond.drawNode(paintBox, Color.Black, Color.Green);
                }
            }

            // if two nodes was clicked - do something
            if (nodeClickedFirst != null && nodeClickedSecond != null)
            {
                shortWay.SizeOfNodes = graph.numberOfNodes();
                shortWay.resetParams();
                ArrayList path = shortWay.findShortWay(nodeClickedFirst, nodeClickedSecond, graph);
                if (path.Count > 0)
                {
                    // show path on paintBox
                    graph.drawShortPath(path, paintBox, Color.LawnGreen);
                }
                nodeClickedFirst = null;
                nodeClickedSecond = null;
            }
        }


    }
}
