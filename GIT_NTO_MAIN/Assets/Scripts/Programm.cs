using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Programm : MonoBehaviour
{
    public bool isOn = false;
    //ifs
    [SerializeField] GameObject ifLines;
    [SerializeField] GameObject ifP;
    [SerializeField] GameObject if0;
    [SerializeField] GameObject if1;
    [SerializeField] GameObject if2;
    [SerializeField] GameObject if3;
    //dos
    [SerializeField] GameObject doP;
    [SerializeField] GameObject doLines;
    [SerializeField] GameObject do1;
    [SerializeField] GameObject do2;
    [SerializeField] GameObject do3;
    //xs
    [SerializeField] GameObject xP;
    [SerializeField] GameObject xLines;

    GameObject call;

    public void Call(GameObject thisCall)
    {
        call = thisCall;
    }

    void Start()
    {
        CloseIt();
    }

    void Update()
    {
        xP.GetComponentInChildren<TMP_Text>().text = ((int)(xLines.GetComponent<Scrollbar>().value*10)).ToString();
    }
    public void CloseIt()
    {
        ifLines.SetActive(false);
        doLines.SetActive(false);
        xLines.SetActive(false);
        isOn = false;

        if(call != null)
        {
            if(call.tag == "Cube")
            {
                call.GetComponent<Box>().Confim();
                call.GetComponent<Box>().isYourTurn = false;
            }
            else if(call.tag == "Enemy")
            {
                call.GetComponent<Enemy_controller>().Confim();
                call.GetComponent<Enemy_controller>().isYourTurn = false;
            }
            else if(call.tag == "RedCube")
            {
                call.GetComponent<RedBox>().Confim();
                call.GetComponent<RedBox>().isYourTurn = false;
            }
            else if(call.tag == "SleepEnemy")
            {
                call.GetComponent<SleepRobot>().Confim();
                call.GetComponent<SleepRobot>().isYourTurn = false;
            }
            else if(call.tag == "Door")
            {
                call.GetComponent<Door>().Confim();
                call.GetComponent<Door>().isYourTurn = false;
            }
        }
        gameObject.SetActive(false);
    }

    public void IfSection()
    {
        ifLines.SetActive(true);
        doLines.SetActive(false);
        xLines.SetActive(false);
    }
    public void If0()
    {
        ifP.GetComponentInChildren<TMP_Text>().text = if0.GetComponentInChildren<TMP_Text>().text;
    }
    public void If1()
    {
        ifP.GetComponentInChildren<TMP_Text>().text = if1.GetComponentInChildren<TMP_Text>().text;
    }
    public void If2()
    {
        ifP.GetComponentInChildren<TMP_Text>().text = if2.GetComponentInChildren<TMP_Text>().text;
    }
    public void If3()
    {
        ifP.GetComponentInChildren<TMP_Text>().text = if3.GetComponentInChildren<TMP_Text>().text;
    }
    public void DoSection()
    {
        ifLines.SetActive(false);
        doLines.SetActive(true);
        xLines.SetActive(false);
    }
    public void Do0()
    {
        doP.GetComponentInChildren<TMP_Text>().text = "Ничего";
    }
    public void Do1()
    {
        doP.GetComponentInChildren<TMP_Text>().text = do1.GetComponentInChildren<TMP_Text>().text;
    }
    public void Do2()
    {
        doP.GetComponentInChildren<TMP_Text>().text = do2.GetComponentInChildren<TMP_Text>().text;
    }
    public void Do3()
    {
        doP.GetComponentInChildren<TMP_Text>().text = do3.GetComponentInChildren<TMP_Text>().text;
    }
    public void XSection()
    {
        ifLines.SetActive(false);
        doLines.SetActive(false);
        xLines.SetActive(true);
    }

    public void Send(string ifStr, string doStr, int xInt)
    {
        ifP.GetComponentInChildren<TMP_Text>().text = ifStr;
        doP.GetComponentInChildren<TMP_Text>().text = doStr;
        xLines.GetComponent<Scrollbar>().value = ((float)xInt)/10;
    }

    public string ProgIfStr()
    {
        return ifP.GetComponentInChildren<TMP_Text>().text;
    }

    public string ProgDoStr()
    {
        return doP.GetComponentInChildren<TMP_Text>().text;
    }

    public int ProgXStr()
    {
        return int.Parse(xP.GetComponentInChildren<TMP_Text>().text);
    }

    public void ItCube()//+
    {
        if0.GetComponentInChildren<TMP_Text>().text = "Ничего";
        if1.GetComponentInChildren<TMP_Text>().text = "Таймер";
        if2.GetComponentInChildren<TMP_Text>().text = "Рядом робот";
        if3.GetComponentInChildren<TMP_Text>().text = "Рядом обьект";

        do1.GetComponentInChildren<TMP_Text>().text = "Деативировать обьект";
        do2.GetComponentInChildren<TMP_Text>().text = "Заклинить дверь";
        do3.GetComponentInChildren<TMP_Text>().text = "Активировать действие увиденного обьекта";
    }

    public void ItRedCube()//+
    {
        if0.GetComponentInChildren<TMP_Text>().text = "Ничего";
        if1.GetComponentInChildren<TMP_Text>().text = "Таймер";
        if2.GetComponentInChildren<TMP_Text>().text = "Рядом робот";
        if3.GetComponentInChildren<TMP_Text>().text = "Рядом обьект";

        do1.GetComponentInChildren<TMP_Text>().text = "Взорваться";
        do2.GetComponentInChildren<TMP_Text>().text = "Включить левитационное поле на X секунд";
        do3.GetComponentInChildren<TMP_Text>().text = "Активировать действие увиденного обьекта";
    }

    public void ItRobot()//+
    {
        if0.GetComponentInChildren<TMP_Text>().text = "Ничего";
        if1.GetComponentInChildren<TMP_Text>().text = "Увидит обьект";
        if2.GetComponentInChildren<TMP_Text>().text = "Увидит игрока";
        if3.GetComponentInChildren<TMP_Text>().text = "Увидит робота";

        do1.GetComponentInChildren<TMP_Text>().text = "Окаменить игрока";
        do2.GetComponentInChildren<TMP_Text>().text = "Отключиться";
        do3.GetComponentInChildren<TMP_Text>().text = "Взорваться";
    }

    public void ItSleepRobot()//+
    {
        if0.GetComponentInChildren<TMP_Text>().text = "Ничего";
        if1.GetComponentInChildren<TMP_Text>().text = "Таймер";
        if2.GetComponentInChildren<TMP_Text>().text = "Рядом робот";
        if3.GetComponentInChildren<TMP_Text>().text = "Хотя-бы один робот умер";

        do1.GetComponentInChildren<TMP_Text>().text = "Активация";
        do2.GetComponentInChildren<TMP_Text>().text = "Деактивация";
        do3.GetComponentInChildren<TMP_Text>().text = "Взрыв";
    }

    public void ItDoor()//+
    {
        if0.GetComponentInChildren<TMP_Text>().text = "Никогда";
        if1.GetComponentInChildren<TMP_Text>().text = "Все роботы мертвы";
        if2.GetComponentInChildren<TMP_Text>().text = "Рядом робот";
        if3.GetComponentInChildren<TMP_Text>().text = "Взорвана";

        do1.GetComponentInChildren<TMP_Text>().text = "Открыть";
        do2.GetComponentInChildren<TMP_Text>().text = "Закрыть";
        do3.GetComponentInChildren<TMP_Text>().text = "Заклинить";
    }
}
