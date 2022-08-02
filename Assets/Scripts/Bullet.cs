using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 shootPoint, Vector3 target)
    {
        transform.position = shootPoint;

        Vector3 directionToTarget = target - transform.position;
        Vector3 directionByXZ = new Vector3(directionToTarget.x, 0f, directionToTarget.z);

        float distanceInDirectLine = directionByXZ.magnitude;
        float heigth = directionToTarget.y;
        float gravity = Physics.gravity.y;
        float angleInRadians = 45 * Mathf.PI / 180;

        int degree = 2;
        float trajectory = Mathf.Sqrt(Mathf.Abs((gravity * Mathf.Pow(distanceInDirectLine, degree) / (degree * (heigth - Mathf.Tan(angleInRadians)) * distanceInDirectLine) * Mathf.Pow(Mathf.Cos(angleInRadians), degree))));

        _rigidbody.velocity = transform.forward * trajectory;
    }
}
