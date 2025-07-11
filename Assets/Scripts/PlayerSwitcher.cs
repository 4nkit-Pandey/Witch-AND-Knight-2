using UnityEngine;

public class PlayerSwitcher : MonoBehaviour
{
    public GameObject fpsController;  // WitchFPS
    public GameObject knightFPS;      // KnightFPS

    public void SwitchToKnight()
    {
        // Step 1: Get all components
        PlayerMovement witchMove = fpsController.GetComponentInChildren<PlayerMovement>();
        MouseLook witchLook = fpsController.GetComponentInChildren<MouseLook>();

        PlayerMovement knightMove = knightFPS.GetComponentInChildren<PlayerMovement>();
        MouseLook knightLook = knightFPS.GetComponentInChildren<MouseLook>();

        // Step 2: Deactivate Witch FPS controller
        witchMove.isActivePlayer = false;
        witchLook.isActivePlayer = false;

        // Step 3: Activate Knight FPS controller
        knightFPS.SetActive(true);  // Make sure the GameObject is on

        knightMove.isActivePlayer = true;
        knightLook.isActivePlayer = true;

        // Optional: Deactivate Witch entirely (optional, not required)
        fpsController.SetActive(false);

        // Step 4: Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
