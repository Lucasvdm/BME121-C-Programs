using System;

// -----------------------------------------------------------------------------
// A Drug object holds information about one fee-for-service outpatient drug 
// reimbursed by Medi-Cal (California's Medicaid program) to pharmacies.
class Drug
{
    string name;            // brand name, strength, dosage form
    string id;              // national drug code number
    double size;            // package size
    string unit;            // unit of measurement
    double quantity;        // number of units dispensed
    double lowest;          // price Medi-Cal is willing to pay
    double ingredientCost;  // estimated ingredient cost
    int    numTar;          // number of claims with a 'treatment authorization request'
    double totalPaid;       // total amount paid
    double averagePaid;     // average paid per prescription
    int    daysSupply;      // total days supply
    int    claimLines;      // total number of claim lines
    
    // Properties providing read-only access to every field.
    public string Name           { get { return name;           } }               
    public string Id             { get { return id;             } }                 
    public double Size           { get { return size;           } }             
    public string Unit           { get { return unit;           } }             
    public double Quantity       { get { return quantity;       } }         
    public double Lowest         { get { return lowest;         } }             
    public double IngredientCost { get { return ingredientCost; } }    
    public int    NumTar         { get { return numTar;         } }                
    public double TotalPaid      { get { return totalPaid;      } }          
    public double AveragePaid    { get { return averagePaid;    } }        
    public int    DaysSupply     { get { return daysSupply;     } }            
    public int    ClaimLines     { get { return claimLines;     } }            
    
    // Constructor which is passed a value for every field.
    public Drug ( string name, string id, double size, string unit, 
        double quantity, double lowest, double ingredientCost, int numTar, 
        double totalPaid, double averagePaid, int daysSupply, int claimLines )
    {
        this.name = name;
        this.id = id;
        this.size = size;
        this.unit = unit;
        this.quantity = quantity;
        this.lowest = lowest;
        this.ingredientCost = ingredientCost;
        this.numTar = numTar;
        this.totalPaid = totalPaid;
        this.averagePaid = averagePaid;
        this.daysSupply = daysSupply;
        this.claimLines = claimLines;
    }

    // Simple string for debugging purposes, showing only selected fields.
    public override string ToString( )
    { 
        return string.Format( 
            "{0}: {1}, {2}", id, name, size ); 
    }
    
    // Parse a string of the form used for each line in the file of drug data.
    // Mostly there are specific columns in the file for each piece of information.
    // The exception is that 'size' and 'unit' are concatenated.  They are collected
    // together and then separated by noting that 'unit' is always the last two characters.
    public static Drug Parse( string line )
    {
        if( line == null ) throw new ArgumentNullException( "String is null.", "line" );
        
        string name = line.Substring( 7, 30 ).Trim( );
        string id = line.Substring( 37, 13 ).Trim( );
        string temp = line.Substring( 50, 14 ).Trim( );
        double size = double.Parse( temp.Substring( 0 , temp.Length - 2 ) );
        string unit = temp.Substring( temp.Length - 2, 2 );
        double quantity = double.Parse( line.Substring( 64, 16 ) );
        double lowest = double.Parse( line.Substring( 80, 10 ) );
        double ingredientCost = double.Parse( line.Substring( 90, 12 ) );
        int numTar = int.Parse( line.Substring( 102, 8 ) );
        double totalPaid = double.Parse( line.Substring( 110, 14 ) );
        double averagePaid = double.Parse( line.Substring( 124, 10 ) );
        int daysSupply = ( int ) double.Parse( line.Substring( 134, 14 ) );  // large values are in 'e' format
        int claimLines = int.Parse( line.Substring( 148 ) );
        
        return new Drug( name, id, size, unit, quantity, lowest, ingredientCost, 
            numTar, totalPaid, averagePaid, daysSupply, claimLines );
    }
}