using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField, Range(1.5f, 10.5f)] private float _targetingRange = 1.5f;

    private Vector3 _targetPosition;
    private Rigidbody _rigidbody;
    private float _launchSpeed;

    void OnEnable()
    {
        OnValidate();
    }

    void OnValidate()
    {
        float x = _targetingRange + 0.251f;
        float y = -transform.position.y;
        _launchSpeed = Mathf.Sqrt(9.81f * (y + Mathf.Sqrt(x * x + y * y)));
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    public void Launch(Vector3 shootPoint, Quaternion rotation, Vector3 target)
    {
        transform.position = shootPoint;
        transform.rotation = rotation;
        _targetPosition = target;
        _rigidbody = GetComponent<Rigidbody>();
        Move();
    }

    private void Move()
    {
        //Vector2 dir;
        //dir.x = _targetPosition.x - transform.position.x;
        //dir.y = _targetPosition.z - transform.position.z;
        //float x = dir.magnitude;
        //float y = -transform.position.y;
        //dir /= x;

        //float g = 9.81f;
        //float s = _launchSpeed;
        //float s2 = s * s;

        //float r = s2 * s2 - g * (g * x * x + 2f * y * s2);
        //float tanTheta = (s2 + Mathf.Sqrt(r)) / (g * x);
        //float cosTheta = Mathf.Cos(Mathf.Atan(tanTheta));
        //float sinTheta = cosTheta * tanTheta;

        //Vector3 launchVelocity = new Vector3(s * cosTheta * dir.x, s * sinTheta, s * cosTheta * dir.y);

        //Vector3 d = launchVelocity;
        //d.y -= 9.81f * Time.fixedDeltaTime;
        //transform.localRotation = Quaternion.LookRotation(new Vector3(dir.x, tanTheta, dir.y));

        //Vector3 p = transform.position + launchVelocity * Time.fixedDeltaTime;
        //p.y -= 9f * 9.81f * Time.fixedDeltaTime * Time.fixedDeltaTime;
        //transform.localPosition = p;

        //Vector3 prev = transform.position, next;
        //for (int i = 1; i <= 10; i++)
        //{
        //    float t = i / 10f;
        //    float dx = s * cosTheta * t;
        //    float dy = s * sinTheta * t - 0.5f * g * t * t;
        //    next = transform.position + new Vector3(dir.x * dx, dy, dir.y * dx);
        //    Debug.DrawLine(prev, next, Color.blue);
        //    prev = next;
        //}

        //Vector3 directionToTarget = _targetPosition - transform.position;
        //Vector3 directionByXZ = new Vector3(0f, directionToTarget.y, directionToTarget.z);

        //float distanceInDirectLine = directionByXZ.magnitude;
        //float heigth = directionToTarget.y;
        //float gravity = Physics.gravity.y;
        //float angleInRadians = 45 * Mathf.PI / 180;

        //int degree = 2;
        //float trajectory = Mathf.Sqrt(Mathf.Abs((gravity * Mathf.Pow(distanceInDirectLine, degree) / (degree * (heigth - Mathf.Tan(angleInRadians)) * distanceInDirectLine) * Mathf.Pow(Mathf.Cos(angleInRadians), degree))));

        //_rigidbody.velocity = transform.forward * trajectory * 5f;

        //Debug.DrawLine(transform.position, _targetPosition, Color.yellow);

        _rigidbody.velocity = _targetPosition * 55f * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
