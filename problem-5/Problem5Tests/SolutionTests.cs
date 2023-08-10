using FluentAssertions;
using Problem5;

namespace Problem5Tests;

[Parallelizable(ParallelScope.All)]
public class SolutionTests
{
	private readonly Solution solution = new();
	
	[TestCase("a", new[] { "a" })]
	[TestCase("aa", new[] { "aa" })]
	[TestCase("ab", new[] { "a", "b" })]
	[TestCase("aba", new[] { "aba" })]
	[TestCase("babad", new[] { "bab", "aba" })]
	[TestCase("cbbd", new[] { "bb" })]
	[TestCase("bbbbbbbbbbbb", new[] { "bbbbbbbbbbbb" })]
	[TestCase("aaaabbbbbbbbbbbbcc", new[] { "bbbbbbbbbbbb" })]
	[TestCase("afabbbbbbbbbbbbaf", new[] { "fabbbbbbbbbbbbaf" })]
	public void FindsLongestPalindrome(string input, string[] acceptable)
	{
		var actual = solution.LongestPalindrome(input);

		actual.Should().BeOneOf(acceptable);
	}
}
