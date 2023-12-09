using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SleepRobot : MonoBehaviour
{
    [SerializeField] GameObject programmShield;
    [SerializeField] GameObject player;
    [SerializeField] GameObject robot;

    public string ifStr = "Рядом робот";
    public string doStr = "Активация";
    public int xInt = 0;
    public float xtInt = 0f;
    bool isDoT = false;
    public bool ifBool;

    public bool deactive = false;

    public bool isYourTurn = false;

    float xtIntAnim = 0f;
    bool isDoTAnim = false;

    Animator animator;

    AudioSource audioSource;
    [SerializeField] AudioClip soundWakeUp;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
                programmShield.GetComponent<Programm>().ItSleepRobot();
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
        else if(ifStr.Trim() == "Хотя-бы один робот умер")//Если какой-нибудь робот умер+
        {
            ifBool = If3do(xInt);
        }

        if(ifBool)
        {
            
            if(doStr.Trim() == "Активация")//Активация+
            {
                Do1do(xInt);
            }
            else if(doStr.Trim() == "Деактивация")//Деактивация+
            {
                Do2do();
            }
            else if(doStr.Trim() == "Взрыв")//Взрыв+
            {
                Do3do(xInt);
            }
        }
    }

    void Do1do(int x)
    {
        if(!deactive)
        {
            animator.Play("SleepRoborWakeUp");
            audioSource.PlayOneShot(soundWakeUp);

            if (isDoTAnim)
            {   
                Destroy(gameObject.GetComponent<SpriteRenderer>());
                Destroy(gameObject.GetComponent<BoxCollider2D>());
                Destroy(gameObject.GetComponent<Rigidbody2D>());
                robot.SetActive(true);
                gameObject.tag = "Untagged";
                Destroy(gameObject.GetComponent<SleepRobot>());
            }
            else
            {
                xtIntAnim += Time.deltaTime;
            }

            if(xtIntAnim >= 0.4)
            {
                isDoTAnim = true;
            }
        }
    }

    public void Do2do()
    {
        deactive = true;
    }

    void Do3do(int x)
    {
        player.GetComponent<Player_Controller>().HelpToExplosionSleepRobor(1f);
        gameObject.GetComponentInChildren<Colider_Detector>().isExplosion = true;
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
        return player.GetComponent<Player_Controller>().isOneRobotDie;
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

    public void Bam()
    {
        player.GetComponent<Player_Controller>().isOneRobotDie = true;
        Destroy(gameObject);
    }
}
