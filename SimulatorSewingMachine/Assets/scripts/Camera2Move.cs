using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2Move : MonoBehaviour
{
    bool move = false;
    float speed = 0.01f;
    float offset = 0;
    Vector3 startPosition;
    Vector3 needPosition;
    Quaternion startRotation;
    Quaternion needRotaton;

    public void Camera1()
    {
        move = true;

        startPosition = transform.position;
        startRotation = transform.rotation;

        needPosition = new Vector3(-7.18f, 6.71f, 9.97f);
        needRotaton = Quaternion.Euler(15.207f, -180f, 0.77f);
    }

    public void Camera2()
    {
        move = true;

        startPosition = transform.position;
        startRotation = transform.rotation;

        needPosition = new Vector3(-5.270518f, 5.773871f, 6.334386f);
        needRotaton = Quaternion.Euler(9.283f, -55.581f, 0.029f);
    }

    public void Camera3()
    {
        move = true;

        startPosition = transform.position;
        startRotation = transform.rotation;

        needPosition = new Vector3(-5.58919f, 6.722529f, 8.414228f);
        needRotaton = Quaternion.Euler(13.065f, -131.209f, 0.029f);
    }

    public void Camera4()
    {
        move = true;

        startPosition = transform.position;
        startRotation = transform.rotation;

        needPosition = new Vector3(-7.94897f, 6.921362f, 8.658796f);
        needRotaton = Quaternion.Euler(7.908f, 179.113f, 0.031f);
    }

    public void Camera5()
    {
        move = true;

        startPosition = transform.position;
        startRotation = transform.rotation;

        needPosition = new Vector3(-6.89000f, 7.471343f, 9.903519f);
        needRotaton = Quaternion.Euler(-2.062f, -179.689f, 0.031f);
    }

    public void Camera6()
    {
        move = true;

        startPosition = transform.position;
        startRotation = transform.rotation;

        needPosition = new Vector3(-8.677933f, 6.625299f, 9.094347f);
        needRotaton = Quaternion.Euler(5.845f, -162.153f, 0.029f);
    }

    public void Camera7()
    {
        move = true;

        startPosition = transform.position;
        startRotation = transform.rotation;

        needPosition = new Vector3(-8.13348f, 6.113232f, 8.734947f);
        needRotaton = Quaternion.Euler(8.595f, -162.495f, 0.029f);
    }

    public void Camera8()
    {
        move = true;

        startPosition = transform.position;
        startRotation = transform.rotation;

        needPosition = new Vector3(-7.19049f, 5.717844f, 8.777198f);
        needRotaton = Quaternion.Euler(4.47f, -167.826f, 0.028f);
    }


    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            offset += speed;
            transform.position = Vector3.Lerp(startPosition, needPosition, offset);
            transform.rotation = Quaternion.Slerp(startRotation, needRotaton, offset);
            if (offset >= 1)
            {
                move = false;
                offset = 0;
            }
        }
    }
}
