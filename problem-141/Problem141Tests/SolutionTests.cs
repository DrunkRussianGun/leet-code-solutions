using FluentAssertions;
using Problem141;

namespace Problem141Tests;

[Parallelizable(ParallelScope.All)]
public class SolutionTests
{
	private readonly Solution solution = new();

	[TestCaseSource(nameof(DeterminesCycleCorrectlyTestCaseSource))]
	public void DeterminesCycleCorrectly(ListNode? list, bool expected)
	{
		var actual = solution.HasCycle(list);

		actual.Should().Be(expected);
	}

	public static IEnumerable<TestCaseData> DeterminesCycleCorrectlyTestCaseSource()
	{
		yield return new TestCaseData(null, false);
		yield return new TestCaseData(NewNode(1), false)
			.SetName("Node without link to next one doesn't form a loop");
		yield return new TestCaseData(NewNode(1, NewNode(2, NewNode(3))), false)
			.SetName("Plain list doesn't form a loop");

		var loopNode = new ListNode(1);
		loopNode.next = loopNode;
		yield return new TestCaseData(loopNode, true)
			.SetName("Self-referencing node forms a loop");

		loopNode = NewNode(2);
		loopNode.next = NewNode(0, NewNode(-4, loopNode));
		yield return new TestCaseData(NewNode(3, loopNode), true)
			.SetName("List with a loop");

		loopNode = NewNode(1);
		loopNode.next = NewNode(2, loopNode);
		yield return new TestCaseData(loopNode, true)
			.SetName("Ring list forms a loop");
	}

	private static ListNode NewNode(int value, ListNode? next = null)
		=> new(value) { next = next };
}
