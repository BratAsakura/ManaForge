using System.Collections.Generic;
using UnityEngine;

public class GeneratorListView : MonoBehaviour
{
    [SerializeField] private GeneratorView _prefab;
    [SerializeField] private Transform _container;

    public void Inject(List<GeneratorState> generators, GeneratorSystem generatorSystem)
    {
        foreach (GeneratorState state in generators)
        {
            GeneratorView current = Instantiate(_prefab, _container, false);
            current.Inject(state);
            current.OnBuyClicked += state => generatorSystem.TryPurchaseGenerator(state);
        }
    }
}