using UnityEngine;

public class GameInstaller : MonoBehaviour 
{
    [SerializeField]private ClickSystem _clickSystem;
    [SerializeField]private ManaView _manaView;

    private ManaWallet _manaWallet;

    private void Awake()
    {
        _manaWallet = new ManaWallet();
        _clickSystem.Inject(_manaWallet);
        _manaView.Inject(_manaWallet);
    }
}