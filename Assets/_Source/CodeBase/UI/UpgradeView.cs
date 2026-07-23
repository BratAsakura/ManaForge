using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private Button _buyButton;

    private UpgradeData _upgradeData;

    public event Action<UpgradeData> OnBuyClicked;

    public void Inject(UpgradeData upgradeData)
    {
        _upgradeData = upgradeData;
        _name.SetText(_upgradeData.UpgradeName);
        _price.SetText(NumberFormatter.Format(_upgradeData.Cost));

        _buyButton.onClick.RemoveListener(HandleBuyClicked);
        _buyButton.onClick.AddListener(HandleBuyClicked);
    }

    private void HandleBuyClicked()
    {
        OnBuyClicked?.Invoke(_upgradeData);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}