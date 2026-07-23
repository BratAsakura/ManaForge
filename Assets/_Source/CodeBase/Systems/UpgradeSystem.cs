using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour, ISaveable
{
    private ManaWallet _manaWallet;
    private List<UpgradeData> _upgradeDatas = new();
    private List<UpgradeData> _allUpgrades = new();

    public event Action<double> OnChangePowerClick;
    public event Action<double> OnChangePowerGenerator;
    public event Action OnUpgradePurchased;
    public event Action<UpgradeData> OnUpgradeLoaded;

    public void Inject(ManaWallet manaWallet, List<UpgradeData> allUpgrades)
    {
        _manaWallet = manaWallet;
        _allUpgrades = allUpgrades;
    }

    public void Load(GameData data)
    {
        for (int i = 0; i < _allUpgrades.Count; i++)
        {
            for (int j = 0; j < data.PurchasedUpgrades.Count; j++)
            {
                if (_allUpgrades[i].UpgradeName == data.PurchasedUpgrades[j])
                {
                    _upgradeDatas.Add(_allUpgrades[i]);
                    OnUpgradeLoaded?.Invoke(_allUpgrades[i]);

                    if (_allUpgrades[i].EffectType == EffectType.ClickMultiplier)
                        OnChangePowerClick?.Invoke(_allUpgrades[i].MultiplierValue);
                    else
                        OnChangePowerGenerator?.Invoke(_allUpgrades[i].MultiplierValue);
                }
            }
        }
    }

    public void Save(GameData data)
    {
        foreach (var upgrade in _upgradeDatas)
            data.PurchasedUpgrades.Add(upgrade.UpgradeName);
    }

    public bool TryPurchaseUpgrade(UpgradeData upgrade)
    {
        if (_manaWallet.TrySpendMana(upgrade.Cost))
        {
            _upgradeDatas.Add(upgrade);

            if (upgrade.EffectType == EffectType.ClickMultiplier)
            {
                OnChangePowerClick?.Invoke(upgrade.MultiplierValue);
                OnUpgradePurchased?.Invoke();
                return true;
            }

            OnChangePowerGenerator?.Invoke(upgrade.MultiplierValue);
            OnUpgradePurchased?.Invoke();
            return true;
        }

        return false;
    }
}