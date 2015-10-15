using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDesigner
{
    class EdgeClass
    {
        private int nextNode;
        private int weight;


        public EdgeClass()
        {

        }

        public EdgeClass(int cNextNode, int cWeight)
        {
            this.nextNode = cNextNode;
            this.weight = cWeight;

        }

        public int NextNode
        {
            get
            {
                return nextNode;
            }

            set
            {
                nextNode = value;
            }
        }

        public int Weight
        {
            get
            {
                return weight;
            }

            set
            {
                weight = value;
            }
        }
    }
}
