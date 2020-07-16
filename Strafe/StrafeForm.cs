using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Strafe {
    public partial class StrafeForm : Form {
        public static Config Config;
        public static Cache Cache;

        private static FileInfo LogFile {
            get {
                FileInfo entry = new FileInfo(System.Reflection.Assembly.GetCallingAssembly().Location);
                return new FileInfo(Path.Combine(entry.Directory.FullName, @"strafe.log"));
            }
        }

        public StrafeForm() {
            InitializeComponent();

            if (LogFile.Exists) LogFile.Delete();
        }

        private void Form1_Load(object sender, EventArgs e) {
            Config = new Config();
            Log("Strafe " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + " starting");

            Log("Setting up backgroundWorker");
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;

            Log("Getting Cache");
            Cache = new Cache();
        }

        private void Form1_Shown(object sender, EventArgs e) {
            // if the tv show root directory is missing, pop a directory browser dialog right away
            if (string.IsNullOrWhiteSpace(Config.TVShowRootFilepath)) {
                Log("TV Show Root Directory config property missing - asking for it");
                if (folderBrowserDialog1.ShowDialog() == DialogResult.Cancel) Application.Exit();
                Config.TVShowRootFilepath = folderBrowserDialog1.SelectedPath;
                Config.Save();
            }
        }

        private void dataGridView1_DragEnter(object sender, DragEventArgs e) {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e) {
            buttonProcess.Enabled = false;
            foreach (string s in (string[])e.Data.GetData(DataFormats.FileDrop, false)) AddFileSystemObject(s);
            progressBar1.Visible = true;
            if (!backgroundWorker1.IsBusy) backgroundWorker1.RunWorkerAsync();
        }

        /// <summary> Add a file or directory to the grid. </summary>
        private void AddFileSystemObject(string fileOrDir) {
            if (!Directory.Exists(fileOrDir)) {
                AddFile(new FileInfo(fileOrDir));
                return;
            }

            foreach (string filename in Directory.GetFiles(fileOrDir)) AddFileSystemObject(filename);

            foreach (string subdir in Directory.GetDirectories(fileOrDir)) AddFileSystemObject(subdir);
        }

        public void AddFile(FileInfo addFile) {
            foreach (DataGridViewRow row in dataGridView1.Rows) {
                TVFile tvf = GetRowTVFile(row);
                if (tvf.OriginalFile.FullName == addFile.FullName) return; // already exists
            }

            TVFile tvFile = new TVFile(addFile);
            Log("Adding [" + addFile.FullName + "]");
            int rowIndex = dataGridView1.Rows.Add(new object[] { tvFile });
            UpdateDataGridRow(dataGridView1.Rows[rowIndex]);
        }

        public void UpdateDataGridRow(DataGridViewRow row) {
            TVFile tvFile = GetRowTVFile(row);
            row.Cells["filename"].Value = tvFile.OriginalFile.Name;
            row.Cells["filetype"].Value = tvFile.Extension_Trimmed;
            row.Cells["action"].Value = tvFile.Action;
            row.Cells["error"].Value = tvFile.ErrorMessage;
            row.Cells["show"].Value = tvFile.Episode != null ? tvFile.Episode.ShowName : "";
            row.Cells["episode"].Value = tvFile.Episode != null ? tvFile.Episode.EpisodeName : "";
        }

        #region Background Worker for doing API stuff
        // https://www.codeproject.com/articles/99143/backgroundworker-class-sample-for-beginners

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e) {
            Log("Begin background episode processing");
            while (ProcessRow())
                ;
        }

        /// <summary> Process a row of the data grid. Returns false if there are no more rows to lookup. </summary>
        protected bool ProcessRow() {
            foreach (DataGridViewRow row in dataGridView1.Rows) {
                TVFile tvFile = GetRowTVFile(row);
                if (!tvFile.NeedsProcessing) continue;

                int numExceptions = 0;
                try {
                    tvFile.PopulateEpisode();
                    UpdateDataGridRow(row);

                } catch (Exception e) {
                    // this can happen if the eu removes items from the list while process is occurring
                    if (numExceptions++ > 10) throw e; // unless it's actually a problem
                }

                backgroundWorker1.ReportProgress(0);
                return true;
            }

            return false;
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            Log("Background episode processing complete");
            buttonProcess.Enabled = dataGridView1.Rows.Count > 0;
            dataGridView1.AutoResizeColumns();
            progressBar1.Visible = false;
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            int n = 0, max = dataGridView1.Rows.Count;
            foreach (DataGridViewRow row in dataGridView1.Rows)
                if (!GetRowTVFile(row).NeedsProcessing) n++;

            progressBar1.Maximum = max;
            progressBar1.Value = n;
        }

        #endregion

        /// <summary> Get the TVFile associated with the currently clicked grid row. </summary>
        private TVFile GetSelectedRowTVFile() {
            int rowIndex = (int)contextMenuStripFile.Tag;
            return GetRowTVFile(dataGridView1.Rows[rowIndex]);
        }

        /// <summary> Get the TVFile associated with the specified row. </summary>
        private TVFile GetRowTVFile(DataGridViewRow row) {
            // return TVFiles.FirstOrDefault(o => o.Id == (Guid) row.Cells["id"].Value);
            return (TVFile)row.Cells["tvfile"].Value;
        }

        #region Context Menu Items

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e) {
            // we do this to make it easy later to figure out which row was clicked on
            contextMenuStripFile.Tag = e.RowIndex;
        }

        private void contextMenuStripFile_Opening(object sender, CancelEventArgs e) {
            // Tag should contain the current RowIndex
            if (contextMenuStripFile.Tag == null || (int)contextMenuStripFile.Tag < 0) e.Cancel = true;

            int rowIndex = (int)contextMenuStripFile.Tag;
            DataGridViewRow row = dataGridView1.Rows[rowIndex];

            // if the current item is not selected, select it now
            if (!dataGridView1.SelectedRows.Contains(row)) {
                dataGridView1.ClearSelection();
                row.Selected = true;
            }

            TVFile tvFile = GetSelectedRowTVFile();
            manuallySelectShowToolStripMenuItem.Enabled = tvFile.Episode != null;
            manuallySelectEpisodeToolStripMenuItem.Enabled = tvFile.Episode != null;
        }

        private void manuallySelectShowToolStripMenuItem_Click(object sender, EventArgs e) {
            TVFile tvFile = GetSelectedRowTVFile(); // GetRowTVFile(dataGridView1.SelectedRows[0]);
            if (tvFile.Episode == null) return;

            ShowSelectionForm showForm = new ShowSelectionForm(tvFile);
            if (showForm.ShowDialog() == DialogResult.Cancel || showForm.SelectedShowId == 0) return;

            foreach (DataGridViewRow row in dataGridView1.SelectedRows) {
                // set show for each selected item
                tvFile = GetRowTVFile(row);
                if (tvFile.Episode == null) continue;

                // set cache item 
                Config.SetTVMazeMapping(tvFile.Episode.RawShowName, showForm.SelectedShowId);
                tvFile.Episode = null;
                tvFile.Action = Config.DefaultAction;
                UpdateDataGridRow(row);
            }
            Config.Save();

            // start the background process
            if (!backgroundWorker1.IsBusy) backgroundWorker1.RunWorkerAsync();
        }

        private void manuallySelectEpisodeToolStripMenuItem_Click(object sender, EventArgs e) {
            // order the selected rows
            List<DataGridViewRow> sortedRows = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dataGridView1.SelectedRows) {
                sortedRows.Add(row);
            }
            sortedRows = sortedRows.OrderBy(o => o.Cells["filename"].Value).ToList();

            string previouslySelectedEpisodeName = "";
            foreach (DataGridViewRow row in sortedRows) {
                TVFile tvFile = GetRowTVFile(row);
                if (tvFile.Episode == null || string.IsNullOrWhiteSpace(tvFile.Episode.ShowName)) continue; // must at least have the showname

                // show chooser dialog
                EpisodeSelectionForm episodeForm = new EpisodeSelectionForm(tvFile, previouslySelectedEpisodeName);
                DialogResult result = episodeForm.ShowDialog();
                if (result == DialogResult.Cancel) return;
                if (result == DialogResult.Ignore || string.IsNullOrWhiteSpace(episodeForm.SelectedEpisodeName)) continue;

                // update season/episode
                tvFile.Episode.Season = episodeForm.SelectedSeason;
                tvFile.Episode.EpisodeNumber = episodeForm.SelectedEpisodeNumber;
                tvFile.Episode.EpisodeName = episodeForm.SelectedEpisodeName;
                previouslySelectedEpisodeName = episodeForm.SelectedEpisodeName;
                tvFile.Action = Config.DefaultAction;
                tvFile.ErrorMessage = "";
                UpdateDataGridRow(row);
            }
        }

        private void requeryFileToolStripMenuItem_Click(object sender, EventArgs e) {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows) {
                TVFile tvFile = GetRowTVFile(row);
                tvFile.Action = Config.DefaultAction;
                tvFile.ErrorMessage = "";
                tvFile.Episode = null;
                UpdateDataGridRow(row);
            }

            // start the background process
            if (!backgroundWorker1.IsBusy) backgroundWorker1.RunWorkerAsync();
        }

        private void ignoreToolStripMenuItem1_Click(object sender, EventArgs e) {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows) SetRowAction(row, TVFile.Actions.Ignore);
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e) {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows) SetRowAction(row, TVFile.Actions.Delete);
        }

        private void moveToolStripMenuItem_Click(object sender, EventArgs e) {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows) SetRowAction(row, TVFile.Actions.Move);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e) {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows) SetRowAction(row, TVFile.Actions.Copy);
        }

        private void SetRowAction(DataGridViewRow row, TVFile.Actions action) {
            TVFile tvFile = GetRowTVFile(row);
            tvFile.Action = action;
            UpdateDataGridRow(row);
        }

        private void openInExplorerToolStripMenuItem_Click(object sender, EventArgs e) {
            List<string> filepaths = new List<string>();
            foreach (DataGridViewRow row in dataGridView1.SelectedRows) filepaths.Add(GetRowTVFile(row).OriginalFile.Directory.FullName);
            foreach (string filepath in filepaths.Distinct()) System.Diagnostics.Process.Start(filepath);
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Delete) removeFromListToolStripMenuItem_Click(sender, e);
        }

        private void removeFromListToolStripMenuItem_Click(object sender, EventArgs e) {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows) dataGridView1.Rows.Remove(row);
        }

        #endregion

        private void buttonProcess_Click(object sender, EventArgs e) {
            Log("Begin action processing");
            List<DirectoryInfo> processedDirectories = new List<DirectoryInfo>();

            int n = progressBar1.Value = 1;
            progressBar1.Maximum = dataGridView1.Rows.Count + 1;
            progressBar1.Visible = true;
            foreach (DataGridViewRow row in dataGridView1.Rows) {
                TVFile tvFile = GetRowTVFile(row);
                bool anyTrouble = false;
                try {
                    switch (tvFile.Action) {
                        case TVFile.Actions.Ignore:
                            break;

                        case TVFile.Actions.Delete:
                            processedDirectories.Add(tvFile.OriginalFile.Directory);
                            tvFile.OriginalFile.Delete();
                            break;

                        case TVFile.Actions.Move:
                        case TVFile.Actions.Copy:
                            processedDirectories.Add(tvFile.OriginalFile.Directory);
                            FileInfo newFile = new FileInfo(Path.Combine(Config.TVShowRoot.FullName, tvFile.GetNewFilepath()));
                            if (tvFile.OriginalFile.FullName.ToLower() == newFile.FullName.ToLower()) break; // file is already where it needs to be

                            if (!newFile.Directory.Exists) newFile.Directory.Create();
                            if (newFile.Exists && Config.ReplaceExistingFiles) {
                                newFile.Delete();
                                newFile.Refresh(); // if we don't do this, then the subsequent call to .Exists is wrong
                            }
                            if (!newFile.Exists) {
                                if (tvFile.Action == TVFile.Actions.Move) tvFile.OriginalFile.MoveTo(newFile.FullName);
                                else if (tvFile.Action == TVFile.Actions.Copy) tvFile.OriginalFile.CopyTo(newFile.FullName); // this hangs with large files - maybe implement this some day: https://stackoverflow.com/a/27179497/3140552 or this https://www.pinvoke.net/default.aspx/kernel32.CopyFileEx
                            } else {
                                anyTrouble = true;
                                tvFile.Action = TVFile.Actions.Ignore;
                                tvFile.ErrorMessage = "Destination file exists";
                                UpdateDataGridRow(row);
                            }
                            break;
                    }

                } catch (Exception exc) {
                    anyTrouble = true;
                    tvFile.Action = TVFile.Actions.Error;
                    tvFile.ErrorMessage = exc.Message;
                    UpdateDataGridRow(row);
                }

                if (!anyTrouble) row.Visible = false;
                progressBar1.Value = ++n;
                Application.DoEvents();
            }
            progressBar1.Visible = false;

            while (DeleteInvisibleRow())
                ;

            dataGridView1.AutoResizeColumns();

            // clean up empty directories (deepest ones first)
            Log("Cleaning up empty directories");
            foreach (string directoryName in processedDirectories.Select(o => o.FullName).Distinct().OrderByDescending(o => o.Length)) {
                DirectoryInfo di = new DirectoryInfo(directoryName);
                Log("Checking [" + di.FullName + "]");
                di.Refresh(); // maybe this will clear up some of these random errors
                if (di.Exists && di.GetDirectories().Count() + di.GetFiles().Count() == 0) {
                    Log("Deleting [" + di.FullName + "]");
                    di.Delete();
                }
            }

            buttonProcess.Enabled = dataGridView1.Rows.Count > 0;
        }

        /// <summary> Tries to remove one invisible row from the data grid and returns true on success. </summary>
        protected bool DeleteInvisibleRow() {
            foreach (DataGridViewRow row in dataGridView1.Rows) {
                if (row.Visible) continue;

                dataGridView1.Rows.Remove(row);
                return true;
            }

            return false;
        }

        #region Main Menu

        private void setTVShowRootDirectoryToolStripMenuItem_Click(object sender, EventArgs e) {
            folderBrowserDialog1.SelectedPath = Config.TVShowRootFilepath;
            if (folderBrowserDialog1.ShowDialog() != DialogResult.Cancel) {
                Config.TVShowRootFilepath = folderBrowserDialog1.SelectedPath;
                Config.Save();
            }
        }

        private void openTVShowRootDirectoryToolStripMenuItem_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start(Config.TVShowRootFilepath);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void clearShowMappingsToolStripMenuItem_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Remove all locally mapped shows?", "Confirm Show Mappings Clear", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK) return;

            Config.ShowMappings.Clear();
            Config.Save();
        }

        private void clearCacheItemsToolStripMenuItem_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Remove all cache items?", "Confirm Cache Clear", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK) return;

            Cache.Clear();
        }

        private void onlineToolStripMenuItem_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("http://jarrin.net/Strafe"));
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            About_Box.FormAbout about = new About_Box.FormAbout() { Description = "Strafe helps you organize your TV show video files by querying web services (like TVMaze.com) to determine the canonical show and episode names and moving and renaming your files in a consistent manner." };
            about.ShowDialog();
        }

        #endregion

        public static void Log(string message) {
            if (!Config.Logging) return;
            File.AppendAllText(LogFile.FullName, DateTime.Now.ToString("yyMMdd HHmmss") + "\t" + message + "\r\n");
        }

        private void StrafeForm_FormClosed(object sender, FormClosedEventArgs e) {
            Cache.Save();
            Log("Strafe closing normally");
        }

    }
}
