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
                Debug.LogError("Animator �� ������ �� ������� gameObject");
            }

            audioSource = gameObject.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogWarning("AudioSource �� ������ �� ������� gameObject. �������� ��������� AudioSource.");
            }
        }
        else
        {
            Debug.LogError("gameObject �� �������� � ����������");
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
                audioSource.Play(); // ����� ����������� ��� �� ����, ��� ������

            flag = false;
        }
    }

    // ���� ����� ������ ���� ������ �� Animation Event � ����� �����
    public void StopSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
