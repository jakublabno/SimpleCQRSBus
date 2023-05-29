namespace CQRSBus.Locator;

public class AdditionInflectionStrategy : INameInflectorStrategy
{
    private readonly string additionString;

    public AdditionInflectionStrategy(string additionString)
    {
        this.additionString = additionString;
    }

    public string Inflect(string originName)
    {
        return $"{originName}{additionString}";
    }
}