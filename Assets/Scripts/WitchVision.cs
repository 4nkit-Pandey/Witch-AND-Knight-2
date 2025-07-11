using UnityEngine;

public class WitchVision : MonoBehaviour
{
    public float visionRange = 100f;
    public LayerMask visionMask;
    
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, visionRange, visionMask))
        {
            if (hit.collider.CompareTag("Knight"))
            {
                hit.collider.GetComponent<KnightBehavior>().SeenByWitch();
            }
        }
    }
}
