using UnityEngine;
using System.Collections.Generic;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab;

    public int maxTrees = 6;
    public float spawnInterval = 5f;

    public float forestRadius = 10f;
    public float minDistance = 2.5f;

    private float timer = 0f;
    private int spawnedCount = 0;

    private List<Vector3> spawnedPositions = new List<Vector3>();

    void Update()
    {
        if (spawnedCount >= maxTrees) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            TrySpawnTree();
        }
    }

    void TrySpawnTree()
    {
        Vector3 center = transform.position;

        int safety = 0;

        while (safety < 20)
        {
            safety++;

            Vector2 rand = Random.insideUnitCircle * forestRadius;

            Vector3 pos = new Vector3(
                center.x + rand.x,
                center.y,
                center.z + rand.y
            );

            bool tooClose = false;

            foreach (Vector3 p in spawnedPositions)
            {
                if (Vector3.Distance(p, pos) < minDistance)
                {
                    tooClose = true;
                    break;
                }
            }

            if (tooClose) continue;

            Instantiate(treePrefab, pos, Quaternion.identity);
            spawnedPositions.Add(pos);

            spawnedCount++;
            break;
        }
    }
}