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
    
    public partial class CreateTournamentform : Form, IPrizeRequester, ITeamRequester {

        List<TeamModel> availableTeams = GlobalConfig.Connection.GetTeam_All();
        List<TeamModel> selectedTeams = new List<TeamModel>();
        List<PrizeModel> selectedPrizes = new List<PrizeModel>();

        public CreateTournamentform() {
            InitializeComponent();

            WireUpLists();
        }

        private void WireUpLists() {
            SelectTeamDropDown.DataSource = null;

            SelectTeamDropDown.DataSource = availableTeams;
            SelectTeamDropDown.DisplayMember = "TeamName";

            TournamentTeamsListBox.DataSource = null;

            TournamentTeamsListBox.DataSource = selectedTeams;
            TournamentTeamsListBox.DisplayMember = "TeamName";

            PrizesListBox.DataSource = null;

            PrizesListBox.DataSource = selectedPrizes;
            PrizesListBox.DisplayMember = "PlaceName";
        }

        private void AddTeamButton_Click(object sender, EventArgs e) {
            TeamModel t = (TeamModel)SelectTeamDropDown.SelectedItem;

            if (t != null) {
                availableTeams.Remove(t);
                selectedTeams.Add(t);

                WireUpLists();
            }
        }

        private void CreatePrizeButton_Click(object sender, EventArgs e) {
            // call the createprizeform
            CreatePrizeForm frm = new CreatePrizeForm(this);
            frm.Show();
        }

        public void PrizeComplete(PrizeModel model) {
            // get back from the form a prizemodel
            //take the prizemodel and put it into our list<prize>selectedprizes
            selectedPrizes.Add(model);

            WireUpLists();
        }

        public void TeamComplete(TeamModel model) {
            selectedTeams.Add(model);

            WireUpLists();
        }

        private void CreateNewTeamLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            CreateTeamForm frm = new CreateTeamForm(this);
            frm.Show();
        }

        private void RemoveSelectedTeamButton_Click(object sender, EventArgs e) {
            //PersonModel p = (PersonModel)TeamMembersListBox.SelectedItem;

            //if (p != null) {
            //    selectedTeamMembers.Remove(p);
            //    availableTeamMembers.Add(p);

            //    WireUpLists();

            TeamModel t = (TeamModel)TournamentTeamsListBox.SelectedItem;

            if (t != null) {
                selectedTeams.Remove(t);
                availableTeams.Add(t);
            }

            WireUpLists();
        }

        private void RemoveSelectedPrizeButton_Click(object sender, EventArgs e) {
            PrizeModel p = (PrizeModel)PrizesListBox.SelectedItem;

            if (p != null) {
                selectedPrizes.Remove(p);
            }

            WireUpLists();
        }

        private void CreateTournamentButton_Click(object sender, EventArgs e) {
            // Validate Data
            decimal fee = 0;

            bool feeAcceptable = decimal.TryParse(EntryFeeValue.Text, out fee);

            if (!feeAcceptable) {
                MessageBox.Show("You need to enter a valid Entry Fee", "Invalid Fee", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Create our Tournament model
            TournamentModel tm = new TournamentModel();
            tm.TournamentName = TournamentNameValue.Text;
            tm.EntryFee = fee;

            tm.Prizes = selectedPrizes;
            tm.EnteredTeams = selectedTeams;

            // TODO - Wire Our Matchups
            TournamentLogic.CreateRounds(tm);

            // Create Tournament Entry
            // Create all of Prizes Entries
            // Create all of Team Entries
            GlobalConfig.Connection.CreateTournament(tm);
            
            tm.AlertUsersToNewRound();

            TournamentViewerForm frm = new TournamentViewerForm(tm);
            frm.Show();
            this.Close();
        }
    }
}