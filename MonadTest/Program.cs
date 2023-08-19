using MonadTest.Monads;

public static class Program
{
    public static void Main()
    {
        CalculateRatio(2, 4).PrintResult();
        CalculateRatio(2, 0).PrintResult();
        CalculateRatio(3, 9).PrintResult();

        // Output:
        //
        // Result: 50 %
        // Error: Cannot divide by zero
        // Result: 33 %
    }

    private static void PrintResult(this Result<decimal, string> result)
    {
        result
            .Match<Result<decimal, string>>(
                val => Result<decimal, string>.Success(val * 100m), // percentage
                err => Result<decimal, string>.Failure(err))
            .ApplyOnSuccess(val => Console.WriteLine($"Result: {val:0}%"))
            .ApplyOnFailure(err => Console.WriteLine($"Error: {err}"));
    }

    public static Result<decimal, string> CalculateRatio(int part, int total)
    {
        // NOTE: Handles error without the need to throw an exception.
        return total == 0 ?
            Result<decimal, string>.Failure("Cannot divide by zero") :
            Result<decimal, string>.Success((decimal)part / (decimal)total);
    }
}