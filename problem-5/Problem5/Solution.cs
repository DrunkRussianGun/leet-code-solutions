namespace Problem5;

public class Solution
{
	public string LongestPalindrome(string input)
	{
		var palindromeMiddleIndex = 0;
		var longestPalindromePosition = new PalindromePosition();
		while (palindromeMiddleIndex < input.Length)
		{
			var leftPalindromeBound = palindromeMiddleIndex;
			var firstDifferentCharIndex = FindFirstDifferentCharIndex(input, leftPalindromeBound);
			if (firstDifferentCharIndex >= input.Length)
			{
				longestPalindromePosition = SelectWithMaxLength(
					longestPalindromePosition,
					new PalindromePosition(leftPalindromeBound, input.Length - leftPalindromeBound));
				break;
			}

			--leftPalindromeBound;
			var rightPalindromeBound = firstDifferentCharIndex;
			while (leftPalindromeBound >= 0 && rightPalindromeBound < input.Length
				&& input[leftPalindromeBound] == input[rightPalindromeBound])
			{
				--leftPalindromeBound;
				++rightPalindromeBound;
			}

			longestPalindromePosition = SelectWithMaxLength(
				longestPalindromePosition,
				PalindromePosition.FromBounds(leftPalindromeBound + 1, rightPalindromeBound - 1));
			palindromeMiddleIndex = firstDifferentCharIndex;
		}

		return input.Substring(longestPalindromePosition.StartIndex, longestPalindromePosition.Length);
	}

	private readonly record struct PalindromePosition(int StartIndex, int Length)
	{
		public static PalindromePosition FromBounds(int leftBound, int rightBound)
			=> new(leftBound, rightBound - leftBound + 1);
	};

	private static int FindFirstDifferentCharIndex(string input, int currentCharIndex)
	{
		var firstDifferentCharIndex = currentCharIndex;
		while (firstDifferentCharIndex < input.Length
				&& input[firstDifferentCharIndex] == input[currentCharIndex])
			++firstDifferentCharIndex;
		return firstDifferentCharIndex;
	}

	private static PalindromePosition SelectWithMaxLength(
			PalindromePosition first,
			PalindromePosition second)
		=> first.Length >= second.Length ? first : second;
}
