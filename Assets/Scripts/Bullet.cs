using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(TrailRenderer))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem _hitEffect;

    private Rigidbody _rigidbody;
    private TrailRenderer _trail;
    private Quaternion _rotationOffset;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _trail = GetComponent<TrailRenderer>();
        _rotationOffset = Quaternion.Euler(90f, 0f, 0f);
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(_rigidbody.velocity) * _rotationOffset;
    }

    public void Launch(Vector3 velocity)
    {
        _trail.Clear();
        _rigidbody.velocity = velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(_hitEffect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}