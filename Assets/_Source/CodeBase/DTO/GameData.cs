using System.Collections.Generic;

public class GameData
{
    public double CurrentMana;
    public List<GeneratorSaveData> Generators = new();
    public List<string> PurchasedUpgrades = new();
}