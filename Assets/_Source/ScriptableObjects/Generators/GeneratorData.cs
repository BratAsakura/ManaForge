using UnityEngine;

[CreateAssetMenu(fileName = "GeneratorData", menuName = "Generators/PassiveGenerator")]
public class GeneratorData : ScriptableObject
{
    [SerializeField] private string _generatorName;
    [SerializeField] private double _baseCost;
    [SerializeField] private double _costGrowthFactor;
    [SerializeField] private double _baseIncomePerSecond;

    public string GeneratorName => _generatorName;
    public double BaseCost => _baseCost;
    public double CostGrowthFactor => _costGrowthFactor;
    public double BaseIncomePerSecond => _baseIncomePerSecond;
}