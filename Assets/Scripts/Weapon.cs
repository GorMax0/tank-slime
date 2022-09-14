using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ShootZone _shootZone;
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private ShootPoint _shootPoint;

    private Ballistics _ballistics;

    private void Awake()
    {
        _ballistics = new Ballistics();
    }

    private void OnEnable()
    {
        _shootZone.Shooted += OnShoot;
    }

    private void OnDisable()
    {
        _shootZone.Shooted -= OnShoot;
    }

    private void OnShoot(Vector3 targetPosition)
    {
        Vector3 trajectory = _ballistics.TrajectoryCalculation(_shootPoint.transform, targetPosition);
        _bulletPool.InvokeBullet(_shootPoint, trajectory);
    }
}
