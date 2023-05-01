namespace Calculator;

public static class Calculator
{
    public static double Calculate(double value1, double value2, string mathOperator)
    {
        double result = 0;

        switch (mathOperator)
        {
            case "÷":
                result = value1 / value2;
                break;
            case "×":
                result = value1 * value2;
                break;
            case "+":
                result = value1 + value2;
                break;
            case "-":
                result = value1 - value2;
                break;
        }

        return result;
    }
}

public static class DoubleExtensions
{
    public static string ToTrimmedString(this double target, string decimalFormat)
    {
        string strValue = target.ToString(decimalFormat); //Get the stock string

        //If there is a decimal point present
        if (strValue.Contains("."))
        {
            //Remove all trailing zeros
            strValue = strValue.TrimEnd('0');

            //If all we are left with is a decimal point
            if (strValue.EndsWith(".")) //then remove it
                strValue = strValue.TrimEnd('.');
        }

        return strValue;
    }
}
public class CalculatorViewModel : INotifyPropertyChanged
{
    private readonly HistoryViewModel historyViewModel;

    public CalculatorViewModel()
    {
        historyViewModel = new HistoryViewModel();
    }

    // ...
}
public void Calculate()
{
    double result = Evaluate(Expression);
    Result = result.ToString();

    // Add the calculation to the history
    historyViewModel.History.Add(new Calculation
    {
        Expression = Expression,
        Result = Result
    });
}
