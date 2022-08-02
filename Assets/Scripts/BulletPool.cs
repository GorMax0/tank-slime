using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private ShootPoint _shootPoint;

    private List<Bullet> _bullets = new List<Bullet>();

    public void InvokeBullet(Vector3 target)
    {
        if (HasFreeBullet() == true)
        {
            _bullet.transform.position = transform.position;
            _bullet.transform.rotation = transform.rotation;
            _bullet.gameObject.SetActive(true);
        }
        else
        {
            CreateBullet(_shootPoint, target);
        }

        _bullet.Move(_shootPoint.transform.position, target);
    }

    private bool HasFreeBullet()
    {
        foreach (Bullet bullet in _bullets)
        {
            if (bullet.gameObject.activeSelf == false)
                return true;
        }

        return false;
    }

    private void CreateBullet(ShootPoint shootPoint,Vector3 target)
    {
        Bullet newBullet = Instantiate(_bullet, shootPoint.transform.position, transform.rotation, transform);
        _bullets.Add(newBullet);
    }
}
