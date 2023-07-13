using System;

namespace Problem239;

public class Solution
{
	public int[] MaxSlidingWindow(int[] values, int windowSize)
	{
		var maximums = new int[values.Length - windowSize + 1];
		for (var windowBeginIndex = 0; windowBeginIndex < maximums.Length; ++windowBeginIndex)
		{
			var maximum = values[windowBeginIndex];
			for (var i = 1; i < windowSize; ++i)
				maximum = Math.Max(maximum, values[windowBeginIndex + i]);
			maximums[windowBeginIndex] = maximum;
		}
		return maximums;
	}
}
