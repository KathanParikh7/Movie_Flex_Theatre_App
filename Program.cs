using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;

namespace MoviePlexTheater
{
    class Program
    {
        public static string[] moviesList;
        public static string[] moviesType;
        public static int[] moviesAge;

        static void Main(string[] args)
        {
            Start();
        }

        public static void Start()
        {
            try
            {


                Console.WriteLine("\t\t\t\t\t************************************");
                Console.WriteLine("\t\t\t\t\t*** Welcome to MoviePlex Theater ***");
                Console.WriteLine("\t\t\t\t\t************************************");

                Console.WriteLine();
                Console.WriteLine("Please Select From The Following Log-In Options:");
                Console.WriteLine("\t1. Administrator");
                Console.WriteLine("\t2. Guest");
                Console.WriteLine();
                Console.Write("Selection:");
                int choice = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                if (choice == 1)
                {
                    Console.WriteLine("\t\t\t\t\t*************************************");
                    Console.WriteLine("\t\t\t\t\t*** Welcome to  MoviePlex Theater ***");
                    Console.WriteLine("\t\t\t\t\t**************************************");

                    AdminPanel();
                }
                else if (choice == 2)
                {
                    Guest();
                }
                else
                {
                    Console.WriteLine("Please Select From Above Options !!.");
                    Start();
                }
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Enter your choice as an integer", Console.ForegroundColor);
                Console.ForegroundColor = ConsoleColor.White;
                Start();
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Something Went Wrong !!", Console.ForegroundColor);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public static void AdminPanel()
        {
            int count = 5;
            while (count > 0)
            {

                Console.Write("Please Enter Password:");

                string pass = "";
                do
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        pass += key.KeyChar;
                        Console.Write("*");
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                        {
                            pass = pass.Substring(0, (pass.Length - 1));
                            Console.Write("\b \b");
                        }
                        else if (key.Key == ConsoleKey.Enter)
                        {
                            break;
                        }
                    }
                } while (true);
                count--;
                if (pass == "Secret")
                {
                    Admin();
                }
                else if (pass == "B")
                {
                    Console.Clear();
                    Start();
                }
                else
                {
                    Console.Write("\n");
                    Console.WriteLine();
                    Console.WriteLine("Invalid Password !!.");
                    if (count == 0)
                    {
                        Console.WriteLine("Account has been locked!! ");
                        Start();
                    }
                    Console.WriteLine("You have {0} more attampt to enter correct password OR Press B to go back to privious screen.", count);
                    Console.WriteLine();
                }
            }
        }

        public static void Admin()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t\t***************************************");
            Console.WriteLine("\t\t\t\t\t*** Welcome MoviePlex Administartor ***");
            Console.WriteLine("\t\t\t\t\t***************************************");

        totalMovie:
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.Write("How many movie are playing today? ");
            int totalMovie = 0;
            try
            {
                totalMovie = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Enter your choice as an integer", Console.ForegroundColor);
                goto totalMovie;
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Something Went Wrong!!!", Console.ForegroundColor);

            }

            if (totalMovie > 10)
            {
                Console.WriteLine();
                Console.WriteLine("You can't store more than 10 movies!!");
                goto totalMovie;
            }
            moviesList = new string[totalMovie];
            moviesType = new string[totalMovie];
            moviesAge = new int[totalMovie];

            for (int i = 0; i < totalMovie; i++)
            {
            MovieName:
                Console.WriteLine();
                Console.Write("Please Enter " + ConvertNumberToString(i + 1) + " Movie's Name:");
                string name = Console.ReadLine();
                if (name == null || name == string.Empty)
                {
                    Console.WriteLine();
                    Console.WriteLine("Movie Name can't blank.");
                    goto MovieName;
                }
                else
                {
                    moviesList[i] = name;
                }
            MovieType:
                Console.Write("Please Enter the Age limit or rating for {0} Movie: ", ConvertNumberToString(i + 1));
                string type = Console.ReadLine();
                if (type == null || type == string.Empty)
                {
                    Console.WriteLine();
                    Console.WriteLine("Select One Option.");
                    goto MovieType;
                }
                else if (int.TryParse(type, out int n))
                {
                    var age = Convert.ToInt32(type);
                    moviesAge[i] = age;

                    string temp = null;
                    if (age > 0 && age <= 10)
                    {
                        temp = "PG";
                    }
                    else if (age > 10 && age <= 13)
                    {
                        temp = "PG-13";
                    }
                    else if (age > 13 && age <= 15)
                    {
                        temp = "R";
                    }
                    else if (age > 15 && age <= 17)
                    {
                        temp = "NC-17";
                    }
                    else if (age > 17 && age <= 90)
                    {
                        temp = "G";
                    }
                    else if (age > 90)
                    {
                        Console.WriteLine("Not A Valid Age !!");
                        goto MovieType;
                    }
                    moviesType[i] = temp;
                }
                else
                {
                    switch (type.ToUpper())
                    {
                        case "G":
                        case "PG":
                        case "PG-13":
                        case "R":
                        case "NC-17":
                            break;
                        default:
                            Console.WriteLine();
                            Console.WriteLine("This movie rating type is not valid,You can select from G,PG,PG-13,R,NC-17");
                            goto MovieType;

                    }
                    moviesType[i] = type.ToUpper();
                }
            }
            Console.Clear();

            Console.WriteLine("\t\t\t\t\t*********************************");
            Console.WriteLine("\t\t\t\t\t*** Welcome MoviePlex Theater ***");
            Console.WriteLine("\t\t\t\t\t*********************************");
            Console.WriteLine();
            for (int i = 0; i < totalMovie; i++)
            {
                Console.WriteLine(i + 1 + "." + moviesList[i] + "(" + moviesType[i] + ")");
            }

