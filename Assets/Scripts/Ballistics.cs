using UnityEngine;

public class Ballistics
{
    public Vector3 TrajectoryCalculation(Transform shootPoint, Vector3 targetPosition)
    {
        const float AngleInDegrees = 45f;
        const float HalfCircleInDegrees = 180f;
        const int Multiplier = 2;

        Vector3 direction = targetPosition - shootPoint.position;
        Vector3 directionWithoutHeight = new Vector3(direction.x, 0f, direction.z);

        float directionLength = directionWithoutHeight.magnitude;
        float height = direction.y;
        float angleInRadians = AngleInDegrees * Mathf.PI / HalfCircleInDegrees;

        float speedSquare = (Physics.gravity.y * directionLength * directionLength) / (Multiplier * (height - Mathf.Tan(angleInRadians) * directionLength) * Mathf.Pow(Mathf.Cos(angleInRadians), Multiplier));
        float speed = Mathf.Sqrt(Mathf.Abs(speedSquare));
        Debug.Log($"Vector {shootPoint.forward * speed}");

        return shootPoint.forward * speed;
    }   
}