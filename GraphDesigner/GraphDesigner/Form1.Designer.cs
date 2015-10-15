namespace GraphDesigner
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonAddNode = new System.Windows.Forms.Button();
            this.buttonAddEdge = new System.Windows.Forms.Button();
            this.pictureBoxGraph = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonAddNode
            // 
            this.buttonAddNode.Location = new System.Drawing.Point(1012, 12);
            this.buttonAddNode.Name = "buttonAddNode";
            this.buttonAddNode.Size = new System.Drawing.Size(75, 23);
            this.buttonAddNode.TabIndex = 0;
            this.buttonAddNode.Text = "Add Node";
            this.buttonAddNode.UseVisualStyleBackColor = true;
            this.buttonAddNode.Click += new System.EventHandler(this.buttonAddNode_Click);
            // 
            // buttonAddEdge
            // 
            this.buttonAddEdge.Location = new System.Drawing.Point(1012, 41);
            this.buttonAddEdge.Name = "buttonAddEdge";
            this.buttonAddEdge.Size = new System.Drawing.Size(75, 23);
            this.buttonAddEdge.TabIndex = 1;
            this.buttonAddEdge.Text = "Add Edge";
            this.buttonAddEdge.UseVisualStyleBackColor = true;
            this.buttonAddEdge.Click += new System.EventHandler(this.buttonAddEdge_Click);
            // 
            // pictureBoxGraph
            // 
            this.pictureBoxGraph.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictureBoxGraph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxGraph.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxGraph.Name = "pictureBoxGraph";
            this.pictureBoxGraph.Size = new System.Drawing.Size(980, 624);
            this.pictureBoxGraph.TabIndex = 2;
            this.pictureBoxGraph.TabStop = false;
            this.pictureBoxGraph.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxGraph_MouseClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1099, 648);
            this.Controls.Add(this.pictureBoxGraph);
            this.Controls.Add(this.buttonAddEdge);
            this.Controls.Add(this.buttonAddNode);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGraph)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonAddNode;
        private System.Windows.Forms.Button buttonAddEdge;
        private System.Windows.Forms.PictureBox pictureBoxGraph;
    }
}

