using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Conference_Track_Management
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please provide the input file path to read the conference talks from:");
            var userInputPath = Console.ReadLine();
            try
            {
                if (!string.IsNullOrEmpty(userInputPath))
                {
                    //Adding all talks from the input text file to a common repository.
                    ConferenceCreator.AddAllConferenceTalks(userInputPath);

                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("Preparing Conference Schedule...");
                    
                    //Call to create conference schedule.
                    var conferenceDetails = ConferenceCreator.CreateConference();
                    if (conferenceDetails.Any())
                    {
                        Console.WriteLine("Conference schedule successfully prepared!!!");
                        Console.WriteLine(Environment.NewLine);
                        Console.WriteLine("Please provide the output file path to write the conference schedule");
                        var userOutputPath = Console.ReadLine();

                        //Writing the conference schedule content to an output text file specified by the user.
                        if (!string.IsNullOrEmpty(userOutputPath))
                        {
                            using (StreamWriter sw = new StreamWriter(userOutputPath))
                            {
                                foreach (var talk in conferenceDetails)
                                {
                                    sw.WriteLine(talk);
                                }
                            }
                            Console.WriteLine(string.Format("Please check the file at location {0} for the conference schedule!!!",userOutputPath));
                            Console.WriteLine(Environment.NewLine);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Please provide an input file path to read from!!!");
                }

            }
            catch (FileNotFoundException fex)
            {
                Console.WriteLine("Please provide a valid input file name");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
