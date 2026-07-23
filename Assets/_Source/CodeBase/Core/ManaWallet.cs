using System;

public class ManaWallet : ISaveable
{
    private double _currentMana;

    public double CurrentMana => _currentMana;

    public event Action<double> OnManaChanged;

    public double AddMana(double amount)
    {
        if (IsInvalidAmount(amount))
        {
            return _currentMana;
        }

        _currentMana += amount;
        OnManaChanged?.Invoke(_currentMana);
        return _currentMana;
    }

    public void Load(GameData data)
    {
        _currentMana = data.CurrentMana;
        OnManaChanged?.Invoke(_currentMana);
    }

    public void Save(GameData data)
    {
        data.CurrentMana = _currentMana;
    }

    public bool TrySpendMana(double amount)
    {
        if (IsInvalidAmount(amount))
        {
            return false;
        }

        if (amount > _currentMana)
        {
            return false;
        }

        _currentMana -= amount;
        OnManaChanged?.Invoke(_currentMana);
        return true;
    }

    private bool IsInvalidAmount(double amount) => amount <= 0;
}