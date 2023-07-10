namespace Problem23;

public class Solution
{
	public ListNode? MergeKLists(ListNode?[]? lists)
	{
		var heap = CreateHeapFromTopNodes(lists);
		if (heap.Count <= 0)
			return null;

		var mergedList = PopAndPushNext(heap);
		var currentNode = mergedList;
		while (heap.Count > 0)
		{
			var nextNode = PopAndPushNext(heap);
			currentNode.next = nextNode;
			currentNode = nextNode;
		}

		return mergedList;
	}

	private static ListNode PopAndPushNext(Heap<int, ListNode> heap)
	{
		var currentNode = heap.Peek().Value;
		var nextNode = currentNode.next;
		if (nextNode is not null)
			heap.PopAndPush(nextNode.val, nextNode);
		else
			heap.Pop();
		return currentNode;
	}

	private static Heap<int, ListNode> CreateHeapFromTopNodes(IEnumerable<ListNode?>? lists)
	{
		var heap = new Heap<int, ListNode>();
		if (lists is not null)
			foreach (var topNode in lists)
				if (topNode is not null)
					heap.Push(topNode.val, topNode);
		return heap;
	}
}

public class Heap<TKey, TValue>
{
	private const int rootIndex = 0;

	private readonly List<KeyValuePair<TKey, TValue>> nodes = new();
	private readonly IComparer<TKey> comparer;

	public int Count => nodes.Count;

	public Heap()
	{
		comparer = Comparer<TKey>.Default;
	}

	public Heap(IComparer<TKey>? comparer)
	{
		this.comparer = comparer ?? Comparer<TKey>.Default;
	}

	public KeyValuePair<TKey, TValue> Peek()
	{
		if (nodes.Count <= 0)
			throw HeapIsEmptyException();

		return nodes[rootIndex];
	}

	public KeyValuePair<TKey, TValue> Pop()
	{
		if (nodes.Count <= 0)
			throw HeapIsEmptyException();

		var previousRoot = nodes[rootIndex];
		var lastNodeIndex = nodes.Count - 1;
		nodes[rootIndex] = nodes[lastNodeIndex];
		nodes.RemoveAt(lastNodeIndex);
		DownHeap(rootIndex);

		return previousRoot;
	}

	public void Push(TKey key, TValue value)
	{
		nodes.Add(NewNode(key, value));
		UpHeap(nodes.Count - 1);
	}

	public KeyValuePair<TKey, TValue> PopAndPush(TKey key, TValue value)
	{
		if (nodes.Count <= 0)
			throw HeapIsEmptyException();

		var previousRoot = nodes[rootIndex];
		nodes[rootIndex] = NewNode(key, value);
		if (comparer.Compare(previousRoot.Key, key) < 0)
			DownHeap(rootIndex);

		return previousRoot;
	}

	private void UpHeap(int nodeIndex)
	{
		var currentIndex = nodeIndex;
		while (currentIndex != rootIndex)
		{
			var parentIndex = GetParentIndex(currentIndex);
			if (comparer.Compare(nodes[parentIndex].Key, nodes[currentIndex].Key) <= 0)
				break;

			SwapNodes(parentIndex, currentIndex);
			currentIndex = parentIndex;
		}
	}

	private void DownHeap(int nodeIndex)
	{
		var currentIndex = nodeIndex;
		while (true)
		{
			var leftChildIndex = GetLeftChildIndex(currentIndex);
			if (leftChildIndex >= nodes.Count)
				break;
			var minNodeIndex = comparer.Compare(nodes[leftChildIndex].Key, nodes[currentIndex].Key) < 0
				? leftChildIndex
				: currentIndex;

			var rightChildIndex = GetRightChildIndex(currentIndex);
			if (rightChildIndex < nodes.Count
					&& comparer.Compare(nodes[rightChildIndex].Key, nodes[minNodeIndex].Key) < 0)
				minNodeIndex = rightChildIndex;

			if (minNodeIndex != currentIndex)
			{
				SwapNodes(minNodeIndex, currentIndex);
				currentIndex = minNodeIndex;
			}
			else
				break;
		}
	}

	private void SwapNodes(int firstIndex, int secondIndex)
	{
		// ReSharper disable once SwapViaDeconstruction
		var buffer = nodes[firstIndex];
		nodes[firstIndex] = nodes[secondIndex];
		nodes[secondIndex] = buffer;
	}
	
	private static KeyValuePair<TKey, TValue> NewNode(TKey key, TValue value)
		=> new(key, value);

	private static int GetParentIndex(int nodeIndex)
		=> (nodeIndex - 1) / 2;

	private static int GetLeftChildIndex(int nodeIndex)
		=> nodeIndex * 2 + 1;

	private static int GetRightChildIndex(int nodeIndex)
		=> nodeIndex * 2 + 2;

	private static InvalidOperationException HeapIsEmptyException() => new("Heap is empty");
}
