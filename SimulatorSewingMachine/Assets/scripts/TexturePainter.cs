using UnityEngine;
using System.Collections.Generic;

public class TexturePainter : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float brushSize = 0.75f;
    [SerializeField] private Color brushColor = Color.red;
    [SerializeField] private int textureSize = 1024;

    [Header("Cursor Settings")]
    [SerializeField] private Texture2D paintCursor;
    [SerializeField] private Texture2D defaultCursor;
    [SerializeField] private Vector2 cursorHotspot = new Vector2(0, 200);

    private Texture2D paintTexture;
    private Renderer objectRenderer;
    private Material paintingMaterial;
    private bool isPainting = false;
    private Color[] originalPixels;
    private List<Vector2> drawPoints = new List<Vector2>();

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer == null)
        {
            Debug.LogError("TexturePainter: Renderer не найден!");
            return;
        }

        paintingMaterial = objectRenderer.material;
        if (paintingMaterial == null || paintingMaterial.mainTexture == null)
        {
            Debug.LogError("TexturePainter: Материал не поддерживает '_MainTex' или текстура не назначена!");
            return;
        }

        if (!TryGetComponent(out Collider _))
        {
            Debug.LogError("TexturePainter: Коллайдер не найден!");
            return;
        }

        mainCamera ??= Camera.main;
        SetDefaultCursor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ResetToOriginal();
        }

        if (paintTexture == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            isPainting = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isPainting = false;
        }

        if (isPainting)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform)
            {
                SetPaintCursor();
                PaintAtUV(hit.textureCoord);
                RecordDrawPoint(hit.textureCoord);
            }
            else
            {
                SetDefaultCursor();
            }
        }
    }

    void SetPaintCursor()
    {
        if (paintCursor != null)
            Cursor.SetCursor(paintCursor, cursorHotspot, CursorMode.Auto);
    }

   public void SetDefaultCursor()
    {
        Cursor.SetCursor(defaultCursor, cursorHotspot, CursorMode.Auto);
    }

    public void LoadImage(Texture2D baseImage)
    {
        Debug.Log("TexturePainter: Загружается изображение из Pickup");

        paintTexture = new Texture2D(textureSize, textureSize, TextureFormat.RGBA32, false);
        paintTexture.filterMode = FilterMode.Bilinear;
        paintTexture.wrapMode = TextureWrapMode.Clamp;

        if (baseImage.width != textureSize || baseImage.height != textureSize)
        {
            Texture2D resized = new Texture2D(textureSize, textureSize, TextureFormat.RGBA32, false);
            RenderTexture rt = RenderTexture.GetTemporary(textureSize, textureSize);
            Graphics.Blit(baseImage, rt);

            RenderTexture prev = RenderTexture.active;
            RenderTexture.active = rt;
            resized.ReadPixels(new Rect(0, 0, textureSize, textureSize), 0, 0);
            resized.Apply();
            RenderTexture.active = prev;
            RenderTexture.ReleaseTemporary(rt);

            originalPixels = resized.GetPixels();
            paintTexture.SetPixels(originalPixels);
        }
        else
        {
            originalPixels = baseImage.GetPixels();
            paintTexture.SetPixels(originalPixels);
        }

        paintTexture.Apply();
        paintingMaterial.mainTexture = paintTexture;

        Debug.Log("TexturePainter: Изображение применено и сохранено как оригинал");
    }

    public void ResetToOriginal()
    {
        if (paintTexture == null || originalPixels == null)
        {
            Debug.LogWarning("TexturePainter: Нет оригинала для отката");
            return;
        }

        paintTexture.SetPixels(originalPixels);
        paintTexture.Apply();
        drawPoints.Clear();
        Debug.Log("TexturePainter: Текстура сброшена до исходного изображения");
    }

    void PaintAtUV(Vector2 uv)
    {
        int x = (int)(uv.x * paintTexture.width);
        int y = (int)(uv.y * paintTexture.height);

        int radius = (int)(brushSize * paintTexture.width / 100f);

        for (int i = -radius; i <= radius; i++)
        {
            for (int j = -radius; j <= radius; j++)
            {
                if (i * i + j * j <= radius * radius)
                {
                    int pixelX = x + i;
                    int pixelY = y + j;

                    if (pixelX >= 0 && pixelX < paintTexture.width &&
                        pixelY >= 0 && pixelY < paintTexture.height)
                    {
                        paintTexture.SetPixel(pixelX, pixelY, brushColor);
                    }
                }
            }
        }

        paintTexture.Apply();
    }

    void RecordDrawPoint(Vector2 uv)
    {
        if (drawPoints.Count == 0 || Vector2.Distance(drawPoints[drawPoints.Count - 1], uv) > 0.02f)
        {
            drawPoints.Add(uv);
            Debug.Log($"TexturePainter: Recorded point UV: {uv}");
        }
    }

    public void ClearTexture()
    {
        if (paintTexture == null) return;

        Color[] pixels = paintTexture.GetPixels();
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = Color.white;
        }

        paintTexture.SetPixels(pixels);
        paintTexture.Apply();
        drawPoints.Clear();
    }

    public Texture2D GetPaintTexture()
    {
        return paintTexture;
    }

    public Color GetBrushColor()
    {
        return brushColor;
    }

    public Renderer GetRenderer()
    {
        return objectRenderer;
    }

    public List<Vector2> GetDrawPoints()
    {
        return new List<Vector2>(drawPoints);
    }
}
