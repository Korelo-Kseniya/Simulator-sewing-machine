using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyF : MonoBehaviour
{
    public AudioSource audioSource; // ���� ������������� AudioSource � ������ ������

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
