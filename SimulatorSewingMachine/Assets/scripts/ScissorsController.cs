using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource), typeof(Animator))]
public class ScissorsController : MonoBehaviour
{
    [SerializeField] private TexturePainter texturePainter;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera secondaryCamera;
    [SerializeField] private GameObject targetPanel;
    [SerializeField] private GameObject errorPanel;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button errorCloseButton;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float bladeOffset = 0.2f;
    [SerializeField] private Image panelImage;
    [SerializeField] private GameObject objectToHide;

    private Sprite currentSprite;
    private AudioSource audioSource;
    private Animator animator;

    private List<Vector3> pathPoints = new List<Vector3>();
    private bool isMoving = false;
    private bool hasAnimated = false;
    private int currentPointIndex = 0;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    public bool HasAnimated => hasAnimated;

    void Start()
    {
        mainCamera ??= Camera.main;
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        if (!audioSource) Debug.LogWarning("AudioSource не найден!");
        if (!animator) Debug.LogWarning("Animator не найден!");

        if (texturePainter == null) Debug.LogError("TexturePainter не назначен!");
        if (targetPanel != null) targetPanel.SetActive(false); else Debug.LogError("Target Panel не назначена!");
        if (errorPanel != null) errorPanel.SetActive(false); else Debug.LogError("Error Panel не назначена!");
        if (secondaryCamera == null) Debug.LogError("Secondary Camera не назначена!");
        if (closeButton != null) closeButton.onClick.AddListener(OnCloseButtonClicked); else Debug.LogWarning("Close Button не назначена!");
        if (errorCloseButton != null) errorCloseButton.onClick.AddListener(OnErrorCloseButtonClicked); else Debug.LogWarning("Error Close Button не назначена!");
        if (objectToHide == null) Debug.LogWarning("Object to hide не назначен!");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving && !hasAnimated)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform)
            {
                StartMoving();
            }
        }

        if (isMoving && pathPoints.Count > 0)
        {
            MoveAlongPath();
        }
    }

    public void SetPanelSprite(Sprite sprite)
    {
        currentSprite = sprite;
    }

    private void StartMoving()
    {
        pathPoints = GetPathPoints();
        if (pathPoints.Count == 0)
        {
            Debug.LogWarning("ScissorsController: Путь не найден!");
            return;
        }

        isMoving = true;
        currentPointIndex = 0;
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        if (animator != null)
        {
            animator.SetBool("cutting", true);
        }

        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }

    private void StopMoving()
    {
        isMoving = false;
        hasAnimated = true;
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        if (animator != null)
        {
            animator.SetBool("cutting", false);
        }

        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        if (targetPanel != null)
        {
            targetPanel.SetActive(true);
            if (panelImage != null && currentSprite != null)
            {
                panelImage.sprite = currentSprite;
                Inventory inventory = FindObjectOfType<Inventory>();
                if (inventory != null)
                {
                    inventory.AddSpriteToSlot(currentSprite);
                }
            }
        }

        if (objectToHide != null)
        {
            objectToHide.SetActive(false);
        }
    }

    private void OnCloseButtonClicked()
    {
        ClosePanel();
        texturePainter.SetDefaultCursor();
        SwitchToSecondaryCamera();
    }

    private void OnErrorCloseButtonClicked()
    {
        if (errorPanel != null)
        {
            errorPanel.SetActive(false);
        }
    }

    public void ShowErrorPanel()
    {
        if (errorPanel != null)
        {
            errorPanel.SetActive(true);
        }
    }

    public void ClosePanel()
    {
        if (targetPanel != null)
        {
            targetPanel.SetActive(false);
        }
    }

    private void SwitchToSecondaryCamera()
    {
        if (mainCamera == null || secondaryCamera == null)
        {
            Debug.LogError("Одна из камер не назначена!");
            if (mainCamera != null)
            {
                mainCamera.enabled = true;
                secondaryCamera.enabled = false;
            }
            return;
        }

        mainCamera.enabled = false;
        secondaryCamera.enabled = true;
    }

    private void MoveAlongPath()
    {
        if (currentPointIndex >= pathPoints.Count)
        {
            StopMoving();
            return;
        }

        Vector3 targetPoint = pathPoints[currentPointIndex];
        Vector3 bladePosition = targetPoint;
        Vector3 centerPosition = bladePosition - transform.right * bladeOffset;
        transform.position = Vector3.MoveTowards(transform.position, centerPosition, moveSpeed * Time.deltaTime);

        if (currentPointIndex < pathPoints.Count - 1)
        {
            Vector3 direction = (pathPoints[currentPointIndex + 1] - pathPoints[currentPointIndex]).normalized;
            if (direction != Vector3.zero)
            {
                Vector3 flatDirection = new Vector3(direction.x, 0, direction.z).normalized;
                if (flatDirection != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(Vector3.Cross(flatDirection, Vector3.up), Vector3.up);
                    Quaternion finalRotation = targetRotation * Quaternion.Euler(90f, 0f, 0f);
                    transform.rotation = Quaternion.Slerp(transform.rotation, finalRotation, rotationSpeed * Time.deltaTime);
                }
            }
        }

        if (Vector3.Distance(transform.position + transform.right * bladeOffset, targetPoint) < 0.01f)
        {
            currentPointIndex++;
        }
    }

    private List<Vector3> GetPathPoints()
    {
        List<Vector3> worldPoints = new List<Vector3>();
        List<Vector2> uvPoints = texturePainter.GetDrawPoints();
        Renderer renderer = texturePainter.GetRenderer();

        if (uvPoints == null || renderer == null)
        {
            Debug.LogError("UV points or renderer is null!");
            return worldPoints;
        }

        foreach (Vector2 uv in uvPoints)
        {
            worldPoints.Add(UVToWorldPoint(uv, renderer));
        }

        return SmoothPathPoints(worldPoints);
    }

    private Vector3 UVToWorldPoint(Vector2 uv, Renderer renderer)
    {
        MeshFilter meshFilter = renderer.GetComponent<MeshFilter>();
        if (meshFilter == null || meshFilter.sharedMesh == null)
        {
            Debug.LogError("MeshFilter or Mesh not found!");
            return Vector3.zero;
        }

        Mesh mesh = meshFilter.sharedMesh;
        Vector2[] uvs = mesh.uv;
        Vector3[] vertices = mesh.vertices;

        Vector3 localPoint = Vector3.zero;
        float minDistance = float.MaxValue;
        for (int i = 0; i < uvs.Length && i < vertices.Length; i++)
        {
            float distance = Vector2.Distance(uv, uvs[i]);
            if (distance < minDistance)
            {
                minDistance = distance;
                localPoint = vertices[i];
            }
        }

        return renderer.transform.TransformPoint(localPoint);
    }

    private List<Vector3> SmoothPathPoints(List<Vector3> points)
    {
        if (points.Count < 2) return points;

        List<Vector3> smoothed = new List<Vector3> { points[0] };
        float smoothingFactor = 0.25f;

        for (int i = 1; i < points.Count - 1; i++)
        {
            Vector3 prev = points[i - 1];
            Vector3 current = points[i];
            Vector3 next = points[i + 1];

            Vector3 smooth = current * (1 - smoothingFactor) + (prev + next) * (smoothingFactor / 2f);
            smoothed.Add(smooth);
        }

        smoothed.Add(points[points.Count - 1]);
        return smoothed;
    }
}