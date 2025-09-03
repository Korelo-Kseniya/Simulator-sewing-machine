using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private bool isFullScreen = true;
    public AudioMixer am;
    public Slider volumeSlider;

    void Start()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("volume");
            volumeSlider.value = savedVolume;  // Устанавливаем значение слайдера
            am.SetFloat("masterVolume", savedVolume);  // Устанавливаем значение в AudioMixer
            Debug.Log("Загружена сохраненная громкость: " + savedVolume);
        }
        else
        {
            volumeSlider.value = 1f;  // Если нет сохраненной громкости, установим по умолчанию
        }
    }

    public void FullScreenToggle()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

    public void AudioVolume(float sliderValue)
    {
        am.SetFloat("masterVolume", sliderValue);
        PlayerPrefs.SetFloat("volume", sliderValue);
        Debug.Log("Сохранили громкость: " + sliderValue);
    }
}
