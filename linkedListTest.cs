using System;

class LinkedList<TData>
{
	int count;
	Node tail;
	Node head;
	
	class Node
	{
		Node next;
		TData data;
		
		public Node(TData data)
		{
			this.next = null;
			this.data = data;
		}
		
		public Node Next
		{
			set
			{
				this.next = value;
			}
			get
			{
				return this.next;
			}
		}
		
		public TData Data
		{
			get
			{
				return this.data;
			}
		}
		
		public override string ToString()
		{
			return string.Format("data = {0}, next = {1}", data, next == null ? "[null]" : "[not null]");
		}
	}
	
	public LinkedList()
	{
		count = 0;
		tail = null;
		head = null;
	}
	
	public void Append(TData data)
	{
		Node newNode = new Node(data);
		if(head == null)
		{
			head = newNode;
			tail = newNode;
		}
		else
		{
			tail.Next = newNode;
			tail = newNode;
		}
		count++;
	}
	
	public string Display()
	{
		string result = null;
		Node currentNode = this.head;
		while(currentNode != null)
		{
			result += currentNode.Data + " ";
			currentNode = currentNode.Next;
		}
		result = result.Substring(0, result.Length - 1);
		return result;
	}
	
	public bool Contains<TTarget>(TTarget target) where TTarget : IComparable<TData>
	{
		Node currentNode = head;
		
		while(currentNode != null)
		{
			if(target.CompareTo(currentNode.Data) == 0) return true;
			currentNode = currentNode.Next;
		}
		
		return false;
	}
	
	public bool Contains(Func<TData, bool> IsTarget)
	{
		Node currentNode = head;
		
		while(currentNode != null)
		{
			if(IsTarget(currentNode.Data)) return true;
			currentNode = currentNode.Next;
		}
		
		return false;
	}
	
	public TData Find(Func<TData, bool> IsTarget)
	{
		Node currentNode = head;
		while(currentNode != null)
		{
			if(IsTarget(currentNode.Data)) return currentNode.Data;
			currentNode = currentNode.Next;
		}
		return default(currentNode.Data);
	}
	
	/*
	public bool Contains(TData target)
	{
		Node currentNode = head;
		
		while(currentNode != null)
		{
			if(((IComparable<TData>)target).CompareTo(currentNode.Data) == 0) return true;
			currentNode = currentNode.Next;
		}
		
		return false;
	}
	*/
	
	public TData[] ToArray()
	{
		TData[] result = new TData[count];
		Node currentNode = head;
		int currentIndex = 0;
		while(currentNode != null)
		{
			result[currentIndex] = currentNode.Data;
			currentNode = currentNode.Next;
			currentIndex++;
		}
		return result;
	}
	
	public override string ToString()
	{
		string tailDisplay;
		if(tail == null) tailDisplay = "[null]";
		else tailDisplay = tail.ToString();
		
		// head == null (test condition) ? "[null]" (result if condition is true) : head.ToString() (result if condition is false) 
		return string.Format( "count = {0}, tail = {1}, head = {2}", count, tailDisplay, head == null ? "[null]" : head.ToString() );
	}
}