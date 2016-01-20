using System;
using System.IO;

// Test sorting on a linked list of Drugs.
static class Program
{
    static int CompareByTotalPaidDecreasing( Drug lhs, Drug rhs ) 
        { return - lhs.TotalPaid.CompareTo( rhs.TotalPaid ); }
    
    static int CompareByQuantityDecreasing( Drug lhs, Drug rhs ) 
        { return - lhs.Quantity.CompareTo( rhs.Quantity ); }
    
    static void Main( )
    {
        const string drugFileName = "RXQT1503.txt";
        
        // Read the drugs from the file into a linked list.
        DrugList drugsList = new DrugList( );
        using( StreamReader sr = new StreamReader( drugFileName ) )
            while( ! sr.EndOfStream ) drugsList.Append( Drug.Parse( sr.ReadLine( ) ) );
            
        // Sort the list by total amount paid for each drug.
        Console.WriteLine( );
        Console.WriteLine( "Top 10 drugs by total amount paid:" );
        drugsList.SelectSort( CompareByTotalPaidDecreasing );
        {
            int count = 0;
            foreach( Drug d in drugsList.Enumeration )
            {
                count ++;
                Console.WriteLine( "{0:d2}: {1,10:n0} - {2}", count, d.TotalPaid, d.Name );
                if( count == 10 ) break;
            }
        }
            
        // Sort the list by number of units dispensed for each drug.
        Console.WriteLine( );
        Console.WriteLine( "Top 10 drugs by number of units dispensed:" );
        drugsList.InsertSort( CompareByQuantityDecreasing );
        {
            int count = 0;
            foreach( Drug d in drugsList.Enumeration )
            {
                count ++;
                Console.WriteLine( "{0:d2}: {1,10:n0} - {2}", count, d.Quantity, d.Name );
                if( count == 10 ) break;
            }
        }
    }
}