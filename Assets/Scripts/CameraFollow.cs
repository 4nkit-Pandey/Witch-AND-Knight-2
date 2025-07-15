using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;         // The target to follow (e.g., head or camera mount on model)
    public Vector3 positionOffset;   // Local offset from target
    public Vector3 rotationOffset;   // Optional: Euler rotation offset
    public bool matchRotation = true;

    void LateUpdate()
    {
        if (target == null) return;

        // Position
        transform.position = target.position + target.TransformDirection(positionOffset);

        // Rotation
        if (matchRotation)
        {
            Quaternion rotOffset = Quaternion.Euler(rotationOffset);
            transform.rotation = target.rotation * rotOffset;
        }
    }
}
