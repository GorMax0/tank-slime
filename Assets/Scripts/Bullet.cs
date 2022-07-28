using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 _startPosition;

    private void OnEnable()
    {
        _startPosition = gameObject.transform.parent.position;
    }
}
