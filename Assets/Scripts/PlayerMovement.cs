using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 12f;
    public CharacterController controller;
    public bool isActivePlayer = true;
    public Transform cameraTransform;       // Drag PlayerCamera or KnightCam
    public Animator modelAnimator;          // Drag WitchModel or KnightModel (the FBX with Animator)

    void Update()
    {
        if (!isActivePlayer) return;

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 moveDir;

        // Use camera-relative movement
        if (cameraTransform != null)
        {
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            moveDir = (right * x + forward * z).normalized;
        }
        else
        {
            moveDir = transform.right * x + transform.forward * z;
        }

        controller.Move(moveDir * speed * Time.deltaTime);

        // ✅ ANIMATION FIX: only set isWalking when actual input
        if (modelAnimator != null)
        {
            bool isWalking = new Vector2(x, z).magnitude > 0.1f;
            modelAnimator.SetBool("isWalking", isWalking);
        }
    }
}
