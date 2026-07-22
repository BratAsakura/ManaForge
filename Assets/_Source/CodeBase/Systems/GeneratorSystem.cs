using System.Collections.Generic;
using UnityEngine;

public class GeneratorSystem : MonoBehaviour
{
    private List<GeneratorState> _generators = new();
    private ManaWallet _manaWallet;
    private float _timer = 0;
    private double _generatorMultiplier = 1;

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

    private void OnChangePowerGenerator(double multiplier)
    {
        _generatorMultiplier *= multiplier;
    }
}