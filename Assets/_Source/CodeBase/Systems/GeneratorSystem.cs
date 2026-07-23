using System;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorSystem : MonoBehaviour, ISaveable
{
    private List<GeneratorState> _generators = new();
    private ManaWallet _manaWallet;
    private float _timer = 0;
    private double _generatorMultiplier = 1;

    public event Action OnGeneratorPurchased;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= 1f)
        {
            _timer = 0;
            foreach (GeneratorState generator in _generators)
            {
                if (generator.PurchasedCount == 0)
                    continue;

                double income = generator.GeneratorData.BaseIncomePerSecond * generator.PurchasedCount * _generatorMultiplier;

                if (income > 0)
                    _manaWallet.AddMana(income);
            }
        }
    }

    public bool TryPurchaseGenerator(GeneratorState generator)
    {
        if (_manaWallet.TrySpendMana(generator.Price))
        {
            generator.IncrementPurchasedCount();
            OnGeneratorPurchased?.Invoke();
            return true;
        }

        return false;
    }

    public void Inject(List<GeneratorState> generators, ManaWallet manaWallet, UpgradeSystem upgradeSystem)
    {
        _generators.AddRange(generators);
        _manaWallet = manaWallet;

        upgradeSystem.OnChangePowerGenerator += OnChangePowerGenerator;
    }

    public void Save(GameData data)
    {
        foreach (GeneratorState generator in _generators)
        {
            GeneratorSaveData saveData = new()
            {
                GeneratorName = generator.GeneratorData.GeneratorName,
                PurchasedCount = generator.PurchasedCount
            };

            data.Generators.Add(saveData);
        }
    }

    public void Load(GameData data)
    {
        for (int i = 0; i < _generators.Count; i++)
        {
            for (int j = 0; j < data.Generators.Count; j++)
            {
                if (_generators[i].GeneratorData.GeneratorName == data.Generators[j].GeneratorName)
                {
                    _generators[i].SetPurchasedCount(data.Generators[j].PurchasedCount);
                }
            }
        }
    }

    private void OnChangePowerGenerator(double multiplier)
    {
        _generatorMultiplier *= multiplier;
    }
}