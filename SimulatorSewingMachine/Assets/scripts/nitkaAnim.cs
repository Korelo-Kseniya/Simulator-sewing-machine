using System.Collections;
using UnityEngine;

public class nitkaAnim : MonoBehaviour
{
    public GameObject[] nitkaObjects;
    public Animator[] animators;
    public AnimationClip[] animationClips; // Массив клипов анимации для определения длительности

    private bool[] flags;

    void Start()
    {
        // Отключаем все объекты
        for (int i = 0; i < nitkaObjects.Length; i++)
        {
            nitkaObjects[i].SetActive(false);
        }

        // Инициализация аниматоров
        for (int i = 0; i < nitkaObjects.Length; i++)
        {
            if (animators[i] == null && nitkaObjects[i] != null)
                animators[i] = nitkaObjects[i].GetComponent<Animator>();
        }

        // Инициализация флагов
        flags = new bool[animators.Length];
    }

    // Этот метод нужно привязать к UI-кнопке
    public void StartAnimationSequence()
    {
        StartCoroutine(PlayChainAnimations());
    }

    IEnumerator PlayChainAnimations()
    {
        for (int i = 0; i < animators.Length; i++)
        {
            // Включаем объект нитки
            nitkaObjects[i].SetActive(true);

            // Запускаем анимацию
            string onTrigger = $"on";
            animators[i].SetTrigger(onTrigger);
            flags[i] = true;

            // Ожидаем завершения анимации
            if (animationClips[i] != null)
            {
                yield return new WaitForSeconds(animationClips[i].length);
            }
            else
            {
                // Запасной вариант, если клип не указан
                yield return new WaitForSeconds(1f);
            }
        }
    }
}