using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float zoomSpeed = 10f;

    void Update()
    {
        Vector2 moveInput = Vector2.zero;

        // WASD
        if (Keyboard.current.wKey.isPressed)
            moveInput.y += 1;

        if (Keyboard.current.sKey.isPressed)
            moveInput.y -= 1;

        if (Keyboard.current.aKey.isPressed)
            moveInput.x -= 1;

        if (Keyboard.current.dKey.isPressed)
            moveInput.x += 1;

        Vector3 move =
            new Vector3(moveInput.x, 0, moveInput.y);

        transform.position +=
            move.normalized *
            moveSpeed *
            Time.deltaTime;

        // Scroll Zoom
        float scroll =
            Mouse.current.scroll.ReadValue().y;

        transform.position +=
            transform.forward *
            scroll *
            zoomSpeed *
            Time.deltaTime;
    }
}