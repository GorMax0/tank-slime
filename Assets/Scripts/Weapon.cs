using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private ShootPoint _shootPoint;
    [SerializeField] private int _damage;

    private Ballistics _ballistics;

    private void Awake()
    {
        _ballistics = new Ballistics();
    }

    public void Shoot()
    {
        Vector3 trajectory = _ballistics.TrajectoryCalculation(_shootPoint.transform, Vector3.forward);
        _bulletPool.InvokeBullet(_bullet, _shootPoint, trajectory, _damage);
    }
}
