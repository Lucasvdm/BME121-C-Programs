// -------------------------------------------------------------------
// Biomedical Engineering Program
// Department of Systems Design Engineering
// University of Waterloo
//
// Student Name:     Lucas Van de Mosselaer
// Userid:           levandem
//
// Assignment:       Programming Assignment 3
// Submission Date:  November 9, 2015
// 
// I declare that, other than the acknowledgements listed below, 
// this program is my original work.
//
// Acknowledgements:
// Starting code, stub methods, some fully functional methods (ReadArrayFromFastaFile, ReadListFromFastaFile, Main)
// and some comments were provided in pa3Start.cs
// -------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

// Represent one protein from the Human Metabolome Database (www.hmdb.ca).
class Protein
{
	//Fields to hold the protein's ID, name, and sequence of amino acids
    public string proteinID;
	public string proteinName;
	public string proteinSequence;
    
    // Construct the protein from FASTA information.
    public Protein( string proteinHeader, string proteinSequence )
    {
		this.proteinSequence = proteinSequence;
		
		//Split the header into sections based on the spaces
        string[] splitHeader = proteinHeader.Split(new char[]{' '});
		
		//Remove the '>' from the start of the ID and assign to the proteinID field
		splitHeader[0] = splitHeader[0].Substring(1, splitHeader[0].Length - 1);
		proteinID = splitHeader[0];
		
		//Concatenate remaining segments into the protein name while replacing the spaces
		proteinName = "";
		for(int i = 1; i < splitHeader.Length; i++)
		{
			if(i == splitHeader.Length - 1)
				proteinName += splitHeader[i];
			else
				proteinName += splitHeader[i] + " ";
		}
		
    }
    
	//Returns an int array of indices where the target subsequence can be found in the protein sequence
    public int[ ] FindAllIndicesOf( string subsequence )
    {
        if( string.IsNullOrEmpty( subsequence ) ) return new int[ 0 ];
        
        List< int > indices = new List< int >( );
        
		//Starting at 0, search through the remainder of the sequence and report the first index at which the
		//subsequence is found; add that to the List of indices, then jump to the next index and start again from there.
		//If the subsequence is not found in any given search, return the array equivalent of the indices List.
		string searchSegment;
        int startingIndex = 0;
		int foundIndex = 0;
		while(foundIndex != -1 && startingIndex < proteinSequence.Length)
		{
			searchSegment = proteinSequence.Substring(startingIndex, proteinSequence.Length - startingIndex);
			foundIndex = searchSegment.IndexOf(subsequence);
			if(foundIndex != -1)
			{
				indices.Add(foundIndex + startingIndex);
				startingIndex = startingIndex + foundIndex + 1;
			}
		}
        
        return indices.ToArray( );
    }
    
    // Get a string representation of the protein in FASTA format.
    public string FastaFormat
    {
        get
        {
			//Format header line by concatenating the header symbol, protein ID followed by a space, protein name, and a new line character
            string formattedString = string.Format(">{0} {1}{2}", proteinID, proteinName, Environment.NewLine);
			
			//If the protein sequence can fit on one line, concatenate it into the formatted string;
			//otherwise, cut the sequence into 80-character segments (if possible) until it is finished
			if(proteinSequence.Length <= 80)
				formattedString += proteinSequence;
			else
			{
				for(int i = 0; i < proteinSequence.Length; i += 80)
				{
					//If the remainder of the protein sequence can fit on the line, concatenate the remainder into the formatted string;
					//otherwise, do the same with the next 80 characters of the sequence as well as a new line character
					if(proteinSequence.Length <= i + 80)
						formattedString += proteinSequence.Substring(i, proteinSequence.Length - i);
					else
					{
						formattedString += proteinSequence.Substring(i, 80) + Environment.NewLine;
					}
				}
			}
            return formattedString;
        }
    }
    
    // Return an array of proteins read from a FASTA file.
    public static Protein[ ] ReadArrayFromFastaFile( string fileName ) 
        { return ReadListFromFastaFile( fileName ).ToArray( ); }
    
    // Return a list of proteins read from a FASTA file.
    public static List< Protein > ReadListFromFastaFile( string fileName )
    {
        List< Protein > proteins = new List< Protein >( );
        
        using( StreamReader sr = new StreamReader( fileName ) )
        {
            string line = sr.ReadLine( );
            while( line != null )
            {
                // Gather the protein header information.
                string proteinHeader = line;
                line = sr.ReadLine( );
                
                // Gather the protein amino-acid sequence information.
                string proteinSequence = null;
                while( line != null && ! line.StartsWith( ">" ) )
                {
                    proteinSequence += line;
                    line = sr.ReadLine( );
                }
                
                // Add a protein object to the list of proteins.
                proteins.Add( new Protein( proteinHeader, proteinSequence ) );
            }
        }
        
        return proteins;
    }
    
    // Write an enumerable collection of proteins into a file in FASTA format.
    public static void WriteToFastaFile( IEnumerable< Protein > proteins, string fileName )
    {
		//Check if a file already exists with the given file name; if so, throw an exception
        if( File.Exists( fileName ) )
        {
            string message = string.Format( "The file '{0}' already exists.", fileName );
            string parameter = "fileName";
            throw new ArgumentException( message, parameter );
        } 
        
		//For each protein in the proteins collection, write its FastaFormat to the file
		using(StreamWriter sw = new StreamWriter(fileName))
		{
			foreach(Protein protein in proteins)
			{
				sw.WriteLine(protein.FastaFormat);
			}
		}
    }
}

// Perform a few simple tests on the Protein class.
static class Program
{
    static string inputFile = "protein.fasta";
    static string outputFile = "testOut.fasta";
    
    static void Main( )
    {
        // Test reading from a FASTA file.
        Protein[ ] proteins = Protein.ReadArrayFromFastaFile( inputFile );
        
        Console.WriteLine( );
        Console.WriteLine( "Read {0} proteins from the file '{1}'.", proteins.Length, inputFile );
        
        // Test writing to a FASTA file.
        Protein.WriteToFastaFile( proteins, outputFile );
        
        Console.WriteLine( );
        Console.WriteLine( "Wrote {0} proteins to the file '{1}'.", proteins.Length, outputFile );
        
        // Test finding the indices of a target subsequence.
        foreach( int proteinIndex in new int[ ]{ 2000, 3600, 4000 } )
        {
            string target = "TEE";
            
            Console.WriteLine( );
            Console.WriteLine( proteins[ proteinIndex ].FastaFormat );
            
            int[ ] targetIndices = proteins[ proteinIndex ].FindAllIndicesOf( target );
            if( targetIndices.Length == 0 ) 
            {
                Console.WriteLine( "{0} not found", target );
            }
            else 
            {
                foreach( int index in targetIndices ) 
                {
                    Console.WriteLine( "{0} found starting at index {1}", target, index );
                }
            }
        }
    }
}