using System.Collections.Generic;
using UnityEngine;

public class Post : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Tank _playerHealth;
    [SerializeField] private float _distance;
    [SerializeField] private float _speed;
    [SerializeField] private List<GameObject> _boxMans;

    private float _cooldown = 0.1f;

    private void Update()
    {
        _weapon.transform.LookAt(_playerHealth.transform);
        Debug.DrawLine(_weapon.transform.position, _playerHealth.transform.position, Color.red);
        DetectTarget();
    }

    private void DetectTarget()
    {
        float distanceToTarget = (_playerHealth.transform.position - transform.position).magnitude;

        if (distanceToTarget <= _distance && _cooldown <= 0)
            Shoot();

        _cooldown -= Time.deltaTime;
    }

    private void Shoot()
    {
        _weapon.Shoot();
        _cooldown = 0.1f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Bullet bullet) && bullet.Type == BulletType.Tank)
        {
            _weapon.gameObject.SetActive(false);
            _boxMans.ForEach(man => man.gameObject.SetActive(false));
            enabled = false;
        }
    }
}
