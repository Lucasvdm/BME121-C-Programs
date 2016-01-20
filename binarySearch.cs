using System;

static class Program
{
	static void Main()
	{
		int[] a = {29, 28, 30, 234, 1000, 300000, 456, 70, 512};
		int index = BinarySearch(a, 234);
		Console.WriteLine(index);
	}
	
	static int BinarySearch(int[] a, int target)
	{
		int minIndex = 0;
		int maxIndex = a.Length - 1;
		
		Array.Sort(a);
		
		while(minIndex <= maxIndex)
		{
			int midIndex = (maxIndex + minIndex) / 2;
			if(target == a[midIndex]) return midIndex;
			else if(target < a[midIndex])
			{
				maxIndex = midIndex - 1;
			}
			else
			{
				minIndex = midIndex + 1;
			}
		}
		return -1;
	}
}