using System;

static class Program
{
    public static void Main()
    {
        int tankSize = 7;
        
        int[] gas = new int[] {3, 6, 2, 3, 8, 2, 4, 9, 10, 4, 5};
        int[] cost = new int[] {4, 5, 2, 3, 1, 5, 6, 2, 3, 4, 5};
        if(TourCircuit(gas, cost, 5) == true)
            Console.WriteLine("Made it!");
        else
            Console.WriteLine("YOU FAILED");
        
        // ^---- Infinite tank size ||| Limited tank size ----v
        
        int[] gas2 = new int[] {3, 6, 2, 3, 8, 2, 4, 9, 10, 4, 5};
        int[] cost2 = new int[] {4, 5, 2, 3, 1, 5, 6, 2, 3, 4, 5};
        if(TourCircuitLimited(gas2, cost2, 2, tankSize) == true)
            Console.WriteLine("Made it!");
        else
            Console.WriteLine("YOU FAILED");
        
    }
    
    public static bool TourCircuit(int[] gas, int[] cost, int start)
    {
        int gasLevel = 0;
        int count = 0;
        int currentStation = start;
        
        while(count < gas.Length)
        {
            gasLevel += gas[currentStation];
            //Console.WriteLine("Filled up - Current level: " + gasLevel);
            gasLevel -= cost[currentStation];
            //Console.WriteLine("Drove to next station - Current level: " + gasLevel);
            //Console.WriteLine("-----------------------------------------");
            if(gasLevel < 0)
                break;
            currentStation++;
            if(currentStation == gas.Length)
                currentStation = 0;
            count++;
        }
        
        if(count == gas.Length)
            return true;
        else
            return false;
    }
    
    public static bool TourCircuitLimited(int[] gas, int[] cost, int start, int tankSize)
    {
        int gasLevel = 0;
        int count = 0;
        int currentStation = start;
        
        while(count < gas.Length)
        {
            gasLevel += gas[currentStation];
            if(gasLevel > tankSize)
                gasLevel = tankSize;
            //Console.WriteLine("Filled up - Current level: " + gasLevel);
            gasLevel -= cost[currentStation];
            //Console.WriteLine("Drove to next station - Current level: " + gasLevel);
            //Console.WriteLine("-----------------------------------------");
            if(gasLevel < 0)
                break;
            currentStation++;
            if(currentStation == gas.Length)
                currentStation = 0;
            count++;
        }
        
        if(count == gas.Length)
            return true;
        else
            return false;
    }
}