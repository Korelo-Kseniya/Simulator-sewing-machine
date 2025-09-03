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
        // ��������� ������ ������ �� ������ ���� (������ 1)
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
                    Debug.Log($"Inventory: ������ {sprite.name} �������� � ���� {targetSlot}");
                }
                else
                {
                    Debug.LogWarning($"Inventory: Image ��������� �� ������ � ����� {targetSlot}");
                }
            }
            else
            {
                Debug.LogWarning($"Inventory: ������ ���� (������ {targetSlot}) ��� ��������!");
            }
        }
        else
        {
            Debug.LogWarning("Inventory: ������ ���� ����������, ������������ ������!");
        }
    }
}