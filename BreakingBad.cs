using System;

class MyGenericNode<Type>
{
	Type data; // The data stored in the node
	MyGenericNode<Type> next; // Link to next node

	// Empty constructor. This just ensures that the value of next is unassigned
	public MyGenericNode ()
	{
		next = null;
	}

	// This constructor assigns a value to data. But makes next unassigned
	public MyGenericNode (Type data)
	{
		this.data = data;
		next = null;
	}

	// This constructor assigns data value and next to the node
	public MyGenericNode (Type data, MyGenericNode<Type> next)
	{
		this.data = data;
		this.next = next;
	}

	// setter/getter for data
	public Type Data
	{
		set { data = value; }
		get { return data; }
	}

	// setter/getter for next
	public MyGenericNode<Type> Next
	{
		set {next = value;}
		get {return next;}
	}

	// for converting the value of data to string
	public override string ToString ()
	{
		return string.Format ("{0}", data);
	}
}

class MyGenericLinkedList<Type>
{
	// The head node of the linked list
	MyGenericNode<Type> head;

	// size of the linked list and its read-only getter
	int size;
	public int Size
	{
		get { return size; }
	}

	// Constructor which assums an empty linked list
	// and assigns values to head and size accordingly
	public MyGenericLinkedList()
	{
		head = null;
		size = 0;
	}

	// insert a node containing the value assigned to data
	// to the head of the linked list
	public void Insert(Type data)
	{
		// Create a new node containing data which points to the current head 
		MyGenericNode<Type> newHead = new MyGenericNode<Type> (data, head);
		// update the head to the newly inserted node
		head = newHead;
		// increment size
		size++;
	}

	// insert a node containing the data value to the desired
	// location in the linked list
	public void Insert(Type data, int idx)
	{
		// idx cannot be greater than size
		if (idx > size) {
			throw new Exception ("the value of 'idx' cannot be greater than" +
				" 'size'");
		}
		// if idx is 0, it is the same as Insert(data)
		if (idx == 0) { // new node is the new head
			Insert(data);
		} else { // ( 0 < idx <= size) new node is not the new head
			// this node will go through nodes from head
			// to the node at index 'idx - 1'
			// This node is important because it points to the new inserted node
			MyGenericNode<Type> before = head;
			for (int i = 0; i < (idx - 1); i++) {
				before = before.Next;
			}
			// This node points to the node after the before node
			// It is important as the newly inserted node must point to it.
			MyGenericNode<Type> after = before.Next;
			// Creates the new node, links it 'after', 
			// and links 'before' to the newly created node
			before.Next = new MyGenericNode<Type> (data,after);
			// increment size
			size++;
		}
	}

	// Delete the head node
	public void Delete ()
	{
		// Cannot delete anything from something already empty!
		if (head == null) {
			throw new Exception ("Cannot delete anything from an empty Linked" +
				" List!");
		}
		// move head to the next node
		head = head.Next;
		// decrement size
		size--;
	}

	// Delete the node in the desired location
	public void Delete (int idx)
	{
		// Checks whether 'idx' is in range
		if (idx >= size) {
			throw new Exception ("the value of 'idx' cannot be greater than" +
				" 'size'");
		}
		// if 'idx' is 0, then it is similar to Delete()
		if (idx == 0) { // deleted node is the head
			Delete ();
		} else { // deleted node is not the head
			// The operation of deletion is linking the node before the
			// deleted node to the node after the deleted node
			// This node should find the node before the deleted node
			MyGenericNode<Type> before = head;
			// Go through node until 'before' is reached
			for (int i = 0; i < (idx - 1); i++) {
				before = before.Next;
			}
			// link the node before deleted node to the node
			// after the deleted node
			before.Next = before.Next.Next;
			// decrement size
			size--;
		}
	}

	// Retrieve data stored in the desired location
	public Type Retrieve (int idx)
	{
		if (idx >= size) {
			throw new Exception ("'idx' out of range");
		}
		MyGenericNode<Type> iterator = head;
		for (int i = 0; i < idx; i++) {
			iterator = iterator.Next;
		}
		return iterator.Data;
	}

	public int Find (Type data)
	{
		MyGenericNode<Type> iterator = head;
		int counter = 0;
		while(!(iterator.Next == null))
		{
			if(iterator.Data.Equals(data)) return counter;
			iterator = iterator.Next;
			counter++;
		}
		if(iterator.Data.Equals(data)) return counter;
		return -1;
	}

	public bool DeleteAt (Type data)
	{
		int index = Find(data);
		if(index != -1)
		{
			Delete(index);
			return true;
		}
		return false;
	}

