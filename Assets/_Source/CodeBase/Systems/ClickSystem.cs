using UnityEngine;

public class ClickSystem : MonoBehaviour
{
    [SerializeField] private double _clickPower = 1;
    
    private ManaWallet _manaWallet;
    private UpgradeSystem _upgradeSystem;

    public void Inject(ManaWallet manaWallet, UpgradeSystem upgradeSystem)
    {
        _manaWallet = manaWallet;
        _upgradeSystem = upgradeSystem;

        _upgradeSystem.OnChangePowerClick += OnChangePowerClick;
    }

    private void OnChangePowerClick(double amount)
    {
        if (amount <= 0)
            return;

        _clickPower += amount;
    }

    public void OnMouseDown()
    {
        _manaWallet.AddMana(_clickPower);
    }
}