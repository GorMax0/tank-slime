using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(SpringJoint))]
public class Bone : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private SphereCollider _sphereCollider;
    private SpringJoint[] _springJoints;
    private float _damping = 0.95f;
    private bool _isStopped;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _sphereCollider = GetComponent<SphereCollider>();
        _springJoints = GetComponents<SpringJoint>();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Ground ground))
        {
            foreach (var joint in _springJoints)
            {
                joint.spring = 10f;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Ground ground))
        {
            foreach (var joint in _springJoints)
            {
                joint.spring = 150f;
            }
        }
    }
    public void SetColliderSize(float radius)
    {
        if (radius <= 0)
            _sphereCollider.radius = 0.001f;
        else
            _sphereCollider.radius = radius;
    }

    public void SetAngularDrag(float angularDrag)
    {
        if (angularDrag < 0)
            _rigidbody.angularDrag = 0;
        else
            _rigidbody.angularDrag = angularDrag;
    }

    public void SettingJoint(Rigidbody bone, float spring, float damper)
    {
        if (bone == null)
            throw new ArgumentNullException(nameof(bone));

        foreach (var joint in _springJoints)
        {
            if (joint.connectedBody == null)
            {
                joint.connectedBody = bone;
                joint.spring = spring;
                joint.damper = damper;
                return;
            }
        }
    }

    public void StopMovement(bool isStopped)
    {
      //  _isStopped = isStopped;
    }
}