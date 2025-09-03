using UnityEngine;

public class PanelToggler : MonoBehaviour
{
    [SerializeField] private GameObject panel; // ������, ������� ����� ���������/���������

    private void Start()
    {
        if (panel != null)
        {
            panel.SetActive(false); // �������� ������ ��� ������
        }
    }

    public void TogglePanel()
    {
        if (panel != null)
        {
            panel.SetActive(!panel.activeSelf); // ����������� ���������� ������
        }
    }
}
