using System.Collections.Generic;
using UnityEngine;

public class UpgradeListView : MonoBehaviour
{
    [SerializeField] private UpgradeView _prefab;
    [SerializeField] private Transform _container;

    private Dictionary<UpgradeData, UpgradeView> _views = new();

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
            _views[data] = view;
        }

        upgradeSystem.OnUpgradeLoaded += OnUpgradeLoaded;
    }

    private void OnUpgradeLoaded(UpgradeData upgrade)
    {
        if (_views.TryGetValue(upgrade, out UpgradeView view))
            view.Hide();
    }
}