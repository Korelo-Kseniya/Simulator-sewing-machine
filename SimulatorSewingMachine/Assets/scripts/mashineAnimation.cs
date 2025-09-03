using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mashineAnimation : MonoBehaviour
{
    public GameObject gameObject;
    public Animator animation;
    public bool flag = false;
    private AudioSource audioSource;

    void Start()
    {
        if (gameObject != null)
        {
            gameObject.SetActive(true);
            animation = gameObject.GetComponent<Animator>();
            if (animation == null)
            {
                Debug.LogError("Animator не найден на объекте gameObject");
            }

            audioSource = gameObject.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogWarning("AudioSource не найден на объекте gameObject. Добавьте компонент AudioSource.");
            }
        }
        else
        {
            Debug.LogError("gameObject не назначен в инспекторе");
        }
    }

    public void OnMouseDown()
    {
        if (animation == null) return;

        if (!flag)
        {
            animation.SetTrigger("on");
            if (audioSource != null)
                audioSource.Play();

            flag = true;
        }
        else
        {
            animation.SetTrigger("off");
            if (audioSource != null)
                audioSource.Play(); // можно проигрывать тот же звук, или другой

            flag = false;
        }
    }

    // Этот метод должен быть вызван из Animation Event в конце клипа
    public void StopSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
