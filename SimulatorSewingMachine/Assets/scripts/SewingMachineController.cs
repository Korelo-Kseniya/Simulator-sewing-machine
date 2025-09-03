using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SewingMachineController : MonoBehaviour
{
    public Transform needle;
    public LineRenderer threadLineRenderer;
    public Transform[] pathPoints;
    public Button startButton;
    public GameObject stitchPrefab;
    public Transform cube1;
    public Transform cube2;
    public GameObject finishPanel;
    public GameObject finishObgect;
    public Transform pillow; // Добавлен объект подушки

    private AudioSource audioSource;
    public AudioSource sewingAudioSource;
    public float chunkLength = 0.5f;
    private float audioProgress = 0f;
    private Coroutine audioCoroutine;

    public Sprite rewardSprite;
    private Inventory inventory;

    public float stitchSpacing = 0.1f;
    public float threadSpeed = 3f;
    public float fabricMoveDistance = 0.05f;
    public float sideLength = 1.243f;

    private readonly Vector3 fixedStitchPosition = new Vector3(-6.5511f, 5.3392f, 7.2906f);
    private readonly Vector3 duplicatePosition = new Vector3(11.122f, 4.896f, 8.852f);
    //(11.122f, 4.896f, 8.852f)
    //(11.766f, 4.896f, 8.057f)
    private bool isSewing = false;
    private Animator needleAnimator;
    public Animator secondAnimator;
    private int currentPointIndex = 0;
    private float threadProgress = 0f;
    private int threadPositionCount = 1;

    private Vector3[] moveDirections;
    private int currentDirectionIndex = 0;
    private float distanceOnSide = 0f;

    private bool isZigzagMode = false;
    private bool zigzagLeft = true;
    public float zigzagOffset = 0f;

    private GameObject stitchContainer;

    void Start()
    {
        finishPanel.SetActive(false);
        needleAnimator = needle ? needle.GetComponent<Animator>() : null;
        audioSource = GetComponent<AudioSource>();
        stitchContainer = new GameObject("StitchContainer");
        stitchContainer.transform.position = cube1.position;

        if (!audioSource)
        {
            Debug.LogError("AudioSource не найден на объекте SewingMachineController!");
        }
        if (!needleAnimator) Debug.LogError("Needle Animator not found!");
        if (!threadLineRenderer) Debug.LogError("Thread LineRenderer not assigned!");
        if (pathPoints.Length == 0) Debug.LogError("Path points not assigned!");
        if (!stitchPrefab) Debug.LogError("Stitch Prefab not assigned!");
        if (!cube1 || !cube2) Debug.LogError("Cube1 or Cube2 not assigned!");
        if (!startButton) Debug.LogError("Start Button not assigned!");
        if (!pillow) Debug.LogError("Pillow not assigned!");

        threadLineRenderer.positionCount = 1;
        threadLineRenderer.SetPosition(0, pathPoints[0].position);
        startButton.onClick.AddListener(StartSewing);

        moveDirections = new Vector3[]
        {
            new Vector3(0, 0, -1),
            new Vector3(0, 0, -1),
            new Vector3(0, 0, -1),
            new Vector3(0, 0, -1)
        };

        inventory = GameObject.FindGameObjectWithTag("InventoryPanel")?.GetComponent<Inventory>();
        if (inventory == null)
        {
            Debug.LogError("Inventory не найден!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isSewing)
        {
            needleAnimator.SetTrigger("on");
            secondAnimator?.SetTrigger("on");
            CreateStitch();
            PlayNextAudioChunk();
        }

        if (isSewing && currentPointIndex < pathPoints.Length - 1)
        {
            threadProgress += threadSpeed * Time.deltaTime;
            if (threadProgress >= 1f)
            {
                threadProgress = 0f;
                currentPointIndex++;
                threadPositionCount++;
                threadLineRenderer.positionCount = threadPositionCount;
                threadLineRenderer.SetPosition(threadPositionCount - 1, pathPoints[currentPointIndex].position);
            }
            else
            {
                Vector3 newPos = Vector3.Lerp(
                    pathPoints[currentPointIndex].position,
                    pathPoints[currentPointIndex + 1].position,
                    threadProgress
                );
                threadLineRenderer.positionCount = threadPositionCount;
                threadLineRenderer.SetPosition(threadPositionCount - 1, newPos);
            }
        }
    }

    void PlayNextAudioChunk()
    {
        if (sewingAudioSource == null || sewingAudioSource.clip == null)
        {
            Debug.LogWarning("AudioSource или аудиоклип не назначены!");
            return;
        }

        if (audioProgress >= sewingAudioSource.clip.length)
        {
            audioProgress = 0f;
            return;
        }

        if (audioCoroutine != null)
        {
            StopCoroutine(audioCoroutine);
        }

        sewingAudioSource.time = audioProgress;
        sewingAudioSource.Play();
        audioCoroutine = StartCoroutine(StopSoundAfterSeconds(chunkLength));

        audioProgress += chunkLength;
    }

    private IEnumerator StopSoundAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        sewingAudioSource.Stop();
    }

    void StartSewing()
    {
        isSewing = true;
        currentPointIndex = 0;
        threadProgress = 0f;
        threadPositionCount = 1;
        threadLineRenderer.positionCount = 1;
        threadLineRenderer.SetPosition(0, pathPoints[0].position);

        currentDirectionIndex = 0;
        distanceOnSide = 0f;
        stitchContainer.transform.position = cube1.position;

        EventSystem.current.SetSelectedGameObject(null);
    }

    void CreateStitch()
    {
        if (isZigzagMode)
        {
            CreateZigzagStitch();
        }
        else
        {
            CreateStraightStitch();
        }

        MoveFabric();
    }

    void CreateStraightStitch()
    {
        GameObject stitch1 = Instantiate(stitchPrefab, fixedStitchPosition, Quaternion.Euler(90, 0, 0));
        GameObject stitch2 = Instantiate(stitchPrefab, fixedStitchPosition, Quaternion.Euler(90, 0, 0));

        stitch1.transform.localScale = new Vector3(0.01f, 0.02f, 0.01f);
        stitch2.transform.localScale = new Vector3(0.01f, 0.02f, 0.01f);

        stitch1.transform.SetParent(cube1, worldPositionStays: true);
        stitch2.transform.SetParent(cube2, worldPositionStays: true);

        stitch1.name = "Stitch1_" + Time.time;
        stitch2.name = "Stitch2_" + Time.time;
    }

    void CreateZigzagStitch()
    {
        float xOffset = zigzagLeft ? -zigzagOffset : zigzagOffset;
        float rotationAngle = zigzagLeft ? -50f : 50f;
        zigzagLeft = !zigzagLeft;

        Vector3 zigzagPos = fixedStitchPosition + new Vector3(xOffset, 0, 0);

        GameObject stitch1 = Instantiate(stitchPrefab, zigzagPos, Quaternion.Euler(90, 0, rotationAngle));
        GameObject stitch2 = Instantiate(stitchPrefab, zigzagPos, Quaternion.Euler(90, 0, rotationAngle));

        stitch1.transform.localScale = new Vector3(0.01f, 0.02f, 0.01f);
        stitch2.transform.localScale = new Vector3(0.01f, 0.02f, 0.01f);

        stitch1.transform.SetParent(cube1, true);
        stitch2.transform.SetParent(cube2, true);

        stitch1.name = "ZigzagStitch1_" + Time.time;
        stitch2.name = "ZigzagStitch2_" + Time.time;
    }

    void MoveFabric()
    {
        if (cube1.GetComponent<Animator>()) cube1.GetComponent<Animator>().enabled = false;
        if (cube2.GetComponent<Animator>()) cube2.GetComponent<Animator>().enabled = false;

        Vector3 move = moveDirections[currentDirectionIndex] * fabricMoveDistance;

        cube1.position += move;
        cube2.position += move;
        distanceOnSide += fabricMoveDistance;

        if (distanceOnSide >= sideLength)
        {
            distanceOnSide = 0f;
            currentDirectionIndex++;

            if (currentDirectionIndex >= 4)
            {
                isSewing = false;

                if (cube1 != null)
                {
                    cube1.gameObject.SetActive(false);
                    Debug.Log("cube1 скрыт перед появлением finishPanel");
                    cube1.gameObject.tag = "finish"; // Добавление тега "finish" после завершения
                }

                // Копирование всех строчек в конце
                foreach (Transform child in cube1)
                {
                    if (child.name.Contains("Stitch"))
                    {
                        GameObject duplicate = Instantiate(child.gameObject, child.position, child.rotation);
                        duplicate.transform.SetParent(stitchContainer.transform, true);
                        duplicate.transform.localScale = child.localScale;
                    }
                }
                stitchContainer.transform.position = duplicatePosition;

                // Изменение размера контейнера
                stitchContainer.transform.localScale = new Vector3(1, -0.0049522f, 1);

                // Установка контейнера как дочернего объекта подушки
                stitchContainer.transform.SetParent(pillow, true);

                if (inventory != null && rewardSprite != null)
                {
                    int targetSlot = 2;
                    if (targetSlot < inventory.slots.Length && !inventory.isFull[targetSlot])
                    {
                        Image slotImage = inventory.slots[targetSlot].GetComponent<Image>();
                        if (slotImage != null)
                        {
                            slotImage.sprite = rewardSprite;
                            slotImage.enabled = true;
                            inventory.isFull[targetSlot] = true;
                            inventory.itemTags[targetSlot] = rewardSprite.name;
                        }
                    }
                }

                if (finishPanel != null)
                {
                    finishPanel.SetActive(true);
                }
              
                cube2.SetParent(cube1, true);
                return;
            }

            cube1.Rotate(0, -90, 0, Space.World);
            cube2.Rotate(0, -90, 0, Space.World);

            cube1.position = new Vector3(-5.902f, 5.33f, 7.912f);
            cube2.position = new Vector3(-5.902f, 5.338f, 7.912f);
        }

        Debug.Log($"Fabric moved {move}. Distance on side: {distanceOnSide:F3}");
    }

    public void CloseFinishPanel()
    {
        if (finishPanel != null)
        {
            finishPanel.SetActive(false);
        }
    }

    public void EnableZigzagMode()
    {
        isZigzagMode = true;
        zigzagOffset = 0f;
        fabricMoveDistance = 0.02f;
        Debug.Log("Режим зигзага ВКЛ — параметры установлены");
    }

    public void DisableZigzagMode()
    {
        isZigzagMode = false;
        zigzagOffset = 0f;
        fabricMoveDistance = 0.05f;
        Debug.Log("Режим зигзага ВЫКЛ — параметры установлены");
    }
}