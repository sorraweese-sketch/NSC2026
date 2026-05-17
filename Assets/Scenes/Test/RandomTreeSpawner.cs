using System.Collections;
using UnityEngine;

public class RandomTreeSpawner : MonoBehaviour
{
    [Header("ใส่ต้นไม้ 3 ชนิด (กล่องสีฟ้า)")]
    public GameObject[] treePrefabs;

    [Header("ตั้งค่าการเกิดต่อเนื่อง")]
    public float spawnInterval = 2.0f; // เกิดใหม่ทุกๆ กี่วินาที
    public int maxTrees = 100;         // จำกัดจำนวนต้นไม้สูงสุด

    [Header("ขอบเขตพื้นที่วงกลม (ปรับแต่งตรงนี้)")]
    public Transform centerPoint;      // วัตถุที่เป็นจุดศูนย์กลางของวงกลมในรั้ว
    public float maxRadius = 50f;      // รัศมีของวงกลม (ความกว้างจากจุดศูนย์กลางไปถึงรั้ว)
    public float yOffset = 0.2f;       // ระยะยกตัวเหนือพื้นเพื่อไม่ให้จมดิน

    private int currentTreeCount = 0;

    void Start()
    {
        if (Terrain.activeTerrain == null)
        {
            Debug.LogError("หาพื้นดิน (Terrain) ไม่เจอ!");
            return;
        }

        // ถ้าไม่ได้ลากวัตถุศูนย์กลางมาใส่ ให้ใช้ตำแหน่งของตัว Spawner เองเป็นจุดศูนย์กลาง
        if (centerPoint == null)
        {
            centerPoint = this.transform;
        }

        StartCoroutine(SpawnTreesLoop());
    }

    IEnumerator SpawnTreesLoop()
    {
        while (currentTreeCount < maxTrees)
        {
            SpawnOneTreeInCircle();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnOneTreeInCircle()
    {
        // 1. ใช้สูตรสุ่มพิกัดให้สร้างเฉพาะภายในรัศมีวงกลม (Inside Unit Circle)
        Vector2 randomCirclePoint = Random.insideUnitCircle * maxRadius;

        // 2. คำนวณพิกัด X และ Z โดยอิงจากตำแหน่งจุดศูนย์กลางที่เราตั้งไว้
        float x = centerPoint.position.x + randomCirclePoint.x;
        float z = centerPoint.position.z + randomCirclePoint.y;

        // 3. หาความสูงของพื้นดินตรงจุดนั้น
        float y = Terrain.activeTerrain.SampleHeight(new Vector3(x, 0, z));

        // 4. สุ่มเลือกต้นไม้จากในรายการ
        int randomIndex = Random.Range(0, treePrefabs.Length);

        // 5. กำหนดตำแหน่งและมุมหมุนให้ตั้งตรง 180 องศา สุ่มหมุนรอบตัวแค่แกน Y
        Vector3 spawnPos = new Vector3(x, y + yOffset, z);
        Quaternion spawnRot = Quaternion.Euler(0, Random.Range(0, 360), 0);

        // 6. สั่งสร้างต้นไม้
        GameObject tree = Instantiate(treePrefabs[randomIndex], spawnPos, spawnRot);

        // เก็บเข้ากลุ่มสปาว์นเนอร์
        tree.transform.parent = this.transform;

        currentTreeCount++;
    }

    // วาดเส้นวงกลมจำลองสีเขียวในหน้า Scene เพื่อให้คุณกะขนาดรั้วได้ง่ายขึ้น (ไม่แสดงในหน้าเกมจริง)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 center = centerPoint != null ? centerPoint.position : transform.position;
        Gizmos.DrawWireSphere(center, maxRadius);
    }
}