using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(SpringJoint))]
public class Bone : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private SphereCollider _sphereCollider;
    private SpringJoint[] _springJoints;
    private ConfigurableJoint _configurableJoint;
    private float _minSpring;
    private float _maxSpring;
    private float _damping = 0.90f;
    private bool _isStopped;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _sphereCollider = GetComponent<SphereCollider>();
        _springJoints = GetComponents<SpringJoint>();
        _configurableJoint = GetComponent<ConfigurableJoint>();
    }

    private void FixedUpdate()
    {
        if (_isStopped == false)
        {
            _rigidbody.velocity *= _damping;

            if (_rigidbody.velocity == Vector3.zero)
                _isStopped = true;
        }
    }

    public void Init(float minSpring,float maxSpring, float radius, float angularDrag)
    {
        if (_minSpring < 0)
            throw new ArgumentOutOfRangeException(nameof(_minSpring));

        if (_maxSpring < 0)
            throw new ArgumentOutOfRangeException(nameof(_maxSpring));

        _minSpring = minSpring;
        _maxSpring = maxSpring;
        SetColliderSize(radius);
        SetAngularDrag(angularDrag);

        _rigidbody.velocity = Vector3.zero;
    }

    public void SettingJoint(Rigidbody bone, float damper)
    {
        if (bone == null)
            throw new ArgumentNullException(nameof(bone));

        foreach (var joint in _springJoints)
        {
            if (joint.connectedBody == null)
            {
                joint.connectedBody = bone;
                joint.spring = _maxSpring;
                joint.damper = damper;
                return;
            }
        }
    }

    public void SetupConfiurableJoint(Rigidbody rootBone)
    {
        _configurableJoint.connectedBody = rootBone;
        _configurableJoint.axis = transform.InverseTransformDirection(transform.localPosition);
    }

    public void StopMovement(bool isStopped)
    {
        _isStopped = isStopped;
    }

    private void SetColliderSize(float radius)
    {
        if (radius <= 0)
            throw new ArgumentOutOfRangeException(nameof(radius));

        _sphereCollider.radius = radius;
    }

    private void SetAngularDrag(float angularDrag)
    {
        if (angularDrag < 0)
            throw new ArgumentOutOfRangeException(nameof(angularDrag));

        _rigidbody.angularDrag = angularDrag;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Ground ground))
        {
            foreach (var joint in _springJoints)
            {
                joint.spring = _minSpring;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Ground ground))
        {
            foreach (var joint in _springJoints)
            {
                joint.spring = _maxSpring;
            }
        }
    }
}