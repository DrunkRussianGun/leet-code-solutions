using FluentAssertions;
using Problem2;

namespace Problem2Tests;

[Parallelizable(ParallelScope.All)]
public class SolutionTests
{
	private readonly Solution solution = new();

	[TestCaseSource(nameof(CalculatesSumOfTwoNumbersTestCaseSource))]
	public void CalculatesSumOfTwoNumbers(ListNode left, ListNode right, ListNode expected)
	{
		var actual = solution.AddTwoNumbers(left, right);

		actual.Should().BeEquivalentTo(expected);
	}

	public static IEnumerable<TestCaseData> CalculatesSumOfTwoNumbersTestCaseSource()
	{
		yield return new TestCaseData(NewNode(0), NewNode(0), NewNode(0))
			.SetName("0 + 0 = 0");
		yield return new TestCaseData(NewNode(0), NewNode(1), NewNode(1))
			.SetName("0 + 1 = 1");
		yield return new TestCaseData(NewNode(1), NewNode(0), NewNode(1))
			.SetName("1 + 0 = 1");
		yield return new TestCaseData(NewNode(1), NewNode(1), NewNode(2))
			.SetName("1 + 1 = 2");
		yield return new TestCaseData(NewNode(1), NewNode(9), NewNode(0, NewNode(1)))
			.SetName("1 + 9 = 10");
		yield return new TestCaseData(NewNode(9), NewNode(1), NewNode(0, NewNode(1)))
			.SetName("9 + 1 = 10");
		yield return new TestCaseData(
				NewNode(5, NewNode(4, NewNode(3))),
				NewNode(1, NewNode(6, NewNode(4))),
				NewNode(6, NewNode(0, NewNode(8))))
			.SetName("345 + 461 = 806");
		yield return new TestCaseData(
				NewNode(2, NewNode(4, NewNode(3))),
				NewNode(5, NewNode(6, NewNode(4))),
				NewNode(7, NewNode(0, NewNode(8))))
			.SetName("342 + 465 = 807");
		yield return new TestCaseData(
				NewNode(9, NewNode(9, NewNode(9, NewNode(9, NewNode(9, NewNode(9, NewNode(9))))))),
				NewNode(9, NewNode(9, NewNode(9, NewNode(9)))),
				NewNode(8, NewNode(9, NewNode(9, NewNode(9, NewNode(0, NewNode(0, NewNode(0, NewNode(1)))))))))
			.SetName("9999999 + 9999 = 10009998");
	}

	private static ListNode NewNode(int value, ListNode? next = null) => new(value, next);
}
