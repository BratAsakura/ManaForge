using System;

public class ManaWallet
{
    private double _currentMana;

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