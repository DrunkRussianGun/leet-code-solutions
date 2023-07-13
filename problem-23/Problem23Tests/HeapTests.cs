using FluentAssertions;
using Problem23;

namespace Problem23Tests;

[Parallelizable(ParallelScope.All)]
public class HeapTests
{
	public class WhenEmpty : HeapTests
	{
		[Test]
		public void NoNodesAvailable()
		{
			var heap = new Heap<int, object>();

			heap.Count.Should().Be(0);
			heap
				.Invoking(x => x.Peek())
				.Should().Throw<InvalidOperationException>();
			heap
				.Invoking(x => x.Pop())
				.Should().Throw<InvalidOperationException>();
			heap
				.Invoking(x => x.PopAndPush(0, new object()))
				.Should().Throw<InvalidOperationException>();
		}

		[Test]
		public void AfterPushAndPopStaysEmpty()
		{
			var heap = new Heap<int, object>();
			heap.Push(1, new object());
			heap.Pop();

			heap.Count.Should().Be(0);
			heap
				.Invoking(x => x.Pop())
				.Should().Throw<InvalidOperationException>();
		}
	}

	public class WhenMinHeapNonEmpty : HeapTests
	{
		[TestCaseSource(nameof(PopsMinKeyTestCaseSource))]
		public void PeeksMinKey(int[] keys, int expected)
		{
			var heap = CreateHeap(keys);

			var actual = heap.Peek().Key;

			actual.Should().Be(expected);
		}

		[TestCaseSource(nameof(PopsMinKeyTestCaseSource))]
		public void PopsMinKey(int[] keys, int expected)
		{
			var heap = CreateHeap(keys);

			var actual = heap.Pop().Key;

			actual.Should().Be(expected);
		}

		public static IEnumerable<TestCaseData> PopsMinKeyTestCaseSource()
		{
			yield return new TestCaseData(new[] { 3 }, 3);
			yield return new TestCaseData(new[] { 1, 2, 3 }, 1);
			yield return new TestCaseData(new[] { 6, 4, 2 }, 2);
			yield return new TestCaseData(new[] { 15, 7, 9 }, 7);
		}

		[TestCase(new[] { 3 }, 3, 4, 4)]
		[TestCase(new[] { 5, 8 }, 5, 7, 7)]
		[TestCase(new[] { 1, 2, 3 }, 1, 0, 0)]
		[TestCase(new[] { 1, 2, 3 }, 1, 5, 2)]
		[TestCase(new[] { 6, 4, 2 }, 2, 5, 4)]
		[TestCase(new[] { 15, 7, 9 }, 7, 2, 2)]
		public void AfterPopAndPushPopsMinKey(
			int[] keys,
			int expectedBeforePush,
			int keyToPush,
			int expectedAfterPush)
		{
			var heap = CreateHeap(keys);

			var actualBeforePush = heap.PopAndPush(keyToPush, null).Key;

			actualBeforePush.Should().Be(expectedBeforePush);

			var actualAfterPush = heap.Pop().Key;

			actualAfterPush.Should().Be(expectedAfterPush);
		}
	}

	public class WhenMaxHeapNonEmpty : HeapTests
	{
		[TestCaseSource(nameof(PopsMaxKeyTestCaseSource))]
		public void PeeksMaxKey(int[] keys, int expected)
		{
			var heap = CreateHeap(
				keys,
				Comparer<int>.Create((x, y) => y.CompareTo(x)));

			var actual = heap.Peek().Key;

			actual.Should().Be(expected);
		}

		[TestCaseSource(nameof(PopsMaxKeyTestCaseSource))]
		public void PopsMaxKey(int[] keys, int expected)
		{
			var heap = CreateHeap(
				keys,
				Comparer<int>.Create((x, y) => y.CompareTo(x)));

			var actual = heap.Pop().Key;

			actual.Should().Be(expected);
		}

		public static IEnumerable<TestCaseData> PopsMaxKeyTestCaseSource()
		{
			yield return new TestCaseData(new[] { 3 }, 3);
			yield return new TestCaseData(new[] { 1, 2, 3 }, 3);
			yield return new TestCaseData(new[] { 6, 4, 2 }, 6);
			yield return new TestCaseData(new[] { 7, 15, 9 }, 15);
		}

		[TestCase(new[] { 3 }, 3, 4, 4)]
		[TestCase(new[] { 5, 8 }, 8, 7, 7)]
		[TestCase(new[] { 1, 2, 3 }, 3, 0, 2)]
		[TestCase(new[] { 1, 2, 3 }, 3, 5, 5)]
		[TestCase(new[] { 6, 4, 2 }, 6, 5, 5)]
		[TestCase(new[] { 15, 7, 9 }, 15, 2, 9)]
		public void AfterPopAndPushPopsMaxKey(
			int[] keys,
			int expectedBeforePush,
			int keyToPush,
			int expectedAfterPush)
		{
			var heap = CreateHeap(
				keys,
				Comparer<int>.Create((x, y) => y.CompareTo(x)));

			var actualBeforePush = heap.PopAndPush(keyToPush, null).Key;

			actualBeforePush.Should().Be(expectedBeforePush);

			var actualAfterPush = heap.Pop().Key;

			actualAfterPush.Should().Be(expectedAfterPush);
		}
	}

	private static Heap<int, object?> CreateHeap(IEnumerable<int> keys, IComparer<int>? comparer = null)
	{
		var heap = new Heap<int, object?>(comparer);
		foreach (var key in keys)
			heap.Push(key, null);
		return heap;
	}
}
