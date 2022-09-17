using System;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField] private ShootZone _shootZone;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private int _maxHealth;

    public Health Health { get; private set; }

    private void Awake()
    {
        Health = new Health(_maxHealth);
    }

    private void OnEnable()
    {
        _shootZone.Shooted += OnShoot;
    }

    private void OnDisable()
    {
        _shootZone.Shooted -= OnShoot;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Bullet bullet) && bullet.Type != BulletType.Tank)
        {
            Health.TakeDamage(bullet.Damage);
            Debug.Log("hit!");
        }

    }

    private void OnShoot()
    {
        _weapon.Shoot();
    }
}
