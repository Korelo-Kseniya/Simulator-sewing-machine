using UnityEngine;

public class LeverEvents : MonoBehaviour
{
    public SewingMachineController controller;

    public void EnableZigzag()
    {
        if (controller != null)
            controller.EnableZigzagMode();
    }

    public void DisableZigzag()
    {
        if (controller != null)
            controller.DisableZigzagMode();
    }
}