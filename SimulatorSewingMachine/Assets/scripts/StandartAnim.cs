using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartAnim : MonoBehaviour
{
    public GameObject gameObject;
    public Animator animation;
    public bool flag = false;

    void Start()
    {
        if (gameObject != null)
        {
            animation = gameObject.GetComponent<Animator>();
            if (animation == null)
            {
                Debug.LogError("Animator не найден на объекте gameObject");
            }
        }
        else
        {
            Debug.LogError("gameObject не назначен в инспекторе");
        }
    }

    public void OnMouseDown()
    {
        if (flag == false)
        {
            animation.SetTrigger("on");
            flag = true;
        }
    }
}
