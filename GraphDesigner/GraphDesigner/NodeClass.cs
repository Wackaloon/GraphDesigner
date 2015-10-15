using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GraphDesigner
{
    class NodeClass
    {
        private int nodeNumber;

        private Point nodePosition;
        // point x = next node, point y = edge weight
        public List<EdgeClass> nodeEdges;

        private const int sizeOfNode = 30;

        public void addEdge(NodeClass nextNode)
        {
            int weight = calculateWeight(nextNode);
            EdgeClass newEdge = new EdgeClass(nextNode.nodeNumber, weight);
            nodeEdges.Add(newEdge);
        }

        private int calculateWeight(NodeClass nextNode)
        {
            int result = 0;
            double x = Math.Pow(Convert.ToDouble(this.nodePosition.X - nextNode.nodePosition.X), 2);
            double y = Math.Pow(Convert.ToDouble(this.nodePosition.Y - nextNode.nodePosition.Y), 2);
            result = Convert.ToInt32( Math.Floor(Math.Sqrt(x + y)) );
            return result;
        }

        public void showNode(Graphics placeWhereShowIt, Color nodeColor)
        {
            Rectangle rectangle = new Rectangle(this.nodePosition.X - sizeOfNode / 2,
                                                this.nodePosition.Y - sizeOfNode / 2,
                                                sizeOfNode, sizeOfNode);

            placeWhereShowIt.DrawEllipse(new Pen(nodeColor, 3), rectangle);

            placeWhereShowIt.FillEllipse(new SolidBrush(Color.White), rectangle);
            // Create string to draw.
            String drawString = this.nodeNumber.ToString();

            // Create font and brush.
            Font drawFont = new Font("Arial", 12);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Create point for upper-left corner of drawing.
            PointF drawPoint = new PointF(this.nodePosition.X - 11, this.nodePosition.Y - 8);

            // Draw string to screen.
            placeWhereShowIt.DrawString(drawString, drawFont, drawBrush, drawPoint);
        }

        public bool nodeWasClicked(Point click)
        {
            int xMiss = this.nodePosition.X - click.X;
            int yMiss = this.nodePosition.Y - click.Y;
            xMiss = xMiss * xMiss; // too long expresion -> (int)Math.Pow(xMiss, 2);
            yMiss = yMiss * yMiss; //                    (int)Math.Pow(yMiss, 2);
            int result = (int)Math.Sqrt(xMiss + yMiss);
            return (result < sizeOfNode/2) ? true : false;
        }

        public int NodeNumber
        {
            get
            {
                return nodeNumber;
            }

            set
            {
                nodeNumber = value;
            }
        }

        public Point NodePosition
        {
            get
            {
                return nodePosition;
            }

            set
            {
                nodePosition = value;
            }
        }

        public NodeClass(Point positionOnMap, int number)
        {
            this.NodePosition = positionOnMap;
            this.nodeNumber = number;
            nodeEdges = new List<EdgeClass>();
        }
    }
}
