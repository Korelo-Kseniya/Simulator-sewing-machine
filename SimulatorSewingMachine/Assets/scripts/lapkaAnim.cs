using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lapkaAnim : MonoBehaviour
{
    public GameObject targetObject;        // объект, чья анимация запускается при клике
    public GameObject otherObject;         // другой объект, чья анимация тоже запускается

    private Animator targetAnimator;
    private Animator otherAnimator;

    private AudioSource audioSource;
    private bool flag = false;

    void Start()
    {
        // Инициализация аниматоров
        if (targetObject != null)
        {
            targetAnimator = targetObject.GetComponent<Animator>();
            audioSource = targetObject.GetComponent<AudioSource>();
            if (targetAnimator == null)
                Debug.LogError("Animator не найден на targetObject");
            if (audioSource == null)
                Debug.LogWarning("AudioSource не найден на targetObject");
        }
        else
        {
            Debug.LogError("targetObject не назначен в инспекторе");
        }

        if (otherObject != null)
        {
            otherAnimator = otherObject.GetComponent<Animator>();
            if (otherAnimator == null)
                Debug.LogError("Animator не найден на otherObject");
        }
        else
        {
            Debug.LogError("otherObject не назначен в инспекторе");
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

    // Этот метод вызывается через Animation Event в конце анимации
    public void StopSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
