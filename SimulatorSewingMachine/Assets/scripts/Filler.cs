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
    public GameObject notificationPanel; // ������ �����������
    private int animationCount = 0;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        if (forthCamera == null)
        {
            Debug.LogError("ForthCamera �� ���������!");
            return;
        }
        if (firstObject == null)
        {
            Debug.LogError("FirstObject �� ��������!");
            return;
        }
        if (secondObject == null)
        {
            Debug.LogError("SecondObject �� ��������!");
            return;
        }
        if (animatedObject == null)
        {
            Debug.LogError("AnimatedObject �� ��������!");
            return;
        }
        if (finalAnimatedObject == null)
        {
            Debug.LogError("FinalAnimatedObject �� ��������!");
            return;
        }
        if (animatedObjectAnimator == null)
        {
            Debug.LogError("AnimatedObjectAnimator �� ��������!");
            return;
        }
        if (finalAnimatedObjectAnimator == null)
        {
            Debug.LogError("FinalAnimatedObjectAnimator �� ��������!");
            return;
        }
        if (cursorTexture == null)
        {
            Debug.LogError("CursorTexture �� ��������!");
            return;
        }
        if (notificationPanel == null)
        {
            Debug.LogError("NotificationPanel �� ��������!");
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
                    Debug.Log("��� ����� ��: " + hit.collider.gameObject.name);

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
                                        notificationPanel.SetActive(true); // �������� ������ ����������� ����� 4-�� ����
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Animator �� ������ ��� animatedObject!");
                        }
                    }
                }
                else
                {
                    Debug.Log("��� �� ����� �� ������");
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