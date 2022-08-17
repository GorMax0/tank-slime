using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private ShootPoint _shootPoint;

    private List<Bullet> _bullets = new List<Bullet>();

    public void InvokeBullet(Vector3 target)
    {
        Bullet freeBullet = GetFreeBullet();

        freeBullet.transform.position = transform.position;
        freeBullet.transform.rotation = transform.rotation;
        freeBullet.gameObject.SetActive(true);

        freeBullet.Launch(_shootPoint.transform.position, _shootPoint.transform.rotation, target);
    }

    private Bullet GetFreeBullet()
    {
        foreach (Bullet bullet in _bullets)
        {
            if (bullet.gameObject.activeSelf == false)
                return bullet;
        }

        return CreateBullet();
    }

    private Bullet CreateBullet()
    {
        Bullet newBullet = Instantiate(_bulletPrefab, _shootPoint.transform.position, _shootPoint.transform.rotation, transform);
        _bullets.Add(newBullet);

        return newBullet;
    }
}
