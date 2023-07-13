namespace Problem206;

public class Solution
{
	public ListNode? ReverseList(ListNode? head)
	{
		if (head is null)
			return null;

		var previous = head;
		var current = head.next;
		head.next = null;

		while (current is not null)
		{
			var next = current.next;
			current.next = previous;

			previous = current;
			current = next;
		}

		return previous;
	}
}
