namespace Problem2;

public class Solution
{
	public ListNode AddTwoNumbers(ListNode leftNumber, ListNode rightNumber)
	{
		var leftDigit = leftNumber;
		var rightDigit = rightNumber;
		var resultNumber = new ListNode(); // Fake node to avoid additional if's in the cycle
		var resultDigit = resultNumber;
		var carryFlag = 0;
		while (leftDigit is not null || rightDigit is not null)
		{
			var sum = GetDigitSafely(leftDigit) + GetDigitSafely(rightDigit) + carryFlag;
			if (sum > 9)
			{
				sum -= 10;
				carryFlag = 1;
			}
			else
				carryFlag = 0;

			var nextResultDigit = new ListNode(sum);
			resultDigit.next = nextResultDigit;

			resultDigit = nextResultDigit;
			leftDigit = leftDigit?.next;
			rightDigit = rightDigit?.next;
		}

		if (carryFlag > 0)
			resultDigit.next = new ListNode(carryFlag);

		return SkipNode(resultNumber); // Skip fake node
	}

	private static int GetDigitSafely(ListNode? node) => node?.val ?? 0;

	private static ListNode SkipNode(ListNode node)
	{
		var nextNode = node.next!;
		node.next = null;
		return nextNode;
	}
}
