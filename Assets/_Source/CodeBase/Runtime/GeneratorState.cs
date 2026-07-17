using System;

public class GeneratorState
{
    private GeneratorData _generatorData;
    private int _purchasedCount;
    private double _price;

    public event Action<int, double> OnChangePurchasedCount;

    public GeneratorData GeneratorData => _generatorData;
    public int PurchasedCount => _purchasedCount;
    public double Price => _price;

    public GeneratorState (GeneratorData generatorData)
    {
        _generatorData = generatorData;
        _price = CalculatePrice();
    }

    public void IncrementPurchasedCount()
    {
        _purchasedCount++;
        _price = CalculatePrice();
        OnChangePurchasedCount?.Invoke(_purchasedCount, _price);
    }

    private double CalculatePrice()
    {
        return _generatorData.BaseCost * Math.Pow(_generatorData.CostGrowthFactor, _purchasedCount);
    }
}