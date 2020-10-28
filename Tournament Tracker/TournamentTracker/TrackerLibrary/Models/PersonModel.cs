using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models {
    /// <summary>
    /// Represents one Person.
    /// </summary>
    public class PersonModel {
        
        /// <summary>
        /// The unique identifier for the person.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Person's First Name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The Person's Last Name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The Person's Email Address.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// The Person's Cellphone Number.
        /// </summary>
        public string CellphoneNumber { get; set; }

        /// <summary>
        /// The Person's Full Name.
        /// </summary>
        public string FullName {
            get {
                return $"{FirstName} {LastName}";
            }
        }
    }
}