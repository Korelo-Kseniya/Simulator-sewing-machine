using UnityEngine;

public class Filler : MonoBehaviour
{
    public Camera forthCamera;
    public GameObject firstObject;
    public GameObject secondObject;
    public GameObject animatedObject;
    public GameObject finalAnimatedObject;
    public Texture2D cursorTexture;
    public Animator animatedObjectAnimator;
    public Animator finalAnimatedObjectAnimator;
    public GameObject notificationPanel; // Панель уведомления
    private int animationCount = 0;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        if (forthCamera == null)
        {
            Debug.LogError("ForthCamera не назначена!");
            return;
        }
        if (firstObject == null)
        {
            Debug.LogError("FirstObject не назначен!");
            return;
        }
        if (secondObject == null)
        {
            Debug.LogError("SecondObject не назначен!");
            return;
        }
        if (animatedObject == null)
        {
            Debug.LogError("AnimatedObject не назначен!");
            return;
        }
        if (finalAnimatedObject == null)
        {
            Debug.LogError("FinalAnimatedObject не назначен!");
            return;
        }
        if (animatedObjectAnimator == null)
        {
            Debug.LogError("AnimatedObjectAnimator не назначен!");
            return;
        }
        if (finalAnimatedObjectAnimator == null)
        {
            Debug.LogError("FinalAnimatedObjectAnimator не назначен!");
            return;
        }
        if (cursorTexture == null)
        {
            Debug.LogError("CursorTexture не назначен!");
            return;
        }
        if (notificationPanel == null)
        {
            Debug.LogError("NotificationPanel не назначен!");
            return;
        }
        notificationPanel.SetActive(false);
    }

    void Update()
    {
        if (forthCamera != null && forthCamera.enabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = forthCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("Луч попал на: " + hit.collider.gameObject.name);

                    if (hit.collider != null && hit.collider.gameObject == firstObject && animationCount < 4)
                    {
                        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
                        animationCount++;
                    }
                    else if (hit.collider != null && hit.collider.gameObject == secondObject && animationCount > 0 && animationCount <= 4)
                    {
                        if (animatedObjectAnimator != null)
                        {
                            Cursor.SetCursor(null, Vector2.zero, cursorMode);
                            switch (animationCount)
                            {
                                case 1:
                                    animatedObjectAnimator.SetTrigger("one");
                                    break;
                                case 2:
                                    animatedObjectAnimator.SetTrigger("two");
                                    break;
                                case 3:
                                    animatedObjectAnimator.SetTrigger("three");
                                    break;
                                case 4:
                                    animatedObjectAnimator.SetTrigger("four");

                                    if (animationCount == 4)
                                    {
                                        notificationPanel.SetActive(true); // Показать панель уведомления после 4-го раза
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Animator не найден для animatedObject!");
                        }
                    }
                }
                else
                {
                    Debug.Log("Луч не попал на объект");
                }
            }
        }
    }

    public void CloseNotificationPanel()
    {
        if (notificationPanel != null)
        {
            notificationPanel.SetActive(false);
        }
    }
}