using FluentAssertions;
using Problem136;

namespace Problem136Tests;

[Parallelizable(ParallelScope.All)]
public class SolutionTests
{
	private readonly Solution solution = new();

	[TestCase(new[] { 1 }, 1)]
	[TestCase(new[] { 2, 2, 1 }, 1)]
	[TestCase(new[] { 4, 1, 2, 4, 1 }, 2)]
	[TestCase(new[] { 4, 1, 2, 1, 2 }, 4)]
	public void FindsNonDuplicate(int[] numbers, int expected)
	{
		var actual = solution.SingleNumber(numbers);

		actual.Should().Be(expected);
	}
}
