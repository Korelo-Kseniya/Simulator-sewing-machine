using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Animator animator1;
    public Animator animator2;
    public Button triggerButton;
    private Inventory inventory;
    public int id;
    public GameObject objectToHide1;
    public GameObject objectToHide2;
    public GameObject objectToHide3;

    public AudioClip clickSound; // 🎵 Назначь в инспекторе
    private AudioSource audioSource;
    private bool soundPlayed = false;

    [System.Serializable]
    private struct StencilObjectPair
    {
        public string spriteName;
        public GameObject object1;
        public GameObject object2;
    }

    [SerializeField] private StencilObjectPair[] stencilObjects;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("InventoryPanel")?.GetComponent<Inventory>();
        if (inventory == null)
        {
            Debug.LogError("Slot: Inventory с тегом InventoryPanel не найден!");
        }

        if (triggerButton != null)
        {
            triggerButton.onClick.RemoveAllListeners();
            triggerButton.onClick.AddListener(OnSlotButtonClicked);
        }

        if (objectToHide1 != null) objectToHide1.SetActive(false);
        if (objectToHide2 != null) objectToHide2.SetActive(false);
        if (objectToHide3 != null) objectToHide3.SetActive(false);

        foreach (var pair in stencilObjects)
        {
            if (pair.object1 != null) pair.object1.SetActive(false);
            if (pair.object2 != null) pair.object2.SetActive(false);
        }

        // 🎧 Настройка AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            Debug.Log("Slot: AudioSource автоматически добавлен");
        }
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
    }

    public void DropItem()
    {
        if (inventory != null)
        {
            inventory.isFull[id] = false;
            inventory.itemTags[id] = "";
        }

        Image slotImage = transform.GetComponent<Image>();
        if (slotImage != null)
        {
            slotImage.sprite = null;
            slotImage.enabled = false;
        }
    }

    public void PlayAnim()
    {
        if (!soundPlayed && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
            soundPlayed = true;
            Debug.Log($"Slot {id}: ▶ Звук воспроизведён из PlayAnim: {clickSound.name}");
        }

        if (inventory != null)
        {
            inventory.isFull[id] = false;
            inventory.itemTags[id] = "";
        }

        Image slotImage = transform.GetComponent<Image>();
        if (slotImage != null)
        {
            slotImage.sprite = null;
            slotImage.enabled = false;
        }

        if (objectToHide1 != null) objectToHide1.SetActive(true);
        if (objectToHide2 != null) objectToHide2.SetActive(true);

        if (animator1 != null) animator1.SetTrigger("on");
        if (animator2 != null) animator2.SetTrigger("on");

        StartCoroutine(CheckAnimationAndHide());
    }

    private void OnSlotButtonClicked()
    {
        if (inventory == null) return;
        if (!inventory.isFull[id]) return;

        if (id == 0)
        {
            PlayAnim();
        }
        else if (id == 1)
        {
            string spriteName = inventory.itemTags[id];
            if (!string.IsNullOrEmpty(spriteName))
            {
                ActivateStencilObjects(spriteName);
                DropItem();
            }
        }
        else if (id == 2)
        {
            SewingMachineController sewingMachine = FindObjectOfType<SewingMachineController>();
            if (sewingMachine != null && sewingMachine.cube1 != null)
            {
                sewingMachine.cube1.gameObject.SetActive(true);
                sewingMachine.cube1.position = new Vector3(10.66f, 4.84f, 9.65f);
                DropItem();
            }
        }
    }

    private void ActivateStencilObjects(string spriteName)
    {
        foreach (var pair in stencilObjects)
        {
            if (pair.spriteName.Equals(spriteName, System.StringComparison.OrdinalIgnoreCase))
            {
                if (pair.object1 != null) pair.object1.SetActive(true);
                if (pair.object2 != null) pair.object2.SetActive(true);
                return;
            }
        }

        Debug.LogWarning($"Slot {id}: Не найдена пара объектов для спрайта {spriteName}!");
    }

    IEnumerator CheckAnimationAndHide()
    {
        if (animator1 != null)
        {
            yield return new WaitForSeconds(animator1.GetCurrentAnimatorStateInfo(0).length);
        }

        if (objectToHide1 != null) objectToHide1.SetActive(false);
        if (objectToHide2 != null) objectToHide2.SetActive(false);
        if (objectToHide3 != null) objectToHide3.SetActive(true);
    }

    public void ResetSlotSound()
    {
        soundPlayed = false;
    }
}
