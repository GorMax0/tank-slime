using UnityEngine;
using Dreamteck.Splines;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private ShotZone _shotZone;
    [SerializeField] private Rigidbody _mover;
    [Header("Control Buttons")]
    [SerializeField] private MoverButton _buttonForward;
    [SerializeField] private MoverButton _buttonBackward;

    private SplineFollower _follower;

    private void Awake()
    {
        _follower = GetComponent<SplineFollower>();
    }

    private void OnEnable()
    {
        _buttonForward.Clicked += Move;
        _buttonBackward.Clicked += Move;
        _shotZone.Clicked += Shoot;
    }

    private void OnDisable()
    {
        _buttonForward.Clicked -= Move;
        _buttonBackward.Clicked -= Move;
        _shotZone.Clicked -= Shoot;
    }

    private void FixedUpdate()
    {
        _mover.velocity = new Vector3(0f, 0f, 1.5f);
      //  _mover.rotation *= Quaternion.Euler(_x, 0f, 0f);
    }

    private void Move(bool isClick, Spline.Direction direction)
    {


    }
    //private void Move(bool isClick, Spline.Direction direction)
    //{
    //    if (_follower.direction != direction)
    //        _follower.direction = direction;

    //    _follower.follow = isClick;       
    //}

    private void Shoot(Vector3 target)
    {
        _bulletPool.InvokeBullet(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out StopPoint stopPoint))
            _follower.follow = false;
    }
}
