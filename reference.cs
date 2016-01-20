//BY: LUCAS VAN DE MOSSELAER

using System;
using System.IO;



//DataType generic receives a variable type when a LinkedList object is created - this variable type will be used
//in place of all occurrences of "DataType"
class LinkedList<DataType>
{
    Node head;
    Node tail;
    
    class Node
    {
        public Node next;
        public DataType data;
        
        public Node(Node next, DataType data)
        {
            this.next = next;
            this.data = data;
        }
    }
    
    public LinkedList()
    {
        head = null;
        tail = null;
    }
    
    //Make a new Node object using the data given and add the new Node to the end of the list
    public void Append(DataType info)
    {
        Node node = new Node(null, info);
        if(head == null)
        {
            head = node;
            tail = node;
            node.next = null;
        }
        else
        {
            tail.next = node;
            node.next = null;
            
            //Note, this only changes which node the variable "tail" is referencing - it does not
            //change the value of the previous tail
            tail = node;
        }
    }

    public override string ToString()
    {
        string str = "";
        Node index = head;
        while(index != null)
        {
            str += string.Format("{0} ", index.data);
            index = index.next;
        }
        return str;
    }
}






static class Point
{
	/*	Receives an int (arbitrarily called testInt here to show the relationship with
		the testInt in the Main() method) and DOES NOT RETURN A VALUE (thus the void) */
	public static void TestValueMethod(int testInt)
	{
		testInt = 200;
	}
	
	/*	Receives an int[] array (arbitrarily called testArray here to show the relationship
		with the testArray in the Main() method) and DOES NOT RETURN A VALUE (thus the void) */
	public static void TestReferenceMethod(int[] testArray)
	{
		testArray[0] = 1;
	}
}






class NonStatic
{
	//Non-static instance fields hold separate values for each instantiated object
	public int objectInt;
	public string objectString;
	
	//Constructor receives values and initializes the instance fields with those variables
	//FOR THE OBJECT THAT IS BEING INSTANTIATED
	public NonStatic(int number, string word)
	{
		//Either works, "this." can be used for clarity or to distinguish between an instance field
		//and a local variable that was passed to the method
		this.objectInt = number;
		objectString = word;
	}
	
	
	//Accessors for instance fields objectInt and objectString
	//Not actually necessary in this case - accessors are usually used when the instance fields are private
	//and therefore cannot be directly accessed by using objects in other classes
	public int ObjectInt
	{
		//Just returns the value of the instance field - pretty simple
		get
		{
			return objectInt;
		}
		//Even though the accessor doesn't explicitly receive any parameters (input variables), the variable "value"
		//is still used to set the instance field to a certain value
		//Note: value must ALWAYS be called "value"
		set
		{
			objectInt = value; //The variable type of value is implicitly defined by the variable type of objectInt
		}
	}
	
	public string ObjectString
	{
		//Just returns the value of the instance field - pretty simple
		get
		{
			return objectString;
		}
		//Even though the accessor doesn't explicitly receive any parameters (input variables), the variable "value"
		//is still used to set the instance field to a certain value
		//Note: value must ALWAYS be called "value"
		set
		{
			objectString = value; //The variable type of value is implicitly defined by the variable type of objectString
		}
	}
}






static class Program
{
	public static void Main()
	{
		TestReferenceTypes();
		
		SeparateSections();
		
		TestObjects();
		
		SeparateSections();
		
		TestNestedLoops();
		
		SeparateSections();
		
		TestForEachLoop();
		
		SeparateSections();
		
		TestTryParse();
        
        SeparateSections();
        
        Test2DArrays();
        
        SeparateSections();
        
        TestFileReading();
        
        SeparateSections();
        
        TestFileWriting();
        
        SeparateSections();
        
        TestLinkedList();
        
        SeparateSections();
        
        TestSorting();
        
        SeparateSections();
        
        TestBinarySearch();
        
        SeparateSections();
        
        FindKthLargestInteger(1);
	}
	
	
	
	public static void TestReferenceTypes()
	{
		//Starting values
		int testInt = 42;
		int[] testArray = new int[1];
		testArray[0] = 0;
		
		//Method calls
		Point.TestValueMethod(testInt); //passes testInt by value
		Point.TestReferenceMethod(testArray); //passes testArray by reference
		
		//Output testing
		Console.WriteLine("Reference type testing:");
		Console.WriteLine(testInt); //testInt does not change
		Console.WriteLine(testArray[0]); //testArray changes
	}
	
	
	
	public static void TestObjects()
	{
		//NonStatic objects
		NonStatic object1 = new NonStatic(1, "one");
		NonStatic object2 = new NonStatic(2, "two");
		
		//Retrieve values from instance fields
		int firstInt = object1.objectInt;
		int secondInt = object2.objectInt;
		string firstString = object1.objectString;
		string secondString = object2.objectString;
		
		//Output retrieved values
		Console.WriteLine("Object and non-static class testing:");
		Console.WriteLine(firstInt);
		Console.WriteLine(secondInt);
		Console.WriteLine(firstString);
		Console.WriteLine(secondString);
		
		//Use accessors to set values of instance fields
		object1.ObjectInt = 10;
		object2.ObjectInt = 20;
		object1.ObjectString = "ten";
		object2.ObjectString = "twenty";
		
		//Use accessors to get values of instance fields, then output them
		Console.WriteLine();
		Console.WriteLine("Accessor testing:");
		Console.WriteLine(object1.ObjectInt);
		Console.WriteLine(object2.ObjectInt);
		Console.WriteLine(object1.ObjectString);
		Console.WriteLine(object2.ObjectString);
	}
	
	
	
