internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Dzień dobry właśnie korzysztasz z programu do oblocznia logarytmów i ciagów, miłego użytku :-)");
        while (true)
        {
            Console.WriteLine("\nWybierz opcję:");
            Console.WriteLine("1. Oblicz logarytm");
            Console.WriteLine("2. Oblicz ciąg");
            Console.WriteLine("3. Zakończ program");
            int opcja;
            while (!int.TryParse(Console.ReadLine(), out opcja) || opcja < 1 || opcja > 3)
            {
                Console.WriteLine("Wybierz poprawną opcję (1-3)!");
            }
            switch (opcja)
            {
                case 1:
                    Console.WriteLine("1. Oblicz działanie z logarytmem");
                    Console.WriteLine("2. Znajdz x np. log_7(x)=2");
                    Console.WriteLine("3. Znajdz x np. log_x(4)=2");
                    int opcja2;
                    while (!int.TryParse(Console.ReadLine(), out opcja2) || opcja2 < 1 || opcja2 < 4)
                    switch (opcja2)
                        {
                            case 1:
                                ObliczLogarytm();
                                break;
                            case 2:
                                ZnajdzX();
                                break;
                            case 3:
                                ZnajdzX2();
                                break;
                        }
                    break;
                case 2:
                    //ObliczCiag();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
            }
        }
    }
    static void ObliczLogarytm()
    {
        Console.WriteLine("Wpisz działanie do wykonania:");
        string expression = Console.ReadLine();

        try
        {
            expression = expression.Replace(" ", "");
            double result = Evaluate(expression);
            Console.WriteLine("Wynik: " + result);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Błąd: " + ex.Message);
            Environment.Exit(0);
        }
    }
    static double Evaluate(string expression)
    {

        while (expression.Contains("log_"))
        {
            int startIndex = expression.IndexOf("log_");
            int endIndex = expression.IndexOf(")", startIndex);
            string logExpr = expression.Substring(startIndex + 4, endIndex - startIndex - 4);
            string[] parts = logExpr.Split('(');
            double baseValue = double.Parse(parts[0]);
            double argument = double.Parse(parts[1].Replace(".", ","));
            double result = Math.Log(argument, baseValue);
            expression = expression.Replace($"log_{logExpr})", result.ToString().Replace(",", "."));
        }


        while (expression.Contains("log("))
        {
            int startIndex = expression.IndexOf("log(");
            int endIndex = expression.IndexOf(")", startIndex);
            string logExpr = expression.Substring(startIndex + 4, endIndex - startIndex - 4);
            double argument = double.Parse(logExpr.Replace(".", ","));
            double result = Math.Log(argument,10);
            expression = expression.Replace($"log({logExpr})", result.ToString().Replace(",", "."));
        }

        return EvaluateMathExpression(expression);
    }

    static double EvaluateMathExpression(string expression)
    {
        expression = expression.Replace("log_", "Math.Log(");

        return Convert.ToDouble(new System.Data.DataTable().Compute(expression, ""));
    }
    static void ZnajdzX()
    {
        Console.WriteLine("Wpisz równanie logarytmiczne do rozwiązania (np. log_2(x)=3):");
        string expression = Console.ReadLine();

        if (!expression.StartsWith("log_") || !expression.Contains("="))
        {
            Console.WriteLine("Błąd: Nieprawidłowy format!");
            return;
        }
        expression = expression.Replace(" ", "");
        int baseEndIndex = expression.IndexOf("(");
        int valueEndIndex = expression.IndexOf(")");
        int equalIndex = expression.IndexOf("=");
        double logBase = double.Parse(expression.Substring(4, baseEndIndex - 4));
        double resultValue = double.Parse(expression.Substring(equalIndex + 1));
        double x = Math.Pow(logBase, resultValue);

        Console.WriteLine($"Wartość x to: {x}");
    }
    static void ZnajdzX2()
    {
        Console.WriteLine("Wpisz równanie logarytmiczne do rozwiązania (np. log_x(4)=2):");
        string expression = Console.ReadLine();
        expression = expression.Replace(" ", "");
        if (!expression.StartsWith("log_") || !expression.Contains(")") || !expression.Contains("="))
        {
            Console.WriteLine("Błąd: Nieprawidłowy format równania!");
            return;
        }
        int baseIndex = expression.IndexOf("log_") + 4;
        int openParenIndex = expression.IndexOf("(");
        int closeParenIndex = expression.IndexOf(")");
        int equalIndex = expression.IndexOf("=");
        if (double.TryParse(expression.Substring(openParenIndex + 1, closeParenIndex - openParenIndex - 1), out double argument) &&
            double.TryParse(expression.Substring(equalIndex + 1), out double result))
        {
            double baseValue = Math.Pow(argument, 1 / result);
            Console.WriteLine($"Wartość podstawy logarytmu x to: {baseValue}");
        }
        else
        {
            Console.WriteLine("Błąd: Wprowadzono nieprawidłowe wartości!");
        }
    }
}
