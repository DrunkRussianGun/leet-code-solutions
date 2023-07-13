using FluentAssertions;
using Problem23;

namespace Problem23Tests;

[Parallelizable(ParallelScope.All)]
public class SolutionTests
{
	private readonly Solution solution = new();

	[TestCaseSource(nameof(MergesCorrectlyTestCaseSource))]
	public void MergesCorrectly(ListNode?[]? input, ListNode? expected)
	{
		var actual = solution.MergeKLists(input);

		actual.Should().BeEquivalentTo(expected);
	}

	public static IEnumerable<TestCaseData> MergesCorrectlyTestCaseSource()
	{
		yield return new TestCaseData(null, null);
		yield return new TestCaseData(Array.Empty<ListNode>(), null);
		yield return new TestCaseData(new ListNode?[] { null }, null);
		yield return new TestCaseData(
			new[]
			{
				ToListNodes(1, 4, 5),
				ToListNodes(1, 3, 4),
				null,
				ToListNodes(2, 6)
			},
			ToListNodes(1, 1, 2, 3, 4, 4, 5, 6));
	}

	private static ListNode ToListNodes(params int[] values)
	{
		var topNode = new ListNode(values.First());

		var currentNode = topNode;
		foreach (var value in values.Skip(1))
		{
			var nextNode = new ListNode(value);
			currentNode.next = nextNode;
			currentNode = nextNode;
		}

		return topNode;
	}
}
