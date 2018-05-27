using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Strafe {
    public partial class ShowSelectionForm : Form {
        public string SelectedShowName;
        public int SelectedShowId;

        public ShowSelectionForm(TVFile tvFile) {
            InitializeComponent();

            labelFilename.Text = tvFile.OriginalFile.Name;
            textBoxSearch.Text = tvFile.Episode.RawShowName;
        }

        private void buttonSearch_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(textBoxSearch.Text)) return;

            comboBoxShow.Items.Clear();
            try {
                List<string> shows = TVMaze.GetShowList(textBoxSearch.Text);
                foreach (string show in shows) comboBoxShow.Items.Add(new ShowSelectionComboItem(show));
            } catch { }

            if (comboBoxShow.Items.Count == 0) comboBoxShow.Items.Add(new ShowSelectionComboItem("0,(No Matches)"));
            comboBoxShow.SelectedItem = comboBoxShow.Items[0];
        }

        private void buttonOK_Click(object sender, EventArgs e) {
            ShowSelectionComboItem selection = (ShowSelectionComboItem) comboBoxShow.SelectedItem;
            SelectedShowId = selection.Id;
            SelectedShowName = selection.ShowName;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            Close();
        }

    }

    public struct ShowSelectionComboItem {
        public int Id;
        public string ShowName;

        public ShowSelectionComboItem(string show) {
            Id = Convert.ToInt32(show.Split(',')[0]);
            ShowName = show.Split(',')[1];
        }

        public override string ToString() {
            return ShowName;
        }
    }
}
