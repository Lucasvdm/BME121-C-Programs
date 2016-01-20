using System;

static class Program
{
    public static void Main()
    {
        int[,] rectangle = new int[,] {{1, 1, 1, 0, 1, 1, 0, 1}, {1, 1, 0, 0, 0, 1, 1, 1}, {1, 0, 0, 1, 1, 0, 0, 1}};
        int leftTopX;
        int leftTopY;
        int bottomRightX;
        int bottomRightY;
        int area;
        LargestSubRectangle(rectangle, out leftTopX, out leftTopY, out bottomRightX, out bottomRightY, out area);
        Console.WriteLine("Top Left X: " + leftTopX);
        Console.WriteLine("Top Left Y: " + leftTopY);
        Console.WriteLine("Bottom Right X: " + bottomRightX);
        Console.WriteLine("Bottom Right Y: " + bottomRightY);
        Console.WriteLine("Area: " + area);
    }
    
    public static void LargestSubRectangle(int[,] rectangle, out int leftTopX, out int leftTopY,
                                           out int bottomRightX, out int bottomRightY, out int area)
    {
        leftTopX = 0;
        leftTopY = 0;
        bottomRightX = 0;
        bottomRightY = 0;
        area = 0;
        
        int r = 0;
        int c = 0;
        while(r < rectangle.GetLength(0) && c < rectangle.GetLength(1))
        {
            if(rectangle[r,c] == 1)
            {
                //Console.WriteLine("ROW = " + r);
                //Console.WriteLine("COLUMN = " + c);
                int rAdjust = 0;
                while(r + rAdjust < rectangle.GetLength(0) && rectangle[r + rAdjust, c] == 1)
                {
                    int cAdjust = 0;
                    int tempArea = 0;
                    //Console.WriteLine("RAdjust: " + rAdjust);
                    while(c + cAdjust + 1 < rectangle.GetLength(1) && rectangle[r + rAdjust, c + cAdjust + 1] == 1)
                    {
                        cAdjust++;
                        bool full = true;
                        for(int i = r; i <= r + rAdjust; i++)
                        {
                            for(int j = c; j <= c + cAdjust; j++)
                            {
                                if(rectangle[i, j] == 0)
                                {
                                    full = false;
                                }
                            }
                        }
                        if(!full)
                        {
                            cAdjust--;
                            break;
                        }
                    }
                    //Console.WriteLine("CAdjust: " + cAdjust);
                    //Console.WriteLine();
                    tempArea = (rAdjust + 1) * (cAdjust + 1);
                    if(tempArea > area)
                    {
                        area = tempArea;
                        leftTopX = c;
                        leftTopY = r;
                        bottomRightX = c + cAdjust;
                        bottomRightY = r + rAdjust;
                    }
                    rAdjust++;
                }
            }
            c++;
            if(c == rectangle.GetLength(1))
            {
                c = 0;
                r++;
            }
        }
    }
}