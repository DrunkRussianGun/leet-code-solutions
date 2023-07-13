using FluentAssertions;
using Problem206;

namespace Problem206Tests;

[Parallelizable(ParallelScope.All)]
public class SolutionTests
{
	private readonly Solution solution = new();

	[Test]
	public void GivenEmptyList_DoesNothing()
	{
		var actual = solution.ReverseList(null);

		actual.Should().BeNull();
	}

	[Test]
	public void GivenSingleNode_DoesNothing()
	{
		var singleNode = NewNode(1);
		
		var actual = solution.ReverseList(singleNode);

		actual.Should().BeSameAs(singleNode);
	}

	[TestCaseSource(nameof(ReversesLinkedListTestCaseSource))]
	public void ReversesLinkedList(ListNode? input, ListNode? expected)
	{
		var actual = solution.ReverseList(input);

		actual.Should().BeEquivalentTo(expected);
	}

	public static IEnumerable<TestCaseData> ReversesLinkedListTestCaseSource()
	{
		yield return new TestCaseData(
				NewNode(1, NewNode(2)),
				NewNode(2, NewNode(1)))
			.SetName("1, 2");
		yield return new TestCaseData(
				NewNode(1, NewNode(2, NewNode(3, NewNode(4, NewNode(5))))),
				NewNode(5, NewNode(4, NewNode(3, NewNode(2, NewNode(1))))))
			.SetName("1, 2, 3, 4, 5");
	}

	private static ListNode NewNode(int value, ListNode? next = null) => new(value, next);
}
