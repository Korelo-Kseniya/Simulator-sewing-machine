using System.Collections;
using UnityEngine;

public class nitkaAnim : MonoBehaviour
{
    public GameObject[] nitkaObjects;
    public Animator[] animators;
    public AnimationClip[] animationClips; // ������ ������ �������� ��� ����������� ������������

    private bool[] flags;

    void Start()
    {
        // ��������� ��� �������
        for (int i = 0; i < nitkaObjects.Length; i++)
        {
            nitkaObjects[i].SetActive(false);
        }

        // ������������� ����������
        for (int i = 0; i < nitkaObjects.Length; i++)
        {
            if (animators[i] == null && nitkaObjects[i] != null)
                animators[i] = nitkaObjects[i].GetComponent<Animator>();
        }

        // ������������� ������
        flags = new bool[animators.Length];
    }

    // ���� ����� ����� ��������� � UI-������
    public void StartAnimationSequence()
    {
        StartCoroutine(PlayChainAnimations());
    }

    IEnumerator PlayChainAnimations()
    {
        for (int i = 0; i < animators.Length; i++)
        {
            // �������� ������ �����
            nitkaObjects[i].SetActive(true);

            // ��������� ��������
            string onTrigger = $"on";
            animators[i].SetTrigger(onTrigger);
            flags[i] = true;

            // ������� ���������� ��������
            if (animationClips[i] != null)
            {
                yield return new WaitForSeconds(animationClips[i].length);
            }
            else
            {
                // �������� �������, ���� ���� �� ������
                yield return new WaitForSeconds(1f);
            }
        }
    }
}