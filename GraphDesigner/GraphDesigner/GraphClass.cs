using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDesigner
{
    class GraphClass
    {
        private List<NodeClass> graphNodes;
        public GraphClass()
        {
            graphNodes = new List<NodeClass>();
        }

        public void addNodeToList(NodeClass node)
        {
            graphNodes.Add(node);
        }

        public int numberOfNodes()
        {
            return graphNodes.Count;
        }

        public void drawGraph(Graphics graphic)
        {
            Pen pen = new Pen(Color.Gray);
            pen.Width = 3;

            graphic.Clear(Color.White);
            // draw edges
            foreach (NodeClass node in graphNodes)
            {
                foreach (EdgeClass edge in node.nodeEdges)
                {
                    //graphic.DrawLine(pen, node.NodePosition, graphNodes[edge.NextNode].NodePosition);
                    drawArrowhead(graphic, pen, node.NodePosition, graphNodes[edge.NextNode].NodePosition, 0.03);
                }
            }
            // draw nodes
            foreach (NodeClass node in graphNodes)
            {
                node.showNode(graphic, Color.Black);
            }
        }

        public NodeClass whichNodeWasClicked(Point click)
        {
            NodeClass result = null;
            foreach(NodeClass node in graphNodes)
            {
                if (node.nodeWasClicked(click))
                    result = node;
            }
            return result;
        }

        private void drawArrowhead(Graphics gr, Pen pen,
           Point start, Point end, double length)
        {
            double deltaY = (end.Y - start.Y);
            double deltaX = (end.X - start.X);
            deltaX *= deltaX;
            deltaY *= deltaY;

            double lenghtOfLine = Math.Sqrt(deltaY + deltaX);
            double lyambda = lenghtOfLine / 15;

            int x = (int)((start.X + lyambda * end.X) / (1 + lyambda));
            int y = (int)((start.Y + lyambda * end.Y) / (1 + lyambda));

            AdjustableArrowCap bigArrow = new AdjustableArrowCap(5, 5);
            pen.CustomEndCap = bigArrow;
            gr.DrawLine(pen, start.X, start.Y, x, y);
            /*
            //normalized direction
            int nx = end.X - start.X;
            int ny = end.Y - start.Y;

            int ax = (int)(length * ((-1 * ny) - nx));
            int ay = (int)(length * (nx - ny));
            Point[] points =
            {
                new Point(x + ax, y + ay),
                end,
                new Point(x - ay, y + ax)
            };
            gr.DrawLines(pen, points);
            */
        }
    }
}
