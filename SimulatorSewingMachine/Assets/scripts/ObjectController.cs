using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public Camera forthCamera;
    public GameObject invisibleObject;
    public GameObject triggerObject;
    public GameObject movingObject;

    private Vector3 targetPosition = new Vector3(11.09f, 4.84f, 7.57f);
    private float moveSpeed = 2f;
    private bool isMoving = false;

    void Start()
    {
        if (invisibleObject != null)
        {
            invisibleObject.SetActive(false);
        }
        if (forthCamera == null)
        {
            Debug.LogError("ForthCamera не назначена!");
        }
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
                    if (hit.collider.gameObject == triggerObject)
                    {
                        isMoving = true;
                    }
                }
            }

            if (isMoving)
            {
                movingObject.transform.position = Vector3.MoveTowards(movingObject.transform.position, targetPosition, moveSpeed * Time.deltaTime);
                if (Vector3.Distance(movingObject.transform.position, targetPosition) < 0.01f)
                {
                    isMoving = false;
                    movingObject.SetActive(false);
                    invisibleObject.SetActive(true);
                }
            }
        }
    }
}