using System;

static class Program
{
    public static void Main()
    {
        int[,] intArr = new int[,] {{1, 2, 3, 4}, {5, 6, 7, 8}, {9, 10, 11, 12}, {13, 14, 15, 16}, {17, 18, 19, 20}};
        SpiralDisplay(intArr);
    }
    
    public static void SpiralDisplay(int[,] array)
    {
        int r = 0;
        int c = 0;
        int topDone = 0;
        int rightDone = 0;
        int botDone = 0;
        int leftDone = 0;
        string direction = "right";
        bool finished = false;
        
        while(!finished)
        {
            Console.Write("" + array[r, c]);
            if(direction == "right")
            {
                if(c == array.GetLength(1) - rightDone - 1)
                {
                    direction = "down";
                    topDone++;
                    r++;
                }
                else
                    c++;
            }
            else if(direction == "down")
            {
                if(r == array.GetLength(0) - botDone - 1)
                {
                    direction = "left";
                    rightDone++;
                    c--;
                }
                else
                    r++;
            }
            else if(direction == "left")
            {
                if(c == leftDone)
                {
                    direction = "up";
                    botDone++;
                    r--;
                }
                else
                    c--;
            }
            else
            {
                if(r == topDone)
                {
                    direction = "right";
                    leftDone++;
                    c++;
                }
                else
                    r--;
            }
            
            if(topDone + botDone == array.GetLength(0) || leftDone + rightDone == array.GetLength(1))
            {
                finished = true;
                break;
            }
            Console.Write(", ");
        }
    }
}