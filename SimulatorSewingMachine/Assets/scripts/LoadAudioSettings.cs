using UnityEngine;
using UnityEngine.Audio;

public class LoadAudioSettings : MonoBehaviour
{
    public AudioMixer am;

    void Start()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("volume");
            Debug.Log("��������� ���������: " + savedVolume);
            am.SetFloat("masterVolume", savedVolume);
        }
    }
}
