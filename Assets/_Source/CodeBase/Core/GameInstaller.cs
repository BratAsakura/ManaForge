using System.Collections.Generic;
using UnityEngine;

public class GameInstaller : MonoBehaviour 
{
    [SerializeField] private ClickSystem _clickSystem;
    [SerializeField] private ManaView _manaView;
    [SerializeField] private List<GeneratorData> _generatorDatas;
    [SerializeField] private GeneratorSystem _generatorSystem;
    [SerializeField] private GeneratorListView _generatorListView;
    [SerializeField] private UpgradeSystem _upgradeSystem;
    [SerializeField] private List<UpgradeData> _upgradeDatas;
    [SerializeField] private SaveSystem _saveSystem;
    [SerializeField] private UpgradeListView _upgradeListView;

    private List<GeneratorState> _allGenerators = new();

    private ManaWallet _manaWallet;

    private void Awake()
    {
        _manaWallet = new ManaWallet();
        _clickSystem.Inject(_manaWallet, _upgradeSystem);
        _manaView.Inject(_manaWallet);
        _upgradeSystem.Inject(_manaWallet, _upgradeDatas);
        _upgradeListView.Inject(_upgradeDatas, _upgradeSystem);

        if (_generatorDatas.Count != 0)
        {
            foreach (GeneratorData data in _generatorDatas)
            {
                GeneratorState state = new GeneratorState(data);
                _allGenerators.Add(state);
            }

            _generatorSystem.Inject(_allGenerators, _manaWallet, _upgradeSystem);
            _generatorListView.Inject(_allGenerators, _generatorSystem);
        }

        _saveSystem.Inject(_manaWallet, _generatorSystem, _upgradeSystem);
        _saveSystem.LoadGame();
    }

    [ContextMenu("Test Buy Upgrade")]
    private void TestBuyUpgrade()
    {
        _upgradeSystem.TryPurchaseUpgrade(_upgradeDatas[0]);
    }
}