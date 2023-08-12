using FluentAssertions;
using Problem3;

namespace Problem3Tests;

[Parallelizable(ParallelScope.All)]
public class SolutionTests
{
	private readonly Solution solution = new();
	
	[TestCase("", 0)]
	[TestCase("a", 1)]
	[TestCase("abc", 3)]
	[TestCase("abcabcbb", 3)]
	[TestCase("bbbbb", 1)]
	[TestCase("pwwkew", 3)]
	public void FindsCorrectLength(string input, int expected)
	{
		var actual = solution.LengthOfLongestSubstring(input);

		actual.Should().Be(expected);
	}
}
