using UnityEngine;

public class SimpleNeedleAnimation : MonoBehaviour
{
    private Animator animator;
    private int currentAnimation = 0;
    private bool firstClickDone = false;

    public string[] animationTriggers = new string[11]
    {
        "anim1", "anim2", "anim3", "anim4", "anim5",
        "anim6", "anim7", "anim8", "anim9", "anim10", "anim11"
    };

    public GameObject[] nitki;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Скрываем все нитки при старте
        foreach (var nitka in nitki)
        {
            nitka.SetActive(false);
        }
    }

    void Update()
    {
        if (firstClickDone && Input.GetKeyDown(KeyCode.Space))
        {
            PlayNextAnimation();
        }
    }

    void OnMouseDown()
    {
        if (!firstClickDone)
        {
            PlayNextAnimation();
            firstClickDone = true;
        }
    }

    private void PlayNextAnimation()
    {
        if (currentAnimation < animationTriggers.Length)
        {
            animator.SetTrigger(animationTriggers[currentAnimation]);
            if (currentAnimation < nitki.Length)
            {
                nitki[currentAnimation].SetActive(true);
            }
            currentAnimation++;
        }
    }
}
