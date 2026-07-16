
using System.Collections.Generic;
using UnityEngine;

public class GameInstaller : MonoBehaviour 
{
    [SerializeField] private ClickSystem _clickSystem;
    [SerializeField] private ManaView _manaView;
    [SerializeField] private List<GeneratorData> _generatorDatas;
    [SerializeField] private GeneratorSystem _generatorSystem;

    private List<GeneratorState> _allGenerators = new();

    private ManaWallet _manaWallet;

    private void Awake()
    {
        _manaWallet = new ManaWallet();
        _clickSystem.Inject(_manaWallet);
        _manaView.Inject(_manaWallet);

        if (_generatorDatas != null)
        {
            foreach (GeneratorData data in _generatorDatas)
            {
                GeneratorState state = new GeneratorState(data);
                _allGenerators.Add(state);
            }

            _generatorSystem.Inject(_allGenerators, _manaWallet);
        }
    }
}