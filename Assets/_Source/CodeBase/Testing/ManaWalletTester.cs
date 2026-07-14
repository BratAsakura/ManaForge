using UnityEngine;

public sealed class ManaWalletTester : MonoBehaviour
{
    private ManaWallet _manaWallet;

    private void Awake()
    {
        _manaWallet = new ManaWallet();
        _manaWallet.OnManaChanged += HandleManaChanged;
    }

    private void Start()
    {
        _manaWallet.AddMana(10);
        _manaWallet.AddMana(5);
        Debug.Log(_manaWallet.TrySpendMana(3));
        Debug.Log(_manaWallet.TrySpendMana(1000));
    }

    private void OnDestroy()
    {
        _manaWallet.OnManaChanged -= HandleManaChanged;
    }

    private void HandleManaChanged(double currentMana)
    {
        Debug.Log(currentMana);
    }
}