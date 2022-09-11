using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxValue;

    private int _currentValue;

    public UnityAction<int> Initialized;
    public UnityAction<int> Changed;

    private void Start()
    {
        _currentValue = _maxValue;
        Initialized?.Invoke(_currentValue);
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException($"Invalid damage value: {damage}");

        _currentValue = Mathf.Clamp(_currentValue - damage, 0, _maxValue);
        Changed?.Invoke(_currentValue);
    }
}
