using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Hunddagis1
{
    class Program
    {


        static void Main()
        {
            Menu();
            MenuChoice();
        }
        public const string path = @"C:\Users\filip\Desktop\Hunddagis1\Hunddagis1\Testa.txt"; //Skapar en konstant för path till textfilen
        public static string[] fileLines = File.ReadAllLines(path); //Läser in alla lines till minnet
        public static void MenuChoice()
        {
            /*
             * En switch för de olika menyvalen
             * Om användaren matar in ett ogiltigt menyval börjar metoden om.
             */
            bool keepAlive = true;

            while (keepAlive) //Om false så avslutas programmet
            {
                int caseSwitch = Convert.ToInt32(Console.ReadLine());

                switch (caseSwitch)
                {
                    case 1: //Menyval 1, sök hund
                        SearchDog();
                        break;
                    case 2: //Menyval 2, lista av alla hundar
                        Console.WriteLine("A list of all dogs:");
                        ListDogs();
                        break;
                    case 3: //Menyval 3, lägg till ny hund
                        AddDog();
                        break;
                    case 4: //Menyval 4, ta bort hund från listan
                        RemoveDog();
                        break;
                    case 5: //Exit
                        keepAlive = false;
                        System.Environment.Exit(1);
                        break;
                    default:
                        Console.WriteLine("No valid choice"); //Går tillbaka till menyvalen vid fel inmatning
                        Thread.Sleep(1000);
                        Console.Clear();
                        break;

                }
                Menu();
            }




        }
        public static void Menu()
        {
            /*
             * Skapar en vektor med menyvalen.
             */
            string[] menu1 = {
        "DOG DAYCARE",
        "-----------",
        "1) Search Dog",
        "2) List/Sort Dogs",
        "3) Add New Dog",
        "4) Remove Dog",
        "5) Exit"
      };

            for (int i = 0; i < menu1.Length; i++) //Går igenom och skriver ut menyval
            {
                if (i <= 1)
                {
                    Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", menu1[i])); //Centrerar WriteLine
                }
                if (i == 1)
                {
                    Console.WriteLine("Press a number and click enter.");
                }
                if (i > 1)
                {
                    Console.WriteLine(menu1[i]);
                }
            }

        }
        public static void ListDogs()
        {
            /*
             * Går igenom alla lines och skriver ut/sorterar efter bokstavsordning. 
             */
            string[] lines = fileLines;

            Array.Sort(lines);
            File.WriteAllLines(path, lines);
            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine(lines[i]);
            }
            Console.WriteLine("Going back to main menu in 10 sec");
            Thread.Sleep(10000); //
            Console.Clear();


        }
        public static void SearchDog()
        {
            /*
             * Söker igenom alla lines efter det användaren skrivit in.
             * Om det innehåller det sökta hundnamnet skrivs all information om hunden ut.
             */
            Console.WriteLine("Search dog name:");
            string dogName = Console.ReadLine(); //Input dog name

            string[] lines = fileLines; //Läser rad för rad i txtfil

            for (int i = 0; i < lines.Length; i++)
            {
                string[] dogLineSplit = lines[i].Split(); // Splittar lines för att kunna jämföra dogLineSplit[0] med dogName

                if (dogLineSplit[0].Equals(dogName)) //Om det innehåller sökta hundnamnet
                {
                    Console.WriteLine(lines[i]);
                    Console.WriteLine("Going back to main menu in 10 sec");
                    Thread.Sleep(10000); //             
                    Console.Clear();

                }
                if (i == dogLineSplit.Length && !dogName.Contains(lines[i]))
                {
                    Console.WriteLine("Dog not found"); //Fixa Att kod ej når hit
                }

            }

        }
        public static void AddDog()
        {
            /*
             * Användaren skriver in information om varje hund.
             * Datan sparas i textfil.
             */
            string doginfo;

            StreamWriter utfil = new StreamWriter(path, append: true);
            Console.WriteLine("Please write: Dog name, dog age, breed, owner:");
            doginfo = Console.ReadLine();

            utfil.Write(Environment.NewLine + doginfo);
            utfil.Close();
            Console.WriteLine(doginfo + " has been added to the list");
            Console.WriteLine("Going back to main menu in 10 sec");
            Thread.Sleep(10000);
            Console.Clear();


        }
        public static void RemoveDog()

        {
            /*
             * Användaren söker efter ett hundnamn att ta bort.
             * Ifall lines innehåller det användaren skrev in, så tas det bort från vektorn.
             */

            Console.WriteLine("Input a dogname to remove");
            string dogToRemove = Console.ReadLine();
            string[] lines = File.ReadAllLines(path); //Läser rad för rad i txtfil
            File.WriteAllText(path, String.Empty); //Tömmer textfilen med Lines sparat i minnet
            StreamWriter file = new StreamWriter(path, append: true); // Öppnar streamwriter
            string[] dogLineSplit;
            bool firstLine = true;

            for (int i = 0; i < lines.Length; i++) //Itererar igenom lines
            {

                dogLineSplit = lines[i].Split(); //Splittar varje rad av lines
                string dogName = dogLineSplit[0]; // Sätter första positionen i arrayn till dogName

                if (dogName == dogToRemove) // Kollar så att dogName är samma som dogToRemove vilket är inmatat från användare
                {
                    Console.WriteLine("Now removing" + " " + dogToRemove); //Om Hundnamnet stämmer skriver vi att det skall tas bort
                }
                else //Stämmer det inte så kommer vi lägga tillbaka namnet här
                {

                    var result = string.Join(" ", dogLineSplit); //Vi tar arrayn dogLineSplit och slåt ihop till en sträng med formateringen " "
                    
                    if (firstLine)
                    {
                        file.Write(result); // Skriver den nya strängen till testa.txt.
                        firstLine = false;
                    }
                    else
                    {
                        file.Write(Environment.NewLine + result);
                    }
                    
                     

                }

                Array.Clear(dogLineSplit, 0, dogLineSplit.Length); //Ta bort all information i array

            }
            file.Close();
            Console.WriteLine("Going back to main menu in 10 sec");
            Thread.Sleep(10000);
            Console.Clear();


        }
    }
}