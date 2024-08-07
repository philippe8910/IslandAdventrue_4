using UnityEngine;

public class UpdateMeshCollider : MonoBehaviour
{
    public MeshFilter oceanMeshFilter; // 需要將海洋的 MeshFilter 連結到這個變數
    public MeshCollider oceanMeshCollider; // 需要將海洋的 MeshCollider 連結到這個變數

    void Start()
    {
        // 初始化時確保 MeshCollider 使用正確的網格
        oceanMeshCollider.sharedMesh = oceanMeshFilter.sharedMesh;
    }

    void Update()
    {
        // 更新海洋網格
        UpdateOceanMesh();

        // 每次更新後重新分配 MeshCollider 的網格
        oceanMeshCollider.sharedMesh = null; // 先設為 null 以強制刷新
        oceanMeshCollider.sharedMesh = oceanMeshFilter.mesh;
    }

    void UpdateOceanMesh()
    {
        // 這裡是你的海洋網格更新邏輯
        // 確保 oceanMeshFilter.mesh 已經被更新

        Mesh mesh = oceanMeshFilter.mesh;
        // 示例更新，實際更新應根據你的海洋波浪邏輯
        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = Mathf.Sin(Time.time + vertices[i].x) * 0.5f;
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        oceanMeshFilter.mesh = mesh;
    }
}
