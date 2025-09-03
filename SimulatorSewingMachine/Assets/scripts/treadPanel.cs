using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treadPanel : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    private void Start()
    {

        if (canvas != null)
        {
            canvas.gameObject.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        if (canvas != null)
        {
            canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
        }
    }

}
