using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models {
    /// <summary>
    /// Represents one Prize.
    /// </summary>
    public class PrizeModel {
        /// <summary>
        /// The unique identifier for the prize
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The Place Number that gets the Prize.
        /// </summary>
        public int PlaceNumber { get; set; }

        /// <summary>
        /// The specific name of the Place that gets the prize (Not Team Name).
        /// </summary>
        public string PlaceName { get; set; }

        /// <summary>
        /// The amount the winner of the prize gets.
        /// </summary>
        public decimal PrizeAmount { get; set; }

        /// <summary>
        /// The percentage the winner of the prize gets.
        /// </summary>
        public double PrizePercentage { get; set; }

        public PrizeModel() {

        }

        public PrizeModel(string placeName, string placeNumber, string prizeAmount, string prizePercentage) {
            PlaceName = placeName;

            int placeNumberValue = 0;
            int.TryParse(placeNumber, out placeNumberValue);
            PlaceNumber = placeNumberValue;

            decimal prizeAmountValue = 0;
            decimal.TryParse(prizeAmount, out prizeAmountValue);
            PrizeAmount = prizeAmountValue;

            double prizePercentageValue = 0;
            double.TryParse(prizePercentage, out prizePercentageValue);
            PrizePercentage = prizePercentageValue;
        }
    }
}