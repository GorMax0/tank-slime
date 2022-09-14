using System;

public class Health
{
    private int _maxValue;
    private int _currentValue;

    public Health(int maxHealth)
    {
        _maxValue = maxHealth;
        _currentValue = _maxValue;

        float normalizedValue = _currentValue / _maxValue;
        Changed?.Invoke(normalizedValue);
    }

    public Action<float> Changed;

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException($"Invalid damage value: {damage}");

        damage = _currentValue - damage < 0 ? damage - _currentValue : damage;
        _currentValue -= damage;

        float normalizedValue = _currentValue / _maxValue;
        Changed?.Invoke(normalizedValue);
    }
}
