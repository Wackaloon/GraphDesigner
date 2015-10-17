using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GraphDesigner
{
    enum stateEnum{ stateNodeAdding, stateEdgeAdding, stateNodeDeleting, stateEdgeDeleting, stageShortWayFinding}
    public partial class AgeichenkoTestTask : Form
    {
        SqlConnection sqlConnect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\C#_Projects\SoftTechTestTaskAgeychenko\GraphDesigner\GraphDesigner\GraphDesigner\GraphDB.mdf;Integrated Security=True");
        SqlCommand sqlCommand = new SqlCommand();
        SqlDataReader sqlDataReader;// = new SqlDataReader();

        stateEnum stateOfForm;
        GraphClass graph = null;
        Graphics paintBox = null;
        NodeClass nodeClickedFirst = null;
        NodeClass nodeClickedSecond = null;

        public AgeichenkoTestTask()
        {
            InitializeComponent();
            stateOfForm = stateEnum.stateNodeAdding;
            paintBox = pictureBoxGraph.CreateGraphics();

            graph = new GraphClass();

            graph.Graphic = paintBox;
            graph.NodeColor = Color.Black;
            graph.EdgeColor = Color.Gray;
            graph.ShortPathColor = Color.LawnGreen;
            graph.SelectedNodeColor = Color.Red;
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

        private void buttonSaveToDB_Click(object sender, EventArgs e)
        {
            sqlConnect.Open();
            //insert data
            int EdgeId = 0;
            int EdgeParent = 0;
            int EdgeDestination = 0;
            sqlCommand.CommandText = "insert into Edges (EdgeId, EdgeParent, EdgeDestination) "
                                   + "values ('"+ EdgeId + "','" + EdgeParent + "','" + EdgeDestination + "')";
            sqlCommand.ExecuteNonQuery();
            sqlCommand.Clone();
            sqlConnect.Close();
            // end of insert

            EdgeId = 0;
            EdgeParent = 0;
            EdgeDestination = 0;

            //read data
            sqlConnect.Open();
            sqlCommand.CommandText = "select * from Edges";
            sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    EdgeId = Convert.ToInt32(sqlDataReader[0].ToString());
                    EdgeParent = Convert.ToInt32(sqlDataReader[1].ToString());
                    EdgeDestination = Convert.ToInt32(sqlDataReader[2].ToString());
                }
            }

            sqlConnect.Close();
            //endd of read data

        }

        private void AgeichenkoTestTask_Load(object sender, EventArgs e)
        {
            sqlCommand.Connection = sqlConnect;
        }
    }
}
