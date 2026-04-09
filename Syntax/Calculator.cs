using System;

class Calc
{
    static void Main(string[] args)
    {
        float arg1 = float.Parse(args[0]);
        string op = args[1];
        float arg2 = float.Parse(args[2]);
        
        float result = 0.0f;
        switch  (op)
        {
            case "+":
                result = arg1 + arg2;
                break;

            case "-":
                result = arg1 - arg2;
                break;

            case "*":
                result = arg1 * arg2;
                break;

            case "/":
                result = arg1 / arg2;
                break;
        }
        
        Console.WriteLine(result);
    }
}
