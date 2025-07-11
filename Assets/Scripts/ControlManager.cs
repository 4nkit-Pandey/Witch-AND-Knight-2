using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public GameObject witchFPS;
    public GameObject knightFPS;

    private bool isKnightControlled = false;

    void Start()
    {
        witchFPS.SetActive(true);
        knightFPS.SetActive(false);
    }

    public void PossessKnight()
    {
        if (!isKnightControlled)
        {
            Debug.Log("Switching control to Knight!");
            witchFPS.SetActive(false);
            knightFPS.SetActive(true);
            isKnightControlled = true;
        }
    }
}
