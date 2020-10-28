using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models {
    /// <summary>
    /// Represents a Matchup Entry.
    /// </summary>
    public class MatchupEntryModel {

        /// <summary>
        /// The unique identifier for this macthup entry.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The unique identifier for the competing team.
        /// </summary>
        public int TeamCompetingId { get; set; }

        /// <summary>
        /// Represents One Team in the Matchup
        /// </summary>
        public TeamModel TeamCompeting { get; set; }

        /// <summary>
        /// Represents the score for this particular Team.
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// The unique identifier for the parent matchup.
        /// </summary>
        public int ParentMatchupId { get; set; }

        /// <summary>
        /// Represents the matchup that this team came
        ///  from as the winner.
        /// </summary>
        public MatchupModel ParentMatchup { get; set; }
    }
}
