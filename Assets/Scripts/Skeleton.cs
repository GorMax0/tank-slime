using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    [SerializeField] private Bone[] _edgesL;
    [SerializeField] private Bone[] _edgesR;
    [SerializeField] private Bone[] _middlesL;
    [SerializeField] private Bone[] _middles;
    [SerializeField] private Bone[] _middlesR;
    [SerializeField] private Bone[] _sidesL;
    [SerializeField] private Bone[] _sidesR;
    [SerializeField] private Bone _muzzle;
    [SerializeField] private Rigidbody _rigidbodyRootBone;
    [Header("Spring Joint Settings")]
    [SerializeField] private float _minSpring;
    [SerializeField] private float _maxSpring;
    [SerializeField] private float _damper;
    [Header("Other Settings")]
    [SerializeField, Range(0.002f, 0.7f)] private float _collidarRadius;
    [SerializeField] private float _angularDrag = 10f;
    [SerializeField] private PlayerController _playerController;

    private List<Bone[]> _allBones = new List<Bone[]>();

    private void Awake()
    {
        _allBones.Add(_sidesL);
        _allBones.Add(_edgesL);
        _allBones.Add(_middlesL);
        _allBones.Add(_middles);
        _allBones.Add(_middlesR);
        _allBones.Add(_edgesR);
        _allBones.Add(_sidesR);
    }

    private void OnEnable()
    {
        _playerController.Stopped += OnStopped;
    }

    private void OnDisable()
    {
        _playerController.Stopped -= OnStopped;
    }

    private void Start()
    {
        CreateJoints();
    }

    private void CreateJoints()
    {
        for (int i = 0; i < _allBones.Count; i++)
        {
            for (int j = 0; j < _allBones[i].Length; j++)
            {
                _allBones[i][j].Init(_minSpring, _maxSpring, _collidarRadius, _angularDrag);
                _allBones[i][j].SetupConfiurableJoint(_rigidbodyRootBone);

                if (i < _allBones.Count - 1)
                {
                    _allBones[i][j].SettingJoint(_allBones[i + 1][j].GetComponent<Rigidbody>(), _damper);

                    if (i > 0)
                        _allBones[i][j].SettingJoint(_allBones[i - 1][j].GetComponent<Rigidbody>(), _damper);
                    else
                        _allBones[i][j].SettingJoint(_allBones[_allBones.Count - 1][j].GetComponent<Rigidbody>(), _damper);
                }
                else
                {
                    _allBones[i][j].SettingJoint(_allBones[_allBones.Count - 1 - i][j].GetComponent<Rigidbody>(), _damper);
                    _allBones[i][j].SettingJoint(_allBones[i - 1][j].GetComponent<Rigidbody>(), _damper);
                }
            }
        }
    }

    private void OnStopped(bool isStopped)
    {
        _muzzle.StopMovement(isStopped);

        foreach (Bone[] bones in _allBones)
        {
            for (int i = 0; i < bones.Length; i++)
                bones[i].StopMovement(isStopped);
        }
    }
}