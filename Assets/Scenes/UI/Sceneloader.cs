using UnityEngine;
using UnityEngine.SceneManagement; // ตัวช่วยเรื่องเปลี่ยนหน้า

public class SceneLoader : MonoBehaviour
{
    // ฟังก์ชันสำหรับปุ่ม PLAY
    public void GoToGame()
    {
        SceneManager.LoadScene(1); // สั่งให้ไปหน้าลำดับที่ 1 (MainGame)
    }

    // ฟังก์ชันสำหรับปุ่ม SETTINGS (ถ้ามีหน้าแยก)
    public void GoToSettings()
    {
        // ถ้ามีหน้า Settings เป็นอีก Scene ให้ใส่เลขลำดับตรงนี้
        Debug.Log("เปิดหน้า Settings");
    }
}