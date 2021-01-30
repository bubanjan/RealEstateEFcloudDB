
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Data;

namespace realestateBubanjaEF
{
    
    public class AppDbContext : DbContext
    {
        public DbSet<Estate> Estate { get; set; }
        public DbSet<Type> Type { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server = den1.mssql8.gear.host;Database=realestatedbef;Uid=realestatedbef;Pwd=Ke0kd3r~?hCU;");
        }  
    }

    public class Type
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class Estate
    {
        [Key]
        public int ID { get; set; }
        public Type Type { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }

    }



    public class Program
    {
        static AppDbContext database;


        static void Main(string[] args)
        {


            Console.ForegroundColor = ConsoleColor.DarkBlue;
            using (database = new AppDbContext())
            {
                while (true)
                {

                    string option = ShowMenu("What do you want to do?", new[] {
                        "Show all properties",
                        "Enter and save new property",
                        "Search properties by size",
                        "Search properties by price",
                        "Delete property ",

                        "Exit"
                    });

                    if (option == "Show all properties") ListEstates();
                    else if (option == "Search properties by size") SearchBySize();
                    else if (option == "Search properties by price") SearchByPrice();
                    else if (option == "Enter and save new property") AddNewProperty2();
                    else if (option == "Delete property ") DeleteEstate();
                    else Environment.Exit(0);

                    Console.WriteLine();


                }
            }
        }


        static string ShowMenu(string prompt, string[] options)
        {



            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.WriteLine("                               ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("===============================");

            Console.WriteLine("          REAL ESTATE          ");

            Console.WriteLine("===============================");

            Console.BackgroundColor = ConsoleColor.Magenta;

            Console.WriteLine("                               ");
            Console.ResetColor();
            Console.WriteLine(prompt);

            int selected = 0;

            // Hide the cursor that will blink after calling ReadKey.
            Console.CursorVisible = false;

            ConsoleKey? key = null;
            while (key != ConsoleKey.Enter)
            {
                // If this is not the first iteration, move the cursor to the first line of the menu.
                if (key != null)
                {
                    Console.CursorLeft = 0;
                    Console.CursorTop = Console.CursorTop - options.Length;
                }

                // Print all the options, highlighting the selected one.
                for (int i = 0; i < options.Length; i++)
                {
                    var option = options[i];
                    if (i == selected)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                    }
                    Console.WriteLine("- " + option);
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }

                // Read another key and adjust the selected value before looping to repeat all of this.
                key = Console.ReadKey().Key;
                if (key == ConsoleKey.DownArrow)
                {
                    selected = Math.Min(selected + 1, options.Length - 1);
                }
                else if (key == ConsoleKey.UpArrow)
                {
                    selected = Math.Max(selected - 1, 0);
                }
            }

            // Reset the cursor and perform the selected action.
            Console.CursorVisible = true;
            Console.Clear();
            return options[selected];
        }

        static void WriteUnderlined(string line)
        {
            Console.WriteLine(line);
            Console.WriteLine(new String('-', line.Length));
        }

        static string ReadString(string prompt)
        {
            Console.Write(prompt + ": ");
            return Console.ReadLine();
        }
        
        static int ReadInt(string prompt)
        {
            Console.Write(prompt + ": ");
            int? number = null;
            while (number == null)
            {
                string input = Console.ReadLine();
                try
                {
                    number = int.Parse(input);
                }
                catch
                {
                    Console.Write("Please enter a valid integer: ");
                }
            }

            return (int)number;
        }
        static void ListEstates()
        {
            var estates = database.Estate.Include(e => e.Type).ToList();
            if (estates.Count() == 0)
            {
                Console.WriteLine("There are no estates in the database.");
            }
            else
            {
                Display(estates);

            }

        }

        static void SearchBySize()
        {          
            int x = ReadInt("Write minimum size of property in m2");        
            int y = ReadInt("Write minimum size of property in m2");

            Console.WriteLine("RESULT OF SEARCHING: ");
            var estates = database.Estate.Include(e => e.Type).Where(s => s.Size >= x && s.Size <= y).ToList();
            if (estates.Count != 0 && estates != null)
            {
                Display(estates);
            }
            else { Console.WriteLine("There is no real estates in this size"); }
           
        }

        static void SearchByPrice()
        {          
            int x = ReadInt("Write minimum price of property in EUR");        
            int y = ReadInt("Write maximum price of property in EUR");

            Console.WriteLine("RESULT OF SEARCHING: ");
            var estates = database.Estate.Include(e => e.Type).Where(s => s.Price >= x && s.Price <= y).ToList();
            if (estates.Count != 0 && estates != null)
            {
                Display(estates);
            }
            else { Console.WriteLine("There is no real estates in this price"); }
        }

        

        static void DeleteEstate()
        {
            Estate[] estates = database.Estate.ToArray();

            
            string estatesID = ShowMenu("SELECT ID:  ", estates.Select(e => Convert.ToString(e.ID)).ToArray());
            var estate = estates.First(e => Convert.ToString(e.ID) == estatesID);
            database.Remove(estate);
            database.SaveChanges();

            Console.WriteLine("estate with ID: " + estate.ID + " is deleted.");
        }

       

        static void AddNewProperty2()
        {
            WriteUnderlined("SELECT TYPE:");

            Type type = new Type();

            string[] types = database.Type.Select(t => t.Name).ToArray();

            string selectedTypeName = ShowMenu("Select type of property:", types);

            Type selectedType = database.Type.First(t => t.Name == selectedTypeName);

            Estate estate = new Estate();
            estate.Type = selectedType;
            estate.Location = ReadString("Location: ");
            estate.Description = ReadString("Description: ");
            estate.Size = ReadInt("Size: ");
            estate.Price = ReadInt("Price: ");

            //UPDATE DATABASE
            database.Add(estate);
            database.SaveChanges();
            Console.WriteLine("The property is added.");

        }

        static void Display(List<Estate> estates)
        {
            foreach (var e in estates)
            {
                Console.WriteLine("____________________________________________________________________________________________________________________________________________________");
                Console.WriteLine("| ID:  " + e.ID + " | " + e.Type.Name + " | LOCATION: " + e.Location + " | DESCRIPTION: " + e.Description + " | SIZE: " + e.Size +
                     " | PRICE: " + e.Price);
                Console.WriteLine("____________________________________________________________________________________________________________________________________________________");
            }
        }
    }
}

















