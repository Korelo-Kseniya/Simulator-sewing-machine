using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nitkaTkan : MonoBehaviour
{
    public GameObject nitka1;
    public GameObject nitka2;
    public GameObject nitka3;
    public GameObject nitka4;
    public GameObject nitka5;
    public GameObject nitka6;
    public GameObject nitka7;
    public GameObject nitka8;
    public GameObject nitka9;
    public int flag = 0;
    public Animator anim1;
    public Animator anim2;
    public Animator anim3;
    public Animator anim4;
    public Animator anim5;
    public Animator anim6;
    public Animator anim7;
    public Animator anim8;
    public Animator anim9;
    // Start is called before the first frame update
    void Start()
    {
        nitka1.SetActive(false);
        nitka2.SetActive(false);
        nitka3.SetActive(false);
        nitka4.SetActive(false);
        nitka5.SetActive(false);
        nitka6.SetActive(false);
        nitka7.SetActive(false);
        nitka8.SetActive(false);
        nitka9.SetActive(false);
        anim1 = nitka1.GetComponent<Animator>();
        anim2 = nitka2.GetComponent<Animator>();
        anim3 = nitka3.GetComponent<Animator>();
        anim4 = nitka4.GetComponent<Animator>();
        anim5 = nitka5.GetComponent<Animator>();
        anim6 = nitka6.GetComponent<Animator>();
        anim7 = nitka7.GetComponent<Animator>();
        anim8 = nitka8.GetComponent<Animator>();
        anim9 = nitka9.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            nitka1.SetActive(true);
            if (flag == 0)
            {
                anim1.SetTrigger("on");
                flag = 1;
            }
            else if (flag == 1)
            {
                nitka2.SetActive(true);
                anim1.SetTrigger("on1");
                anim2.SetTrigger("on1");
                flag = 2;
            }
            else if (flag == 2)
            {
                nitka3.SetActive(true);
                anim1.SetTrigger("on2");
                anim2.SetTrigger("on2");
                anim3.SetTrigger("on2");
                flag = 3;
            }
            else if (flag == 3)
            {
                nitka4.SetActive(true);
                anim1.SetTrigger("on3");
                anim2.SetTrigger("on3");
                anim3.SetTrigger("on3");
                anim4.SetTrigger("on3");
                flag = 4;
            }
            else if (flag == 4)
            {
                nitka5.SetActive(true);
                anim1.SetTrigger("on4");
                anim2.SetTrigger("on4");
                anim3.SetTrigger("on4");
                anim4.SetTrigger("on4");
                anim5.SetTrigger("on4");
                flag = 5;
            }
            else if (flag == 5)
            {
                nitka6.SetActive(true);
                anim1.SetTrigger("on5");
                anim2.SetTrigger("on5");
                anim3.SetTrigger("on5");
                anim4.SetTrigger("on5");
                anim5.SetTrigger("on5");
                anim6.SetTrigger("on5");
                flag = 6;
            }
            else if (flag == 6)
            {
                nitka7.SetActive(true);
                anim1.SetTrigger("on6");
                anim2.SetTrigger("on6");
                anim3.SetTrigger("on6");
                anim4.SetTrigger("on6");
                anim5.SetTrigger("on6");
                anim6.SetTrigger("on6");
                anim7.SetTrigger("on6");
                flag = 7;
            }
            else if (flag == 7)
            {
                nitka8.SetActive(true);
                anim1.SetTrigger("on7");
                anim2.SetTrigger("on7");
                anim3.SetTrigger("on7");
                anim4.SetTrigger("on7");
                anim5.SetTrigger("on7");
                anim6.SetTrigger("on7");
                anim7.SetTrigger("on7");
                anim8.SetTrigger("on7");
                flag = 8;
            }
            else if (flag == 8)
            {
                nitka9.SetActive(true);
                anim1.SetTrigger("on8");
                anim2.SetTrigger("on8");
                anim3.SetTrigger("on8");
                anim4.SetTrigger("on8");
                anim5.SetTrigger("on8");
                anim6.SetTrigger("on8");
                anim7.SetTrigger("on8");
                anim8.SetTrigger("on8");
                anim9.SetTrigger("on8");
                flag = 9;
            }
        }
    }
}
