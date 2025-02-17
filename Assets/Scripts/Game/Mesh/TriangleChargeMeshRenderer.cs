using System;
using UnityEngine;

namespace Game.Mesh
{
    [RequireComponent(typeof(MeshFilter))]
    public class TriangleChargeMeshRenderer : ChargeMeshRenderer
    {
        [SerializeField] private float _width = 0.5f;
        [SerializeField] private float _minLength = 0.1f;
        [SerializeField] private Vector3 _planeNormal = Vector3.up;
        
        private Vector3[] _vertices = new Vector3[3];
        private int[] _triangles = { 0, 1, 2 };
        private Vector2[] _uvs = {
            new(0.5f, 1f),
            new(0f, 0f),
            new(1f, 0f)
        };
        
        private UnityEngine.Mesh _mesh;

        private void Awake()
        {
            _mesh = new UnityEngine.Mesh(); 
            _mesh.vertices = _vertices;
            _mesh.triangles = _triangles;
            _mesh.uv = _uvs;
            
            GetComponent<MeshFilter>().mesh =_mesh;
        }

        public override void OnChargeChange(Vector3 startPoint, Vector3 endPoint)
        {
            RebuildMesh(startPoint, endPoint);
        }

        public override void Reset()
        {
            if (_mesh)
            {
                _mesh.Clear();
            }
        }

        private void RebuildMesh(Vector3 startPoint, Vector3 endPoint)
        {
            float length = Vector3.Distance(startPoint, endPoint);
            
            if (length < _minLength)
            {
                _mesh.Clear();
                return;
            }
            
            Vector3 direction = (endPoint - startPoint).normalized;
            
            Vector3 tip = direction * length;
            Vector3 baseLine = Vector3.Cross(direction, Vector3.up).normalized * _width / 2f;
            
            _vertices[2] = tip;
            _vertices[1] = baseLine;
            _vertices[0] = -baseLine;
            
            float uvScale = 1.0f / length;
            _uvs[0] = new Vector2(0.5f, 1f) * uvScale;
            _uvs[1] = new Vector2(0f, 0f) * uvScale;
            _uvs[2] = new Vector2(1f, 0f) * uvScale;
            
            _mesh.Clear();
            _mesh.vertices = _vertices;
            _mesh.triangles = _triangles;
            _mesh.uv = _uvs;
            _mesh.RecalculateNormals();
        }
    }
}