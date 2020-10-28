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
    public partial class CreateTeamForm : Form {
        private List<PersonModel> availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();
        private ITeamRequester callingForm;

        public CreateTeamForm(ITeamRequester caller) {
            InitializeComponent();

            callingForm = caller;

            //CreateSampleData();

            WireUpLists();
        }

        private void CreateSampleData () {
            availableTeamMembers.Add(new PersonModel {
                FirstName = "Tim",
                LastName = "Corey"
            });
            availableTeamMembers.Add(new PersonModel {
                FirstName = "Sue",
                LastName = "Storm"
            });

            selectedTeamMembers.Add(new PersonModel {
                FirstName = "Daulton",
                LastName = "Nelson"
            });
            selectedTeamMembers.Add(new PersonModel {
                FirstName = "Tyler",
                LastName = "Spina"
            });
        }

        private void WireUpLists () {
            SelectTeamMemberDropDown.DataSource = null;

            SelectTeamMemberDropDown.DataSource = availableTeamMembers;
            SelectTeamMemberDropDown.DisplayMember = "FullName";

            TeamMembersListBox.DataSource = null;

            TeamMembersListBox.DataSource = selectedTeamMembers;
            TeamMembersListBox.DisplayMember = "FullName";

            
        }

        private void CreateMemberButton_Click(object sender, EventArgs e) {
            if (ValidateForm()) {
                PersonModel p = new PersonModel();

                p.FirstName = FirstNameValue.Text;
                p.LastName = LastNameValue.Text;
                p.EmailAddress = EmailAddressValue.Text;
                p.CellphoneNumber = CellphoneValue.Text;

                GlobalConfig.Connection.CreatePerson(p);

                selectedTeamMembers.Add(p);

                WireUpLists();

                FirstNameValue.Text = "";
                LastNameValue.Text = "";
                EmailAddressValue.Text = "";
                CellphoneValue.Text = "";
            } else {
                MessageBox.Show("You need to fill in all of the fields.");
            }
        }

        private bool ValidateForm () {
            if (FirstNameValue.Text.Length == 0) {
                return false;
            }

            if (LastNameValue.Text.Length == 0) {
                return false;
            }

            if (EmailAddressValue.Text.Length == 0) {
                return false;
            }

            if (CellphoneValue.Text.Length == 0) {
                return false;
            }

            return true;
        }

        private void AddMemberButton_Click(object sender, EventArgs e) {
            PersonModel p = (PersonModel)SelectTeamMemberDropDown.SelectedItem;

            if (p != null) {
                availableTeamMembers.Remove(p);
                selectedTeamMembers.Add(p);

                WireUpLists(); 
            }
        }

        private void DeleteSelectedMemberButton_Click(object sender, EventArgs e) {
            PersonModel p = (PersonModel)TeamMembersListBox.SelectedItem;

            if (p != null) {
                selectedTeamMembers.Remove(p);
                availableTeamMembers.Add(p);

                WireUpLists(); 
            }
        }

        private void CreateTeamButton_Click(object sender, EventArgs e) {
            TeamModel t = new TeamModel();
            t.TeamName = TeamNameValue.Text;
            t.TeamMembers = selectedTeamMembers;

            GlobalConfig.Connection.CreateTeam(t);

            callingForm.TeamComplete(t);

            this.Close();
        }
    }
}