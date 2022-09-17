using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private List<Bullet> _bullets = new List<Bullet>();

    public void InvokeBullet(Bullet template, ShootPoint shootPoint, Vector3 trajectory, int damage)
    {
        Bullet freeBullet = GetFreeBullet(template);
        
        freeBullet.transform.position = shootPoint.transform.position;
        freeBullet.transform.rotation = new Quaternion(-shootPoint.transform.localRotation.x, 0f, 0f, 1f);
        freeBullet.gameObject.SetActive(true);

        freeBullet.Launch(trajectory,damage);
    }

    private Bullet GetFreeBullet(Bullet template)
    {
        foreach (Bullet bullet in _bullets)
        {
            if (bullet.gameObject.activeSelf == false && bullet.Type == template.Type)
                return bullet;
        }

        return CreateBullet(template);
    }

    private Bullet CreateBullet(Bullet template)
    {
        Bullet newBullet = Instantiate(template, transform);
        _bullets.Add(newBullet);

        return newBullet;
    }
}
