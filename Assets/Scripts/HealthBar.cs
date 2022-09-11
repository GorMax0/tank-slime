using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Health _health;
    [SerializeField] private Movement _playerMovement;
    [SerializeField] private Transform _mover;
    [SerializeField] private float _positionOffsetY;

    private Vector3 _currentPosition;

    private void OnEnable()
    {
        _playerMovement.Stopped += OnStopped;
        _health.Initialized += OnInitialized;
        _health.Changed += OnChanged;
    }


    private void OnDisable()
    {
        _playerMovement.Stopped -= OnStopped;
        _health.Changed -= OnChanged;
    }

    private void Start()
    {
        transform.rotation = Camera.main.transform.rotation;
    }

    private void FixedUpdate()
    {
        _currentPosition = new Vector3(_mover.position.x, _mover.position.y + _positionOffsetY, _mover.position.z);
        transform.position = _currentPosition;
    }

    private void OnInitialized(int health)
    {
        _slider.maxValue = health;
        _slider.value = _slider.maxValue;

        _health.Initialized -= OnInitialized;
    }

    private void OnChanged(int currentHealth)
    {
        if (currentHealth >= 0)
            _slider.value = currentHealth;
    }

    private void OnStopped(bool isMove)
    {
        //_playerMove = isMove;
    }
}
