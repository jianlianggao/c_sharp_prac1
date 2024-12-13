using System;
using System.IO;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Input Player Information");
            Console.WriteLine("2. Load Player Information from File");
            Console.WriteLine("3. Exit");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    InputPlayerInformation();
                    break;
                case "2":
                    LoadAndDisplayPlayerInformation();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void InputPlayerInformation()
    {
        string[,] teamInfo = new string[5, 4];

        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"Enter information for Player {i + 1}:");
            Console.Write("Name: ");
            teamInfo[i, 0] = Console.ReadLine();
            Console.Write("Goals: ");
            teamInfo[i, 1] = Console.ReadLine();
            Console.Write("Yellow Cards: ");
            teamInfo[i, 2] = Console.ReadLine();
            Console.Write("Red Cards: ");
            teamInfo[i, 3] = Console.ReadLine();
            Console.WriteLine();
        }

        SaveArrayToFile(teamInfo, "teamInfo.txt");
        Console.WriteLine("Player information saved to teamInfo.txt\n");
    }

    static void LoadAndDisplayPlayerInformation()
    {
        string[,] loadedTeamInfo = LoadArrayFromFile("teamInfo.txt");
        DisplayAndCalculateTotals(loadedTeamInfo);
    }

    static void SaveArrayToFile(string[,] array, string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    writer.Write(array[i, j]);
                    if (j < array.GetLength(1) - 1)
                        writer.Write(",");
                }
                writer.WriteLine();
            }
        }
    }

    static string[,] LoadArrayFromFile(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        string[,] array = new string[lines.Length, 4];

        for (int i = 0; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(',');
            for (int j = 0; j < parts.Length; j++)
            {
                array[i, j] = parts[j];
            }
        }

        return array;
    }

    static void DisplayAndCalculateTotals(string[,] array)
    {
        int totalGoals = 0;
        int totalYellowCards = 0;
        int totalRedCards = 0;

        Console.WriteLine("Player Name\tGoals\tYellow Cards\tRed Cards");
        for (int i = 0; i < array.GetLength(0); i++)
        {
            Console.WriteLine($"{array[i, 0]}\t{array[i, 1]}\t{array[i, 2]}\t{array[i, 3]}");

            totalGoals += int.Parse(array[i, 1]);
            totalYellowCards += int.Parse(array[i, 2]);
            totalRedCards += int.Parse(array[i, 3]);
        }

        Console.WriteLine($"\nTotal Goals: {totalGoals}");
        Console.WriteLine($"Total Yellow Cards: {totalYellowCards}");
        Console.WriteLine($"Total Red Cards: {totalRedCards}");
    }
}
