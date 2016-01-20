using System;
using System.Collections.Generic;

// A DrugList object holds one singly linked list of Node objects
// where each Node object holds one Drug object.  Fields in the DrugList
// object hold the head and tail Nodes and an integer count of the
// number of Nodes in the list.
partial class DrugList
{
    // Nodes are the objects which are linked together to form the list.
    // A node is constructed holding its Drug object. It is not possible to change 
    // which particular Drug object is held by a Node. For debugging, ToString will 
    // display a Node using the Drug object's 'id' field.
    class Node
    {
        Node next;
        Drug data;
        
        public Node( Drug data ) { next = null; this.data = data; }
        
        public Node Next{ get { return next; } set { next = value; } }
        public Drug Data{ get { return data; } }
        
        public override string ToString( )
        {
            return string.Format( "[ {0}, next->[{1}] ]", 
                data.Id, next == null ? "null" : next.data.Id );
        }
    }
    
    int count;
    Node tail;
    Node head;
    
    public int Count { get { return count; } }
    
    // A new DrugList is constructed empty.
    public DrugList( ) { tail = null; head = null; count = 0; }
    
    // Add a new Drug item to the end of this linked list.
    public void Append( Drug data ) { InsertNode( new Node( data ), tail, null ); }
    
    // Select sort
    // The way it's done here is that it creates an entirely new list (sorted) which it then begins to fill with the
    // nodes from the original list.
    public void SelectSort( Comparison< Drug > UsersDrugComparer )
    {
        //Create new "sorted" list
        DrugList sorted = new DrugList( );
        
        //While there are still nodes in the original list...
        while( this.count > 0 )
        {
            Node previous, minimum, removed;
            
            //Find the smallest node in the original list using the comparison method and return it and the one before it
            //NOTE: The comparison method in this case (found in pa4Test.cs) will actually sort it in descending order, but
            //this can be easily changed by removing the negative in the return statement in that method
            this.FindMinimalNode( out previous, out minimum, UsersDrugComparer );
            
            //Remove that node and store it in the variable "removed"
            removed = this.RemoveNode( previous, minimum );
            
            //Insert that node at the end of the sorted list
            sorted.InsertNode( removed, sorted.tail, null );
        }
        
        this.count = sorted.count;
        this.tail = sorted.tail;
        this.head = sorted.head;
    }
    
    // Insert sort
    // Similar to the selection sort above, this creates an entirely new list and fills it with the nodes from the original list
    public void InsertSort( Comparison< Drug > UsersDrugComparer )
    {
        //Create new "sorted" list
        DrugList sorted = new DrugList( );
        
        //While there are still nodes in the original list...
        while( this.count > 0 )
        {
            Node previous, current, removed;
            
            //Remove the head node from the original list and store it in the variable "removed"
            removed = this.RemoveNode( null, this.head );
            
            //Find the first node larger than "removed" in the sorted list using the comparison method and return it and the one before it
            sorted.FindFirstLargerNode( removed, out previous, out current, UsersDrugComparer );
            
            //Insert "removed" in the correct position in the sorted list
            sorted.InsertNode( removed, previous, current );
        }
        
        this.count = sorted.count;
        this.tail = sorted.tail;
        this.head = sorted.head;
    }
    
    // Sort using Array.Sort
    public void ArraySort( Comparison< Drug > UsersDrugComparer )
    {
        Drug[ ] temp = ToArray( );
        
        Array.Sort( temp, UsersDrugComparer );
        
        count = 0;
        tail = null;
        head = null;
        
        foreach( Drug d in temp ) Append( d );
    }
    
    // Return, as an array, references to all the Drug objects on the list.
    public Drug[ ] ToArray( )
    {
        Drug[ ] result = new Drug[ count ];
        
        int nextIndex = 0;
        Node current = head;
        while( current != null )
        {
            result[ nextIndex ] = current.Data;
            nextIndex ++;
            current = current.Next;
        }
        
        return result;
    }
    
    // Return, as an enumeration, references to all the Drug objects on the list.
    // The 'yield return' statement allows this property to return just one Drug at a time,
    // as they are consumed by a foreach statement. It's not part of the course but you
    // can read about it under the general topic of Linq (language-integrated query).
    // The basic idea is that the property pauses after the yield return then when the next 
    // data item is requested, it starts up again from that point.
    public IEnumerable< Drug > Enumeration
    {
        get
        {
            Node current = head;
            while( current != null )
            {
                yield return current.Data;
                current = current.Next;
            }
        }
    }
    
    // For debugging, ToString will show the count, head, and tail of the linked list.
    public override string ToString( )
    {
        return string.Format( "{{ {0}: head->{1}, tail->{2} }}", count, 
            head == null ? "[null]" : head.ToString( ), tail == null ? "[null]" : tail.ToString( ) );
    }
}