	public static void TestNestedLoops()
	{
		int[] primeArray = new int[7]; //Array of prime numbers of some length - currently empty
		int counter = 2; //int that counts up one at a time and finds the prime numbers
		
		//Outer loop loops through the indexes of primeArray
		for(int i = 0; i < primeArray.Length; i++)
		{
			//Inner loop increments counter until it equals a prime number
			while(!IsPrime(counter)) counter++;
			primeArray[i] = counter;
			counter++;
		}
		
		//Loops through and outputs values of all indexes of primeArray
		Console.WriteLine("Nested loop prime number testing: ");
		for(int i = 0; i < primeArray.Length; i++)
		{
			Console.WriteLine(primeArray[i]);
		}
	}
	
	
	
	public static void TestForEachLoop()
	{
		//Array for foreach loop
		int[] array = new int[10];
		for(int i = 0; i < 10; i++) array[i] = i + 1;
		
		//For-Each loop -> For each value in the array, perform the function in the loop
		//For-Each loops are READ-ONLY: Cannot change the value of the indexes in the array
		Console.WriteLine("For-Each loop testing: ");
        
        //"var" - variable type of the array (int, double, string, etc. - var is a general variable type)
        //"value" - arbitrary name given to the element that is currently being used (could be anything)
        //"array" - name of the array being looped through
		foreach(var value in array)
		{
			Console.WriteLine(value);
		}
	}
	
	
	
	public static void TestTryParse()
	{
		int i = 0;
		string number = "1";
		string word = "one";
		bool valid = false;
		
		//dataType.TryParse(string, out int) receives a string to be parsed and an int (that is passed by reference using the "out"
		//keyword)
		valid = int.TryParse(word, out i);
		
		//Testing the ouputs after the TryParse fails
		Console.WriteLine("{0}, {1}, {2}", valid, word, i);
		
		valid = int.TryParse(number, out i);
		
		//Testing the outputs after the TryParse succeeds
		Console.WriteLine("{0}, {1}, {2}", valid, number, i);
	}
    
    
    
    public static void Test2DArrays()
    {
        //Make a new 2D array of ints in the form of 1's surrounding a 2 x 2 square of 0's
        int[,] squareArray = new int[,] {{1, 1, 1, 1}, {1, 0, 0, 1}, {1, 0, 0, 1}, {1, 1, 1, 1}};
        //Alternatively, could define the size of the array here as normal ( [4,4] ) and
        //insert the values for the rows/columns later
        //int[,] squareArray = new int[4,4];
        
        //Nested for loops to loop through the entire 2D array, row by row, column by column
        //Outer loop loops through rows, inner loops through columns - indexes in 2D arrays always
        //referred to using ROW then COLUMN.
        //
        //GetLength returns the length of the array at the given dimension, starting at 0 -- i.e.
        //0 returns the length of the first dimension (the # of rows), 1 returns the length of the second
        //dimension (the # of columns), and etc. if there are more dimensions (3D arrays and such - not covered
        //in this course)
        for(int i = 0; i < squareArray.GetLength(0); i++)
        {
            for(int j = 0; j < squareArray.GetLength(1); j++)
            {
                if(j == squareArray.GetLength(1) - 1)
                    Console.WriteLine(squareArray[i,j]);
                else
                    Console.Write("{0}, ", squareArray[i,j]);
            }
        }
        
        //Note: As the loop above simply receives the length of each row instead of assuming that all rows are similar,
        //it would also work for a jagged 2D array (an array with rows of different lengths)
    }
    
    
    public static void TestFileReading()
    {
        //NOTE: For any sort of file reading/writing, you must use the System.IO namespace similar to how the System
        //namespace is used for Console.  See the top of the file.
        
        //The using keyword is a handy-dandy way of opening and AUTOMATICALLY CLOSING a file when you're done with it
        using(StreamReader sr = new StreamReader("testTextFile.txt"))
        {
            //EndOfStream is a property that returns a boolean value when called like this
            //True if the reader has reached the end of the file - otherwise false
            //While loop condition is only true while it has not yet reached the end of the file
            while(!sr.EndOfStream)
            {
                //Reader commands similar to Console commands - Read, ReadLine, etc.
                Console.Write("{0} ", sr.ReadLine());
            }
        }
        Console.WriteLine();
    }
    
    
    
