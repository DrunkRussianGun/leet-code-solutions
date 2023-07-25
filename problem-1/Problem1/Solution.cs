using System;
using System.Collections.Generic;

namespace Problem1;

public class Solution
{
	public int[] TwoSum(int[] numbers, int target)
	{
		var possibleOperandIndexes = new Dictionary<int, int>();
		for (var secondIndex = 0; secondIndex < numbers.Length; ++secondIndex)
		{
			var second = numbers[secondIndex];
			var first = target - second;
			if (possibleOperandIndexes.TryGetValue(first, out var firstIndex))
				return new[] { firstIndex, secondIndex };

			possibleOperandIndexes.TryAdd(second, secondIndex);
		}

		return Array.Empty<int>();
	}
}
