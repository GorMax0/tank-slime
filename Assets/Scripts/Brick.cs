using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Brick : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Break(Vector3 force)
    {
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(force, ForceMode.VelocityChange);
    }
}