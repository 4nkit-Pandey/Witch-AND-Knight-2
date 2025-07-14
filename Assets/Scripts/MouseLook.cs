using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;       // This should be the rotating parent, e.g., Knight or FPS capsule
    public bool isActivePlayer = true;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (playerBody == null)
        {
            Debug.LogWarning("MouseLook: playerBody not assigned!");
        }
    }

    void Update()
    {
        if (!isActivePlayer || playerBody == null) return;

        // Mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Clamp vertical rotation (camera up/down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal rotation (body left/right)
        playerBody.Rotate(Vector3.up * mouseX);

        // ✅ Optional: Lock rotation axis to prevent tilting
        Vector3 clampedEuler = playerBody.eulerAngles;
        playerBody.rotation = Quaternion.Euler(0f, clampedEuler.y, 0f);
    }
}
