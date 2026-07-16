public class GeneratorState
{
    private GeneratorData _generatorData;
    private int _purchasedCount;

    public GeneratorData GeneratorData => _generatorData;
    public int PurchasedCount => _purchasedCount;

    public GeneratorState (GeneratorData generatorData)
    {
        _generatorData = generatorData;
    }

    public void IncrementPurchasedCount()
    {
        _purchasedCount++;
    }
}