namespace GitCrud;

internal class Program
{
    public static List<Game> games = new List<Game>();
    static void Main(string[] args)
    {
        Game.seedGames();
        Rental.seedRental();

        while (true)
        {
            menuMessages();
            int input = forcedIntInput();
            switch (input)
            {
                case 1:
                    Game.printGames();
                    break;
                case 2:
                    Game.addGame();
                    break;
                case 3:
                    Game.editGame();

                    break;
                case 4:
                    Game.deleteGame();
                    break;
                case 5:
                    Rental.rentGame();
                    break;
                case 6:
                    Rental.printAvailableGames();
                    break;
                case 7:
                    Rental.printRentedGames();
                    break;
                case 8:
                    Rental.returnGame();
                    break;
                case 9:
                    Rental.editRental();
                    break;
                case 10:
                    Rental.printGameRentalHistory();
                    break;
                case 11:
                    Rental.sortGamesByRentCount();
                    break;
                case 12:
                    exitProgram();
                    break;
                case 13://
                    exitProgram();
                    break;
            }
        }
    }

    public static int forcedIntInput()
    {
        int input = 0;
        while (input == 0)
        {
            int.TryParse(Console.ReadLine(), out input);
            if (input == 0)
            {
                Console.WriteLine("Iveskite teisinga skaiciaus formata");
            }
        }
        return input;
    }
    public static void exitProgram()
    {
        Environment.Exit(0);
    }
    public static void menuMessages()
    {
        Console.WriteLine();
        Console.WriteLine("----------------------");
        Console.WriteLine("1. perziureti zaimimus");
        Console.WriteLine("2. prideti zaidima");
        Console.WriteLine("3. redaguoti zaidima");
        Console.WriteLine("4. istrinti zaidima");
        Console.WriteLine("5. nuomuotis zaidima");
        Console.WriteLine("6. rodyti laisvus zaidimus");
        Console.WriteLine("7. rodyti isnuomuotus zaidimus");
        Console.WriteLine("8. grazinti zaidima");
        Console.WriteLine("9. redaguoti zaidimo nuomos duomenis");
        Console.WriteLine("10. perziureti zaidimo nuomos istorija");
        Console.WriteLine("11. rusiuoti zaidimus pagal populiariaruma /not implenmented");
        Console.WriteLine("12. rusiuoti zaidimus pagal nuomavimosi trukmes vidurki /not implenmented");
        Console.WriteLine("13. iseiti is programos");
        Console.WriteLine("----------------------");
        Console.WriteLine();
    }
}
