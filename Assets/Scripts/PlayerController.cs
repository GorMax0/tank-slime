using UnityEngine;
using UnityEngine.EventSystems;
using Dreamteck.Splines;

[RequireComponent(typeof(SplineFollower))]
public class PlayerController : MonoBehaviour
{
    private SplineFollower _follower;

    private void Awake()
    {
        _follower = GetComponent<SplineFollower>();
    }

    public void MoveForward()
    {
        if (_follower.direction != Spline.Direction.Forward)
            _follower.direction = Spline.Direction.Forward;

        _follower.follow = true;
    }

    public void MoveBackward()
    {
        if (_follower.direction != Spline.Direction.Backward)
            _follower.direction = Spline.Direction.Backward;

        _follower.follow = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<Obstacle>(out Obstacle obstacle))
            _follower.follow = false;
    }
}
