using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _mover;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _turnSpeed;
    [Header("Control Buttons")]
    [SerializeField] private MoverButton _buttonForward;
    [SerializeField] private MoverButton _buttonBackward;

    private Vector3 _direction;
    private Vector3 _rotationDirection;
    private bool _canMove;
    private bool _isStopped;

    public event UnityAction<bool> Stopped;

    private void OnEnable()
    {
        _buttonForward.Moved += OnMove;
        _buttonBackward.Moved += OnMove;
    }

    private void OnDisable()
    {
        _buttonForward.Moved -= OnMove;
        _buttonBackward.Moved -= OnMove;
    }

    private void FixedUpdate()
    {
        if (_canMove == true)
        {
              transform.Translate(_direction * _moveSpeed * Time.fixedDeltaTime);
            _mover.transform.rotation *= Quaternion.AngleAxis(_turnSpeed * Time.fixedDeltaTime, _rotationDirection);
          //  _mover.MovePosition(_mover.position + _direction * _moveSpeed * Time.fixedDeltaTime);
            _mover.isKinematic = false;
        }
        else if (_isStopped == false)
        {
            _mover.velocity = Vector3.zero;
            _mover.isKinematic = true;
            _isStopped = true;
        }
    }

    private void OnMove(bool canMove, bool isMoveForward)
    {
        _canMove = canMove;
        _direction = isMoveForward ? Vector3.forward : -Vector3.forward;
        _rotationDirection = isMoveForward ? -Vector3.left : Vector3.left;
        _isStopped = false;

        Stopped?.Invoke(_isStopped);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out StopPoint stopPoint))
        {
            _canMove = false;
            Debug.Log("Stop!");
        }
    }
}