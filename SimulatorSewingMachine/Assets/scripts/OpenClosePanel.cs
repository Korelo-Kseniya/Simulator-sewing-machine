using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpenClosePanel : MonoBehaviour
{
    [SerializeField]
    public RectTransform panel; // Ссылка на панель
    public RectTransform button; // Ссылка на кнопку
    public float animationSpeed = 5f; // Скорость анимации
    private bool flag = false;
    [SerializeField] private Image buttonImage; // Ссылка на компонент Image в кнопке
    [SerializeField] private Sprite closedSprite; // Спрайт для закрытого состояния
    [SerializeField] private Sprite openedSprite; // Спрайт для открытого состояния

    private Vector2 panelStartPosition;
    private Vector2 buttonStartPosition;
    private Vector2 targetPanelPosition;
    private Vector2 targetButtonPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Запоминаем стартовые позиции
        panelStartPosition = panel.anchoredPosition;
        buttonStartPosition = button.anchoredPosition;

        // Начальное положение (скрытые за экраном)
        panel.anchoredPosition = new Vector2(panelStartPosition.x, -Screen.height / 1.55f);
        button.anchoredPosition = new Vector2(buttonStartPosition.x, -Screen.height / 2.2f);

        // Инициализация целевых позиций в скрытом состоянии
        targetPanelPosition = panel.anchoredPosition;
        targetButtonPosition = button.anchoredPosition;

        buttonImage.sprite = openedSprite;

    }

    public void TogglePanel()
    {
        if (!flag)
        {
            // Открыть панель и кнопку
            targetPanelPosition = panelStartPosition;
            targetButtonPosition = buttonStartPosition;

            // Меняем внешний вид кнопки
            buttonImage.sprite = closedSprite;

            flag = true;
        }
        else
        {
            // Скрыть панель и кнопку
            targetPanelPosition = new Vector2(panelStartPosition.x, -Screen.height / 1.55f);
            targetButtonPosition = new Vector2(buttonStartPosition.x, -Screen.height / 2.2f);

            // Меняем внешний вид кнопки
            buttonImage.sprite = openedSprite;

            flag = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Плавное движение панели и кнопки к целевым позициям
        panel.anchoredPosition = Vector2.Lerp(panel.anchoredPosition, targetPanelPosition, Time.deltaTime * animationSpeed);
        button.anchoredPosition = Vector2.Lerp(button.anchoredPosition, targetButtonPosition, Time.deltaTime * animationSpeed);
    }
}
