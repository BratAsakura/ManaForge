using System.Collections.Generic;
using UnityEngine;

public class UpgradeListView : MonoBehaviour
{
    [SerializeField] private UpgradeView _prefab;
    [SerializeField] private Transform _container;

    public void Inject(List<UpgradeData> upgrades, UpgradeSystem upgradeSystem)
    {
        foreach (UpgradeData data in upgrades)
        {
            UpgradeView view = Instantiate(_prefab, _container, false);
            view.Inject(data);
            view.OnBuyClicked += upgrade =>
            {
                if (upgradeSystem.TryPurchaseUpgrade(upgrade))
                    view.Hide();
            };
        }
    }
}