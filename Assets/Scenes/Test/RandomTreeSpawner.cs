using System.Collections;
using UnityEngine;

public class RandomTreeSpawner : MonoBehaviour
{
    [Header("ใส่ต้นไม้ 3 ชนิด (กล่องสีฟ้า)")]
    public GameObject[] treePrefabs;

    [Header("ตั้งค่าการเกิดต่อเนื่อง")]
    public float spawnInterval = 3.0f; // เกิดใหม่ทุกๆ กี่วินาที (ปรับเปลี่ยนได้ตามใจชอบ)
    public int maxTrees = 300;         // จำกัดจำนวนต้นไม้สูงสุดในแมพ ไม่ให้เยอะเกินจนเกมกระตุก

    [Header("ขอบเขตพื้นที่ในแมพ (กว้าง x ยาว)")]
    public float mapSize = 400f;
    public float yOffset = 0f;

    private int currentTreeCount = 0; // ตัวนับจำนวนต้นไม้ปัจจุบัน

    void Start()
    {
        if (Terrain.activeTerrain == null)
        {
            Debug.LogError("หาพื้นดิน (Terrain) ไม่เจอ!");
            return;
        }

        // สั่งให้เริ่มทำงานระบบเกิดเรื่อยๆ (Coroutine)
        StartCoroutine(SpawnTreesLoop());
    }

    // ฟังก์ชันนี้จะวนลูปทำงานไปเรื่อยๆ ตลอดทั้งเกม
    IEnumerator SpawnTreesLoop()
    {
        //Loop นี้จะทำงานไปเรื่อยๆ ตราบใดที่เกมยังเล่นอยู่ และต้นไม้ยังไม่เกินจำนวนสูงสุด
        while (currentTreeCount < maxTrees)
        {
            SpawnOneTree(); // สั่งสร้างต้นไม้ 1 ต้น

            // สั่งให้ระบบ "รอ" ตามเวลาที่ตั้งไว้ในช่อง spawnInterval ก่อนจะเริ่มสร้างต้นต่อไป
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnOneTree()
    {
        // 1. สุ่มพิกัด X และ Z
        float x = Random.Range(0, mapSize);
        float z = Random.Range(0, mapSize);

        // 2. หาความสูงของพื้นดินตรงจุดนั้น
        float y = Terrain.activeTerrain.SampleHeight(new Vector3(x, 0, z));

        // 3. สุ่มเลือกต้นไม้จาก Array (แคปซูล, กล่อง, วงกลม)
        int randomIndex = Random.Range(0, treePrefabs.Length);

        // 4. ตั้งค่าตำแหน่งและมุมหมุนให้ตั้งตรง
        Vector3 spawnPos = new Vector3(x, y + yOffset, z);
        Quaternion spawnRot = Quaternion.Euler(0, Random.Range(0, 360), 0);

        // 5. สั่งสร้างต้นไม้ขึ้นมา
        GameObject tree = Instantiate(treePrefabs[randomIndex], spawnPos, spawnRot);

        // เก็บเข้ากลุ่มสปาว์นเนอร์
        tree.transform.parent = this.transform;

        // นับจำนวนต้นไม้เพิ่มขึ้น 1 ต้น
        currentTreeCount++;
    }
}