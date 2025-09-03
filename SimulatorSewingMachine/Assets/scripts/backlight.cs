using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backlight : MonoBehaviour
{
    public Color col;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Renderer>().material.color;

    }

    public void ChangCol()
    {
        GetComponent<Renderer>().material.color = new Color(0.56f, 0.0669f, 0.754f);
    }

    public void ChangCol1()
    {
        GetComponent<Renderer>().material.color = col;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
