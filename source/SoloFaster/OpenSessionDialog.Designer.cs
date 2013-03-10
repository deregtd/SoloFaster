namespace SoloFaster
{
    partial class OpenSessionDialog
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
            this.openSession = new System.Windows.Forms.Button();
            this.deleteSession = new System.Windows.Forms.Button();
            this.sessionListbox = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.driverCombo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.eventCombo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openSession
            // 
            this.openSession.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.openSession.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.openSession.Enabled = false;
            this.openSession.Location = new System.Drawing.Point(247, 233);
            this.openSession.Name = "openSession";
            this.openSession.Size = new System.Drawing.Size(75, 22);
            this.openSession.TabIndex = 13;
            this.openSession.Text = "Open";
            this.openSession.UseVisualStyleBackColor = true;
            this.openSession.Click += new System.EventHandler(this.openSession_Click);
            // 
            // deleteSession
            // 
            this.deleteSession.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteSession.Location = new System.Drawing.Point(12, 233);
            this.deleteSession.Name = "deleteSession";
            this.deleteSession.Size = new System.Drawing.Size(75, 22);
            this.deleteSession.TabIndex = 12;
            this.deleteSession.Text = "Delete";
            this.deleteSession.UseVisualStyleBackColor = true;
            this.deleteSession.Click += new System.EventHandler(this.deleteSession_Click);
            // 
            // sessionListbox
            // 
            this.sessionListbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sessionListbox.FormattingEnabled = true;
            this.sessionListbox.Location = new System.Drawing.Point(15, 105);
            this.sessionListbox.Name = "sessionListbox";
            this.sessionListbox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.sessionListbox.Size = new System.Drawing.Size(307, 121);
            this.sessionListbox.Sorted = true;
            this.sessionListbox.TabIndex = 11;
            this.sessionListbox.SelectedIndexChanged += new System.EventHandler(this.sessionList_SelectedIndexChanged);
            this.sessionListbox.DoubleClick += new System.EventHandler(this.sessionListbox_DoubleClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Session(s)";
            // 
            // driverCombo
            // 
            this.driverCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.driverCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.driverCombo.FormattingEnabled = true;
            this.driverCombo.Location = new System.Drawing.Point(15, 65);
            this.driverCombo.Name = "driverCombo";
            this.driverCombo.Size = new System.Drawing.Size(307, 21);
            this.driverCombo.TabIndex = 9;
            this.driverCombo.SelectedIndexChanged += new System.EventHandler(this.driverCombo_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Driver";
            // 
            // eventCombo
            // 
            this.eventCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eventCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.eventCombo.FormattingEnabled = true;
            this.eventCombo.Location = new System.Drawing.Point(15, 25);
            this.eventCombo.Name = "eventCombo";
            this.eventCombo.Size = new System.Drawing.Size(307, 21);
            this.eventCombo.TabIndex = 7;
            this.eventCombo.SelectedIndexChanged += new System.EventHandler(this.eventCombo_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Event";
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(166, 233);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 22);
            this.cancelButton.TabIndex = 14;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // OpenSessionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 267);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.openSession);
            this.Controls.Add(this.deleteSession);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.sessionListbox);
            this.Controls.Add(this.eventCombo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.driverCombo);
            this.MinimumSize = new System.Drawing.Size(200, 250);
            this.Name = "OpenSessionDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Open Session";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openSession;
        private System.Windows.Forms.Button deleteSession;
        private System.Windows.Forms.ListBox sessionListbox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox driverCombo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox eventCombo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button cancelButton;
    }
}