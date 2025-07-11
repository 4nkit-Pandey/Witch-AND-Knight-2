using UnityEngine; // 👈 This line is required

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public bool isActivePlayer = false;

    float xRotation = 0f;

    void Start()
    {
        if (isActivePlayer)
            Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (!isActivePlayer) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
