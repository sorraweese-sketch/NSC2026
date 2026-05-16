using UnityEngine;

using UnityEngine;

public class GameUIController : MonoBehaviour
{
    // ตัวแปรสำหรับลาก Panel มาใส่
    public GameObject shopWindow;
    public GameObject bookWindow;

    // ฟังก์ชันเปิด-ปิดร้านค้า
    public void ToggleShop()
    {
        if (shopWindow != null)
        {
            shopWindow.SetActive(!shopWindow.activeSelf);
        }
    }

    // ฟังก์ชันเปิด-ปิดหนังสือ
    public void ToggleBook()
    {
        if (bookWindow != null)
        {
            bookWindow.SetActive(!bookWindow.activeSelf);
        }
    }
}