using System;
using System.Drawing;
using System.Windows.Forms;
using GraphDesigner.Properties;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Globalization;

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
        string currentLanguage;


        public AgeichenkoTestTask()
        {
            InitializeComponent();
            stateOfForm = stateEnum.stateNodeAdding;
            paintBox = pictureBoxGraph.CreateGraphics();

            graph = new GraphClass();
            sqlHandler = new SqlHandlerClass();
            serializer = new SerializeHandlerClass();
            notifiers = new List<ToolTip>();

            updatePaintingSettings();

            currentLanguage = "en";
            switchLanguage(currentLanguage);
        }

        /* +++++++++++++++++++++++ click evets handlers +++++++++++++++++++++++++*/

        private void buttonAddNode_Click(object sender, EventArgs e)
        {
            stateOfForm = stateEnum.stateNodeAdding;
            infoUpdate("InfoTextDefaultS");
        }

        private void buttonAddEdge_Click(object sender, EventArgs e)
        {
            stateOfForm = stateEnum.stateEdgeAdding;
            infoUpdate("InfoTextDefaultS");
        }

        private void buttonDeleteNode_Click(object sender, EventArgs e)
        {
            stateOfForm = stateEnum.stateNodeDeleting;
            infoUpdate("InfoTextDefaultS");
        }

        private void buttonDeleteEdge_Click(object sender, EventArgs e)
        {
            stateOfForm = stateEnum.stateEdgeDeleting;
            infoUpdate("InfoTextDefaultS");
        }

        private void buttonShortWay_Click(object sender, EventArgs e)
        {
            stateOfForm = stateEnum.stageShortWayFinding;
            infoUpdate("InfoTextDefaultS");
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
            updatePaintingSettings();
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
                        if(!graph.shortPathCalculation(nodeClickedFirst, nodeClickedSecond));
                            infoUpdate("PathNotExistS");
                        break;
                }

                nodeClickedFirst = null;
                nodeClickedSecond = null;
            }
        }



        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm(currentLanguage);
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

        /* +++++++++++++++++++++++ END click evets handlers +++++++++++++++++++++++++*/

        // save to DB
        private void toDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlHandler.saveGraphToDB(graph))
                infoUpdate("InfoSuccessSaveS");
            else
                infoUpdate("InfoFailSaveS");
        }
        //save to file
        private void toFileToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            if (serializer.saveGraphToFile(graph))
                infoUpdate("InfoSuccessSaveS");
            else
                infoUpdate("InfoFailSaveS");
        }
        // load from DB
        private void fromDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlHandler.loadGraphFromDB(graph))
                infoUpdate("InfoSuccessLoadS");
            else
                infoUpdate("InfoFailLoadS");
        }
        // load from file
        private void fromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraphClass newGraph = null;
            newGraph = serializer.loadGraphFromFile();
            if (newGraph != null)
            {
                graph = newGraph;
                updatePaintingSettings();
                graph.drawGraph();
                infoUpdate("InfoSuccessLoadS");
            }
            else
            {
                infoUpdate("InfoFailLoadS");
            }
                
        }


        public void updatePaintingSettings()
        {
            graph.Graphic = paintBox;
            graph.NodeColor = Settings.Default.NodeColor;
            graph.EdgeColor = Settings.Default.EdgeColor;
            graph.ShortPathEdgeColor = Settings.Default.ShortPathEdgeColor;
            graph.ShortPathNodeColor = Settings.Default.ShortPathNodeColor;
        }

        private void AgeichenkoTestTask_Load(object sender, EventArgs e)
        {
            updatePaintingSettings();
        }

        private void AgeichenkoTestTask_Shown(object sender, EventArgs e)
        {
            updatePaintingSettings();
            graph.drawGraph();
        }

        /* ++++++++++++++++++++++ language settings ++++++++++++++++++++++++++++++++*/

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // switch all texts to english language
            currentLanguage = "en";
            switchLanguage(currentLanguage);
        }

        private void russianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // switch all texts to russian language
            currentLanguage = "ru";
            switchLanguage(currentLanguage);
        }

        private void switchLanguage(string language)
        {
            // load current language texts
            Assembly assemble = Assembly.Load("GraphDesigner");
            ResourceManager resourceManager = new ResourceManager("GraphDesigner.Languages.Translation", assemble);
            CultureInfo cultureInfo = new CultureInfo(language);

            addBox.Text = resourceManager.GetString("addBoxS", cultureInfo);
            deleteBox.Text = resourceManager.GetString("deleteBoxS", cultureInfo);

            buttonAddNode.Text = resourceManager.GetString("addNodeS", cultureInfo);
            buttonAddEdge.Text = resourceManager.GetString("addEdgeS", cultureInfo);
            buttonDeleteEdge.Text = resourceManager.GetString("deleteEdgeS", cultureInfo);
            buttonDeleteNode.Text = resourceManager.GetString("deleteNodeS", cultureInfo);
            buttonShortWay.Text = resourceManager.GetString("findShortPathS", cultureInfo);

            fileToolStripMenuItem1.Text = resourceManager.GetString("FileS", cultureInfo);

                newToolStripMenuItem.Text = resourceManager.GetString("NewS", cultureInfo);

                openToolStripMenuItem.Text = resourceManager.GetString("OpenS", cultureInfo);
                    loadFromDatabaseToolStripMenuItem.Text = resourceManager.GetString("loadFromDBS", cultureInfo);
                    loadFromFileToolStripMenuItem.Text = resourceManager.GetString("loadFromFileS", cultureInfo);
                
                saveToolStripMenuItem.Text = resourceManager.GetString("SaveS", cultureInfo);
                    saveToDatabaseToolStripMenuItem.Text = resourceManager.GetString("saveToDBS", cultureInfo);
                    saveToFileToolStripMenuItem.Text = resourceManager.GetString("saveToFileS", cultureInfo);

                exitToolStripMenuItem1.Text = resourceManager.GetString("ExitS", cultureInfo);


            toolsToolStripMenuItem.Text = resourceManager.GetString("ToolsS", cultureInfo);

                optionsToolStripMenuItem.Text = resourceManager.GetString("OptionsS", cultureInfo);

                languageToolStripMenuItem.Text = resourceManager.GetString("LanguageS", cultureInfo);
                    englishToolStripMenuItem.Text = resourceManager.GetString("EnglishS", cultureInfo);
                    russianToolStripMenuItem.Text = resourceManager.GetString("RussianS", cultureInfo);

            helpToolStripMenuItem1.Text = resourceManager.GetString("HelpS", cultureInfo);

                aboutToolStripMenuItem1.Text = resourceManager.GetString("AboutS", cultureInfo);

            toolTip.SetToolTip(buttonAddNode, resourceManager.GetString("notaddNodeS", cultureInfo));
            toolTip.SetToolTip(buttonAddEdge, resourceManager.GetString("notaddEdgeS", cultureInfo));
            toolTip.SetToolTip(buttonDeleteEdge, resourceManager.GetString("notdeleteEdgeS", cultureInfo));
            toolTip.SetToolTip(buttonDeleteNode, resourceManager.GetString("notdeleteNodeS", cultureInfo));
            toolTip.SetToolTip(buttonShortWay, resourceManager.GetString("notfindShortPathS", cultureInfo));

            labelInfoStatic.Text = resourceManager.GetString("InfoLabelS", cultureInfo);
            labelInfo.Text = resourceManager.GetString("InfoTextDefaultS", cultureInfo);
        }

        private void infoUpdate(string infoString)
        {
            // load current language texts
            Assembly assemble = Assembly.Load("GraphDesigner");
            ResourceManager resourceManager = new ResourceManager("GraphDesigner.Languages.Translation", assemble);
            CultureInfo cultureInfo = new CultureInfo(currentLanguage);
            labelInfo.Text = resourceManager.GetString(infoString, cultureInfo);
        }
        /* ++++++++++++++++++++++ END language settings ++++++++++++++++++++++++++++++++*/
    }
}
