/**       
 * -------------------------------------------------------------------
 * 	   File name: Program.cs
 * 	Project name: Lab4-FileIOAndTextManipulation
 * -------------------------------------------------------------------
 * Author’s name and email:	Michael Ng, ngmw01@etsu.edu			
 *          Course-Section: CSCI-2910-001
 *           Creation Date:	02/15/2022	
 *           Last Modified: 02/17/2022
 * -------------------------------------------------------------------
 */

using Lab4_FileIOAndTextManipulation;
using System.IO;
using System.Text;

//Useful for bringing up a folder menu.
using System.Diagnostics;

//Fields for the Program class.
List<Person> listOfPeople = new List<Person>();
FileRoot fileRoot = new FileRoot();
string[] megaString;
int menuChoice = 0;


//Call on the main method. 
main();

/*
 * The main method. Runs the code.
 * 
 * Date Created: 02/17/2022
 */
void main() 
{
    Console.WriteLine("Welcome to File I/O and Text Manipulation!\nPlease wait as we intialize the program...");
    //Sometimes, the program stops. You just need to press a button to encourage it.
    Console.WriteLine(">> Press any button to continue! <<");
    initializeMegaString();

    Console.WriteLine("The program was successfully intialized!");

    while (true) 
    {
        menu();
        launchMethod();
    }
}


/**       
 * -------------------------------------------------------------------
 * 	            
 * 	            Menu Methods
 * 	            
 * -------------------------------------------------------------------
 */
/**
 * This method acts as the menu. 
 * Lists the available applications and inputs.
 * Validates user input.
 * 
 * Date Created: 02/17/2022
 */
