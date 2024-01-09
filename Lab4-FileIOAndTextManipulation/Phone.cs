/**       
 * -------------------------------------------------------------------
 * 	   File name: Phone.cs
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
    public class Phone
    {
        //Fields for the Phone Class.
        int CountryCode { get; init; }
        int AreaCode { get; init; }
        int ExchangeCode { get; init; }
        int LineNumber { get; init; }
        StringBuilder FullNumber { get; init; }

        /**
         * This constructor creates a random 10-digit number 
         * and assigns it to the Number field.
         * 
         * Date Created: 02/17/2022
         */
        public Phone(int countryCode, int areaCode, int exchangeCode, int lineNumber) 
        {
            FullNumber = new StringBuilder();

            //The country code is assumed to be 1.
            CountryCode = countryCode;
            AreaCode = areaCode;
            ExchangeCode = exchangeCode;
            LineNumber = lineNumber;
            FullNumber.Append($"{countryCode}-{areaCode}-{exchangeCode}-{lineNumber}");
        }

        /**
         * A ToString method for the Number class.
         * Utilizes an optional argument.
         * 
         * Date Created: 02/17/2022
         * @param separator (char)
         * @return Number (string)
         */
        public string Format(char separator = '-') 
        {
            //We reset FullNumber and set it based on the new separator.
            FullNumber.Clear();
            FullNumber.Append($"{CountryCode}{separator}{AreaCode}{separator}{ExchangeCode}{separator}{LineNumber}");
            return $"{CountryCode}{separator}{AreaCode}{separator}{ExchangeCode}{separator}{LineNumber}";
        }
    }
}
