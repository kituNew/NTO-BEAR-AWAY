using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject programmShield;
    [SerializeField] GameObject player;
    [SerializeField] GameObject mainDoor;
    [SerializeField] GameObject breakAnim;
    BoxCollider2D box;

    public string ifStr = "Ничего";
    public string doStr = "Ничего";
    public int xInt = 0;
    public bool ifBool;

    public bool isBam = false;
    public bool isYourTurn = false;
    public bool isZakl = false;

    public bool isOpen = false;

    Color col;

    void Start()
    {
        col = Color.white;
        box = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        IfItProgramm();
        ProgrammBox();
        Open();
    }

    void Open()
    {
        if(isZakl)
        {
            breakAnim.SetActive(true);
        }

        if(!isZakl && isOpen)
        {
            if(col.a > 0f)
            {
                col.a -= Time.deltaTime;
                mainDoor.GetComponent<Renderer>().material.color = col;
            }
            else
            {
                col.a = 0f;
                mainDoor.GetComponent<Renderer>().material.color = col;
            }

            if(box.IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                player.GetComponent<Player_Controller>().NextLevel();
            }
        }
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
                programmShield.GetComponent<Programm>().ItDoor();
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
        if(ifStr.Trim() == "Никогда")
        {
            ifBool = false;
        }
        else if(ifStr.Trim() == "Все роботы мертвы")//Все роботы мертвы+
        {
            ifBool = If1do(xInt);
        }
        else if(ifStr.Trim() == "Рядом робот")//рядом робот+
        {
            ifBool = If2do(xInt);
        }
        else if(ifStr.Trim() == "Взорвана")//Взорвана+
        {
            ifBool = If3do(xInt);
        }

        if(ifBool)
        {
            
            if(doStr.Trim() == "Открыть")//Открыть+
            {
                Do1do(xInt);
            }
            else if(doStr.Trim() == "Закрыть")//Закрыть+
            {
                Do2do(xInt);
            }
            else if(doStr.Trim() == "Заклинить")//Заклинить+
            {
                Do3do(xInt);
            }
        }
    }

    void Do1do(int x)
    {
        isOpen = true;
    }

    void Do2do(int x)
    {
        if(col.a < 1f)
        {
            col.a += Time.deltaTime;
            mainDoor.GetComponent<Renderer>().material.color = col;
        }
        else
        {
            col.a = 1f;
            mainDoor.GetComponent<Renderer>().material.color = col;
        }

        isOpen = false;
    }

    void Do3do(int x)
    {
        isZakl = true;
    }

    bool If1do(int x)
    {
        return player.GetComponent<Player_Controller>().countOfRobots == 0;
    }
    bool If2do(int x)
    {
        return gameObject.GetComponentInChildren<Colider_Detector>().isColliderEnemy;
    }
    bool If3do(int x)
    {
        return isBam;
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
