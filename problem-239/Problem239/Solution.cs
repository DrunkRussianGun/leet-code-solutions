using System;
using System.Collections.Generic;

namespace Problem239;

public class Solution
{
	public int[] MaxSlidingWindow(int[] values, int windowSize)
	{
		var results = new int[values.Length - windowSize + 1];
		var currentMaximums = new Deque<int>(windowSize);

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

	private static void Add(Deque<int> currentMaximums, int nextValue)
	{
		var nextValueIndex = currentMaximums.BinarySearch(nextValue);
		if (nextValueIndex < 0)
			nextValueIndex = ~nextValueIndex;

		if (nextValueIndex > 0)
			currentMaximums.RemoveFirst(nextValueIndex);
		currentMaximums.Prepend(nextValue);
	}
}

public class Deque<T>
{
	private readonly T[] elements;

	private int firstElementIndex;
	private int lastElementIndex => IncreaseIndex(firstElementIndex, Size - 1);
	private int elementToAppendIndex => IncreaseIndex(firstElementIndex, Size);

	public int Size { get; private set; }
	public int MaxSize => elements.Length;

	public Deque(int maxSize)
	{
		elements = new T[maxSize];
	}

	public T this[int index]
	{
		get
		{
			var innerIndex = IncreaseIndex(firstElementIndex, index);
			return index < Size ? elements[innerIndex] : throw OutOfSize(index);
		}
		set
		{
			if (index >= Size)
				throw OutOfSize(index);
			
			var innerIndex = IncreaseIndex(firstElementIndex, index);
			elements[innerIndex] = value;
		}
	}

	public T First() => Size > 0 ? elements[firstElementIndex] : throw Empty();

	public T Last() => Size > 0 ? elements[lastElementIndex] : throw Empty();

	public int BinarySearch(T elementToSearch, Comparer<T>? comparer = null)
	{
		if (Size <= 0)
			return 0;

		comparer ??= Comparer<T>.Default;
		var leftBound = firstElementIndex;
		var rightBound = lastElementIndex;
		while (true)
		{
			var difference = GetDifference(leftBound, rightBound);
			if (difference <= 0)
				break;

			var currentElementIndex = IncreaseIndex(leftBound, difference / 2);
			var currentElement = elements[currentElementIndex];
			if (comparer.Compare(elementToSearch, currentElement) <= 0)
				rightBound = currentElementIndex;
			else
				leftBound = NextIndex(currentElementIndex);
		}

		var outerIndex = DecreaseIndex(leftBound, firstElementIndex);
		return comparer.Compare(elements[leftBound], elementToSearch) switch
		{
			< 0 => ~(outerIndex + 1),
			> 0 => ~outerIndex,
			0 => outerIndex
		};
	}

	public void Append(T value)
	{
		if (Size >= MaxSize)
			throw MaxSizeReached();

		elements[elementToAppendIndex] = value;
		++Size;
	}

	public void Prepend(T value)
	{
		if (Size >= MaxSize)
			throw MaxSizeReached();

		firstElementIndex = PreviousIndex(firstElementIndex);
		++Size;
		elements[firstElementIndex] = value;
	}

	public void RemoveFirst() => RemoveFirst(1);

	public void RemoveFirst(int count)
	{
		if (Size < count)
			throw InvalidElementsToRemoveCount(count);

		firstElementIndex = IncreaseIndex(firstElementIndex, count);
		Size -= count;
	}

	public void RemoveLast() => RemoveLast(1);

	public void RemoveLast(int count)
	{
		if (Size < count)
			throw InvalidElementsToRemoveCount(count);

		Size -= count;
	}

	private int PreviousIndex(int index) => index > 0 ? index - 1 : MaxSize - 1;

	private int NextIndex(int index) => index < MaxSize - 1 ? index + 1 : 0;

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

	private ArgumentException OutOfSize(int index) => new($"Index {index} is out of size {Size}");

	private ArgumentException InvalidElementsToRemoveCount(int elementsToRemoveCount)
		=> new($"Can't remove {elementsToRemoveCount} elements from deque of size {Size}");

	private InvalidOperationException MaxSizeReached() => new($"Max size of {MaxSize} is reached");

	private static InvalidOperationException Empty() => new($"Deque is empty");
}
