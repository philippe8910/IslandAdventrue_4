using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureSpawnPoint : MonoBehaviour
{
    public List<GameObject> prefabList; // 存储 Prefab 列表
    public List<Transform> spawnPoints; // 存储生成点列表
    private int spawnCounter = 0; // 生成计数器
    public float moveSpeed = 2f; // 移动速度

    private void Start()
    {
        StartCoroutine(SpawnPrefabCoroutine()); // 启动协程
    }

    // 协程，用于每两秒生成一个 Prefab
    private IEnumerator SpawnPrefabCoroutine()
    {
        while (true) // 无限循环
        {
            yield return new WaitForSeconds(1f); // 等待2秒

            SpawnPrefab(); // 调用生成方法
        }
    }

    // 方法用于生成 Prefab
    public void SpawnPrefab()
    {
        if (spawnPoints.Count == 0 || prefabList.Count == 0)
        {
            Debug.LogWarning("Prefab list or spawn point list is empty.");
            return;
        }

        // 每 3 次生成一个 Prefab
        if (spawnCounter % 3 == 0)
        {
            // 随机选择一个 Prefab 和一个生成点
            GameObject prefabToSpawn = prefabList[Random.Range(0, prefabList.Count)];
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

            // 在 (spawnPoint.y - 5) 的位置生成 Prefab
            Vector3 spawnPosition = new Vector3(spawnPoint.position.x, spawnPoint.position.y - 5, spawnPoint.position.z);
            GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, spawnPoint.rotation);
            Debug.Log("Spawned: " + prefabToSpawn.name + " at " + spawnPosition);

            // 启动移动到生成点的协程
            StartCoroutine(MoveToSpawnPoint(spawnedObject, spawnPoint.position));
        }

        spawnCounter++;
    }

    // 协程，用于移动生成的物体到目标位置
    private IEnumerator MoveToSpawnPoint(GameObject obj, Vector3 targetPosition)
    {
        while (obj.transform.position.y < targetPosition.y)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null; // 等待下一帧
        }
    }
}
