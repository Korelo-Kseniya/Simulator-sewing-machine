using UnityEngine;
using UnityEngine.UI;

public class treadColorChange : MonoBehaviour
{
    [SerializeField] private Renderer[] objectsToChange; // Массив объектов для изменения цвета

    // Получаем цвет напрямую с кнопки через GameObject
    public void ChangeColorFromButton(GameObject button)
    {
        Debug.Log("Button clicked: " + button.name); // Проверка, что метод вызывается и кнопка передаётся

        // Проверяем, что объект это кнопка
        Button clickedButton = button.GetComponent<Button>();
        if (clickedButton != null)
        {
            Debug.Log("Button component found"); // Убедимся, что кнопка была найдена

            Image buttonImage = clickedButton.GetComponent<Image>();
            if (buttonImage != null)
            {
                Color newColor = buttonImage.color;
                Debug.Log("Changing color to: " + newColor); // Выводим цвет, на который будем менять
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
        // Применяем новый цвет ко всем объектам в массиве
        foreach (Renderer obj in objectsToChange)
        {
            if (obj != null)
            {
                obj.sharedMaterial.color = color;
                Debug.Log("Color applied to: " + obj.name); // Проверка, что цвет применяется к объекту
            }
            else
            {
                Debug.LogWarning("One of the objects in objectsToChange is null!");
            }
        }
    }
}
