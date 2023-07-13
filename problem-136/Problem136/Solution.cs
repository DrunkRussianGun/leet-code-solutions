namespace Problem136;

public class Solution
{
	public int SingleNumber(int[] numbers)
	{
		var xorSum = 0;
		foreach (var number in numbers)
			xorSum ^= number;
		return xorSum;
	}
}