        SaveMovie:
            Console.WriteLine();
            Console.Write("Your Movies Playing Today Are Listed Above. Are You satisfied? (Y/N)?");
            string dicision = Console.ReadLine();

            if (dicision.ToUpper() == "Y")
            {
                Console.Clear();
                Start();
            }
            else if (dicision.ToUpper() == "N")
            {
                Admin();
            }
            else
            {
                Console.WriteLine("Please Make Proper Selection");
                goto SaveMovie;
            }
        }

        public static void Guest()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t\t************************************");
            Console.WriteLine("\t\t\t\t\t*** Welcome to MoviePlex Theater ***");
            Console.WriteLine("\t\t\t\t\t************************************");

            if (moviesList == null)
            {
                //Console.Clear();
                Console.WriteLine("There are no movies are playing today.");
                Console.WriteLine("Press 0 to go back to the Start page");
                string selection = Console.ReadLine();
                if (selection.ToUpper() == "0")
                {
                    Console.Clear();
                    Start();
                }
                else
                {
                    Console.WriteLine("Please Enter Proper Selection value.");
                    Guest();
                }
                //Start();
            }

            if (moviesList != null && moviesList.Length > 0)
            {
                Console.WriteLine("There are {0} movies playing today. Please choose from the following movies:", moviesList.Length);
                Console.WriteLine();
                for (int i = 0; i < moviesList.Length; i++)
                {
                    Console.WriteLine("\t" + (i + 1) + "." + moviesList[i] + "(" + moviesType[i] + ")");
                }

            MovieSlection:
                Console.WriteLine();
                Console.Write("Which Movie Would you like To Watch?? ");
                int choice = 0;
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    // Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Enter a proper number", Console.ForegroundColor);
                    Console.ForegroundColor = ConsoleColor.White;
                    goto MovieSlection;
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Something Went Wrong!!", Console.ForegroundColor);
                }

                if (choice > moviesList.Length)
                {
                    Console.WriteLine("Please Enter Proper Selection!!.");
                    goto MovieSlection;
                }
                bool ageFlag = false;

            AgeSlection:
                if (moviesType[choice - 1].ToUpper() == "PG")
                {
                    try
                    {
                        Console.WriteLine("Please Enter Age For Verification: ");
                        int age = Convert.ToInt32(Console.ReadLine());
                        if (age != 0 && age >= 10)
                        {
                            ageFlag = true;
                        }
                    }
                    catch (FormatException)
                    {
                        // Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Enter a proper number", Console.ForegroundColor);
                        Console.ForegroundColor = ConsoleColor.White;
                        goto AgeSlection;
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Something Went Wrong", Console.ForegroundColor);

                    }

                }
                else if (moviesType[choice - 1].ToUpper() == "PG-13")
                {
                    try
                    {
                        Console.WriteLine("Please Enter Age For Verification: ");
                        int age = Convert.ToInt32(Console.ReadLine());
                        if (age >= 13)
                        {
                            ageFlag = true;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Enter a proper number", Console.ForegroundColor);
                        Console.ForegroundColor = ConsoleColor.White;
                        goto AgeSlection;
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Something Went Wrong", Console.ForegroundColor);

                    }

                }
                else if (moviesType[choice - 1].ToUpper() == "R")
                {
                    try
                    {
                        Console.Write("Please Enter Age For Verification: ");
                        int age = Convert.ToInt32(Console.ReadLine());
                        if (age >= 15)
                        {
                            ageFlag = true;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Enter a proper number", Console.ForegroundColor);
                        Console.ForegroundColor = ConsoleColor.White;
                        goto AgeSlection;
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Something Went Wrong", Console.ForegroundColor);

                    }

                }
                else if (moviesType[choice - 1].ToUpper() == "NC-17")
                {
                    try
                    {
                        Console.Write("Please Enter Age For Verification:- ");
                        int age = Convert.ToInt32(Console.ReadLine());
                        if (age >= 17)
                        {
                            ageFlag = true;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Enter a proper valid Age !!", Console.ForegroundColor);
                        Console.ForegroundColor = ConsoleColor.White;
                        goto AgeSlection;
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Something's Wrong !!", Console.ForegroundColor);

                    }

                }
                else if (moviesType[choice - 1].ToUpper() == "G")
                {
                    ageFlag = true;
                }
                else
                {
                    ageFlag = false;
                }

                if (ageFlag == true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Enjoy the movie!!");
                    goto lastSelection;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("You Are Still A Kid !!");
                    goto lastSelection;
                }

            lastSelection:
                Console.WriteLine();
                Console.WriteLine("Press 1 to go back to the Guest Page");
                Console.WriteLine("Press 0 to go back to the Start page");
                string selection = Console.ReadLine();

                if (selection != "" && selection != null)
                {
                    if (selection.ToUpper() == "1")
                    {
                        Guest();
                    }
                    else if (selection.ToUpper() == "0")
                    {
                        Console.Clear();
                        Start();
                    }
                    else
                    {
                        Console.WriteLine("Please Enter Proper value.");
                        goto lastSelection;
                    }
                }
                else
                {
                    Console.WriteLine("Please Enter Proper value.");
                    goto lastSelection;
                }
            }
        }

        public static string ConvertNumberToString(int number)
        {
            switch (number)
            {
                case 1:
                    return "First";
                case 2:
                    return "Second";
                case 3:
                    return "Third";
                case 4:
                    return "Fourth";
                case 5:
                    return "Fifth";
                case 6:
                    return "Sixth";
                case 7:
                    return "Seventh";
                case 8:
                    return "Eighth";
                case 9:
                    return "Ninth";
                case 10:
                    return "Tenth";

                default:
                    return "";
            }

        }
    }
}
