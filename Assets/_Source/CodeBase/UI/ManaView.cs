using TMPro;
using UnityEngine;

public class ManaView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private ManaWallet _manaWallet;

    private void OnDisable()
    {
        _manaWallet.OnManaChanged -= HandleManaChanged;
    }

    public void Inject(ManaWallet manaWallet)
    {
        _manaWallet = manaWallet;
        _manaWallet.OnManaChanged += HandleManaChanged;
        _text.SetText(_manaWallet.CurrentMana.ToString());
    }

    private void HandleManaChanged(double value)
    {
        _text.SetText(value.ToString());
    }
}
