namespace Strafe {
    partial class StrafeForm {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StrafeForm));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tvfile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filetype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.show = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.episode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.action = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.error = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStripFile = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.manuallySelectShowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manuallySelectEpisodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.requeryFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.setActionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openInExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFromListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonProcess = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setTVShowRootDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTVShowRootDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearShowMappingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearCacheItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStripFile.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowDrop = true;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tvfile,
            this.filename,
            this.filetype,
            this.show,
            this.episode,
            this.action,
            this.error});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStripFile;
            this.dataGridView1.Location = new System.Drawing.Point(12, 27);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.Size = new System.Drawing.Size(988, 429);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellMouseEnter);
            this.dataGridView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridView1_DragDrop);
            this.dataGridView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataGridView1_DragEnter);
            this.dataGridView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyUp);
            // 
            // tvfile
            // 
            this.tvfile.HeaderText = "TV File";
            this.tvfile.Name = "tvfile";
            this.tvfile.ReadOnly = true;
            this.tvfile.Visible = false;
            // 
            // filename
            // 
            this.filename.HeaderText = "File Name";
            this.filename.Name = "filename";
            this.filename.ReadOnly = true;
            // 
            // filetype
            // 
            this.filetype.HeaderText = "Type";
            this.filetype.Name = "filetype";
            this.filetype.ReadOnly = true;
            // 
            // show
            // 
            this.show.HeaderText = "Show";
            this.show.Name = "show";
            this.show.ReadOnly = true;
            // 
            // episode
            // 
            this.episode.HeaderText = "Episode";
            this.episode.Name = "episode";
            this.episode.ReadOnly = true;
            // 
            // action
            // 
            this.action.HeaderText = "Action";
            this.action.Name = "action";
            this.action.ReadOnly = true;
            this.action.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // error
            // 
            this.error.HeaderText = "Error";
            this.error.Name = "error";
            this.error.ReadOnly = true;
            // 
            // contextMenuStripFile
            // 
            this.contextMenuStripFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manuallySelectShowToolStripMenuItem,
            this.manuallySelectEpisodeToolStripMenuItem,
            this.requeryFileToolStripMenuItem,
            this.toolStripSeparator1,
            this.setActionToolStripMenuItem,
            this.openInExplorerToolStripMenuItem,
            this.removeFromListToolStripMenuItem});
            this.contextMenuStripFile.Name = "contextMenuStripFile";
            this.contextMenuStripFile.Size = new System.Drawing.Size(202, 142);
            this.contextMenuStripFile.Text = "Action";
            this.contextMenuStripFile.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripFile_Opening);
            // 
            // manuallySelectShowToolStripMenuItem
            // 
            this.manuallySelectShowToolStripMenuItem.Name = "manuallySelectShowToolStripMenuItem";
            this.manuallySelectShowToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.manuallySelectShowToolStripMenuItem.Text = "Manually Select &Show";
            this.manuallySelectShowToolStripMenuItem.Click += new System.EventHandler(this.manuallySelectShowToolStripMenuItem_Click);
            // 
            // manuallySelectEpisodeToolStripMenuItem
            // 
            this.manuallySelectEpisodeToolStripMenuItem.Name = "manuallySelectEpisodeToolStripMenuItem";
            this.manuallySelectEpisodeToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.manuallySelectEpisodeToolStripMenuItem.Text = "Manually Select &Episode";
            this.manuallySelectEpisodeToolStripMenuItem.Click += new System.EventHandler(this.manuallySelectEpisodeToolStripMenuItem_Click);
            // 
            // requeryFileToolStripMenuItem
            // 
            this.requeryFileToolStripMenuItem.Name = "requeryFileToolStripMenuItem";
            this.requeryFileToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.requeryFileToolStripMenuItem.Text = "Re&query File";
            this.requeryFileToolStripMenuItem.Click += new System.EventHandler(this.requeryFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(198, 6);
            // 
            // setActionToolStripMenuItem
            // 
            this.setActionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ignoreToolStripMenuItem1,
            this.deleteToolStripMenuItem1,
            this.renameToolStripMenuItem1});
            this.setActionToolStripMenuItem.Name = "setActionToolStripMenuItem";
            this.setActionToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.setActionToolStripMenuItem.Text = "Set &Action";
            // 
            // ignoreToolStripMenuItem1
            // 
            this.ignoreToolStripMenuItem1.Name = "ignoreToolStripMenuItem1";
            this.ignoreToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.ignoreToolStripMenuItem1.Text = "&Ignore";
            this.ignoreToolStripMenuItem1.Click += new System.EventHandler(this.ignoreToolStripMenuItem1_Click);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.deleteToolStripMenuItem1.Text = "&Delete";
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.deleteToolStripMenuItem1_Click);
            // 
            // renameToolStripMenuItem1
            // 
            this.renameToolStripMenuItem1.Name = "renameToolStripMenuItem1";
            this.renameToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.renameToolStripMenuItem1.Text = "&Rename";
            this.renameToolStripMenuItem1.Click += new System.EventHandler(this.renameToolStripMenuItem1_Click);
            // 
            // openInExplorerToolStripMenuItem
            // 
            this.openInExplorerToolStripMenuItem.Name = "openInExplorerToolStripMenuItem";
            this.openInExplorerToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.openInExplorerToolStripMenuItem.Text = "Open in E&xplorer";
            this.openInExplorerToolStripMenuItem.Click += new System.EventHandler(this.openInExplorerToolStripMenuItem_Click);
            // 
            // removeFromListToolStripMenuItem
            // 
            this.removeFromListToolStripMenuItem.Name = "removeFromListToolStripMenuItem";
            this.removeFromListToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.removeFromListToolStripMenuItem.Text = "&Remove from List";
            this.removeFromListToolStripMenuItem.Click += new System.EventHandler(this.removeFromListToolStripMenuItem_Click);
            // 
            // buttonProcess
            // 
            this.buttonProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonProcess.BackColor = System.Drawing.SystemColors.Highlight;
            this.buttonProcess.Enabled = false;
            this.buttonProcess.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonProcess.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonProcess.Location = new System.Drawing.Point(858, 462);
            this.buttonProcess.Name = "buttonProcess";
            this.buttonProcess.Size = new System.Drawing.Size(142, 33);
            this.buttonProcess.TabIndex = 1;
            this.buttonProcess.Text = "Process Files";
            this.buttonProcess.UseVisualStyleBackColor = false;
            this.buttonProcess.Click += new System.EventHandler(this.buttonProcess_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Locate the root directory for your TV shows.";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1012, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setTVShowRootDirectoryToolStripMenuItem,
            this.openTVShowRootDirectoryToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // setTVShowRootDirectoryToolStripMenuItem
            // 
            this.setTVShowRootDirectoryToolStripMenuItem.Name = "setTVShowRootDirectoryToolStripMenuItem";
            this.setTVShowRootDirectoryToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.setTVShowRootDirectoryToolStripMenuItem.Text = "&Set TV Show Root Directory";
            this.setTVShowRootDirectoryToolStripMenuItem.Click += new System.EventHandler(this.setTVShowRootDirectoryToolStripMenuItem_Click);
            // 
            // openTVShowRootDirectoryToolStripMenuItem
            // 
            this.openTVShowRootDirectoryToolStripMenuItem.Name = "openTVShowRootDirectoryToolStripMenuItem";
            this.openTVShowRootDirectoryToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.openTVShowRootDirectoryToolStripMenuItem.Text = "&Explore TV Show Root Directory";
            this.openTVShowRootDirectoryToolStripMenuItem.Click += new System.EventHandler(this.openTVShowRootDirectoryToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearShowMappingsToolStripMenuItem,
            this.clearCacheItemsToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "&Settings";
            // 
            // clearShowMappingsToolStripMenuItem
            // 
            this.clearShowMappingsToolStripMenuItem.Name = "clearShowMappingsToolStripMenuItem";
            this.clearShowMappingsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.clearShowMappingsToolStripMenuItem.Text = "Clear Show &Mappings";
            this.clearShowMappingsToolStripMenuItem.Click += new System.EventHandler(this.clearShowMappingsToolStripMenuItem_Click);
            // 
            // clearCacheItemsToolStripMenuItem
            // 
            this.clearCacheItemsToolStripMenuItem.Name = "clearCacheItemsToolStripMenuItem";
            this.clearCacheItemsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.clearCacheItemsToolStripMenuItem.Text = "Clear &Cache Items";
            this.clearCacheItemsToolStripMenuItem.Click += new System.EventHandler(this.clearCacheItemsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(13, 462);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(839, 32);
            this.progressBar1.TabIndex = 3;
            this.progressBar1.Visible = false;
            // 
            // StrafeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 507);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.buttonProcess);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "StrafeForm";
            this.Text = "Strafe";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StrafeForm_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStripFile.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFile;
        private System.Windows.Forms.ToolStripMenuItem openInExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setActionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ignoreToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem removeFromListToolStripMenuItem;
        private System.Windows.Forms.Button buttonProcess;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setTVShowRootDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openTVShowRootDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStripMenuItem manuallySelectEpisodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manuallySelectShowToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn tvfile;
        private System.Windows.Forms.DataGridViewTextBoxColumn filename;
        private System.Windows.Forms.DataGridViewTextBoxColumn filetype;
        private System.Windows.Forms.DataGridViewTextBoxColumn show;
        private System.Windows.Forms.DataGridViewTextBoxColumn episode;
        private System.Windows.Forms.DataGridViewTextBoxColumn action;
        private System.Windows.Forms.DataGridViewTextBoxColumn error;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem requeryFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearShowMappingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearCacheItemsToolStripMenuItem;
    }
}

