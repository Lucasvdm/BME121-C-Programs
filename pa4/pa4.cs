// -------------------------------------------------------------------
// Biomedical Engineering Program
// Department of Systems Design Engineering
// University of Waterloo
//
// Student Name:     Lucas Van de Mosselaer
// Userid:           levandem
//
// Assignment:       Programming Assignment 4
// Submission Date:  December 1, 2015
// 
// I declare that, other than the acknowledgements listed below, 
// this program is my original work.
//
// Acknowledgements:
// Drug.cs, DrugList.cs, and various files for running the program (test.cs, test2.cs, etc.)
// were provided as part of the assignment.  RemoveNode, several stub methods, and some comments were provided
// in an incomplete pa4.cs
// -------------------------------------------------------------------

using System;

// This part of the DrugList class definition just holds the four
// private routines which manipulate nodes to do insert, remove, and find
// in support of select sort and insert sort.
partial class DrugList
{
    // Insert node 'target' between nodes 'previous' and 'current'.
    void InsertNode( Node target, Node previous, Node current )
    {
        //If the target node is null, return without changing anything in the list
        if(target == null) return;
        
        //If the node that comes before the target is null, the node is being inserted
        //at the head of the list.  If the tail of the list is null, the list has no nodes and the target
        //becomes both the head and the tail, with no next node.  If there is a tail, set the target's next node
        //to the current head and then set the head to the target.
        else if(previous == null)
        {
            if(current == null)
            {
                head = target;
                tail = target;
                target.Next = null;
            }
            else
            {
                target.Next = head;
                head = target;
            }
        }
        
        //If the node that comes after the target is null, the node is being inserted at
        //the tail of the list; therefore, set tail to the target.
        else if(current == null)
        {
            previous.Next = target;
            target.Next = null;
            tail = target;
        }
        
        //If none of the special cases apply, insert the node in the list by linking
        //the target to the current node and linking the previous node to the target.
        else
        {
            target.Next = current;
            previous.Next = target;
        }
        count ++;
        return;
    }
    
    // Remove (and return) node 'current'.
    Node RemoveNode( Node previous, Node current )
    {
        if( current == null ) return null; // nothing to remove

        if( current == head ) // remove at head
        {
            head = head.Next;
            if( head == null ) tail = null; // removed only element
        }
        else if( current == tail ) // remove at tail
        {
            previous.Next = null;
            tail = previous;
        }
        else // remove in middle
        {
            previous.Next = current.Next;
        }
        
        count --;
        
        current.Next = null; // isolate the returned node
        return current;
    }
    
    // Find the minimal node using UsersDrugComparer to compare Drugs held in nodes.
    void FindMinimalNode( out Node previous, out Node minimum, Comparison< Drug > UsersDrugComparer )
    {
        //Start at the head of the list - if no smaller node is found, the previous node will be null
        //because there is no node before the head
        minimum = head;
        previous = null;
        
        //Loop through the list - if any node is found to be smaller than the current minimum, make that node the new minimum
        Node index = head;
        while(index.Next != null)
        {
            int comparison = UsersDrugComparer(minimum.Data, index.Next.Data);
            if(comparison >= 0)
            {
                minimum = index.Next;
                previous = index;
            }
            index = index.Next;
        }
    }
    
    // Find the first node larger than 'target' using UsersDrugComparer to compare Drugs held in nodes.
    void FindFirstLargerNode( Node target, out Node previous, out Node current, Comparison< Drug > UsersDrugComparer )
    {   
        //Start at the head of the list - if the first larger node is at the head, the previous node will be null
        //because there is no node before the head
        current = head;
        previous = null;
        
        //If the head is null, the list has no nodes - return null for both current and previous.
        //If the target is null, return after setting current to head and previous to null - the head
        //node is considered to be the first larger node
        if(head == null || target == null) return;
        
        //If the head node is greater than the target node, immediately return with current set to head
        //and previous set to null
        if(UsersDrugComparer(target.Data, head.Data) <= 0) return;
        
        //Loop through the list - if any node is found to be larger than the target, take that node as the first larger node
        //and break out of the loop so that it stops searching
        bool foundLarger = false;
        Node index = head;
        while(index.Next != null)
        {
            int comparison = UsersDrugComparer(target.Data, index.Next.Data);
            if(comparison <= 0)
            {
                current = index.Next;
                previous = index;
                foundLarger = true;
                break;
            }
            index = index.Next;
        }
        
        //If the loop completed without finding a larger node, set current to null and previous to the tail so that
        //the target is appended to the end of the list
        if(foundLarger == false)
        {
            current = null;
            previous = tail;
        }
    }
}