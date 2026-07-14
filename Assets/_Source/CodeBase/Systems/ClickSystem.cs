using UnityEngine;

public class ClickSystem : MonoBehaviour
{
    [SerializeField] private double _clickPower = 1;

    private ManaWallet _manaWallet;

    public void Inject(ManaWallet manaWallet)
    {
        _manaWallet = manaWallet;
    }

    public void OnMouseDown()
    {
        _manaWallet.AddMana(_clickPower);
    }
}