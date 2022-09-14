using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Tank _tank;
    [SerializeField] private Movement _playerMovement;
    [SerializeField] private Transform _mover;
    [SerializeField] private float _positionOffsetY;

    private Health _health;
    private Vector3 _currentPosition;

    private void OnEnable()
    {
        _playerMovement.Stopped += OnStopped;

        if (_health != null)
            _health.Changed += OnChanged;
    }


    private void OnDisable()
    {
        _playerMovement.Stopped -= OnStopped;
        _health.Changed -= OnChanged;
    }

    private void Start()
    {
        _health = _tank.Health;
        _health.Changed += OnChanged;

        transform.rotation = Camera.main.transform.rotation;
    }

    private void FixedUpdate()
    {
        _currentPosition = new Vector3(_mover.position.x, _mover.position.y + _positionOffsetY, _mover.position.z);
        transform.position = _currentPosition;
    }

    private void OnChanged(float currentHealth)
    {
        if (currentHealth >= 0)
            _slider.value = currentHealth;
    }

    private void OnStopped(bool isMove)
    {
        //_playerMove = isMove;
    }
}
