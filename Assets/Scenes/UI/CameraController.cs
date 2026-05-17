using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Speed")]
    public float lookSpeed = 0.2f;     // ความเร็วในการหมุนกล้อง
    public float moveSpeed = 10.0f;    // ความเร็วในการเลื่อนกล้อง

    [Header("Map Boundaries (ตั้งค่าขอบเขตแมพตรงนี้)")]
    public float minX = 0f;       // ขอบซ้ายสุดของแมพ
    public float maxX = 500f;     // ขอบขวาสุดของแมพ
    public float minZ = 0f;       // ขอบล่างสุดของแมพ
    public float maxZ = 500f;     // ขอบบนสุดของแมพ

    [Header("Height Boundaries (ตั้งค่าความสูง)")]
    public float minHeight = 2f;   // ความสูงต่ำสุดจากพื้นดิน (กันกล้องมุดดิน)
    public float maxHeight = 50f;  // ความสูงสูงสุดที่กล้องบินขึ้นไปได้

    private float rotationX = 0;
    private float rotationY = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 1. ระบบหมุนมุมกล้องด้วยเมาส์ (ระบบ New Input System)
        if (InputSystemAvailable())
        {
            Vector2 mouseDelta = UnityEngine.InputSystem.Pointer.current != null ?
                UnityEngine.InputSystem.Pointer.current.delta.ReadValue() : Vector2.zero;

            rotationY += mouseDelta.x * lookSpeed * 0.1f;
            rotationX -= mouseDelta.y * lookSpeed * 0.1f;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);

            transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);

            // 2. คำนวณการเคลื่อนที่ด้วยปุ่ม W, A, S, D
            Vector3 move = Vector3.zero;
            var keyboard = UnityEngine.InputSystem.Keyboard.current;
            if (keyboard != null)
            {
                if (keyboard.wKey.isPressed) move += transform.forward;
                if (keyboard.sKey.isPressed) move -= transform.forward;
                if (keyboard.aKey.isPressed) move -= transform.right;
                if (keyboard.dKey.isPressed) move += transform.right;
            }

            // ขยับตำแหน่งกล้อง
            transform.position += move * moveSpeed * Time.deltaTime;

            // 3. ระบบล็อกตำแหน่งไม่ให้ออกนอกแมพ และไม่ให้มุดดิน (Clamp Position)
            float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
            float clampedZ = Mathf.Clamp(transform.position.z, minZ, maxZ);

            // หาความสูงของพื้น Terrain ณ จุดที่กล้องอยู่ เพื่อไม่ให้กล้องมุดทะลุเนินเขาหรือพื้นดิน
            float currentTerrainHeight = Terrain.activeTerrain != null ?
                Terrain.activeTerrain.SampleHeight(transform.position) : 0f;

            // ล็อกความสูงให้อยู่เหนือพื้นดินเสมอตามค่า minHeight ที่ตั้งไว้
            float clampedY = Mathf.Clamp(transform.position.y, currentTerrainHeight + minHeight, currentTerrainHeight + maxHeight);

            // ส่งค่าที่ถูกล็อกแล้วกลับไปที่ตัวกล้อง
            transform.position = new Vector3(clampedX, clampedY, clampedZ);
        }
    }

    private bool InputSystemAvailable()
    {
#if ENABLE_INPUT_SYSTEM
        return true;
#else
        return false;
#endif
    }
}