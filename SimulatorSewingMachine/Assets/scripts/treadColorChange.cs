using UnityEngine;
using UnityEngine.UI;

public class treadColorChange : MonoBehaviour
{
    [SerializeField] private Renderer[] objectsToChange; // ������ �������� ��� ��������� �����

    // �������� ���� �������� � ������ ����� GameObject
    public void ChangeColorFromButton(GameObject button)
    {
        Debug.Log("Button clicked: " + button.name); // ��������, ��� ����� ���������� � ������ ���������

        // ���������, ��� ������ ��� ������
        Button clickedButton = button.GetComponent<Button>();
        if (clickedButton != null)
        {
            Debug.Log("Button component found"); // ��������, ��� ������ ���� �������

            Image buttonImage = clickedButton.GetComponent<Image>();
            if (buttonImage != null)
            {
                Color newColor = buttonImage.color;
                Debug.Log("Changing color to: " + newColor); // ������� ����, �� ������� ����� ������
                ApplyColorToObjects(newColor);
            }
            else
            {
                Debug.LogWarning("Button does not have an Image component!");
            }
        }
        else
        {
            Debug.LogWarning("Clicked object is not a button!");
        }
    }

    private void ApplyColorToObjects(Color color)
    {
        // ��������� ����� ���� �� ���� �������� � �������
        foreach (Renderer obj in objectsToChange)
        {
            if (obj != null)
            {
                obj.sharedMaterial.color = color;
                Debug.Log("Color applied to: " + obj.name); // ��������, ��� ���� ����������� � �������
            }
            else
            {
                Debug.LogWarning("One of the objects in objectsToChange is null!");
            }
        }
    }
}
