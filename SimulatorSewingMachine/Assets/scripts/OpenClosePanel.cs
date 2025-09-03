using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpenClosePanel : MonoBehaviour
{
    [SerializeField]
    public RectTransform panel; // ������ �� ������
    public RectTransform button; // ������ �� ������
    public float animationSpeed = 5f; // �������� ��������
    private bool flag = false;
    [SerializeField] private Image buttonImage; // ������ �� ��������� Image � ������
    [SerializeField] private Sprite closedSprite; // ������ ��� ��������� ���������
    [SerializeField] private Sprite openedSprite; // ������ ��� ��������� ���������

    private Vector2 panelStartPosition;
    private Vector2 buttonStartPosition;
    private Vector2 targetPanelPosition;
    private Vector2 targetButtonPosition;

    // Start is called before the first frame update
    void Start()
    {
        // ���������� ��������� �������
        panelStartPosition = panel.anchoredPosition;
        buttonStartPosition = button.anchoredPosition;

        // ��������� ��������� (������� �� �������)
        panel.anchoredPosition = new Vector2(panelStartPosition.x, -Screen.height / 1.55f);
        button.anchoredPosition = new Vector2(buttonStartPosition.x, -Screen.height / 2.2f);

        // ������������� ������� ������� � ������� ���������
        targetPanelPosition = panel.anchoredPosition;
        targetButtonPosition = button.anchoredPosition;

        buttonImage.sprite = openedSprite;

    }

    public void TogglePanel()
    {
        if (!flag)
        {
            // ������� ������ � ������
            targetPanelPosition = panelStartPosition;
            targetButtonPosition = buttonStartPosition;

            // ������ ������� ��� ������
            buttonImage.sprite = closedSprite;

            flag = true;
        }
        else
        {
            // ������ ������ � ������
            targetPanelPosition = new Vector2(panelStartPosition.x, -Screen.height / 1.55f);
            targetButtonPosition = new Vector2(buttonStartPosition.x, -Screen.height / 2.2f);

            // ������ ������� ��� ������
            buttonImage.sprite = openedSprite;

            flag = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ������� �������� ������ � ������ � ������� ��������
        panel.anchoredPosition = Vector2.Lerp(panel.anchoredPosition, targetPanelPosition, Time.deltaTime * animationSpeed);
        button.anchoredPosition = Vector2.Lerp(button.anchoredPosition, targetButtonPosition, Time.deltaTime * animationSpeed);
    }
}
