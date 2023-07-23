namespace Problem704;

public class Solution
{
	public int Search(int[] values, int target)
	{
		var leftBound = 0;
		var rightBound = values.Length - 1;
		while (leftBound < rightBound)
		{
			var middleIndex = leftBound + (rightBound - leftBound) / 2;
			var middleValue = values[middleIndex];

			if (target < middleValue)
				rightBound = middleIndex - 1;
			else if (target > middleValue)
				leftBound = middleIndex + 1;
			else
				return middleIndex;
		}

		return values[leftBound] == target ? leftBound : -1;
	}
}
