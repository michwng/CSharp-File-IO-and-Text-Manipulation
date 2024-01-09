/**       
 * -------------------------------------------------------------------
 * 	   File name: Person.cs
 * 	Project name: Lab4-FileIOAndTextManipulation
 * -------------------------------------------------------------------
 * Author’s name and email:	Michael Ng, ngmw01@etsu.edu			
 *          Course-Section: CSCI-2910-001
 *           Creation Date:	02/15/2022	
 *           Last Modified: 02/17/2022
 * -------------------------------------------------------------------
 */

using System;
using System.Text;
using Lab4_FileIOAndTextManipulation;

//This was mostly copied from Lab 3's version of Person. 
namespace Lab4_FileIOAndTextManipulation
{
    public class Person
    {
        //fields for the Person class.
        public string FirstName {  get; init; }
        public string LastName { get; init; }
        public Address Address { get; init; }
        public Phone Phone { get; init; }


        /**
         * The constructor for the Person Class.
         * Utilizes some string attributes for parameters.
         * 
         * Date Created: 02/17/2022
         * @param firstName, lastName, address, phone (string)
         */
        public Person(string firstName, string lastName, string address, string phone) 
        {
            FirstName = firstName;
            LastName = lastName;

            //We need to format address and phone in order to accurately use their constructors.
            string[] addressArray = address.Split(',');

            //Remember? The big address is ordered as follows.
            // - {Address},{City},{County},{State},{Zip}
            //We skip County because it is not necessary. Unless...
            
            Address = new Address(addressArray[0], addressArray[1], addressArray[3], addressArray[4]);
            //Another implementation, including the County! Comment the other one out if you uncomment this one!
            //Address = new Address(addressArray[0], addressArray[1], addressArray[2], addressArray[3], addressArray[4]);


            //We also need to format phone.
            string[] phoneParts = phone.Split('-');

            //Phone Numbers are dissected into 4 things:
            //+{CountryCode}({AreaCode})-{ExchangeCode}-{LineNumber}

            //We will assume that the Country code is 1. All states in the data are in the US (probably).
            Phone = new Phone(1, int.Parse(phoneParts[0]), int.Parse(phoneParts[1]), int.Parse(phoneParts[2]));
        }


        /**
         * The toString() method for the Person class.
         * 
         * Date Created: 02/17/2022
         * 
         * @return sb.ToString() (string)
         */
        public string ToString(char separator = '-') 
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{FirstName} | {LastName} | {Address.ToString()} | {Phone.Format()}");
            sb.Append("\n------------------------------");

            return sb.ToString();
        }

        /**
         * The shorter toString() method for the Person class.
         * 
         * Date Created: 02/17/2022
         * 
         * @return sb.ToString() (string)
         */
        public string ShortToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{FirstName} {LastName} | {Address.ShortToString()}");
            sb.Append("\n------------------------------");

            return sb.ToString();
        }
    }
}

