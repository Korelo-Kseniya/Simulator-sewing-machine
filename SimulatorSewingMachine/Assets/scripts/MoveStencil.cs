using UnityEngine;

public class MoveStencil : MonoBehaviour
{
    public Vector3 targetPosition = new Vector3(-5.89f, 4.675f, 2.98f);
    public float moveSpeed = 10f;

    public GameObject objectToHide1;
    public GameObject objectToHide2;
    public Sprite stencilSprite;

    private bool isMoving = false;
    private bool hasMoved = false;

    private Vector3 startPosition;
    private Vector3 moveTarget;

    void Start()
    {
        startPosition = transform.position;
        moveTarget = targetPosition;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, moveTarget, Time.deltaTime * moveSpeed);

            if (Vector3.Distance(transform.position, moveTarget) < 0.01f)
            {
                transform.position = moveTarget;
                isMoving = false;
                hasMoved = true;
            }
        }
    }

    void OnMouseDown()
    {
        if (!isMoving && !hasMoved)
        {
            // Устанавливаем цель и запускаем движение
            moveTarget = targetPosition;
            isMoving = true;

            // Скрываем объекты
            if (objectToHide1 != null)
                objectToHide1.SetActive(false);
            if (objectToHide2 != null)
                objectToHide2.SetActive(false);

            // Уведомляем ScissorsController об обновлении спрайта
            ScissorsController scissorsController = FindObjectOfType<ScissorsController>();
            if (scissorsController != null && stencilSprite != null)
            {
                scissorsController.SetPanelSprite(stencilSprite);
                Debug.Log($"Трафарет {gameObject.name} отправил спрайт в ScissorsController");
            }
            else
            {
                Debug.LogWarning("ScissorsController не найден или спрайт не назначен!");
            }
        }
    }
}
