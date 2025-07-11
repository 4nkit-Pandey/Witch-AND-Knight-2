using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    public PlayerSwitcher switcher;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            switcher.SwitchToKnight();
        }
    }
}
