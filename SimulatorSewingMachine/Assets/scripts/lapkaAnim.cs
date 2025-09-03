using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lapkaAnim : MonoBehaviour
{
    public GameObject targetObject;        // ������, ��� �������� ����������� ��� �����
    public GameObject otherObject;         // ������ ������, ��� �������� ���� �����������

    private Animator targetAnimator;
    private Animator otherAnimator;

    private AudioSource audioSource;
    private bool flag = false;

    void Start()
    {
        // ������������� ����������
        if (targetObject != null)
        {
            targetAnimator = targetObject.GetComponent<Animator>();
            audioSource = targetObject.GetComponent<AudioSource>();
            if (targetAnimator == null)
                Debug.LogError("Animator �� ������ �� targetObject");
            if (audioSource == null)
                Debug.LogWarning("AudioSource �� ������ �� targetObject");
        }
        else
        {
            Debug.LogError("targetObject �� �������� � ����������");
        }

        if (otherObject != null)
        {
            otherAnimator = otherObject.GetComponent<Animator>();
            if (otherAnimator == null)
                Debug.LogError("Animator �� ������ �� otherObject");
        }
        else
        {
            Debug.LogError("otherObject �� �������� � ����������");
        }
    }

    void OnMouseDown()
    {
        if (targetAnimator == null || otherAnimator == null) return;

        if (!flag)
        {
            targetAnimator.SetTrigger("on");
            otherAnimator.SetTrigger("up");
            flag = true;
        }
        else
        {
            targetAnimator.SetTrigger("off");
            otherAnimator.SetTrigger("down");
            flag = false;
        }

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    // ���� ����� ���������� ����� Animation Event � ����� ��������
    public void StopSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
