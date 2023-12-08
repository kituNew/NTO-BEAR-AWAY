using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Box : MonoBehaviour
{
    [SerializeField] GameObject programmShield;
    [SerializeField] GameObject player;
    [SerializeField] GameObject door;
    CapsuleCollider2D capsule;

    public string ifStr = "Ничего";
    public string doStr = "Ничего";
    public int xInt = 0;
    float xtInt = 0f;
    bool isDoT = false;
    public bool ifBool;
    public bool isYourTurn = false;

    GameObject currentRobot;

    void Start()
    {
        capsule = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        IfItProgramm();
        ProgrammBox();
    }

    void IfItProgramm()
    {
        if (gameObject.GetComponentInChildren<Colider_Detector>().isColliderPlayer)
        {
            if(player.GetComponent<Player_Controller>().isDo && !programmShield.GetComponent<Programm>().isOn)
            {
                programmShield.SetActive(true);
                programmShield.GetComponent<Programm>().isOn = true;
                programmShield.GetComponent<Programm>().Send(ifStr, doStr, xInt);
                isYourTurn = true;
                programmShield.GetComponent<Programm>().Call(gameObject); 
                programmShield.GetComponent<Programm>().ItCube();
            }
        }
        else
        {
            if(isYourTurn)
            {
                Confim();
                programmShield.GetComponent<Programm>().isOn = false;
                programmShield.GetComponent<Programm>().CloseIt();
                programmShield.SetActive(false);
                isYourTurn = false;
            }
        }
    }
    
    void ProgrammBox()
    {
        if(ifStr.Trim() == "Ничего")
        {
            ifBool = true;
        }
        else if(ifStr.Trim() == "Таймер")//время(таймер)+
        {
            ifBool = If1do(xInt);
        }
        else if(ifStr.Trim() == "Рядом робот")//рядом робот+
        {
            ifBool = If2do(xInt);
        }
        else if(ifStr.Trim() == "Рядом обьект")//рядом обьект+
        {
            ifBool = If3do(xInt);
        }

        if(ifBool)
        {
            
            if(doStr.Trim() == "Деативировать обьект")//деативировать обьект+
            {
                Do1do(xInt);
            }
            else if(doStr.Trim() == "Заклинить дверь")//Заклинить дверь+
            {
                Do2do(xInt);
            }
            else if(doStr.Trim() == "Активировать действие увиденного обьекта")//активировать действие+
            {
                Do3do(xInt);
            }
        }
    }

    void Do1do(int x)
    {
        gameObject.GetComponentInChildren<Colider_Detector>().isDeactivation = true;
    }

    void Do2do(int x)
    {
        door.GetComponent<Door>().isZakl = true;
    }

    void Do3do(int x)
    {
        gameObject.GetComponentInChildren<Colider_Detector>().isOnDo = true;
    }

    bool If1do(int x)
    {
        if (isDoT)
        {   
            xtInt = 0f;
            isDoT = false;
            return true;
        }
        else
        {
            xtInt += Time.deltaTime;
        }

        if(xtInt >= x)
        {
            isDoT = true;
        }

        return false;
    }
    bool If2do(int x)
    {
        return gameObject.GetComponentInChildren<Colider_Detector>().isColliderEnemy;
    }
    bool If3do(int x)
    {
        return gameObject.GetComponentInChildren<Colider_Detector>().isColliderEnemy || gameObject.GetComponentInChildren<Colider_Detector>().isColliderPlayer || gameObject.GetComponentInChildren<Colider_Detector>().isColliderDoor || gameObject.GetComponentInChildren<Colider_Detector>().isColliderKub;
    }

    public void Confim()
    {
        if(isYourTurn)
        {
            ifStr = programmShield.GetComponent<Programm>().ProgIfStr();
            doStr = programmShield.GetComponent<Programm>().ProgDoStr();
            xInt = programmShield.GetComponent<Programm>().ProgXStr();
        }
    }
}
