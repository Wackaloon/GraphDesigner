namespace GraphDesigner
{
    partial class AgeichenkoTestTask
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
            this.buttonDeleteNode = new System.Windows.Forms.Button();
            this.buttonDeleteEdge = new System.Windows.Forms.Button();
            this.buttonShortWay = new System.Windows.Forms.Button();
            this.buttonSaveToDB = new System.Windows.Forms.Button();
            this.buttonLoadFromDB = new System.Windows.Forms.Button();
            this.buttonLoadFromFile = new System.Windows.Forms.Button();
            this.buttonSaveToFile = new System.Windows.Forms.Button();
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
            this.pictureBoxGraph.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBoxGraph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxGraph.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxGraph.Name = "pictureBoxGraph";
            this.pictureBoxGraph.Size = new System.Drawing.Size(980, 624);
            this.pictureBoxGraph.TabIndex = 2;
            this.pictureBoxGraph.TabStop = false;
            this.pictureBoxGraph.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxGraph_MouseClick);
            // 
            // buttonDeleteNode
            // 
            this.buttonDeleteNode.Location = new System.Drawing.Point(1011, 71);
            this.buttonDeleteNode.Name = "buttonDeleteNode";
            this.buttonDeleteNode.Size = new System.Drawing.Size(75, 23);
            this.buttonDeleteNode.TabIndex = 3;
            this.buttonDeleteNode.Text = "Delete Node";
            this.buttonDeleteNode.UseVisualStyleBackColor = true;
            this.buttonDeleteNode.Click += new System.EventHandler(this.buttonDeleteNode_Click);
            // 
            // buttonDeleteEdge
            // 
            this.buttonDeleteEdge.Location = new System.Drawing.Point(1012, 101);
            this.buttonDeleteEdge.Name = "buttonDeleteEdge";
            this.buttonDeleteEdge.Size = new System.Drawing.Size(75, 23);
            this.buttonDeleteEdge.TabIndex = 4;
            this.buttonDeleteEdge.Text = "Delete Edge";
            this.buttonDeleteEdge.UseVisualStyleBackColor = true;
            this.buttonDeleteEdge.Click += new System.EventHandler(this.buttonDeleteEdge_Click);
            // 
            // buttonShortWay
            // 
            this.buttonShortWay.Location = new System.Drawing.Point(1010, 131);
            this.buttonShortWay.Name = "buttonShortWay";
            this.buttonShortWay.Size = new System.Drawing.Size(75, 23);
            this.buttonShortWay.TabIndex = 5;
            this.buttonShortWay.Text = "Short Way";
            this.buttonShortWay.UseVisualStyleBackColor = true;
            this.buttonShortWay.Click += new System.EventHandler(this.buttonShortWay_Click);
            // 
            // buttonSaveToDB
            // 
            this.buttonSaveToDB.Location = new System.Drawing.Point(998, 208);
            this.buttonSaveToDB.Name = "buttonSaveToDB";
            this.buttonSaveToDB.Size = new System.Drawing.Size(89, 23);
            this.buttonSaveToDB.TabIndex = 6;
            this.buttonSaveToDB.Text = "Save to DB";
            this.buttonSaveToDB.UseVisualStyleBackColor = true;
            this.buttonSaveToDB.Click += new System.EventHandler(this.buttonSaveToDB_Click);
            // 
            // buttonLoadFromDB
            // 
            this.buttonLoadFromDB.Location = new System.Drawing.Point(998, 237);
            this.buttonLoadFromDB.Name = "buttonLoadFromDB";
            this.buttonLoadFromDB.Size = new System.Drawing.Size(87, 23);
            this.buttonLoadFromDB.TabIndex = 7;
            this.buttonLoadFromDB.Text = "Load from DB";
            this.buttonLoadFromDB.UseVisualStyleBackColor = true;
            this.buttonLoadFromDB.Click += new System.EventHandler(this.buttonLoadFromDB_Click);
            // 
            // buttonLoadFromFile
            // 
            this.buttonLoadFromFile.Location = new System.Drawing.Point(998, 320);
            this.buttonLoadFromFile.Name = "buttonLoadFromFile";
            this.buttonLoadFromFile.Size = new System.Drawing.Size(87, 23);
            this.buttonLoadFromFile.TabIndex = 9;
            this.buttonLoadFromFile.Text = "Load from file";
            this.buttonLoadFromFile.UseVisualStyleBackColor = true;
            this.buttonLoadFromFile.Click += new System.EventHandler(this.buttonLoadFromFile_Click);
            // 
            // buttonSaveToFile
            // 
            this.buttonSaveToFile.Location = new System.Drawing.Point(998, 291);
            this.buttonSaveToFile.Name = "buttonSaveToFile";
            this.buttonSaveToFile.Size = new System.Drawing.Size(89, 23);
            this.buttonSaveToFile.TabIndex = 8;
            this.buttonSaveToFile.Text = "Save to file";
            this.buttonSaveToFile.UseVisualStyleBackColor = true;
            this.buttonSaveToFile.Click += new System.EventHandler(this.buttonSaveToFile_Click);
            // 
            // AgeichenkoTestTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1099, 648);
            this.Controls.Add(this.buttonLoadFromFile);
            this.Controls.Add(this.buttonSaveToFile);
            this.Controls.Add(this.buttonLoadFromDB);
            this.Controls.Add(this.buttonSaveToDB);
            this.Controls.Add(this.buttonShortWay);
            this.Controls.Add(this.buttonDeleteEdge);
            this.Controls.Add(this.buttonDeleteNode);
            this.Controls.Add(this.pictureBoxGraph);
            this.Controls.Add(this.buttonAddEdge);
            this.Controls.Add(this.buttonAddNode);
            this.Name = "AgeichenkoTestTask";
            this.Text = "Graph Designer";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGraph)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonAddNode;
        private System.Windows.Forms.Button buttonAddEdge;
        private System.Windows.Forms.PictureBox pictureBoxGraph;
        private System.Windows.Forms.Button buttonDeleteNode;
        private System.Windows.Forms.Button buttonDeleteEdge;
        private System.Windows.Forms.Button buttonShortWay;
        private System.Windows.Forms.Button buttonSaveToDB;
        private System.Windows.Forms.Button buttonLoadFromDB;
        private System.Windows.Forms.Button buttonLoadFromFile;
        private System.Windows.Forms.Button buttonSaveToFile;
    }
}

