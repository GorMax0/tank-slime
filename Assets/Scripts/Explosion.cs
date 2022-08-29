using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]private float _explosionPower;
     [SerializeField]private Transform _parts;    

    private bool _isExploded;
    private MeshRenderer _meshRenderer;
    private Rigidbody [] _rigidbodies; 

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbodies = _parts.GetComponentsInChildren<Rigidbody>();
    }

    private void Start()
    {
        _parts.gameObject.SetActive(false);
    }

    public void Explode()
    {
        if (_isExploded)
        {
            return;
        }

        _isExploded = true;

        Vector3 origin = GetAveragePosition();
        _parts.gameObject.SetActive(true);
        _meshRenderer.enabled = false;

        foreach (var rigidbody in _rigidbodies)
        {
            var randomForce = Random.Range(0.3f, 1.5f);
            Vector3 force = (rigidbody.transform.position - origin).normalized * _explosionPower + new Vector3(0f,randomForce,randomForce);
            rigidbody.isKinematic = false;
            rigidbody.AddForce(force, ForceMode.VelocityChange);
        }
    }

    private Vector3 GetAveragePosition()
    {
        Vector3 position = Vector3.zero;       

        foreach(var rigidbody in _rigidbodies)
        {
            position += rigidbody.transform.position;
        }

        position /= _rigidbodies.Length;
        return position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bullet bullet))
        {
            Explode();
        }
    }  
}