using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace GraphDesigner
{
    [Serializable()]
    class SerializeHandlerClass
    {
        [field: NonSerialized()]
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
            // saving to user named file and user selected dir

            Stream TestFileStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Graph file (*.gph)|*.gph";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((TestFileStream = saveFileDialog1.OpenFile()) != null)
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    serializer.Serialize(TestFileStream, graph);
                    TestFileStream.Close();
                }
            }
            
        }

        public GraphClass loadGraphFromFile()
        {
            // load from user file

            Stream TestFileStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            GraphClass graph = new GraphClass();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Graph file (*.gph)|*.gph";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((TestFileStream = openFileDialog1.OpenFile()) != null)
                    {
                        BinaryFormatter deserializer = new BinaryFormatter();
                        graph = (GraphClass)deserializer.Deserialize(TestFileStream);
                        TestFileStream.Close();
                        graph.Graphic = paintBox;
                        graph.drawGraph();
                        return graph;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

            return graph;
        }
    }
}
