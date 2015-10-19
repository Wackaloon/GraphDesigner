namespace GraphDesigner
{
    partial class SettingsForm
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
            this.labelNode = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.labelEdge = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.labelEdgeShort = new System.Windows.Forms.Label();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.labelNodeShort = new System.Windows.Forms.Label();
            this.buttonAccept = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelNode
            // 
            this.labelNode.AutoSize = true;
            this.labelNode.Location = new System.Drawing.Point(9, 12);
            this.labelNode.Name = "labelNode";
            this.labelNode.Size = new System.Drawing.Size(35, 13);
            this.labelNode.TabIndex = 0;
            this.labelNode.Text = "label1";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 28);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(170, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(12, 68);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(170, 21);
            this.comboBox2.TabIndex = 3;
            this.comboBox2.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            // 
            // labelEdge
            // 
            this.labelEdge.AutoSize = true;
            this.labelEdge.Location = new System.Drawing.Point(12, 52);
            this.labelEdge.Name = "labelEdge";
            this.labelEdge.Size = new System.Drawing.Size(35, 13);
            this.labelEdge.TabIndex = 2;
            this.labelEdge.Text = "label2";
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(12, 108);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(170, 21);
            this.comboBox3.TabIndex = 5;
            this.comboBox3.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            // 
            // labelEdgeShort
            // 
            this.labelEdgeShort.AutoSize = true;
            this.labelEdgeShort.Location = new System.Drawing.Point(12, 92);
            this.labelEdgeShort.Name = "labelEdgeShort";
            this.labelEdgeShort.Size = new System.Drawing.Size(35, 13);
            this.labelEdgeShort.TabIndex = 4;
            this.labelEdgeShort.Text = "label3";
            // 
            // comboBox4
            // 
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(12, 148);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(170, 21);
            this.comboBox4.TabIndex = 7;
            this.comboBox4.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            // 
            // labelNodeShort
            // 
            this.labelNodeShort.AutoSize = true;
            this.labelNodeShort.Location = new System.Drawing.Point(12, 132);
            this.labelNodeShort.Name = "labelNodeShort";
            this.labelNodeShort.Size = new System.Drawing.Size(35, 13);
            this.labelNodeShort.TabIndex = 6;
            this.labelNodeShort.Text = "label4";
            // 
            // buttonAccept
            // 
            this.buttonAccept.Location = new System.Drawing.Point(107, 190);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(75, 23);
            this.buttonAccept.TabIndex = 8;
            this.buttonAccept.Text = "button1";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 225);
            this.Controls.Add(this.buttonAccept);
            this.Controls.Add(this.comboBox4);
            this.Controls.Add(this.labelNodeShort);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.labelEdgeShort);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.labelEdge);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.labelNode);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelNode;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label labelEdge;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label labelEdgeShort;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.Label labelNodeShort;
        private System.Windows.Forms.Button buttonAccept;
    }
}