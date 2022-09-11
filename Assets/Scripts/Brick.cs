using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Brick : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Coroutine _lerpRotation;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Break(Vector3 force)
    {
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(force, ForceMode.VelocityChange);

        StartLerpRotation();
    }

    private void StartLerpRotation()
    {
        if (_lerpRotation != null)
            StopCoroutine(_lerpRotation);

        _lerpRotation = StartCoroutine(LerpRotation());
    }

    private IEnumerator LerpRotation()
    {
        float minAngle = -15f;
        float maxAngle = 45f;
        float rotationAngle = Random.Range(minAngle, maxAngle);

        float rotationTime = 1f;
        float stepRotation = 0.1f;
        float currentAngleRotation;

        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

        while (rotationTime > 0)
        {
            currentAngleRotation = Mathf.Lerp(transform.rotation.x, rotationAngle, stepRotation);
            transform.Rotate(new Vector3(currentAngleRotation, 0, 0), Space.Self);

            rotationTime -= Time.fixedDeltaTime;

            yield return waitForFixedUpdate;
        }
    }
}