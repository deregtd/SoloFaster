namespace SoloFaster
{
    partial class AnalysisTape
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.velocityTape = new SoloFaster.DoubleBufferPanel();
            this.deltaTape = new SoloFaster.DoubleBufferPanel();
            //((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.velocityTape);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.deltaTape);
            this.splitContainer1.Size = new System.Drawing.Size(543, 479);
            this.splitContainer1.SplitterDistance = 327;
            this.splitContainer1.TabIndex = 0;
            // 
            // velocityTape
            // 
            this.velocityTape.Dock = System.Windows.Forms.DockStyle.Fill;
            this.velocityTape.Location = new System.Drawing.Point(0, 0);
            this.velocityTape.Name = "velocityTape";
            this.velocityTape.Size = new System.Drawing.Size(543, 327);
            this.velocityTape.TabIndex = 0;
            // 
            // deltaTape
            // 
            this.deltaTape.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deltaTape.Location = new System.Drawing.Point(0, 0);
            this.deltaTape.Name = "deltaTape";
            this.deltaTape.Size = new System.Drawing.Size(543, 148);
            this.deltaTape.TabIndex = 0;
            // 
            // AnalysisTape
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "AnalysisTape";
            this.Size = new System.Drawing.Size(543, 479);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            //((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DoubleBufferPanel velocityTape;
        private DoubleBufferPanel deltaTape;
    }
}
