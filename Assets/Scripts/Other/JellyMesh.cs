using UnityEngine;

public class JellyMesh : MonoBehaviour
{
    [SerializeField] private TankMesh _tankMesh;
    [SerializeField] private float _intensity = 1f;
    [SerializeField] private float _mass = 1f;
    [SerializeField] private float _stiffness = 1f;
    [SerializeField] private float _damping = 0.75f;

    private Mesh _originalMesh;
    private Mesh _meshClone;
    private SkinnedMeshRenderer _skinnedMeshRenderer;
    private JellyVertex[] _vertices;
    private Vector3[] _verticesArray;

    private void Start()
    {
        _skinnedMeshRenderer = _tankMesh.GetComponent<SkinnedMeshRenderer>();
        _originalMesh = _skinnedMeshRenderer.sharedMesh;
        _meshClone = _originalMesh;
        _skinnedMeshRenderer.sharedMesh = _meshClone;

        _vertices = new JellyVertex[_meshClone.vertices.Length];

        for (int i=0; i < _meshClone.vertices.Length; i++)
            _vertices[i] = new JellyVertex(i, transform.TransformPoint(_meshClone.vertices[i]));

    }

    private void FixedUpdate()
    {
        _verticesArray = _originalMesh.vertices;

        for (int i = 0; i < _vertices.Length; i++)
        {
            Vector3 target = transform.TransformPoint(_verticesArray[_vertices[i].Id]);
            float intensity = (1 - (_skinnedMeshRenderer.bounds.max.y - target.y) / _skinnedMeshRenderer.bounds.size.y) * _intensity;

            _vertices[i].Shake(target, _mass, _stiffness, _damping);
            target = transform.InverseTransformPoint(_vertices[i].Position);
            _verticesArray[_vertices[i].Id] = Vector3.Lerp(_verticesArray[_vertices[i].Id],target,intensity);
        }

        _meshClone.vertices = _verticesArray;
    }
}
