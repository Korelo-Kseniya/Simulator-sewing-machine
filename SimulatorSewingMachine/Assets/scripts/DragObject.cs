using UnityEngine;

public class DragObject : MonoBehaviour
{
    [SerializeField] private Camera dragCamera; // Камера для перетаскивания
    private bool isDragging = false;
    private Vector3 offset;
    private SwitchCameraG cameraSwitcher; // Ссылка на SwitchCameraG
    private float cameraDistance; // Расстояние от камеры до объекта

    void Start()
    {
        // Находим компонент SwitchCameraG
        cameraSwitcher = FindObjectOfType<SwitchCameraG>();
        if (cameraSwitcher == null)
        {
            Debug.LogError("DragObject: SwitchCameraG component not found!");
        }

        // Если камера не назначена, используем главную камеру
        if (dragCamera == null)
        {
            dragCamera = Camera.main;
            if (dragCamera == null)
            {
                Debug.LogError("DragObject: No camera assigned and no main camera found!");
            }
        }
    }

    void Update()
    {
        if (dragCamera == null || cameraSwitcher == null) return;
        if (!dragCamera.enabled) return; // Пропускаем, если камера не активна

        // Начало перетаскивания (правая кнопка мыши)
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = dragCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                isDragging = true;
                // Вычисляем расстояние от камеры до объекта
                cameraDistance = Vector3.Distance(dragCamera.transform.position, hit.point);
                // Вычисляем смещение
                Vector3 mouseWorldPos = dragCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
                offset = transform.position - mouseWorldPos;
            }
        }

        // Завершение перетаскивания
        if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
        }

        // Перетаскивание объекта
        if (isDragging)
        {
            // Получаем мировую позицию мыши
            Vector3 mouseWorldPos = dragCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
            // Обновляем позицию объекта, сохраняя Y
            transform.position = new Vector3(mouseWorldPos.x + offset.x, transform.position.y, mouseWorldPos.z + offset.z);
        }
    }
}