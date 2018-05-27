namespace Strafe {
    partial class EpisodeSelectionForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.comboBoxEpisodes = new System.Windows.Forms.ComboBox();
            this.labelFilepath = new System.Windows.Forms.Label();
            this.labelShowName = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSkip = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxEpisodes
            // 
            this.comboBoxEpisodes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxEpisodes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEpisodes.FormattingEnabled = true;
            this.comboBoxEpisodes.Location = new System.Drawing.Point(9, 75);
            this.comboBoxEpisodes.Margin = new System.Windows.Forms.Padding(0, 0, 10, 20);
            this.comboBoxEpisodes.Name = "comboBoxEpisodes";
            this.comboBoxEpisodes.Size = new System.Drawing.Size(358, 21);
            this.comboBoxEpisodes.TabIndex = 5;
            // 
            // labelFilepath
            // 
            this.labelFilepath.AutoSize = true;
            this.labelFilepath.Location = new System.Drawing.Point(9, 42);
            this.labelFilepath.Margin = new System.Windows.Forms.Padding(0, 0, 10, 20);
            this.labelFilepath.Name = "labelFilepath";
            this.labelFilepath.Size = new System.Drawing.Size(54, 13);
            this.labelFilepath.TabIndex = 1;
            this.labelFilepath.Text = "File Name";
            // 
            // labelShowName
            // 
            this.labelShowName.AutoSize = true;
            this.labelShowName.Location = new System.Drawing.Point(9, 9);
            this.labelShowName.Margin = new System.Windows.Forms.Padding(0, 0, 10, 20);
            this.labelShowName.Name = "labelShowName";
            this.labelShowName.Size = new System.Drawing.Size(65, 13);
            this.labelShowName.TabIndex = 3;
            this.labelShowName.Text = "Show Name";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(292, 114);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(211, 114);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSkip
            // 
            this.buttonSkip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSkip.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.buttonSkip.Location = new System.Drawing.Point(130, 114);
            this.buttonSkip.Name = "buttonSkip";
            this.buttonSkip.Size = new System.Drawing.Size(75, 23);
            this.buttonSkip.TabIndex = 1;
            this.buttonSkip.Text = "Skip";
            this.buttonSkip.UseVisualStyleBackColor = true;
            this.buttonSkip.Click += new System.EventHandler(this.buttonSkip_Click);
            // 
            // EpisodeSelectionForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(386, 171);
            this.ControlBox = false;
            this.Controls.Add(this.buttonSkip);
            this.Controls.Add(this.comboBoxEpisodes);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelFilepath);
            this.Controls.Add(this.labelShowName);
            this.MinimumSize = new System.Drawing.Size(402, 187);
            this.Name = "EpisodeSelectionForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Episode Selection";
            this.Shown += new System.EventHandler(this.EpisodeSelectionForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelFilepath;
        private System.Windows.Forms.Label labelShowName;
        private System.Windows.Forms.ComboBox comboBoxEpisodes;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSkip;
    }
}