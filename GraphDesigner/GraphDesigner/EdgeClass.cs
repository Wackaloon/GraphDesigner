using System;

namespace GraphDesigner
{
    [Serializable()]
    class EdgeClass
    {
        private NodeClass nextNode;
        private int weight;


        public EdgeClass()
        {

        }

        public EdgeClass(NodeClass cNextNode, int cWeight)
        {
            this.nextNode = cNextNode;
            this.weight = cWeight;

        }

        public EdgeClass(NodeClass cNextNode)
        {
            this.nextNode = cNextNode;
            this.weight = 10000000;

        }

        public NodeClass NextNode
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
