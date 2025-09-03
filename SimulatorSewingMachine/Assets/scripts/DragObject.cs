using UnityEngine;

public class DragObject : MonoBehaviour
{
    [SerializeField] private Camera dragCamera; // ������ ��� ��������������
    private bool isDragging = false;
    private Vector3 offset;
    private SwitchCameraG cameraSwitcher; // ������ �� SwitchCameraG
    private float cameraDistance; // ���������� �� ������ �� �������

    void Start()
    {
        // ������� ��������� SwitchCameraG
        cameraSwitcher = FindObjectOfType<SwitchCameraG>();
        if (cameraSwitcher == null)
        {
            Debug.LogError("DragObject: SwitchCameraG component not found!");
        }

        // ���� ������ �� ���������, ���������� ������� ������
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
        if (!dragCamera.enabled) return; // ����������, ���� ������ �� �������

        // ������ �������������� (������ ������ ����)
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = dragCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                isDragging = true;
                // ��������� ���������� �� ������ �� �������
                cameraDistance = Vector3.Distance(dragCamera.transform.position, hit.point);
                // ��������� ��������
                Vector3 mouseWorldPos = dragCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
                offset = transform.position - mouseWorldPos;
            }
        }

        // ���������� ��������������
        if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
        }

        // �������������� �������
        if (isDragging)
        {
            // �������� ������� ������� ����
            Vector3 mouseWorldPos = dragCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
            // ��������� ������� �������, �������� Y
            transform.position = new Vector3(mouseWorldPos.x + offset.x, transform.position.y, mouseWorldPos.z + offset.z);
        }
    }
}