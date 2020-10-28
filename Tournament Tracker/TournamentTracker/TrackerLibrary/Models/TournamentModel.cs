using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models {
    /// <summary>
    /// Represents one Tournament.
    /// </summary>
    public class TournamentModel {

        //events are alert systems
        //you have events and subscribers
        //we're going to trigger event
        //they have to subscribe to this event
        //it will broadcast when complete
        //subscribers will get message, and run code
        //Think like a button
        public event EventHandler<DateTime> OnTournamentComplete;

        /// <summary>
        /// The unique identifier for the tournament.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the Tournament.
        /// </summary>
        public string TournamentName { get; set; }

        /// <summary>
        /// The entry fee require to pay to enter the Tourney.
        /// </summary>
        public decimal EntryFee { get; set; }

        /// <summary>
        /// The list of Entered Teams in the Tournament.
        /// </summary>
        public List<TeamModel> EnteredTeams { get; set; } = new List<TeamModel>();

        /// <summary>
        /// The List of Prizes belonging to the Tournament.
        /// </summary>
        public List<PrizeModel> Prizes { get; set; } = new List<PrizeModel>();

        /// <summary>
        /// The List of Lists which contain the matchup.  These are the Rounds.
        /// </summary>
        public List<List<MatchupModel>> Rounds { get; set; } = new List<List<MatchupModel>>();

        public void CompleteTournament () {
            //exclusive to certain version of c#
            OnTournamentComplete?.Invoke(this, DateTime.Now);
        }
    }
}
