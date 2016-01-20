using System;

class SortingStrings
{
    public static void SelectSort(string[] array)
    {
		int minIndex;
        string temp;
        int i, j;
        for (i = 0; i < array.Length; i++)
        {
            minIndex = i;
            // finding minimum of remained unsorted part
            for (j = i + 1; j < array.Length; j++)
            {
                if (array[j].CompareTo(array[minIndex]) < 0)
                {
                    minIndex = j;
                }
            }
            //swap min with place i
            temp = array[minIndex];
            array[minIndex] = array[i];
            array[i] = temp;
        }
    }

    public static void InsertSort(string[] array)
    {
		int i, j; 
        string temp;
        for (i = 0; i < array.Length; i++)
        {
            temp = array[i];
            j = i - 1;
            while (j >= 0 && temp.CompareTo(array[j]) < 0)
            {
                array[j + 1] = array[j];
                j--;
            }
            //Insertion
            array[j + 1] = temp;
        }
    }

}

static class Prog3
{
    static void Main()
    {
        string[] array1 = { "John", "Sarah", "Joly", "Jolian", "Ahmed", "Phill"};
        string[] array2 = (string[])array1.Clone();
        string[] array3 = (string[])array1.Clone();

        Console.Write("Original Array : ");
        foreach (string number in array1)
            Console.Write(number + " ");
        Console.WriteLine();

        SortingStrings.SelectSort(array2);

        SortingStrings.InsertSort(array3);

        Console.Write("Selection Sort : ");
        foreach (string number in array2)
            Console.Write(number + " ");
        Console.WriteLine();

        Console.Write("Insertion Sort : ");
        foreach (string number in array3)
            Console.Write(number + " ");
        Console.WriteLine();

    }
}
