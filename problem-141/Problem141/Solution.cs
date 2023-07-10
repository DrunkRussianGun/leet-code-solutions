namespace Problem141;

public class Solution
{
	public bool HasCycle(ListNode? head)
	{
		if (head?.next is null)
			return false;

		var slowPointer = head.next;
		var fastPointer = slowPointer?.next;
		while (slowPointer != fastPointer)
		{
			if (fastPointer is null)
				return false;

			#pragma warning disable CS8602
			slowPointer = slowPointer.next;
			#pragma warning restore CS8602
			fastPointer = fastPointer.next?.next;
		}

		return true;
	}
}
