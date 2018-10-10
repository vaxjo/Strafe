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
                //foreach (string show in shows) comboBoxShow.Items.Add(new ShowSelectionComboItem(show));
                foreach (ShowSelectionComboItem option in shows.Select(o => new ShowSelectionComboItem(o)).OrderByDescending(o => o.Year)) comboBoxShow.Items.Add(option);
            } catch { }

            if (comboBoxShow.Items.Count == 0) comboBoxShow.Items.Add(new ShowSelectionComboItem("0,,(No Matches)"));
            comboBoxShow.SelectedItem = comboBoxShow.Items[0];
        }

        private void buttonOK_Click(object sender, EventArgs e) {
            if (comboBoxShow.SelectedItem == null) return;

            ShowSelectionComboItem selection = (ShowSelectionComboItem) comboBoxShow.SelectedItem;
            if (selection.Id == 0) return;

            SelectedShowId = selection.Id;
            SelectedShowName = selection.ShowName;
            DialogResult = DialogResult.OK;
        }
    }

    public struct ShowSelectionComboItem {
        public int Id;
        public int? Year;
        public string ShowName;

        public ShowSelectionComboItem(string show) {
            Id = Convert.ToInt32(show.Split(',')[0]);
            Year = string.IsNullOrWhiteSpace(show.Split(',')[1]) ? (int?) null : Convert.ToInt32(show.Split(',')[1]);
            ShowName = show.Split(new char[] { ',' }, 3)[2];
        }

        public override string ToString() {
            return ShowName + (Year.HasValue ? " (" + Year.Value + ")" : "");
        }
    }
}