	public bool InsertAfter (Type findData, Type data)
	{
		int index = Find(findData);
		if(index != -1)
		{
			Insert(data, index + 1);
			return true;
		}
		return false;
	}

	public bool InsertBefore (Type findData, Type data)
	{
		int index = Find(findData);
		if(index != -1)
		{
			Insert(data, index);
			return true;
		}
		return false;
	}

	// print a  -> "-separated list of the values to string
	public override string ToString ()
	{
		// the case with empty linked list
		if (head == null) {
			return "<Empty Linked List>";
		}
		// Print values head-to-tail
		// add head first
		string returnValue = string.Format("{{{0}", head);
		MyGenericNode<Type> iterator = head.Next;
		while (iterator != null) {
			// and next value to string
			returnValue += string.Format (" -> {0}", iterator);
			iterator = iterator.Next;
		}
		returnValue += "}";
		return returnValue;
	}
}

class Program
{
	static void Main ()
	{
		// Create object
		MyGenericLinkedList<string> methRelations =
			new MyGenericLinkedList<string>();
		Console.WriteLine ("Who gave meth to whom in Breaking Bad?" +
			" SPOILERS ALERT!!");
		Console.WriteLine ("----------------------------------------" +
			"---------------------------------------");
		methRelations.Insert ("Methheads");
		methRelations.Insert ("Jesse");
		methRelations.Insert ("Walter");
		Console.WriteLine ("First, the business was small so: {0}",
			methRelations);
		Console.WriteLine ("----------------------------------------" +
			"---------------------------------------");
		
		
		// PART1: for this part, complete TO DO 1.
		// Walter and Jesse are going to be merged
		methRelations.Insert ("Walter & Jesse");
		methRelations.Delete (methRelations.Find("Walter"));
		methRelations.Delete (methRelations.Find("Jesse"));
		// Tuco started selling
		// Tuco should go after Jesse
		methRelations.Insert ("Tuco",methRelations.Find("Walter & Jesse") + 1);
		// Tuco's gang should be after
		methRelations.Insert ("Tuco's gang", methRelations.Find("Tuco") + 1);
		Console.WriteLine ("Then Tuco started selling the blue meth and " +
			"Jesse learned to help in the cook: {0}", methRelations);
		Console.WriteLine ("----------------------------------------" +
			"---------------------------------------");
		

		
		// PART2: for this part, complete TO DO 2.
		// Tuco died
		methRelations.DeleteAt ("Tuco");
		methRelations.DeleteAt ("Tuco's gang");
		// Walt and Jesse started distributin through Jesse's buddies
		methRelations.Insert("Jesse's friends",
			methRelations.Find("Walter & Jesse") + 1);
		Console.WriteLine ("They started their own distribution system " +
			"through Jesse's friends: {0}", methRelations);
		Console.WriteLine ("----------------------------------------" +
			"---------------------------------------");
		

		
		// PART3: for this part, finish TO DO 3:
		// Walt and Jesse Separated
		methRelations.DeleteAt ("Walter & Jesse");
		methRelations.DeleteAt ("Jesse's friends");
		// Walter and Gale started to work together
		methRelations.Insert ("Walter & Gale");
		methRelations.InsertAfter ("Walter & Gale","Gus");
		methRelations.InsertAfter ("Gus","Gus's gang");
		Console.WriteLine ("Walter and Jesse splitted up, Walter started " +
			"working for gus with the help of Gale in the laundry lab: {0}",
			methRelations);
		Console.WriteLine ("----------------------------------------" +
			"---------------------------------------");
		
		methRelations.DeleteAt ("Walter & Gale");
		methRelations.Insert ("Walter & Jesse");
		Console.WriteLine ("Gale died, Jesse and Walter parterned up again," +
			" {0}", methRelations);
		Console.WriteLine ("----------------------------------------" +
			"---------------------------------------");
		

		
		// Part 4: For this part, finish TO DO 4:
		// Gus died
		methRelations.DeleteAt ("Gus");
		methRelations.DeleteAt ("Gus's gang");
		methRelations.InsertBefore ("Methheads","Mike");
		methRelations.InsertBefore ("Methheads","Mike's gang");
		Console.WriteLine ("Guss died, Walter started the " +
			"distribution using Mike's people, {0}", methRelations);
		Console.WriteLine ("----------------------------------------" +
			"---------------------------------------");
		Console.WriteLine (" I dunno the rest");
		Console.WriteLine ("----------------------------------------" +
			"---------------------------------------");
		
	}
}
