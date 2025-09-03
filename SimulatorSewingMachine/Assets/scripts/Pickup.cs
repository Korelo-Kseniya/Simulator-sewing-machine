using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    private Inventory inventory;
    public Sprite itemSprite; // ������ �������, ������� ����� ������������ � ���������
    public string itemTag = "Cloth";

    public Texture2D baseImage; // <- ��������, ������� �������� ��� �����
    public TexturePainter canvasPainter; // <- ������, ������� ����� ��������

    // ����� ������ �� �������, ��� ������� ����� �������� ��������
    public Renderer objectToChangeMaterial1;
    public Renderer objectToChangeMaterial2;

    public Material newMaterial1; // ����� �������� ��� ������� �������
    public Material newMaterial2; // ����� �������� ��� ������� �������

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("InventoryPanel").GetComponent<Inventory>();

        // ���� ������ �� ���������� � ����������, �������� �������� ��� �������������
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
        // ��������: ��� ���� ������� � ������ �����?
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.itemTags[i] == itemTag)
            {
                Debug.Log("������� �� ��������� �����");
                return;
            }
        }

        // ���������� ������ ��������
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
                inventory.itemTags[j] = itemTag; // ��������� ��� ��������

                // ������ ��������� ���� �������� ��� ���������� ��������
                ChangeMaterials();

                break;
            }
        }

    }

    // ����� ��� ����� ���������� � ���� ��������
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

        // ��������� �������� � ������
        if (canvasPainter != null && baseImage != null)
        {
            canvasPainter.LoadImage(baseImage);
        }
    }
}