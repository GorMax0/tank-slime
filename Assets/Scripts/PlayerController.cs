using UnityEngine;
using UnityEngine.EventSystems;
using Dreamteck.Splines;

[RequireComponent(typeof(SplineFollower))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private ControlButton _buttonForward;
    [SerializeField] private ControlButton _buttonBackward;
    [SerializeField] private ControlButton _buttonFire;

    private SplineFollower _follower;

    private void Awake()
    {
        _follower = GetComponent<SplineFollower>();
    }

    private void OnEnable()
    {
        _buttonForward.PointerDown += Move;
        _buttonForward.PointerUp += Move;
        _buttonBackward.PointerDown += Move;
        _buttonBackward.PointerUp += Move;
    }

    private void OnDisable()
    {
        _buttonForward.PointerDown -= Move;
        _buttonForward.PointerUp -= Move;
        _buttonBackward.PointerDown -= Move;
        _buttonBackward.PointerUp -= Move;
    }

    public void Move(bool isClick, Spline.Direction direction)
    {
        if (_follower.direction != direction)
            _follower.direction = direction;

        _follower.follow = isClick;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Obstacle obstacle))
            _follower.follow = false;
    }
}
