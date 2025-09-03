using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowYstanPanel : MonoBehaviour
{
    [SerializeField]
    public GameObject btn1;
    public GameObject btn2;
    public GameObject btn3;
    public GameObject btn4;
    public GameObject btn5;
    public GameObject btn6;
    public GameObject btn7;
    public GameObject btn8;
    public GameObject btn9;
    public GameObject btn10;
    public GameObject btn11;
    public GameObject btnclose;
    public TMP_Text message;

    void Start()
    {
        btn1.SetActive(false);
        btn2.SetActive(false);
        btn3.SetActive(false);
        btn4.SetActive(false);
        btn5.SetActive(false);
        btn6.SetActive(false);
        btn7.SetActive(false);
        btn8.SetActive(false);
        btn9.SetActive(false);
        btn10.SetActive(false);
        btn11.SetActive(false);
        btnclose.SetActive(false);
    }

    public void Open()
    {
        btn1.SetActive(true);
        btn2.SetActive(true);
        btn3.SetActive(true);
        btn4.SetActive(true);
        btn5.SetActive(true);
        btn6.SetActive(true);
        btn7.SetActive(true);
        btn8.SetActive(true);
        btn9.SetActive(true);
        btn10.SetActive(true);
        btn11.SetActive(true);
        btnclose.SetActive(true);
    }

    public void Close()
    {
        btn1.SetActive(false);
        btn2.SetActive(false);
        btn3.SetActive(false);
        btn4.SetActive(false);
        btn5.SetActive(false);
        btn6.SetActive(false);
        btn7.SetActive(false);
        btn8.SetActive(false);
        btn9.SetActive(false);
        btn10.SetActive(false);
        btn11.SetActive(false);
        btnclose.SetActive(false);
    }

    public void TextMachine()
    {
        message.text = "Швейная машина — техническое устройство для соединения и отделки материалов методом шитья";
    }

    public void TextLapka()
    {
        message.text = "Лапка нажимная — устройство, которое удерживает ткань";
    }

    public void TextNaprav()
    {
        message.text = "Нитенаправитель — деталь, с помощью которого нитка немного натягивается и не запутывается";
    }

    public void TextPritag()
    {
        message.text = "Нитепритягиватель — предназначен для выполнения следующих функций: ‒ подача нити игле и челноку; ‒ выбирание нити и затяжка стежка; ‒ сдергивание нити с бобины с целью создания запаса для следующего стежка";
    }


    public void TextVibStroch()
    {
        message.text = "Ручка выбора вида строчки — деталь при помощи которой швея изменяет ширину зигзага и выбирает необходимый вид шва, на своей машине.";
    }

    public void TextShirZigzag()
    {
        message.text = "Указатель ширины зигзага — это настройка, которая определяет ширину стежка зигзагообразной формы";
    }

    public void TextKatushka()
    {
        message.text = "Катушка — небольшой цилиндр, на который наматывается нитка";
    }

    public void TextMahovik()
    {
        message.text = "Маховик — массивное вращающееся колесо, запускающее работу иглы";
    }

    public void TextObrPodacha()
    {
        message.text = " Рычаг обратной подачи —  это важный элемент, который позволяет швее перемещать ткань в обратном направлении";
    }

    public void TextDlinStigka()
    {
        message.text = "Ручка регулятора длины стежка — элемент управления швейной машиной, который позволяет настраивать длину стежка";
    }

    public void TextShpulka()
    {
        message.text = "Шпулька — маленький цилиндр с нитками, который отвечает за подачу нижней нитки во время шитья";
    }
}
