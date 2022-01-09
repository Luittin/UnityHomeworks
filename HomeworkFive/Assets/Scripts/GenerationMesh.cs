using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct MeshData
{
    [SerializeField] public Vector3 position;
    [SerializeField] public Vector3 normal;
    [SerializeField] public Vector2 uv;
}

public class GenerationMesh : MonoBehaviour
{
    [SerializeField]
    private MeshData[] _meshDatas;
    [SerializeField]
    private Vector3Int[] _triangles;
    [SerializeField]
    private MeshFilter _meshFilter;

    private Mesh _mesh;

    void Update()
    {
        UpdateMesh();
    }

    private void OnValidate()
    {
        UpdateMesh();
    }

    private void UpdateMesh()
    {
        _mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> uv = new List<Vector2>();

        foreach(MeshData meshData in _meshDatas)
        {
            vertices.Add(meshData.position);
            normals.Add(meshData.normal);
            uv.Add(meshData.uv);
        }

        List<int> triangles = new List<int>();

        foreach(Vector3Int triangle in _triangles)
        {
            triangles.Add(triangle.x);
            triangles.Add(triangle.y);
            triangles.Add(triangle.z);
        }

        _mesh.vertices = vertices.ToArray();
        _mesh.normals = normals.ToArray();
        _mesh.uv = uv.ToArray();
        _mesh.triangles = triangles.ToArray();
        _mesh.RecalculateBounds();
        _mesh.RecalculateTangents();
        _meshFilter.mesh = _mesh;
    }
}
