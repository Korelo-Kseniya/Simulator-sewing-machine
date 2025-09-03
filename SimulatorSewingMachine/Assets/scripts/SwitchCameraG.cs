using UnityEngine;

public class SwitchCameraG : MonoBehaviour
{
    public Camera mainCamera;
    public Camera secondCamera;
    public Camera thirdCamera;
    public Camera forthCamera;
    public GameObject plane1;
    public GameObject plane2;
    public GameObject plane3;
    public GameObject panel;
    public GameObject errorPanel;
    [SerializeField] private ScissorsController scissorsController;

    private void Start()
    {
        if (mainCamera == null || secondCamera == null || thirdCamera == null || forthCamera == null)
        {
            Debug.LogError("SwitchCameraG: Одна или несколько камер не назначены!");
        }
        if (scissorsController == null)
        {
            Debug.LogError("SwitchCameraG: ScissorsController не назначен!");
        }
        if (errorPanel == null)
        {
            Debug.LogError("SwitchCameraG: ErrorPanel не назначен!");
        }

        mainCamera.enabled = true;
        secondCamera.enabled = false;
        thirdCamera.enabled = false;
        forthCamera.enabled = false;

        plane1.SetActive(false);
        plane2.SetActive(false);
        plane3.SetActive(false);
        panel.SetActive(false);
        errorPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SwitchToSecondCamera();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            HandleThirdCameraSwitch();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            SwitchToForthCamera();
        }
    }

    void SwitchToSecondCamera()
    {
        if (mainCamera.enabled)
        {
            mainCamera.enabled = false;
            secondCamera.enabled = true;
            thirdCamera.enabled = false;
            forthCamera.enabled = false;

            plane1.SetActive(true);
            plane2.SetActive(true);
            plane3.SetActive(true);
            panel.SetActive(true);
            errorPanel.SetActive(false);
        }
        else
        {
            secondCamera.enabled = false;
            mainCamera.enabled = true;
            thirdCamera.enabled = false;
            forthCamera.enabled = false;

            plane1.SetActive(false);
            plane2.SetActive(false);
            plane3.SetActive(false);
            panel.SetActive(false);
            errorPanel.SetActive(false);
        }
    }

    void HandleThirdCameraSwitch()
    {
        if (scissorsController != null && scissorsController.HasAnimated)
        {
            scissorsController.ShowErrorPanel();
            return;
        }

        if (mainCamera.enabled)
        {
            mainCamera.enabled = false;
            secondCamera.enabled = false;
            thirdCamera.enabled = true;
            forthCamera.enabled = false;

            plane1.SetActive(false);
            plane2.SetActive(false);
            plane3.SetActive(false);
            panel.SetActive(false);
            errorPanel.SetActive(false);
        }
        else if (thirdCamera.enabled)
        {
            thirdCamera.enabled = false;
            mainCamera.enabled = true;
            secondCamera.enabled = false;
            forthCamera.enabled = false;

            plane1.SetActive(false);
            plane2.SetActive(false);
            plane3.SetActive(false);
            panel.SetActive(false);
            errorPanel.SetActive(false);
        }
    }

    void SwitchToForthCamera()
    {
        if (mainCamera.enabled)
        {
            mainCamera.enabled = false;
            secondCamera.enabled = false;
            thirdCamera.enabled = false;
            forthCamera.enabled = true;

            plane1.SetActive(true);
            plane2.SetActive(true);
            plane3.SetActive(true);
            panel.SetActive(true);
            errorPanel.SetActive(false);
        }
        else
        {
            secondCamera.enabled = false;
            mainCamera.enabled = true;
            thirdCamera.enabled = false;
            forthCamera.enabled = false;

            plane1.SetActive(false);
            plane2.SetActive(false);
            plane3.SetActive(false);
            panel.SetActive(false);
            errorPanel.SetActive(false);
        }
    }

}