using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    public Health Health { get; private set; }

    private void Awake()
    {
        Health = new Health(_maxHealth);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Bullet bullet))
        {
            Health.TakeDamage(bullet.Damage);
            Debug.Log("hit!");
        }

    }
}
