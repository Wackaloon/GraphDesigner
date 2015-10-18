using System;
using System.Drawing;
using System.Windows.Forms;
using GraphDesigner.Properties;
using System.Collections.Generic;

namespace GraphDesigner
{
    enum stateEnum{ stateNodeAdding, stateEdgeAdding, stateNodeDeleting, stateEdgeDeleting, stageShortWayFinding}
    public partial class AgeichenkoTestTask : Form
    {
        stateEnum stateOfForm;
        GraphClass graph = null;
        SqlHandlerClass sqlHandler = null;
        SerializeHandlerClass serializer = null;
        Graphics paintBox = null;
        NodeClass nodeClickedFirst = null;
        NodeClass nodeClickedSecond = null;
        List<ToolTip> notifiers = null;


        public AgeichenkoTestTask()
        {
            InitializeComponent();
            stateOfForm = stateEnum.stateNodeAdding;
            paintBox = pictureBoxGraph.CreateGraphics();

            graph = new GraphClass();
            sqlHandler = new SqlHandlerClass();
            serializer = new SerializeHandlerClass();
            notifiers = new List<ToolTip>();
            setupNotifiers();

            serializer.PaintBox = paintBox;
            graph.Graphic = paintBox;
            updateSettings();

            buttonAddNode.Text = "Add node";
            buttonAddEdge.Text = "Add edge";
            buttonDeleteEdge.Text = "Delete edge";
            buttonDeleteNode.Text = "Delete node";
            saveToDatabaseToolStripMenuItem.Text = "Save to DB";
            saveToFileToolStripMenuItem.Text = "Save to file";
            loadFromDatabaseToolStripMenuItem.Text = "Load from DB";
            loadFromFileToolStripMenuItem.Text = "Load from file";
            buttonShortWay.Text = "Find short path";
            toolStripStatusLabel1.Text = "Welcome!";
        }

        private void buttonAddNode_Click(object sender, EventArgs e)
        {
            stateOfForm = stateEnum.stateNodeAdding;
            toolStripStatusLabel1.Text = "Click on empty space to add node.";
        }

        private void buttonAddEdge_Click(object sender, EventArgs e)
        {
            stateOfForm = stateEnum.stateEdgeAdding;
            toolStripStatusLabel1.Text = "Choose two nodes to add edge between them.";
        }

        private void buttonDeleteNode_Click(object sender, EventArgs e)
        {
            stateOfForm = stateEnum.stateNodeDeleting;
            toolStripStatusLabel1.Text = "Click on node to delete it.";
        }

        private void buttonDeleteEdge_Click(object sender, EventArgs e)
        {
            stateOfForm = stateEnum.stateEdgeDeleting;
            toolStripStatusLabel1.Text = "Click on edge to delete it.";
        }

        private void buttonShortWay_Click(object sender, EventArgs e)
        {
            stateOfForm = stateEnum.stageShortWayFinding;
            toolStripStatusLabel1.Text = "Choose two nodes to find shortest path between them.";
        }

        private void pictureBoxGraph_MouseClick(object sender, MouseEventArgs e)
        {
            switch (stateOfForm)
            {
                case stateEnum.stateNodeDeleting:
                    deleteNode(e);
                    break;
                case stateEnum.stateNodeAdding:
                    addNode(e);
                    break;
                case stateEnum.stateEdgeDeleting:
                    deleteEdge(e);
                    break;
                case stateEnum.stateEdgeAdding:
                    chooseTwoNodes(e);
                    break;
                case stateEnum.stageShortWayFinding:
                    chooseTwoNodes(e);
                    break;
            }
            
        }

        private void addNode(MouseEventArgs e)
        {
            Point position = new Point(e.X, e.Y);
            graph.addNode(position);
        }

        private void deleteNode(MouseEventArgs e)
        {
            Point position = new Point(e.X, e.Y);
            graph.deleteNode(position);
        }

        private void deleteEdge(MouseEventArgs e)
        {
            Point position = new Point(e.X, e.Y);
            graph.deleteEdge(position);
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            updateSettings();
            graph.drawGraph();
        }


        private void chooseTwoNodes(MouseEventArgs e)
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
                switch (stateOfForm)
                {
                    case stateEnum.stateEdgeAdding:
                        graph.addEdge(nodeClickedFirst, nodeClickedSecond);
                        break;

                    case stateEnum.stageShortWayFinding:
                        graph.shortPathCalculation(nodeClickedFirst, nodeClickedSecond);
                        break;
                }

                nodeClickedFirst = null;
                nodeClickedSecond = null;
            }
        }

        // save to DB
        private void toDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sqlHandler.saveGraphToDB(graph);
        }
        //save to file
        private void toFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            serializer.saveGraphToFile(graph);
        }
        // load from DB
        private void fromDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graph.clearGraph();
            sqlHandler.loadGraphFromDB(graph);
        }
        // load from file
        private void fromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graph = null;
            graph = serializer.loadGraphFromFile();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm();
            settings.Show();
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.Show();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graph.clearGraph();
        }

        public  void updateSettings()
        {
            graph.NodeColor = Settings.Default.NodeColor;
            graph.EdgeColor = Settings.Default.EdgeColor;
            graph.ShortPathEdgeColor = Settings.Default.ShortPathEdgeColor;
            graph.ShortPathNodeColor = Settings.Default.ShortPathNodeColor;
        }

        private void AgeichenkoTestTask_Load(object sender, EventArgs e)
        {
            updateSettings();
        }

        private void setupNotifiers()
        {

            ToolTip notify = null;

            notify = new ToolTip();
            notify.ToolTipTitle = "NOTE!";
            notify.SetToolTip(statusStrip1, "This string will help you! Look here from time to time.");
            notifiers.Add(notify);

            notify = new ToolTip();
            notify.ToolTipTitle = "Adding a new node";
            notify.SetToolTip(buttonAddNode, "Click on empty place on PaintBox to add new node");
            notifiers.Add(notify);

            notify = new ToolTip();
            notify.ToolTipTitle = "Adding a new edge";
            notify.SetToolTip(buttonAddEdge, "Click two nodes one by one and new edge will appear!");
            notifiers.Add(notify);

            notify = new ToolTip();
            notify.ToolTipTitle = "Deleting an item!";
            notify.SetToolTip(buttonDeleteEdge, "Click item to delete it.");
            notify.SetToolTip(buttonDeleteNode, "Click item to delete it.");
            notifiers.Add(notify);

            notify = new ToolTip();
            notify.ToolTipTitle = "Find shortest path!";
            notify.SetToolTip(buttonShortWay, "Click two nodes one by one and shortest path will appear!");
            notifiers.Add(notify);

            foreach(ToolTip not in notifiers) { 
                not.AutoPopDelay = 5000;
                not.InitialDelay = 1000;
                not.ReshowDelay = 500;
                not.ShowAlways = true;
                not.Active = true;
            }

        }

        private void AgeichenkoTestTask_Shown(object sender, EventArgs e)
        {
            notifiers[0].Show("This string will help you! Look here from time to time.", this.statusStrip1, 5000);
        }
    }
}
