using UnityEngine;

public class GameInstaller : MonoBehaviour 
{
    [SerializeField]private ClickSystem _clickSystem;

    private ManaWallet _manaWallet;

    private void Awake()
    {
        _manaWallet = new ManaWallet();
        _clickSystem.Inject(_manaWallet);
    }
}