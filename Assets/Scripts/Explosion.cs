using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(BoxCollider))]
public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionPower;
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
        {
            return;
        }

        _isExploded = true;

        Vector3 origin = GetAveragePosition();
        _meshRenderer.enabled = false;

        foreach (Brick brick in _bricks)
        {
            var randomForce = Random.Range(0.3f, 1.5f);
            Vector3 force = (brick.transform.position - origin).normalized * _explosionPower + new Vector3(0f, randomForce, randomForce);

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