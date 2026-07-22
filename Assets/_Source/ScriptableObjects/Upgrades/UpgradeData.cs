using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Upgrade/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    [SerializeField] private string _upgradeName;
    [SerializeField] private double _cost;
    [SerializeField] private EffectType _effectType;
    [SerializeField] private double _multiplierValue;

    public string UpgradeName => _upgradeName;
    public double Cost => _cost;
    public EffectType EffectType => _effectType;
    public double MultiplierValue => _multiplierValue;
}

