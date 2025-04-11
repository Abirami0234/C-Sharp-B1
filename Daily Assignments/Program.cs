using System;
using System.Reflection.Metadata;


namespace C__Assignment_1;

internal class Program
{
    static void Main(string[] args)
    {
        CheckEquality();
        CheckPositiveOrNegative();
        PerformOperations();
        PrintMultiplicationTable();
        ComputeSum();

    }
    static void CheckEquality()
    {
        Console.WriteLine("Input 1st num: ");
        int num1 = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Input 2nd num: ");
        int num2 = Convert.ToInt32(Console.ReadLine());

        if (num1 != num2)
            Console.WriteLine($"{num1} is not equal to {num2}");
        else
            Console.WriteLine($"{num1} is equal to {num2}");
    }
    static void CheckPositiveOrNegative()
    {
        Console.WriteLine("Enter a Number: ");
        int num = Convert.ToInt32(Console.ReadLine());
        if (num > 0)
            Console.WriteLine($"{num} is Positive Number");
        else if (num < 0)
            Console.WriteLine($"{num} is Negative Number");
        else
            Console.WriteLine($"{num} is Zero");
    }

    static void PerformOperations()
    {
        Console.WriteLine("Enter 1st Number: ");
        int num1 = Convert.ToInt32(Console.ReadLine());
        c
    }
}