using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading;
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

        private string connStr;

        public SqlHandlerClass()
        {
            sqlCommand = new SqlCommand();
            /*
             Data Source - server name, by default (local)\SQLEXPRESS for sql2014 BUT! for sql 2012 (LocalDB)\v11.0
             Initial Catalog - DB name
             Integrated Security - something important
             */
            connStr = @"Data Source=(localdb)\v11.0;" +
                @"AttachDbFilename=" + Application.StartupPath + "\\GraphDBv11.mdf;" +
                @"Integrated Security=True;";

            // is this database availible?
            sqlConnect = new SqlConnection(connStr);
            try
            {
                sqlConnect.Open();
            }
            catch (SqlException existanceE)
            {
                MessageBox.Show("Error: Unexpected error Original error: " + existanceE.Message);
            }
            finally
            {
                sqlConnect.Close();
            }
        }


        public bool saveGraphToDB(GraphClass graph)
        {
            int EdgeId = 0;
            int EdgeParent = 0;
            int EdgeDestination = 0;
            int NodeId = 0;
            int NodeX = 0;
            int NodeY = 0;
            
            //string connStr = @"Data Source=(LocalDB)\v11.0;Initial Catalog=GraphDBv11;Integrated Security=True";
            // is this data base exist?
            sqlConnect = new SqlConnection(connStr);
            try
            {
                sqlConnect.Open();
                sqlCommand.Connection = sqlConnect;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: Could not open  database. Original error: " + ex.Message);
                return false;
            }

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

            return true;
        }

        public bool loadGraphFromDB(GraphClass graph)
        {
            int EdgeId = 0;
            int EdgeParent = 0;
            int EdgeDestination = 0;
            int NodeId = -1;
            int NodeX = -1;
            int NodeY = -1;

            //read data
            try
            {
                sqlConnect.Open();
                sqlCommand.Connection = sqlConnect;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not open  database. Original error: " + ex.Message);
                return false;
            }

            graph.clearGraph();

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

            return true;
        }
    }
}
