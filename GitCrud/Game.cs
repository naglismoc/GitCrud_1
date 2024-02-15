using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitCrud
{
    internal class Game
    {
        private static int idCount = 0;

        private int id;
        private string title;
        private string type;
        private int releaseYear;
        private string[] runsOn;
        private List<Rental> rents;

        public Game()
        {
            this.id = ++idCount;
            rents = new();
        }

        public Game(string title, string type, int releaseYear)
        {
            this.id = ++idCount;
            this.title = title;
            this.type = type;
            this.releaseYear = releaseYear;
            rents = new();
        }

        public static void seedGames()
        {
            Program.games.Add(new Game("LOTR", "action RPG", 2006));
            Program.games.Add(new Game("WOW", "MMORPG", 2004));
            Program.games.Add(new Game("lol", "MMORPG", 2006));
            Program.games.Add(new Game("dota", "MMORPG", 2004));
            Program.games.Add(new Game("osrs", "MMORPG", 2001));
        }
        public static void addGame()
        {
            Game g = new Game();
            Console.WriteLine("Iveskite zaidimo pavadinima");
            g.Title = Console.ReadLine();
            Console.WriteLine("Iveskite zaidimo tipa");
            g.Type = Console.ReadLine();
            Console.WriteLine("Iveskite zaidimo isleidimo metus");
            g.ReleaseYear = Convert.ToInt32(Console.ReadLine());
            Program.games.Add(g);
        }
        public static void editGame()
        {
            Console.WriteLine("Iveskite zaidimo Id kuri norite redaguoti:");
            int id = Convert.ToInt32(Console.ReadLine());

            foreach (var game in Program.games)
            {

                if (game.Id == id)
                {
                    Console.WriteLine("Iveskite nauja zaidimo pavadinima");
                    game.Title = Console.ReadLine();
                    Console.WriteLine("Iveskite nauja zaidimo tipa");
                    game.Type = Console.ReadLine();
                    Console.WriteLine("Iveskite naujus zaidimo isleidimo metus");
                    game.ReleaseYear = Convert.ToInt32(Console.ReadLine());
                    break;
                }
            }
        }
        public static void deleteGame()
        {
            Console.WriteLine("Iveskite zaidimo Id kuri norite trinti:");
            int id = Convert.ToInt32(Console.ReadLine());
            bool found = false;
            string title = "";
            foreach (var game in Program.games)
            {

                if (game.Id == id)
                {
                    title = game.Title;
                    found = Program.games.Remove(game);
                    break;
                }
            }
            if (found)
            {
                Console.WriteLine($"Zaidimas {title} sekmingai istrintas.");
            }
            else
            {
                Console.WriteLine($"Deja tokio zaidomo kurio Id yra {id} neradome");
            }

        }
        public static void printGames()
        {
            Console.WriteLine("----------------------");
            foreach (var game in Program.games)
            {
                Console.WriteLine(game);
            }
            Console.WriteLine("----------------------");
        }


        public List<Rental> Rents
        {
            get { return rents; }
            set { rents = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public int ReleaseYear
        {
            get { return releaseYear; }
            set { releaseYear = value; }
        }

        public override string ToString()
        {
            return $"{id}: {title} {type} {releaseYear} rentCount:{rents.Count}";
        }
    }
}
