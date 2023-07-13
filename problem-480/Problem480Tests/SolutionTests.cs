using FluentAssertions;
using Problem480;

namespace Problem480Tests;

[Parallelizable(ParallelScope.All)]
public class SolutionTests
{
	private readonly Solution solution = new();

	[TestCase(
		new[] { 10 },
		1,
		new double[] { 10 })]
	[TestCase(
		new[] { 2, 3, 4 },
		3,
		new double[] { 3 })]
	[TestCase(
		new[] { 1, 2, 3, 4 },
		4,
		new[] { 2.5 })]
	[TestCase(
		new[] { 1, 3, -1, -3, 5, 3, 6, 7 },
		5,
		new double[] { 1, -1, -1, 3, 5, 6 })]
	[TestCase(
		new[] { 1, 2, 3, 4, 2, 3, 1, 4, 2 },
		3,
		new double[] { 2, 3, 3, 3, 2, 3, 2 })]
	public void CalculatesSlidingMedian(int[] values, int windowSize, double[] expected)
	{
		var actual = solution.MedianSlidingWindow(values, windowSize);

		actual.Should().BeEquivalentTo(
			expected,
			o => o
				.Using<double>(x => x.Subject.Should().BeApproximately(x.Expectation, 1e-5))
				.WhenTypeIs<double>());
	}
}
