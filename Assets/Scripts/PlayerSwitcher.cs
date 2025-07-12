using UnityEngine;

public class PlayerSwitcher : MonoBehaviour
{
    public GameObject fpsController;   // WitchFPS
    public GameObject knightFPS;       // KnightFPS

    public Camera witchCamera;         // Drag Witch Camera here
    public Camera knightCamera;        // Drag Knight Camera here

    public void SwitchToKnight()
    {
        // Disable Witch control scripts
        PlayerMovement witchMove = fpsController.GetComponentInChildren<PlayerMovement>();
        MouseLook witchLook = fpsController.GetComponentInChildren<MouseLook>();
        witchMove.isActivePlayer = false;
        witchLook.isActivePlayer = false;

        // Enable Knight GameObject if needed
        knightFPS.SetActive(true);

        // Enable Knight control scripts
        PlayerMovement knightMove = knightFPS.GetComponentInChildren<PlayerMovement>();
        MouseLook knightLook = knightFPS.GetComponentInChildren<MouseLook>();
        knightMove.isActivePlayer = true;
        knightLook.isActivePlayer = true;

        // ✅ Switch cameras
        witchCamera.enabled = false;
        knightCamera.enabled = true;

        // Optional: Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
