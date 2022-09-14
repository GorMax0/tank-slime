using System.Collections.Generic;
using UnityEngine;

public class Post : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private ShootPoint _shootPoint;
    [SerializeField] private Health _playerHealth;
    [SerializeField] private Transform _weapon;
    [SerializeField] private float _distance;
    [SerializeField] private float _speed;
    [SerializeField] private List<GameObject> _boxMans;

    private Vector3 _direction;
    private float _cooldown = 0.1f;

    private void Update()
    {
        _direction = _playerHealth.transform.position;

        DetectTarget();
    }

    private void DetectTarget()
    {
        float distanceToTarget = (_playerHealth.transform.position - _shootPoint.transform.position).magnitude;

        if (distanceToTarget <= _distance && _cooldown <= 0)
            Shoot();

        _cooldown -= Time.deltaTime;
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(_bullet, _shootPoint.transform.position, Quaternion.identity);
        bullet.Launch(-new Vector3(0f, _playerHealth.transform.position.y, _playerHealth.transform.position.z).normalized * _speed);
        _cooldown = 0.1f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Bullet bullet))
        {
            _weapon.gameObject.SetActive(false);
            _boxMans.ForEach(man => man.gameObject.SetActive(false));
            enabled = false;
        }
    }
}
