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
        Vector3 fromTo = targetPosition - transform.position;
        Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

        float x = fromToXZ.magnitude;
        float y = fromTo.y;
        float angleInRadians = _angleInDegrees * Mathf.PI / 180;

        float v2 = (Physics.gravity.y * x * x) / (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));

        return _shootPoint.transform.forward * v;
    }

    private void OnShoot(Vector3 targetPosition)
    {
        Vector3 trajectory = TrajectoryCalculation(targetPosition);
        _bulletPool.InvokeBullet(_shootPoint, trajectory);
    }
}
