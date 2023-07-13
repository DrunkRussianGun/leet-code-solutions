using FluentAssertions;
using Problem239;

namespace Problem239Tests;

[Parallelizable(ParallelScope.All)]
public class SolutionTests
{
	private readonly Solution solution = new();

	[TestCase(new[] { 1 }, 1, new[] { 1 })]
	[TestCase(
		new[] { 1, 3, -1, -3, 5, 3, 6, 7 },
		3,
		new[] { 3, 3, 5, 5, 6, 7 })]
	[TestCase(
		new[] { 3, -2, 1, -1, -3, 5, 6, 0, 3, 2, -8 },
		4,
		new[] { 3, 1, 5, 6, 6, 6, 6, 3 })]
	public void CalculatesSlidingMaximum(int[] values, int windowSize, int[] expected)
	{
		var actual = solution.MaxSlidingWindow(values, windowSize);

		actual.Should().BeEquivalentTo(expected);
	}
}
