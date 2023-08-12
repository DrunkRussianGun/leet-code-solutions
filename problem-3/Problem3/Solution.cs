using System;
using System.Collections.Generic;

namespace Problem3;

public class Solution
{
	public int LengthOfLongestSubstring(string input)
	{
		var longestSubstringLength = 0;

		var seenChars = new HashSet<char>();
		var leftBound = 0;
		var nextCharIndex = 0;
		while (true)
		{
			char nextChar = default;
			while (nextCharIndex < input.Length)
			{
				nextChar = input[nextCharIndex];
				if (!seenChars.Add(nextChar))
					break;

				++nextCharIndex;
			}

			longestSubstringLength = Math.Max(longestSubstringLength, nextCharIndex - leftBound);
			if (nextCharIndex >= input.Length)
				break;

			char leftBoundChar;
			do
			{
				leftBoundChar = input[leftBound];
				seenChars.Remove(leftBoundChar);
				++leftBound;
			}
			while (leftBoundChar != nextChar); // nextChar must be initialized by this point
		}

		return longestSubstringLength;
	}
}
