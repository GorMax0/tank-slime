using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(BoxCollider))]
public class Explosion : MonoBehaviour
{
    [SerializeField] private float _minExplosionPower;
    [SerializeField] private float _maxExplosionPower;
    [SerializeField] private float _minForceY;
    [SerializeField] private float _maxForceY;
    [SerializeField] private float _minForceZ;
    [SerializeField] private float _maxForceZ;
    [SerializeField] private Brick[] _bricks;

    private bool _isExploded;
    private MeshRenderer _meshRenderer;
    private BoxCollider _collider;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        foreach (Brick brick in _bricks)
            brick.gameObject.SetActive(false);
    }

    public void Explode()
    {
        if (_isExploded)
            return;     

        _isExploded = true;

        Vector3 origin = GetAveragePosition();
        _meshRenderer.enabled = false;

        foreach (Brick brick in _bricks)
        {
            float randomForceY = Random.Range(_minForceY, _maxForceY);
            float randomForceZ = Random.Range(_minForceZ, _maxForceZ);
            float explosionPower = Random.Range(_minExplosionPower, _maxExplosionPower);
            Vector3 force = (brick.transform.position - origin).normalized + explosionPower * new Vector3(0f, randomForceY, randomForceZ);

            brick.gameObject.SetActive(true);
            brick.Break(force);
        }
    }

    private Vector3 GetAveragePosition()
    {
        Vector3 position = Vector3.zero;

        foreach (var brick in _bricks)
        {
            position += brick.transform.position;
        }

        position /= _bricks.Length;
        return position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Bullet bullet))
        {
            Explode();
            _collider.enabled = false;
        }
    }
}