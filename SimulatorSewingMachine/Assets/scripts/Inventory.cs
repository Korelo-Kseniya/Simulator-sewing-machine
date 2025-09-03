using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public string[] itemTags;

    private void Start()
    {
        itemTags = new string[slots.Length];
    }

    public void AddSpriteToSlot(Sprite sprite)
    {
        // Добавляем спрайт строго во второй слот (индекс 1)
        int targetSlot = 1;
        if (targetSlot < slots.Length)
        {
            if (!isFull[targetSlot])
            {
                isFull[targetSlot] = true;
                itemTags[targetSlot] = sprite.name;

                Image slotImage = slots[targetSlot].GetComponent<Image>();
                if (slotImage != null)
                {
                    slotImage.sprite = sprite;
                    slotImage.enabled = true;
                    Debug.Log($"Inventory: Спрайт {sprite.name} добавлен в слот {targetSlot}");
                }
                else
                {
                    Debug.LogWarning($"Inventory: Image компонент не найден в слоте {targetSlot}");
                }
            }
            else
            {
                Debug.LogWarning($"Inventory: Второй слот (индекс {targetSlot}) уже заполнен!");
            }
        }
        else
        {
            Debug.LogWarning("Inventory: Второй слот недоступен, недостаточно слотов!");
        }
    }
}