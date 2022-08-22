using UnityEngine;

public class Ballistics : MonoBehaviour
{
    [SerializeField] private ShootZone _shootZone;
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private ShootPoint _shootPoint;
    [SerializeField] private float _angleInDegrees;

    private void OnEnable()
    {
        _shootZone.Shooted += OnShoot;
    }

    private void OnDisable()
    {
        _shootZone.Shooted -= OnShoot;
    }

    private Vector3 TrajectoryCalculation(Vector3 targetPosition)
    {
        const float HalfCircleInDegrees = 180f;
        const int Multiplier = 2;

        Vector3 direction = targetPosition - transform.position;
        Vector3 directionWithoutHeight = new Vector3(direction.x, 0f, direction.z);

        float directionLength = directionWithoutHeight.magnitude;
        float height = direction.y;
        float angleInRadians = _angleInDegrees * Mathf.PI / HalfCircleInDegrees;

        float speedSquare = (Physics.gravity.y * directionLength * directionLength) / (Multiplier * (height - Mathf.Tan(angleInRadians) * directionLength) * Mathf.Pow(Mathf.Cos(angleInRadians), Multiplier));
        float speed = Mathf.Sqrt(Mathf.Abs(speedSquare));
        Debug.Log($"Vector {_shootPoint.transform.forward * speed}");

        return _shootPoint.transform.forward * speed;
    }

    private void OnShoot(Vector3 targetPosition)
    {
        Vector3 trajectory = TrajectoryCalculation(targetPosition);
        _bulletPool.InvokeBullet(_shootPoint, trajectory);
    }
}