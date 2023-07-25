using FluentAssertions;
using Problem1;

namespace Problem1Tests;

[Parallelizable(ParallelScope.All)]
public class SolutionTests
{
	private readonly Solution solution = new();

	[TestCase(new[] { 0, 1 }, 1, new[] { 0, 1 })]
	[TestCase(new[] { 2, 7, 11, 15 }, 9, new[] { 0, 1 })]
	[TestCase(new[] { 3, 2, 4 }, 6, new[] { 1, 2 })]
	[TestCase(new[] { 3, 3 }, 6, new[] { 0, 1 })]
	[TestCase(new[] { 3, 4, 3, 4 }, 6, new[] { 0, 2 })]
	[TestCase(new[] { 4, 3, 4, 3 }, 6, new[] { 1, 3 })]
	public void FindsNumbersCorrectly(int[] numbers, int target, int[] expected)
	{
		var actual = solution.TwoSum(numbers, target);

		actual.Should().BeEquivalentTo(expected);
	}
}
