using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    private Inventory inventory;
    public Sprite itemSprite; // Спрайт объекта, который будет отображаться в инвентаре
    public string itemTag = "Cloth";

    public Texture2D baseImage; // <- Картинка, которую применим как холст
    public TexturePainter canvasPainter; // <- Объект, который будет рисовать

    // Новые ссылки на объекты, для которых будет меняться материал
    public Renderer objectToChangeMaterial1;
    public Renderer objectToChangeMaterial2;

    public Material newMaterial1; // Новый материал для первого объекта
    public Material newMaterial2; // Новый материал для второго объекта

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("InventoryPanel").GetComponent<Inventory>();

        // Если спрайт не установлен в инспекторе, пытаемся получить его автоматически
        if (itemSprite == null)
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                itemSprite = renderer.sprite;
            }
        }
    }

    private void OnMouseDown()
    {
        // Проверка: уже есть предмет с нужным тегом?
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.itemTags[i] == itemTag)
            {
                Debug.Log("Удалите из инвентаря ткань");
                return;
            }
        }

        // Добавление нового предмета
        for (int j = 0; j < inventory.slots.Length; j++)
        {
            if (!inventory.isFull[j])
            {
                Image slotImage = inventory.slots[j].GetComponent<Image>();
                if (slotImage != null && itemSprite != null)
                {
                    slotImage.sprite = itemSprite;
                    slotImage.enabled = true;
                }

                inventory.isFull[j] = true;
                inventory.itemTags[j] = itemTag; // сохраняем тег предмета

                // Меняем материалы двух объектов при добавлении предмета
                ChangeMaterials();

                break;
            }
        }

    }

    // Метод для смены материалов у двух объектов
    private void ChangeMaterials()
    {
        if (objectToChangeMaterial1 != null && newMaterial1 != null)
        {
            objectToChangeMaterial1.material = newMaterial1;
        }

        if (objectToChangeMaterial2 != null && newMaterial2 != null)
        {
            objectToChangeMaterial2.material = newMaterial2;
        }

        // Применяем картинку к холсту
        if (canvasPainter != null && baseImage != null)
        {
            canvasPainter.LoadImage(baseImage);
        }
    }
}