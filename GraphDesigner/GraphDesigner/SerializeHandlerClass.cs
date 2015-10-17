using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GraphDesigner
{
    [Serializable()]
    class SerializeHandlerClass
    {
        private const string FileName = @".\SavedGraph.xml";
        private Graphics paintBox;

        public Graphics PaintBox
        {
            get
            {
                return paintBox;
            }

            set
            {
                paintBox = value;
            }
        }

        public void saveGraphToFile(GraphClass graph)
        {
            Stream TestFileStream = File.Create(FileName);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(TestFileStream, graph);
            TestFileStream.Close();
        }

        public GraphClass loadGraphFromFile()
        {
            GraphClass graph = new GraphClass();
            if (File.Exists(FileName))
            {
                Stream TestFileStream = File.OpenRead(FileName);
                BinaryFormatter deserializer = new BinaryFormatter();
                graph = (GraphClass)deserializer.Deserialize(TestFileStream);
                TestFileStream.Close();
            }
            graph.Graphic = paintBox;
            graph.drawGraph();
            return graph;
        }
    }
}
