using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private Quaternion _startRotation;
    [SerializeField] private Quaternion _endRotation;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //_endRotation = Quaternion.Euler(_rigidbody.velocity.y, 0f, 0f);
        //transform.rotation = Quaternion.Slerp(transform.rotation, _endRotation, 0.05f);
    }

    public void Launch(Vector3 velocity)
    {
     //   _startRotation = transform.rotation;        
        _rigidbody.velocity = velocity;        

        Debug.Log($"Start rotation: {_rigidbody.rotation}");
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
