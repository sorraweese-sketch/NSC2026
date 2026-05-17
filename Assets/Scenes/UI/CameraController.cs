using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float lookSpeed = 0.5f;     // ความเร็วในการหมุนกล้อง
    public float moveSpeed = 10.0f;    // ความเร็วในการเลื่อนกล้องขยับไปมา

    private float rotationX = 0;
    private float rotationY = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // ใช้คำสั่งระบบใหม่แทนการใช้ Input.GetAxis เดิม
        if (InputSystemAvailable())
        {
            // ดึงค่าการขยับเมาส์จากระบบ New Input System
            Vector2 mouseDelta = UnityEngine.InputSystem.Pointer.current != null ?
                UnityEngine.InputSystem.Pointer.current.delta.ReadValue() : Vector2.zero;

            rotationY += mouseDelta.x * lookSpeed * 0.1f;
            rotationX -= mouseDelta.y * lookSpeed * 0.1f;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);

            transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);

            // ดึงค่าปุ่มกด W, A, S, D หรือปุ่มลูกศรจากระบบใหม่
            Vector3 move = Vector3.zero;
            var keyboard = UnityEngine.InputSystem.Keyboard.current;
            if (keyboard != null)
            {
                if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) move += transform.forward;
                if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) move -= transform.forward;
                if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) move -= transform.right;
                if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) move += transform.right;
            }

            transform.position += move * moveSpeed * Time.deltaTime;
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