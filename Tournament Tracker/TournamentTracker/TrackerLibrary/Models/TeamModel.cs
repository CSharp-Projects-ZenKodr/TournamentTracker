using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models {
     /// <summary>
     /// Represents one Team.
     /// </summary>
    public class TeamModel {
        /// <summary>
        /// The unique identifier for the Team.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The list of Persons that belong to the Team.
        /// </summary>
        public List<PersonModel> TeamMembers { get; set; } = new List<PersonModel>();

        /// <summary>
        /// The Name of the Team.
        /// </summary>
        public string TeamName { get; set; }
    }
}