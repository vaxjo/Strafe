using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Strafe {
    public partial class EpisodeSelectionForm : Form {
        public int SelectedSeason;
        public int SelectedEpisodeNumber;
        public string SelectedEpisodeName;

        public EpisodeSelectionForm(TVFile tvFile, string previouslySelectedEpisodeName = "") {
            InitializeComponent();

            labelFilepath.Text = tvFile.OriginalFile.Name;
            labelShowName.Text = tvFile.Episode.ShowName;

            // get list of episodes and display in combo box
            try {
                List<string> episodeList = TVMaze.GetEpisodeList(tvFile.Episode.TVMazeId);
                episodeList.Reverse(); // let's make the last one first
                foreach (string episodeResult in episodeList) {
                    comboBoxEpisodes.Items.Add(new EpisodeSelectionComboItem(episodeResult));
                }
            } catch (TVMazeException tvExc) {
                // something went wrong, couldn't get episodes
            }

            if (comboBoxEpisodes.Items.Count == 0) comboBoxEpisodes.Items.Add(new EpisodeSelectionComboItem("-1,-1,(No Episodes)"));
            comboBoxEpisodes.SelectedItem = comboBoxEpisodes.Items[0];

            if (!string.IsNullOrWhiteSpace(previouslySelectedEpisodeName)) {
                for (int i = 0; i < comboBoxEpisodes.Items.Count; i++) {
                    var esci = (EpisodeSelectionComboItem)comboBoxEpisodes.Items[i];
                    if (esci.EpisodeName == previouslySelectedEpisodeName) {
                        comboBoxEpisodes.SelectedItem = comboBoxEpisodes.Items[i - 1];
                        break;
                    }
                }
            }
        }

        private void EpisodeSelectionForm_Shown(object sender, EventArgs e) {
            comboBoxEpisodes.Focus();
        }

        private void buttonOK_Click(object sender, EventArgs e) {
            EpisodeSelectionComboItem selection = (EpisodeSelectionComboItem) comboBoxEpisodes.SelectedItem;
            if (selection.Season >= 0) {
                SelectedSeason = selection.Season;
                SelectedEpisodeName = selection.EpisodeName;
                SelectedEpisodeNumber = selection.EpisodeNumber;
            }
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            Close();
        }

        private void buttonSkip_Click(object sender, EventArgs e) {
            Close();
        }

    }


    public struct EpisodeSelectionComboItem {
        public int Season, EpisodeNumber;
        public string EpisodeName;

        public EpisodeSelectionComboItem(string episode) {
            Season = Convert.ToInt32(episode.Split(',')[0]);
            EpisodeNumber = Convert.ToInt32(episode.Split(',')[1]);
            EpisodeName = episode.Split(',')[2];
        }

        public override string ToString() {
            return "s" + Season.ToString("D2") + " e" + EpisodeNumber.ToString("D2") + " - " + EpisodeName;
        }
    }
}
