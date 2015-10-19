using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace GraphDesigner
{
    [Serializable()]
    class SqlHandlerClass
    {
        [field: NonSerialized()]
        private SqlConnection sqlConnect;
        [field: NonSerialized()]
        private SqlCommand sqlCommand;
        [field: NonSerialized()]
        private SqlDataReader sqlDataReader;
        public SqlHandlerClass()
        {
            sqlConnect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|GraphDB.mdf;Integrated Security=True");
            sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnect;
        }

        public void saveGraphToDB(GraphClass graph)
        {
            int EdgeId = 0;
            int EdgeParent = 0;
            int EdgeDestination = 0;
            int NodeId = 0;
            int NodeX = 0;
            int NodeY = 0;

            sqlConnect.Open();

            //clear all  data
            sqlCommand.CommandText = "delete from Nodes";
            sqlCommand.ExecuteNonQuery();
            sqlCommand.CommandText = "delete from Edges";
            sqlCommand.ExecuteNonQuery();

            //insert new data
            try
            {
                    foreach (NodeClass node in graph.GraphNodes)
                    {
                        NodeId = node.NodeNumber;
                        NodeX = node.NodePosition.X;
                        NodeY = node.NodePosition.Y;
                        sqlCommand.CommandText = "insert into Nodes (NodeId, NodeX, NodeY) "
                               + "values ('" + NodeId + "','" + NodeX + "','" + NodeY + "')";
                        sqlCommand.ExecuteNonQuery();

                        foreach (EdgeClass edge in node.nodeEdges)
                        {
                            EdgeParent = node.NodeNumber;
                            EdgeDestination = edge.NextNode.NodeNumber;
                            sqlCommand.CommandText = "insert into Edges (EdgeId, EdgeParent, EdgeDestination) "
                                           + "values ('" + EdgeId++ + "','" + EdgeParent + "','" + EdgeDestination + "')";
                            sqlCommand.ExecuteNonQuery();
                        }

                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not insert data into database. Original error: " + ex.Message);
            }
            sqlConnect.Close();
            // end of insert
        }

        public void loadGraphFromDB(GraphClass graph)
        {
            int EdgeId = 0;
            int EdgeParent = 0;
            int EdgeDestination = 0;
            int NodeId = -1;
            int NodeX = -1;
            int NodeY = -1;

            //read data
            sqlConnect.Open();

            // read nodes
            sqlCommand.CommandText = "select * from Nodes";
            sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    NodeId = -1;
                    NodeX = -1;
                    NodeY = -1;
                    NodeId = Convert.ToInt32(sqlDataReader[0].ToString());
                    NodeX = Convert.ToInt32(sqlDataReader[1].ToString());
                    NodeY = Convert.ToInt32(sqlDataReader[2].ToString());

                    if (NodeX > 0 && NodeY > 0)
                    { 
                        Point position = new Point(NodeX, NodeY);

                        graph.addNode(position, NodeId);
                    }
                    else
                    {
                        MessageBox.Show("Error: Database doesn't have any nodes");
                    }
                }
            }
            sqlDataReader.Close();

            // read Edges
            sqlCommand.CommandText = "select * from Edges";
            sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    EdgeId = Convert.ToInt32(sqlDataReader[0].ToString());
                    EdgeParent = Convert.ToInt32(sqlDataReader[1].ToString());
                    EdgeDestination = Convert.ToInt32(sqlDataReader[2].ToString());

                    NodeClass parentNode = null;
                    NodeClass destinationNode = null;

                    parentNode = graph.findNodeByNodeNumber(EdgeParent);
                    destinationNode = graph.findNodeByNodeNumber(EdgeDestination);

                    if (parentNode != null && destinationNode != null)
                    { 
                        graph.addEdge(parentNode, destinationNode);
                    }
                    else
                    {
                        MessageBox.Show("Error: Database doesn't have any edges");
                    }
                }
            }


            sqlDataReader.Close();
            sqlConnect.Close();
            //end of read data

            graph.drawGraph();
        }
    }
}
