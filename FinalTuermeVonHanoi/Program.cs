using System;

namespace FinalTuermeVonHanoi;

class Program
{
    static List<int>[] TowerArray = new List<int>[3];

    public static void Main()
    {
        int amountOfDisks = RequestDiskAmount();

        Console.WriteLine($"AMOUNT OF DISKS ENTERED: {amountOfDisks}");


        TowerArray = InitializeGame(amountOfDisks);
        Console.WriteLine($"FIRST TOWER SIZE: {TowerArray[0].Count}");

        int gameType = RequestGametype();
        PrintTowers();
        Console.WriteLine($"GAMETYPE CHOSEN: {gameType}\n");


        if (gameType == 1)
        {
            AutoSolve(amountOfDisks, 'A', 'B', 'C');
        }
        else
        {
            ManualSolve();
        }
    }
    public static int IntegerInput(string prompt, int min, int max)
    {
        Console.WriteLine(prompt);

        int temp;
        while (
            !int.TryParse(Console.ReadLine(), out temp) ||
            (temp < min || temp > max)
            )
        { }

        return temp;
    }

    public static int RequestDiskAmount()
    {
        string text = "Please enter the number of disks to be used (between 1 and 8): ";

        return IntegerInput(text, 1, 8);
    }

    public static List<int>[] InitializeGame(int amountOfDisks)
    {
        List<int>[] towerArray = new List<int>[3];

        for (int i = 0; i < towerArray.Length; i++)
        {
            towerArray[i] = new List<int>();
        }

        // Initializing the first tower
        int j = amountOfDisks;
        for (int i = 0; i < amountOfDisks; i++, j--)
        {
            towerArray[0].Add(j);
        }

        return towerArray;
    }



    public static int RequestGametype()
    {
        string text = "Choose your game type: Auto(1), Manual(2)";

        return IntegerInput(text, 1, 2);
    }


    static void AutoSolve(int amountOfDisks, char x, char y, char z)

    {
        if (amountOfDisks == 1)
        {
            Console.WriteLine($"Moving disk 1 from {x} to {z}");
            return;
        }

        AutoSolve(amountOfDisks - 1, x, z, y);
        Console.WriteLine($"Moving disk {amountOfDisks} from {x} to {z}");

        AutoSolve(amountOfDisks - 1, y, x, z);

    }

    public static void ManualSolve()
    {
        while (!CheckWinningCondition())
        {
            Turn();
            PrintTowers();
        }
        Console.WriteLine("Congratulation! You solved the game.");
    }

    private static bool CheckWinningCondition()
    {
        return TowerArray[0].Count == 0 && TowerArray[1].Count == 0;
    }

    private static void Turn()
    {
        int sourceTower = 0;
        int targetTower = 0;
        bool validMove = false;

        while (!validMove)
        {
            sourceTower = IntegerInput("From: ", 1, 3);
            targetTower = IntegerInput("To ", 1, 3);
            validMove = CheckIfMovable(sourceTower - 1, targetTower - 1);
        }
        MoveDisc(sourceTower - 1, targetTower - 1);
    }
    public static bool CheckIfMovable(int sourceTower, int targetTower)
    {
        if (sourceTower == targetTower || TowerArray[sourceTower].Count == 0)
        {
            return false;
        }

        var sourceDisc = GetTopDisc(sourceTower);
        var targetDisc = GetTopDisc(targetTower);

        // both valid discs and size difference ok (target greater than source)
        if (sourceDisc == 0 || (sourceDisc >= targetDisc && targetDisc != 0))
        {
            return false;
        }
        return true;
    }

    private static int GetTopDisc(int towerIndex)
    {
        if (TowerArray[towerIndex].Count == 0)
        {
            return 0;
        }
        return TowerArray[towerIndex][TowerArray[towerIndex].Count - 1];
    }

    

    private static void MoveDisc(int sourceTower, int targetTower)
    {
        int temp = TowerArray[sourceTower][TowerArray[sourceTower].Count - 1];
        TowerArray[sourceTower].Remove(temp);
        TowerArray[targetTower].Add(temp);
    }

    

    private static void PrintTowers()
    {
        int towerNumber = 1;
        foreach (var tower in TowerArray)
        {
            Console.Write($"Tower {towerNumber}: ");

            foreach (var disc in tower)
            {
                Console.Write(disc);
                Console.Write(" ");
            }
            Console.WriteLine();
            towerNumber++;
        }
    }

    




    
}

