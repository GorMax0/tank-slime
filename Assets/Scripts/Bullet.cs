using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(TrailRenderer))]
public class Bullet : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private TrailRenderer _trail;
    private Quaternion _rotationOffset;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _trail = GetComponent<TrailRenderer>();
        _rotationOffset = Quaternion.Euler(90f, 0f, 0f);
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(_rigidbody.velocity) * _rotationOffset;        
    }

    public void Launch(Vector3 velocity)
    { 
        _trail.Clear();
        _rigidbody.velocity = velocity;        

        Debug.Log($"Start rotation: {_rigidbody.rotation}");
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}