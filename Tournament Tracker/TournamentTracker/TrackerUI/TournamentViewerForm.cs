using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI {
    public partial class TournamentViewerForm : Form {

        private TournamentModel tournament;
        BindingList<int> rounds = new BindingList<int>();
        BindingList<MatchupModel> selectedMatchups = new BindingList<MatchupModel>();

        public TournamentViewerForm(TournamentModel tournamentModel) {
            InitializeComponent();

            tournament.OnTournamentComplete += Tournament_OnTournamentComplete;

            tournament = tournamentModel;

            WireUpLists();

            LoadFormData();

            LoadRounds();
        }

        private void Tournament_OnTournamentComplete(object sender, DateTime e) {
            this.Close();
        }

        private void LoadFormData() {
            tournamentName.Text = tournament.TournamentName;
        }

        private void WireUpLists() {
            RoundDropdown.DataSource = rounds;
            matchupListBox.DataSource = selectedMatchups;
            matchupListBox.DisplayMember = "DisplayName";
        }

        private void LoadRounds() {
            rounds.Clear();

            rounds.Add(1);
            int currRound = 1;

            foreach (List<MatchupModel> matchups in tournament.Rounds) {
                if (matchups.First().MatchupRound > currRound) {
                    currRound = matchups.First().MatchupRound;
                    rounds.Add(currRound);
                }
            }

            LoadMatchups(1);
        }

        #region Please Don't Use
        private void label1_Click(object sender, EventArgs e) {
            //DO NOT USE!!!!!
        }

        #endregion

        private void RoundDropdown_SelectedIndexChanged(object sender, EventArgs e) {
            LoadMatchups((int)RoundDropdown.SelectedItem);
        }

        //dealing with a list of matchup
        private void LoadMatchups(int round) {

            foreach (List<MatchupModel> matchups in tournament.Rounds) {
                if (matchups.First().MatchupRound == round) {
                    selectedMatchups.Clear();
                    foreach (MatchupModel m in matchups) {
                        if (m.Winner == null || !UnplayedOnlyCheckbox.Checked) {
                            selectedMatchups.Add(m);
                        }
                    }
                }
            }

            if (selectedMatchups.Count > 0) {
                LoadMatchup(selectedMatchups.First()); 
            }

            DisplayMatchupInfo();
        }

        private void DisplayMatchupInfo() {
            bool isVisible = (selectedMatchups.Count > 0);

            TeamOneNameLabel.Visible = isVisible;
            TeamOneScoreLabel.Visible = isVisible;
            TeamOneScoreValue.Visible = isVisible;
            TeamTwoNameLabel.Visible = isVisible;
            TeamTwoScoreLabel.Visible = isVisible;
            TeamTwoScoreValue.Visible = isVisible;
            versusLabel.Visible = isVisible;
            scoreButton.Visible = isVisible;

        }

        //dealing with one matchup
        private void LoadMatchup(MatchupModel m) {

            for (int i = 0; i < m.Entries.Count; i++) {
                if (i == 0) {
                    if (m.Entries[0].TeamCompeting != null) {
                        TeamOneNameLabel.Text = m.Entries[0].TeamCompeting.TeamName;
                        TeamOneScoreValue.Text = m.Entries[0].Score.ToString();

                        TeamTwoNameLabel.Text = "<bye>";
                        TeamTwoScoreValue.Text = "0";
                    }
                    else {
                        TeamOneNameLabel.Text = "Not Yet Set";
                        TeamOneScoreValue.Text = "";
                    }
                }
                if (i == 1) {
                    if (m.Entries[1].TeamCompeting != null) {
                        TeamTwoNameLabel.Text = m.Entries[1].TeamCompeting.TeamName;
                        TeamTwoScoreValue.Text = m.Entries[1].Score.ToString();
                    }
                    else {
                        TeamTwoNameLabel.Text = "Not Yet Set";
                        TeamTwoScoreValue.Text = "";
                    }
                }
            }
        }

        private void matchupListBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (matchupListBox.SelectedItem != null) {
                LoadMatchup((MatchupModel)matchupListBox.SelectedItem);
            }
        }

        private void UnplayedOnlyCheckbox_CheckedChanged(object sender, EventArgs e) {
            LoadMatchups((int)RoundDropdown.SelectedItem);
        }

        private string ValidateData () {
            string output = "";

            double teamOneScore = 0;
            double teamTwoScore = 0;

            bool scoreOneValid = double.TryParse(TeamOneScoreValue.Text, out teamOneScore);
            bool scoreTwoValid = double.TryParse(TeamTwoScoreValue.Text, out teamTwoScore);

            if (!scoreOneValid) {
                output = "The Score One value is not a valid number.";
            } else if (!scoreTwoValid) {
                output = "The Score Two value is not a valid number.";
            } else if (teamOneScore == 0 && teamTwoScore == 0) {
                output = "You did not enter a score for either team.";
            } else if (teamOneScore == teamTwoScore) {
                output = "We do not allow ties in this application.";
            }

            return output;
        }

        private void scoreButton_Click(object sender, EventArgs e) {
            string errorMessage = ValidateData();

            if (errorMessage.Length != 0) {
                MessageBox.Show($"Input Error: {errorMessage}");
                return;
            }

            MatchupModel m = (MatchupModel)matchupListBox.SelectedItem;
            double teamOneScore = 0;
            double teamTwoScore = 0;

            for (int i = 0; i < m.Entries.Count; i++) {
                if (i == 0) {
                    if (m.Entries[0].TeamCompeting != null) {
                        bool scoreValid = double.TryParse(TeamOneScoreValue.Text, out teamOneScore);

                        if (scoreValid) {
                            m.Entries[0].Score = teamOneScore; 
                        } else {
                            MessageBox.Show("Please enter a valid score for Team One");
                            return;
                        }
                    }
                }
                if (i == 1) {
                    if (m.Entries[1].TeamCompeting != null) {
                        bool scoreValid = double.TryParse(TeamTwoScoreValue.Text, out teamTwoScore);

                        if (scoreValid) {
                            m.Entries[1].Score = teamTwoScore;
                        }
                        else {
                            MessageBox.Show("Please enter a valid score for Team Two");
                            return;
                        }
                    }
                }
            }

            try {
                TournamentLogic.UpdateTournamentResults(tournament);
            } catch (Exception ex) {
                MessageBox.Show($"The application had the following error: {ex.Message}");
                return;
            }

            LoadMatchups((int)RoundDropdown.SelectedItem);
        }
    }
}