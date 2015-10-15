using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphDesigner
{
    enum stateEnum{ stateNodeAdding, stateEdgeAdding, stateNodeDeleting, stateEdgeDeleting}
    public partial class Form1 : Form
    {


        List<Point> list = new List<Point>();
        stateEnum stateOfForm = stateEnum.stateNodeAdding;
        GraphClass graph = new GraphClass();
        Graphics paintBox = null;
        NodeClass nodeClickedFirst = null;
        NodeClass nodeClickedSecond = null;

        public Form1()
        {
            InitializeComponent();
            paintBox = pictureBoxGraph.CreateGraphics();
        }

        private void buttonAddNode_Click(object sender, EventArgs e)
        {
            stateOfForm = stateEnum.stateNodeAdding;
        }

        private void buttonAddEdge_Click(object sender, EventArgs e)
        {
            stateOfForm = stateEnum.stateEdgeAdding;
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
                    break;
                case stateEnum.stateNodeDeleting:
                    break;
            }
            
        }

        private void addNode(MouseEventArgs e)
        {
            int nodeNumber = graph.numberOfNodes();
            Point position = new Point(e.X, e.Y);
            NodeClass newNode = graph.whichNodeWasClicked(new Point(e.X, e.Y));
            if (newNode == null) { 
                newNode = new NodeClass(position, nodeNumber);
                graph.addNodeToList(newNode);
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
                    nodeClickedFirst.showNode(paintBox, Color.Green);
            }

            else
            {
                if (nodeClickedSecond == null)
                {
                    nodeClickedSecond = graph.whichNodeWasClicked(new Point(e.X, e.Y));
                    if (nodeClickedSecond != null)
                        nodeClickedSecond.showNode(paintBox, Color.Green);
                }
            }


            if (nodeClickedFirst != null && nodeClickedSecond != null)
            {
                nodeClickedFirst.addEdge(nodeClickedSecond);
                graph.drawGraph(paintBox);
                nodeClickedFirst = null;
                nodeClickedSecond = null;
            }



        }
    }
}
