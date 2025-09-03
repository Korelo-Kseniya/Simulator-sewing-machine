using UnityEngine;

public class PanelToggler : MonoBehaviour
{
    [SerializeField] private GameObject panel; // Панель, которую нужно открывать/закрывать

    private void Start()
    {
        if (panel != null)
        {
            panel.SetActive(false); // Скрываем панель при старте
        }
    }

    public void TogglePanel()
    {
        if (panel != null)
        {
            panel.SetActive(!panel.activeSelf); // Переключаем активность панели
        }
    }
}