    public static void TestFileWriting()
    {
        //NOTE: For any sort of file reading/writing, you must use the System.IO namespace similar to how the System
        //namespace is used for Console.  See the top of the file.
        
        //The using keyword is a handy-dandy way of opening and AUTOMATICALLY CLOSING a file when you're done with it
        using(StreamWriter sw = new StreamWriter("newTestTextFile.txt"))
        {
            //Writer commands similar to Console commands - Write, WriteLine, etc.
            sw.WriteLine("If you see this, it worked.");
        }
        
        Console.WriteLine("Check your folder and look for newTestTextFile.txt");
    }
    
    
    
    public static void TestLinkedList()
    {
        LinkedList<int> list = new LinkedList<int>();
        list.Append(1);
        list.Append(2);
        list.Append(3);
        list.Append(4);
        list.Append(5);
        Console.WriteLine(list);
    }
    
    
    
    public static void TestSorting()
    {
        int[] array1 = new int[] {1, 3, 5, 2, 4, 6, 9, 8, 7, 0};
        int[] array2 = new int[] {10, 19, 14, 16, 17, 13, 12, 11, 18, 15};
        
        InsertSort(array1);
        SelectionSort(array2);
        
        foreach(int num in array1)
        {
            Console.Write("{0} ", num);
        }
        Console.WriteLine();
        foreach(int num in array2)
        {
            Console.Write("{0} ", num);
        }
        Console.WriteLine();
    }
	
    
    
    public static void TestBinarySearch()
    {
        //Note: Only works on sorted arrays
        
        int[] array = new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9};
        int target = 6;
        
        //Set boundaries on the left and right starting with the first and last indices
        int leftIndex = 0;
        int rightIndex = array[array.Length - 1]; //Works for any array
        int midpoint = 0;
        bool found = false;
        
        while(!found && leftIndex < rightIndex)
        {
            //Find the index in between the left and right boundaries
            midpoint = (leftIndex + rightIndex) / 2;
            
            //If the target is found at the midpoint, you've found the target!
            if(target == array[midpoint])
            {
                found = true;
            }
            
            //If the target is less than the element at the midpoint, set the right boundary to the midpoint - 1 and search in the reduced area
            else if(target < array[midpoint])
            {
                rightIndex = midpoint - 1;
            }
            
            //If the target is greater than the element at the midpoint, set the left boundary to the midpoint + 1 and search in the reduced area
            else if(target > array[midpoint])
            {
                leftIndex = midpoint + 1;
            }
        }
        
        if(found)
            Console.WriteLine("Target ({0}) found at index {1}", target, midpoint);
        else
            Console.WriteLine("Target ({0}) not found in the array", target);
    }
    
    
    
    public static void FindKthLargestInteger(int k)
    {
        int[] array = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
        int[] indexesFound = new int[k];
        
        //Find the largest integer "k" times
        for(int j = 0; j < k; j++)
        {
            int max = 0;
            int maxIndex = 0;
            for(int i = 0; i < array.Length; i++)
            {
                bool foundI = false;
                
                //If the current index has previously been found to be the largest, ignore it this time
                foreach(int num in indexesFound)
                {
                    if(num == i) foundI = true;
                }
                
                if(!foundI && array[i] > max)
                {
                    max = array[i];
                    maxIndex = i;
                }
            }
            
            //Add the max that was just found to the list of previously-found maximums
            indexesFound[j] = maxIndex;
        }
        Console.WriteLine("The {0}st/rd/th largest number in the array is {1}", k, indexesFound[k - 1]);
    }
	
    
	
	//Checks if num is a prime by seeing if it divides evenly with any x (2 <= x <= num)
	public static bool IsPrime(int num)
	{
		for(int i = 2; i < num; i++)
		{
			if(num % i == 0) return false;
		}
		return true;
	}
    	
	
	
    public static void InsertSort(int[] array)
    {
        //Loop through the entire array to do the full sort
        for(int i = 0; i < array.Length; i++)
        {
            int num = array[i];
            int index = i - 1;
            int temp;
            
            //Loop BACKWARDS from the outer loop's current index, swapping each pair of values as the loop progresses to "bump"
            //the other elements forward and the num backward until it reaches a point where the num is greater than the element before it
            //(i.e. the num is in the correct position) or the num is the first element
            while(index >= 0 && array[index] > num)
            {
                temp = array[index + 1];
                array[index + 1] = array[index];
                array[index] = temp;
                index--;
            }
        }
    }
    
    
    
    public static void SelectionSort(int[] array)
    {
        //Loop through the entire array to do the full sort
        for(int i = 0; i < array.Length; i++)
        {
            int minimum = 2147483647;
            int minIndex = 0;
            
            //Loop through the array starting from the current index in the outer loop and find the index with the smallest element
            for(int j = i; j < array.Length; j++)
            {
                if(array[j] < minimum)
                {
                    minimum = array[j];
                    minIndex = j;
                }
            }
            
            //Swap the element at the current index in the outer loop with the smallest element in the rest of the array
            int temp = array[i];
            array[i] = array[minIndex];
            array[minIndex] = temp;
        }
    }
    
    
    
	public static void SeparateSections()
	{
		//Separation for clarity
		Console.WriteLine();
		Console.WriteLine("-----------------------");
		Console.WriteLine();
	}
}