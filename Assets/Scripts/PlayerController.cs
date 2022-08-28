using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _mover;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private InputHandler _inputHandler;

    private Vector3 _direction;
    private Vector3 _rotationDirection;
    private bool _canMove;
    private bool _isStopped;

    public event UnityAction<bool> Stopped;

    private void OnEnable()
    {
        _inputHandler.Moved += OnMove;
    }

    private void OnDisable()
    {
        _inputHandler.Moved -= OnMove;
    }

    private void FixedUpdate()
    {
        if (_canMove == true)
        {
            transform.Translate(_direction * _moveSpeed * Time.fixedDeltaTime);
            _mover.transform.rotation *= Quaternion.AngleAxis(_turnSpeed * Time.fixedDeltaTime, _rotationDirection);
            _mover.isKinematic = false;
        }
        else if (_isStopped == false)
        {
            _mover.isKinematic = true;
            _isStopped = true;
        }
    }

    private void OnMove(bool canMove, Vector3 direction, Vector3 rotation)
    {
        _canMove = canMove;
        _direction = direction;
        _rotationDirection = rotation;
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