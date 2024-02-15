using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitCrud;

internal class Rental
{
    private string renter;
    private DateTime rentDate;
    private DateTime returnDate;
    private TimeSpan timeDifference;
    public Rental()
    {
        rentDate = DateTime.Now;
    }

    public Rental(string renter, DateTime rentDate, DateTime returnDate)
    {
        this.renter = renter;
        this.rentDate = rentDate;
        this.returnDate = returnDate;
    }
    public string Renter
    {
        get { return renter; }
        set { renter = value; }
    }

    public DateTime RentDate
    {
        get { return rentDate; }
        set { rentDate = value; }
    }

    public DateTime ReturnDate
    {
        get { return returnDate; }
        set { returnDate = value; }
    }

    public static DateTime RandomDateTimeFromPast3Months(DateTime? optionalStartDate = null)
    {
        var now = DateTime.UtcNow;

        var startDate = optionalStartDate ?? now.AddMonths(-3);
        var endDate = now;

        if (startDate > endDate)
        {
            throw new ArgumentException("Optional startDate cannot be later than current date.");
        }
        var randomTick = new Random().NextInt64(startDate.Ticks, endDate.Ticks);
        var randomDateTime = new DateTime(randomTick, DateTimeKind.Utc);
        return randomDateTime;
    }

    public static void sortGamesByRentCount()
    {
        Console.WriteLine("asc for ascending, desc for descending");
        string order = Console.ReadLine().ToLower().Trim();
        for (int i = 0; i < Program.games.Count; i++)
        {
            Game gameA = Program.games[i];
            for (int a = i; a < Program.games.Count; a++)
            {
                Game gameB = Program.games[a];
                if (order.Equals("desc") && gameA.Rents.Count > gameB.Rents.Count)
                {
                    Game temp = Program.games[i];
                    Program.games[i] = Program.games[a];
                    Program.games[a] = temp;
                }
                if (order.Equals("asc") && gameA.Rents.Count < gameB.Rents.Count)
                {
                    Game temp = Program.games[i];
                    Program.games[i] = Program.games[a];
                    Program.games[a] = temp;
                }
            }
        }
    }
    public static void seedRental()
    {
        string[] names = { "Jonas", "Petras", "Antanas", "MC Purvinis", "Vilhelmas" };
        Random rnd = new Random();
        foreach (var game in Program.games)
        {
            int rentedTimes = rnd.Next(0, 5);

            for (int i = 0; i < rentedTimes; i++)
            {
                Rental r = new();
                game.Rents.Add(r);
                r.renter = names[rnd.Next(names.Length - 1)];
                DateTime dt = RandomDateTimeFromPast3Months();
                r.rentDate = dt;
                if (i + 1 == rentedTimes && rnd.Next(2) == 0)
                {
                    break;
                }
                r.returnDate = RandomDateTimeFromPast3Months(dt);
                r.timeDifference = r.returnDate - r.rentDate;
            }
        }
    }
    public static void rentGame()
    {
        Game.printGames();
        Console.WriteLine("iveskite zaidimo kuri norite nuomuotis id");
        int id = Convert.ToInt32(Console.ReadLine());
        foreach (var game in Program.games)
        {
            if (game.Id == id)
            {
                if (game.Rents.Count > 0 && game.Rents.Last().returnDate == DateTime.MinValue)
                {
                    Console.WriteLine("Sis zaidimas jau isnuomuotas");
                    break;
                }
                Rental r = new();
                Console.WriteLine("Iveskite nuomininko Varda");
                r.renter = Console.ReadLine();
                game.Rents.Add(r);
                Console.WriteLine($"Zaidimas \"{game.Title}\" sekmingai isnuomuotas");
                break;
            }
        }
    }
    public static void returnGame()
    {
        Game.printGames();
        Console.WriteLine("iveskite zaidimo kuri norite grazinti id");
        int id = Convert.ToInt32(Console.ReadLine());
        foreach (var game in Program.games)
        {
            if (game.Id == id)
            {
                if (game.Rents.Count == 0)
                {
                    Console.WriteLine("Sis zaidimas niekada nebuvo isnuomuotas!");
                    break;
                }
                if (game.Rents.Last().returnDate != DateTime.MinValue)
                {
                    Console.WriteLine("Sis zaidimas jau yra grazintas!");
                    break;
                }
                game.Rents.Last().returnDate = DateTime.Now;
                game.Rents.Last().timeDifference = game.Rents.Last().returnDate - game.Rents.Last().rentDate;
                Console.WriteLine($"Zaidimas \"{game.Title}\" sekmingai grazintas");
                break;
            }
        }
    }
    public static void editRental()
    {
        Game.printGames();
        Console.WriteLine("iveskite zaidimo Id kurio nuomos duomenis norite redaguoti");
        int id = Convert.ToInt32(Console.ReadLine());
        foreach (var game in Program.games)
        {
            if (game.Id == id)
            {
                if (game.Rents.Count == 0 || (game.Rents.Last().returnDate != DateTime.MinValue))
                {
                    Console.WriteLine("Sis zaidimas niekada nebuvo isnuomuotas arba jau grazintas!");
                    break;
                }
                Console.WriteLine("Iveskite nauja nuomininko varda");
                game.Rents.Last().renter = Console.ReadLine();
                Console.WriteLine($"Zaidimo \"{game.Title}\" nuomos duomenys buvo sekmingai atnaujinti");
                break;
            }
        }
    }
    public static void printGameRentalHistory()
    {
        Console.WriteLine("iveskite zaidimo Id kurio nuomos duomenis norite perziureti");
        int id = Convert.ToInt32(Console.ReadLine());
        foreach (var game in Program.games)
        {
            if (game.Id == id)
            {
                foreach (var rent in game.Rents)
                {
                    Console.WriteLine(rent);

                }
                break;
            }
        }
    }
    public static void printAvailableGames()
    {
        Console.WriteLine("----------------------");
        foreach (var game in Program.games)
        {
            if ((game.Rents.Count > 0 && game.Rents.Last().ReturnDate != DateTime.MinValue) ||
                game.Rents.Count == 0
               )
            {
                Console.WriteLine(game);
            }
        }
        Console.WriteLine("----------------------");
    }
    public static void printRentedGames()
    {
        Console.WriteLine("----------------------");
        foreach (var game in Program.games)
        {
            if (game.Rents.Count > 0 && game.Rents.Last().ReturnDate == DateTime.MinValue)
            {
                Console.WriteLine(game);
            }
        }
        Console.WriteLine("----------------------");
    }

    public override string? ToString()
    {
        return $"{renter} {rentDate} {returnDate} nuomos trukme: Days: {timeDifference.Days}, Hours: {timeDifference.Hours}, Minutes: {timeDifference.Minutes}, Seconds: {timeDifference.Seconds}";
    }
}
