using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    private List<ISaveable> _saveables = new();
    private string _saveFolder;

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void Inject(ManaWallet manaWallet, GeneratorSystem generatorSystem, UpgradeSystem upgradeSystem)
    {
        _saveFolder = Application.persistentDataPath + "/save.json";
        _saveables.Add(manaWallet);
        _saveables.Add(generatorSystem);
        _saveables.Add(upgradeSystem);

        generatorSystem.OnGeneratorPurchased += SaveGame;
        upgradeSystem.OnUpgradePurchased += SaveGame;
    }

    public void SaveGame()
    {
        GameData data = new GameData();

        foreach (ISaveable saveable in _saveables)
            saveable.Save(data);

        Save(data);
    }

    public bool HasSave()
    {
        bool exists = File.Exists(_saveFolder);
        return exists;
    }

    public void LoadGame()
    {
        if (!HasSave())
            return;

        string json = File.ReadAllText(_saveFolder);
        GameData data = JsonConvert.DeserializeObject<GameData>(json);

        foreach (ISaveable saveable in _saveables)
            saveable.Load(data);
    }

    private void Save(GameData data)
    {
        string json = JsonConvert.SerializeObject(data);
        File.WriteAllText(_saveFolder, json);
    }
}