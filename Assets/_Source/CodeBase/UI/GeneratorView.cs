using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private TextMeshProUGUI _count;
    [SerializeField] private Image _image;
    [SerializeField] private Button _buyButton;

    private GeneratorState _generatorState;

    public event Action<GeneratorState> OnBuyClicked;

    public void Inject(GeneratorState generatorState)
    {
        _generatorState = generatorState;
        _name.SetText(_generatorState.GeneratorData.GeneratorName);
        _count.SetText(_generatorState.PurchasedCount.ToString());
        _price.SetText(NumberFormatter.Format(_generatorState.Price));
        _generatorState.OnChangePurchasedCount += OnChangePurchasedCount;
        _buyButton.onClick.RemoveListener(HandleBuyClicked);
        _buyButton.onClick.AddListener(HandleBuyClicked);
    }

    private void OnChangePurchasedCount(int value, double price)
    {
        _count.SetText(value.ToString());
        _price.SetText(NumberFormatter.Format(price));
    }

    private void HandleBuyClicked()
    {
        OnBuyClicked?.Invoke(_generatorState);
    }
}