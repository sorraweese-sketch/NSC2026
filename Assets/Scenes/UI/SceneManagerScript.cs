using UnityEngine;
using UnityEngine.SceneManagement; // สำคัญมาก: ต้องมีบรรทัดนี้เพื่อสั่งเปลี่ยนหน้า

public class SceneManagerScript : MonoBehaviour
{
    // ฟังก์ชันนี้จะถูกเรียกเมื่อเรากดปุ่ม Play
    public void StartGame()
    {
        // เลข 1 คือลำดับของ Scene หน้าหลักที่มีแมพที่เราตั้งไว้ใน Build Settings
        SceneManager.LoadScene(1);
    }
}