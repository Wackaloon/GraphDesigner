using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GraphDesigner.Properties;

namespace GraphDesigner
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            labelNode.Text = "Node color";
            labelEdge.Text = "Edge color";
            labelEdgeShort.Text = "Short path edge color";
            labelNodeShort.Text = "Short path node color";
            buttonAccept.Text = "Accept";
            confugureComboBox(comboBox1);
            confugureComboBox(comboBox2);
            confugureComboBox(comboBox3);
            confugureComboBox(comboBox4);
            
        }

        private void confugureComboBox(ComboBox combo)
        {
            combo.DrawMode = DrawMode.OwnerDrawVariable; ;
            foreach (System.Reflection.PropertyInfo prop in typeof(Color).GetProperties())
            {
                if (prop.PropertyType.FullName == "System.Drawing.Color")
                    combo.Items.Add(prop.Name);
            }
        }

        private void comboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds; //Rectangle of item
            if (e.Index >= 0)
            {
                //Get item color name
                string itemName = ((ComboBox)sender).Items[e.Index].ToString();

                //Get instance a font to draw item name with this style
                Font itemFont = new Font("Arial", 9, FontStyle.Regular);

                //Get instance color from item name
                Color itemColor = Color.FromName(itemName);

                //Get instance brush with Solid style to draw background
                Brush brush = new SolidBrush(itemColor);

                //Draw the background with my brush style and rectangle of item
                g.FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);

                //Draw the item name
                g.DrawString(itemName, itemFont, Brushes.Black, rect.X, rect.Top);


            }
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
                Settings.Default.NodeColor = Color.FromName(comboBox1.Text);
            if (comboBox2.SelectedIndex >= 0)
                Settings.Default.EdgeColor = Color.FromName(comboBox2.Text);
            if (comboBox3.SelectedIndex >= 0)
                Settings.Default.ShortPathEdgeColor = Color.FromName(comboBox3.Text);
            if (comboBox4.SelectedIndex >= 0)
                Settings.Default.ShortPathNodeColor = Color.FromName(comboBox4.Text);

            Settings.Default.Save();

            this.Close();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            string name = Settings.Default.NodeColor.Name;
            int index = comboBox1.Items.IndexOf(name);
            comboBox1.SelectedIndex = index;

            name = Settings.Default.EdgeColor.Name;
            index = comboBox2.Items.IndexOf(name);
            comboBox2.SelectedIndex = index;

            name = Settings.Default.ShortPathEdgeColor.Name;
            index = comboBox3.Items.IndexOf(name);
            comboBox3.SelectedIndex = index;

            name = Settings.Default.ShortPathNodeColor.Name;
            index = comboBox4.Items.IndexOf(name);
            comboBox4.SelectedIndex = index;

        }
    }
}