void menu()
{
    String input = "";
    Console.WriteLine("\nPlease type in the number beside the action you would like to perform.");

    //Continues asking the user for the right input.
    do
    {
        Console.WriteLine("Please type the number next to the option to proceed:");
        Console.WriteLine("1. View All People");
        Console.WriteLine("2. View All People (Short)");
        Console.WriteLine("3. View CSV File");
        Console.WriteLine("4. Export CSV to PSV File");
        Console.WriteLine("5. View PSV File (Must Export First)");
        Console.WriteLine("6. Exit the Application");
        try
        {
            input = Console.ReadLine();

            menuChoice = Int32.Parse(input.Trim());

            if (menuChoice >= 1 && menuChoice <= 6)
            {
                break;
            }
            else
            {
                Console.WriteLine($"\nOops! \"{input}\" number is not an option.");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine($"\nOops! \"{input}\" isn't a number.");
        }
        catch (OverflowException)
        {
            Console.WriteLine($"\nOops! \"{input}\" number is not an option.");
        }
        catch (ArgumentNullException)
        {
            //Repeat the top message again, asking the user to input a number.
        }
    }
    while (true);
}
//end menu()


/**
 * This method launches a method based on 
 * user input in the menu() method.
 * 
 * Date Created: 02/08/2022
 */
void launchMethod()
{
    Console.Clear();

    switch (menuChoice)
    {
        //View All People
        case 1:
            ViewPeople(false);
            break;


        //View All People (Short)
        case 2:
            ViewPeople(true);
            break;


        //View CSV File
        case 3:
            openFile("data.csv");
            break;


        //Export CSV to PSV File
        case 4:
            exportPeople();
            break;


        //View PSV File (Must Export First)
        case 5:
            if (File.Exists($"{fileRoot.ToString()}{Path.DirectorySeparatorChar}data.psv"))
            {
                openFile("data.psv");
            }
            else 
            {
                Console.WriteLine("Oops! It looks like you haven't exported the data yet.");
                Console.WriteLine("Please export the CSV file into a PSV file (Type '4') first!");
            }

            break;


        //Exit the Application
        case 6:
            Console.WriteLine("Thank you for using File I/O and Text Manipulation!");
            System.Environment.Exit(0);
            break;


        //Debug variable, in case there are errors with the Console.
        default:
            Console.WriteLine("Oops! An error happened somewhere. Ending the Application.");
            System.Environment.Exit(0);
            break;
    }

    Console.WriteLine("\n----- End of Method -----\n\n");
}
//end launchMethod()


/**       
 * -------------------------------------------------------------------
 * 	            
 * 	            Other Methods
 * 	            
 * -------------------------------------------------------------------
 */

/**
 * This method reads the information in the file and intializes a 
 * StringBuilder to be equal to all text in that file.
 * 
 * Date Created: 02/17/2022
 */
void initializeMegaString() 
{
    do
    {
        try
        {
            megaString = File.ReadAllLines($"{fileRoot.ToString()}{Path.DirectorySeparatorChar}data.csv");
            importPeople();
            return;
        }
        //Throws an exception if the same file is open in another application. 
        catch (FileNotFoundException)
        {
            Console.WriteLine(">> The file couldn't be found in the specified location. <<\n >>Please put the data.csv file in the top-level directory (The Lab4 directory inside the Lab4 directory)<<");

            //This opens the folder to the top-level directory!
            //Thanks to https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.process?view=net-6.0
            //and https://stackoverflow.com/questions/9646114/open-file-location
            Process.Start("explorer.exe", fileRoot.ToString());
        }
        //Included since Anti-Virus software may not like the application accessing a file on the first attempt to open it.
        catch (System.UnauthorizedAccessException)
        {
            Console.WriteLine(">>> This application cannot access the file! <<<\n >>>Did you receive a pop-up from your Anti-Virus software? <<<");
        }
        //This is most commonly thrown when data.csv is open in another file.
        catch (System.IO.IOException)
        {
            Console.WriteLine(">>> The file seems to be open in another program! <<<\n >>>Please close that program before continuing. <<<");
        }
        Console.WriteLine("\nPress Enter to try loading the file again.");
        Console.ReadLine();
    }
    while (true);
}
//end of initializeMegaString()


/**
 * importPeople utilizes an algorithm to accurately store data and create Person objects.
 * 
 * Date Created: 02/17/2022
 */
void importPeople() 
{
    //An extremely complicated algorithm that broke Visual Studio.
    //PLEASE SEE THE EXPLANATION AFTER THIS FOR-LOOP!!!

    //We start at line 1 because line 0 contains the headers.
    //Trying to parse line 0 would break the algorithm, so we skip it.
    for (int i = 1; i < megaString.Length; i += 1)
    {
        //sb's job is to store everything but the person's company.
        //There is one exception: if the person's company is not enclosed in quotes.
        StringBuilder sb = new StringBuilder();

        //Whenever the algorithm detects a person's company, it will be stored here.
        StringBuilder fullCompany = new StringBuilder();

        //Company may have a comma inside, in which case it would be surrounded in quotes.
        //A person with the company "Affiliated With Travelodge" would be in sb.
        //A person with the company "Mcrae, James L" would be in full company.
        Boolean companyComma = true;

        //We turn the entire string into a char, and look for a quote in the nexted for-loop.
        char[] stringDissection = megaString[i].ToCharArray();

        for (int j = 0; j < megaString[i].Length; j++)
        {
            //Look for a quote within the char array.
            if (stringDissection[j] == '"')
            {
                //Signify that Company will be stored in the fullCompany StringBuilder.
                companyComma = false ;

                //Skip the opening quote, moving to the first letter of the Company.
                j += 1;

                //Extremely big brain move. Gets all letters from the beginning quote to the ending quote.
                while (stringDissection[j] != '"')
                {
                    //Add the letter to the fullCompany StringBuilder.
                    fullCompany.Append(stringDissection[j]);

                    //Increment to the next letter. Otherwise, this becomes a forever loop.
                    j += 1;
                }
                //Without this, we include the comma after the ending quote.
                //So we might get (Lili, Paskin,, 20113 4th...) without this statement.
                //We add this statement so we get (Lili, Paskin, 20113 4th...)
                j += 1;
            }
            else
            {
                //Append each char from the line slowly onto sb.
                sb.Append(stringDissection[j]);
            }
        }
        /* ------------------------ EXPLANATION!!! ----------------------------------
         * Let me clearly explain what this algorithm does.
         * 
         * We have a problem where Company may or may not have a Comma in its value.
         * Without solving this problem, the first half of the company and second half of the company...
         * will be recognized by the algorithm as SEPERATE VALUES!!! 
         * This will mess up the way the algorithm categorizes each person's info.
         * 
         * So, I included the nested for loop (the one that uses variable j) to fix this issue.
         * It looks for a quote in the company. When it finds the quote, the algorithm stores everything until the ending quote.
         * Everything else is stored by sb Stringbuilder.
         * Anything in quotes is stored in the fullCompany StringBuilder. 
         * 
         * Up next, we do the actual splitting of the sb Stringbuilder.
         * Now that we have safely dealt with company, the algorithm can safely split, but must follow rules to do it safely.
         */

        //Split the 'formatted' line by each comma.
        string[] smallerString = sb.ToString().Split(',');

        //Because we MIGHT remove company entirely from the b Stringbuilder, 
        //We add correctionNum to adjust what each string should be.
        int correctionNum = 0;
        if (companyComma)
        {
            fullCompany.Append(smallerString[2]);
            correctionNum += 1;
        }

        /* A piece of code used for debugging that can now be used as a reference.
        Console.WriteLine("First Name: " + smallerString[0]);
        Console.WriteLine("Last Name: " + smallerString[1]);
        Console.WriteLine("Full Company: " + fullCompany);
        Console.WriteLine("Address: " + smallerString[2 + correctionNum]);
        Console.WriteLine("City: " + smallerString[3 + correctionNum]);
        Console.WriteLine("Country: " + smallerString[4 + correctionNum]);
        Console.WriteLine("State: " + smallerString[5 + correctionNum]);
        Console.WriteLine("Zip: " + smallerString[6 + correctionNum]);
        Console.WriteLine("Phone 1: " + smallerString[7 + correctionNum]);
        Console.WriteLine("Phone 2: " + smallerString[8 + correctionNum]);
        Console.WriteLine("Email: " + smallerString[9 + correctionNum]);
        Console.WriteLine("Web: " + smallerString[10 + correctionNum]);
        Console.WriteLine("Full Data: " + sb);
        */

        //Rather than storing everyone's information into several arrays, we just make a person.
        Person person = new Person(smallerString[0], smallerString[1], 
            $"{smallerString[2 + correctionNum]}," +
            $"{smallerString[3 + correctionNum]}," +
            $"{smallerString[4 + correctionNum]}," +
            $"{smallerString[5 + correctionNum]}," +
            $"{smallerString[6 + correctionNum]}", $"{smallerString[7 + correctionNum]}");
        //Yup, that's a big address. It's ordered as follows:
        // - {Address},{City},{Country},{State},{Zip}
        
        listOfPeople.Add(person);

        //Tell the user that the user was created.
        Console.WriteLine($"{smallerString[0]} {smallerString[1]} was created as a Person Object!");
    }
}
//end of importPeople()

/*
 * Allows the user to view all people in the array.
 * 
 * Date Created: 02/17/2022
 */
void ViewPeople(Boolean small)
{
    if (small)
    {
        foreach (Person person in listOfPeople)
        {
            Console.WriteLine(person.ShortToString());
        }
    }
    else
    {
        foreach (Person person in listOfPeople)
        {
            Console.WriteLine(person.ToString());
        }
    }

}
//end of ViewPeople()

/**
 * exportPeople utilizes an algorithm to export a PSV.
 * 
 * Date Created: 02/17/2022
 */
void exportPeople()
{
    //We will be exporting the contents of uberString StringBuilder.
    StringBuilder uberString = new StringBuilder();

    //A less complicated algorithm. This loop goes through the lines.
    for (int i = 0; i < megaString.Length; i++)
    {
        //We turn the entire string into a char, and look for a quote in the nexted for-loop.
        char[] stringDissection = megaString[i].ToCharArray();

        //This loop goes through each character in stringDissection.
        for (int j = 0; j < megaString[i].Length; j++)
        {
            //Look for a quote within the char array.
            if (stringDissection[j] == '"')
            {
                //We append the beginning quote and move to the next.
                //Without this, the while loop ends immediately.
                uberString.Append(stringDissection[j]);
                j += 1;

                //Add all letters from the beginning quote to the ending quote.
                //This while loop allows the program to skip any commas until the next quote.
                while (stringDissection[j] != '"')
                {
                    //Add the letter to the fullCompany StringBuilder.
                    uberString.Append(stringDissection[j]);

                    //Increment to the next letter. Otherwise, this becomes a forever loop.
                    j += 1;
                }

                //append the end quotation.
                uberString.Append(stringDissection[j]);
            }
            else
            {
                //Look for a comma within the char array.
                if (stringDissection[j] == ',')
                {
                    //Then, we add a PIPE!!
                    uberString.Append("|");
                }
                else
                {
                    //Append each char from the line slowly onto uberString.
                    uberString.Append(stringDissection[j]);
                }
            }            
        }
        //Subsequent people will be put in new lines!
        uberString.Append(System.Environment.NewLine);
        /* ------------------------ Explanation ----------------------------------
         * This algorithm appends everything onto the uberString 1 character at a time.
         * 
         * We comb each letter for a comma. When we find one, we add a pipe instead.
         */

        //Tell the user that the user was created.
        File.WriteAllText($"{fileRoot.ToString()}{Path.DirectorySeparatorChar}data.psv", uberString.ToString());
    }
    //Tell the user that the export was successful.
    Console.WriteLine($"The Export was Successful!");
}
//end of exportPeople()

/*
 * Opens a file located in the top-level directory.
 * Uses the default program to open the file extension.
 * 
 * Date Created: 02/17/2022
 * @param fileName (string)
 */
void openFile(string fileName = "data.csv") 
{
    //In theory, we don't need this if-statement, but it's included as a last-resort.
    if (File.Exists($"{fileRoot.ToString()}{Path.DirectorySeparatorChar}{fileName}")) 
    {
        //Quite a complicated process.
        //Thanks to:
        //https://stackoverflow.com/questions/23092885/open-a-csv-file-in-excel-via-c
        //and
        //https://stackoverflow.com/questions/58102696/the-specified-executable-is-not-a-valid-application-for-this-os-platform
        Process p = new Process();

        //Here, we define specifications for starting up the process.
        p.StartInfo = new ProcessStartInfo()
        {
            CreateNoWindow = true,
            UseShellExecute = true,
            Verb = "open",
            FileName = fileRoot.ToString() + Path.DirectorySeparatorChar + fileName
        };
        
        //Start the process.
        p.Start();
        Console.WriteLine($"Success! {fileName} will be opened up in a few moments!");
    }
    else 
    {
        //If the file didn't exist in the top-level directory, we notify the user and return.
        Console.WriteLine($"Oops! {fileName} couldn't be found in the top-level directory.");
        return;
    }
}