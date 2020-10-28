using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models {
    /// <summary>
    /// Represents a Matchup.
    /// </summary>
    public class MatchupModel {

        /// <summary>
        /// A unique identifier for this matchup.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// A List of Matchup Entries for the Matchup.
        /// </summary>
        public List<MatchupEntryModel> Entries { get; set; } = new List<MatchupEntryModel>();

        /// <summary>
        /// The Id from the database that will be used to identify the winner.
        /// </summary>
        public int WinnerId { get; set; }

        /// <summary>
        /// The Winner of the Matchup.
        /// </summary>
        public TeamModel Winner { get; set; }

        /// <summary>
        /// Represents the numbered Round the Matchup belongs to.
        /// </summary>
        public int MatchupRound { get; set; }

        public string DisplayName {
            get {
                string output = "";

                foreach (MatchupEntryModel me in Entries) {
                    if (me.TeamCompeting != null) {
                        if (output.Length == 0) {
                            output = me.TeamCompeting.TeamName;
                        }
                        else {
                            output += $" vs. {me.TeamCompeting.TeamName}";
                        } 
                    } else {
                        output = "Matchup Not Yet Determined...";
                        break;
                    }
                }

                return output;
            }
        }
    }
}
