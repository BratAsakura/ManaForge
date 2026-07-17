using System.Collections.Generic;
using UnityEngine;

public class GameInstaller : MonoBehaviour 
{
    [SerializeField] private ClickSystem _clickSystem;
    [SerializeField] private ManaView _manaView;
    [SerializeField] private List<GeneratorData> _generatorDatas;
    [SerializeField] private GeneratorSystem _generatorSystem;
    [SerializeField] private GeneratorListView _generatorListView;

    private List<GeneratorState> _allGenerators = new();

    private ManaWallet _manaWallet;

    private void Awake()
    {
        _manaWallet = new ManaWallet();
        _clickSystem.Inject(_manaWallet);
        _manaView.Inject(_manaWallet);

        if (_generatorDatas.Count != 0)
        {
            foreach (GeneratorData data in _generatorDatas)
            {
                GeneratorState state = new GeneratorState(data);
                _allGenerators.Add(state);
            }

            _generatorSystem.Inject(_allGenerators, _manaWallet);
            _generatorListView.Inject(_allGenerators, _generatorSystem);
        }
    }
}