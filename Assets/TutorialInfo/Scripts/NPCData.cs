using UnityEngine;

public class NPCData : MonoBehaviour
{
    [Header("Status")]

    public float health = 100f;

    public float hunger = 100f;

    public float thirst = 100f;

    public int age = 20;

    public int relationship = 50;

    [Header("Decrease Per Second")]

    // นาทีละ 1
    public float hungerDecrease = 0.0167f;

    // นาทีละ 2
    public float thirstDecrease = 0.0333f;

    // เลือดลดตอนใกล้ตาย
    public float healthDecrease = 5f;

    [Header("Profile")]

    public Texture profilePicture;

    void Update()
    {
        // ลดความหิว
        hunger -= hungerDecrease * Time.deltaTime;

        // ลดความกระหาย
        thirst -= thirstDecrease * Time.deltaTime;

        // จำกัดค่า
        hunger = Mathf.Clamp(hunger, 0, 100);

        thirst = Mathf.Clamp(thirst, 0, 100);

        // ถ้าหิวหรือกระหายน้ำจนหมด
        if (hunger <= 0 || thirst <= 0)
        {
            health -= healthDecrease * Time.deltaTime;
        }

        // จำกัดเลือด
        health = Mathf.Clamp(health, 0, 100);
    }
}