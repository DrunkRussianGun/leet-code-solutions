using FluentAssertions;
using Problem704;

namespace Problem704Tests;

[Parallelizable(ParallelScope.All)]
public class SolutionTests
{
	private readonly Solution solution = new();

	[TestCase(new[] { 1 }, 0, -1)]
	[TestCase(new[] { 1 }, 1, 0)]
	[TestCase(new[] { 1, 2, 3, 4, 5 }, 0, -1)]
	[TestCase(new[] { 1, 2, 3, 4, 5 }, 3, 2)]
	[TestCase(new[] { 2, 5, 10, 15 }, 2, 0)]
	[TestCase(new[] { 2, 5, 10, 15 }, 15, 3)]
	public void SearchesCorrectly(int[] values, int target, int expected)
	{
		var actual = solution.Search(values, target);

		actual.Should().Be(expected);
	}
}
