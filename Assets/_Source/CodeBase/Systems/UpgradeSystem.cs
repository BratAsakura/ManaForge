using System;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    private ManaWallet _manaWallet;

    public event Action<double> OnChangePowerClick;
    public event Action<double> OnChangePowerGenerator;

    public void Inject(ManaWallet manaWallet)
    {
        _manaWallet = manaWallet;
    }

    public bool TryPurchaseUpgrade(UpgradeData upgrade)
    {
        if (_manaWallet.TrySpendMana(upgrade.Cost))
        {
            if (upgrade.EffectType == EffectType.ClickMultiplier)
            {
                OnChangePowerClick?.Invoke(upgrade.MultiplierValue);
                return true;
            }

            OnChangePowerGenerator?.Invoke(upgrade.MultiplierValue);
            return true;
        }

        return false;
    }
}