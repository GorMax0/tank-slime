using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;

    private List<Bullet> _bullets = new List<Bullet>();

    public void InvokeBullet(ShootPoint shootPoint, Vector3 trajectory)
    {
        Bullet freeBullet = GetFreeBullet();

        freeBullet.transform.position = shootPoint.transform.position;
        freeBullet.transform.rotation = new Quaternion(-shootPoint.transform.localRotation.x, 0f, 0f, 1f);
        freeBullet.gameObject.SetActive(true);

        freeBullet.Launch(trajectory);
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
        Bullet newBullet = Instantiate(_bulletPrefab, transform);
        _bullets.Add(newBullet);

        return newBullet;
    }
}
