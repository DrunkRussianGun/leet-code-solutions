using System;

namespace Problem239;

public class Solution
{
	public int[] MaxSlidingWindow(int[] values, int windowSize)
	{
		var results = new int[values.Length - windowSize + 1];
		var currentMaximums = new IntDeque(windowSize);

		for (var i = 0; i < windowSize; ++i)
			Add(currentMaximums, values[i]);
		results[0] = currentMaximums.Last();

		for (var windowEndIndex = windowSize; windowEndIndex < values.Length; ++windowEndIndex)
		{
			var droppedElementIndex = windowEndIndex - windowSize;
			if (values[droppedElementIndex] == currentMaximums.Last())
				currentMaximums.RemoveLast();

			Add(currentMaximums, values[windowEndIndex]);
			results[windowEndIndex - windowSize + 1] = currentMaximums.Last();
		}

		return results;
	}

	private static void Add(IntDeque currentMaximums, int nextValue)
	{
		var nextValueIndex = currentMaximums.BinarySearch(nextValue);
		if (nextValueIndex < 0)
			nextValueIndex = ~nextValueIndex;

		if (nextValueIndex > 0)
			currentMaximums.RemoveFirst(nextValueIndex);
		currentMaximums.Prepend(nextValue);
	}
}

public class IntDeque
{
	private readonly int[] elements;

	private int firstElementIndex;
	private int size;

	public readonly int MaxSize;
	public int Size => size;

	public IntDeque(int maxSize)
	{
		elements = new int[maxSize];
		MaxSize = maxSize;
	}

	public int this[int index]
	{
		get
		{
			var innerIndex = IncreaseIndex(firstElementIndex, index);
			return index < size ? elements[innerIndex] : throw OutOfSize(index);
		}
		set
		{
			if (index >= size)
				throw OutOfSize(index);
			
			var innerIndex = IncreaseIndex(firstElementIndex, index);
			elements[innerIndex] = value;
		}
	}

	public int First() => size > 0 ? elements[firstElementIndex] : throw Empty();

	public int Last() => size > 0 ? elements[IncreaseIndex(firstElementIndex, size - 1)] : throw Empty();

	public int BinarySearch(int elementToSearch)
	{
		if (size <= 0)
			return 0;

		var leftBound = firstElementIndex;
		var rightBound = IncreaseIndex(firstElementIndex, size - 1);
		while (true)
		{
			var difference = GetDifference(leftBound, rightBound);
			if (difference <= 0)
				break;

			var currentElementIndex = IncreaseIndex(leftBound, difference / 2);
			var currentElement = elements[currentElementIndex];
			if (elementToSearch <= currentElement)
				rightBound = currentElementIndex;
			else
			{
				leftBound = currentElementIndex + 1;
				if (leftBound >= MaxSize)
					leftBound = 0;
			}
		}

		var outerIndex = DecreaseIndex(leftBound, firstElementIndex);
		var leftBoundElement = elements[leftBound];
		if (leftBoundElement < elementToSearch)
			return ~(outerIndex + 1);
		if (leftBoundElement > elementToSearch)
			return ~outerIndex;
		return outerIndex;
	}

	public void Append(int value)
	{
		if (size >= MaxSize)
			throw MaxSizeReached();

		elements[IncreaseIndex(firstElementIndex, size)] = value;
		++size;
	}

	public void Prepend(int value)
	{
		if (size >= MaxSize)
			throw MaxSizeReached();

		firstElementIndex = (firstElementIndex > 0 ? firstElementIndex : MaxSize) - 1;
		++size;
		elements[firstElementIndex] = value;
	}

	public void RemoveFirst() => RemoveFirst(1);

	public void RemoveFirst(int count)
	{
		if (size < count)
			throw InvalidElementsToRemoveCount(count);

		firstElementIndex = IncreaseIndex(firstElementIndex, count);
		size -= count;
	}

	public void RemoveLast() => RemoveLast(1);

	public void RemoveLast(int count)
	{
		if (size < count)
			throw InvalidElementsToRemoveCount(count);

		size -= count;
	}

	private int DecreaseIndex(int index, int decrement)
	{
		var decreasedIndex = index - decrement;
		if (decreasedIndex < 0)
			decreasedIndex += MaxSize;
		return decreasedIndex;
	}

	private int IncreaseIndex(int index, int increment)
	{
		var increasedIndex = index + increment;
		if (increasedIndex >= MaxSize)
			increasedIndex -= MaxSize;
		return increasedIndex;
	}

	private int GetDifference(int leftIndex, int rightIndex)
	{
		var length = rightIndex - leftIndex;
		return length >= 0 ? length : length + MaxSize;
	}

	private ArgumentException OutOfSize(int index) => new($"Index {index} is out of size {size}");

	private ArgumentException InvalidElementsToRemoveCount(int elementsToRemoveCount)
		=> new($"Can't remove {elementsToRemoveCount} elements from deque of size {size}");

	private InvalidOperationException MaxSizeReached() => new($"Max size of {MaxSize} is reached");

	private static InvalidOperationException Empty() => new($"Deque is empty");
}
