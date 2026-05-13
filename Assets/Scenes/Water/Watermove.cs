using UnityEngine;

public class WaterMove : MonoBehaviour
{
    void Update()
    {
        // ทำให้หน้าไหลไปเรื่อยๆ ตามเวลา
        float speed = 0.1f;
        float offset = Time.time * speed;

        // ใช้คำสั่งนี้เพื่อเลื่อน Offset ของ Texture
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offset, 0);
    }
}