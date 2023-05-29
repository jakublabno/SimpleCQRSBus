namespace CQRSBus.Locator;

public interface INameInflectorStrategy
{
    string Inflect(string originName);
}