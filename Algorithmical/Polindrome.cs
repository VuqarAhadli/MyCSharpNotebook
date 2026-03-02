using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter an integer: ");
        string input = Console.ReadLine();

        if (!int.TryParse(input, out int number))
        {
            Console.WriteLine("Invalid integer!");
            return;
        }

        if (IsPalindrome(number))
            Console.WriteLine($"{number} is a palindrome!");
        else
            Console.WriteLine($"{number} is not a palindrome!");
    }

    static bool IsPalindrome(int num)
    {

        if (num < 0) return false;

        int original = num;
        int reversed = 0;

        while (num > 0)
        {
            int digit = num % 10;
            reversed = reversed * 10 + digit;
            num /= 10;
        }

        return original == reversed;
    }
}