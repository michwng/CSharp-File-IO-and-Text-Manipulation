/**       
 * -------------------------------------------------------------------
 * 	   File name: State.cs
 * 	Project name: Lab4-FileIOAndTextManipulation
 * -------------------------------------------------------------------
 * Author’s name and email:	Michael Ng, ngmw01@etsu.edu			
 *          Course-Section: CSCI-2910-001
 *           Creation Date:	02/15/2022	
 *           Last Modified: 02/17/2022
 * -------------------------------------------------------------------
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_FileIOAndTextManipulation
{
    public class State
    {
        //Fields for the State Class.
        string Abbreviation { get; init; }
        public string FullName { get; init; }

        /*
         * The constructor for the State class.
         * 
         * Date Created: 02/17/2022
         * @param abbreviation
         */
        public State(string abbreviation) 
        {
            Abbreviation = abbreviation;

            FullName = GetState(abbreviation);
        }

        /*
         * Obviously, this method was copy-pasted.
         * Thanks to 
         * http://www.vandelayweb.com/code-library-c-switch-statement-state-names-abbreviations/
         * for having this case already done!
         * 
         * Date Added: 02/17/2022
         * @param state (string)
         */
        public string GetState(string abbreviation)
        {
            switch (abbreviation)
            {
                case "AL":
                    return "Alabama";
                case "AK":
                    return "Alaska";
                case "AR":
                    return "Arkansas";
                case "AZ":
                    return "Arizona";
                case "CA":
                    return "California";
                case "CO":
                    return "Colorado";
                case "CT":
                    return "Connecticut";
                case "DE":
                    return "Delaware";
                case "FL":
                    return "Florida";
                case "GA":
                    return "Georgia";
                case "HI":
                    return "Hawaii";
                case "ID":
                    return "Idaho";
                case "IL":
                    return "Illinois";
                case "IN":
                    return "Indiana";
                case "IA":
                    return "Iowa";
                case "KS":
                    return "Kansas";
                case "KY":
                    return "Kentucky";
                case "LA":
                    return "Louisiana";
                case "ME":
                    return "Maine";
                case "MD":
                    return "Maryland";
                case "MA":
                    return "Massachusetts";
                case "MI":
                    return "Michigan";
                case "MN":
                    return "Minnesota";
                case "MS":
                    return "Mississippi";
                case "MO":
                    return "Missouri";
                case "MT":
                    return "Montana";
                case "NE":
                    return "Nebraska";
                case "NV":
                    return "Nevada";
                case "NH":
                    return "New Hampshire";
                case "NJ":
                    return "New Jersey";
                case "NM":
                    return "New Mexico";
                case "NY":
                    return "New York";
                case "NC":
                    return "North Carolina";
                case "ND":
                    return "North Dakota";
                case "OH":
                    return "Ohio";
                case "OK":
                    return "Oklahoma";
                case "OR":
                    return "Oregon";
                case "PA":
                    return "Pennsylvania";
                case "RI":
                    return "Rhode Island";
                case "SC":
                    return "South Carolina";
                case "SD":
                    return "South Dakota";
                case "TN":
                    return "Tennessee";
                case "TX":
                    return "Texas";
                case "UT":
                    return "Utah";
                case "VT":
                    return "Vermont";
                case "VA":
                    return "Virginia";
                case "WA":
                    return "Washington";
                case "WV":
                    return "West Virginia";
                case "WI":
                    return "Wisconsin";
                case "WY":
                    return "Wyoming";
                default:
                    return abbreviation;
            }
        }
    }
}
