using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(AudioSource))]
public class HoverScale : MonoBehaviour
{
    public GameObject cloth;
    public Animator clothAnim;
    private AudioSource hoverAudio;

    private bool hasPlayed = false;

    void Start()
    {
        if (cloth != null)
        {
            cloth.SetActive(true);
            clothAnim = cloth.GetComponent<Animator>();
            if (clothAnim == null)
            {
                Debug.LogError("Animator �� ������ �� ������� cloth");
            }
        }
        else
        {
            Debug.LogError("cloth �� �������� � ����������");
        }

        hoverAudio = GetComponent<AudioSource>();
        if (hoverAudio == null)
        {
            Debug.LogError("AudioSource �� ������!");
        }
    }

    void OnMouseEnter()
    {
        if (clothAnim != null)
        {
            clothAnim.SetTrigger("on");
        }

        if (!hasPlayed && hoverAudio != null && hoverAudio.clip != null)
        {
            hoverAudio.Play();
            hasPlayed = true;
        }
    }

    void OnMouseExit()
    {
        if (clothAnim != null)
        {
            clothAnim.SetTrigger("off");
        }

        hasPlayed = false;
    }
}
