using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_controller : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    [SerializeField] GameObject LookPrefab;

    GameObject lastLine;

    Rigidbody2D rb2d;

    int dir = 1;

    public float lazF = 0f;
    bool lazB = false;
    GameObject look;
    int a = 0;

    [SerializeField] GameObject programmShield;
    [SerializeField] GameObject player;

    public string ifStr = "Увидит игрока";
    public string doStr = "Окаменить игрока";
    public int xInt = 0;
    public bool ifBool;

    public bool isYourTurn = false;

    public bool isOff = false;

    Animator animator;
    AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
        look = Instantiate(LookPrefab, rb2d.position + new Vector2(dir, 0f), Quaternion.identity);
        look.GetComponent<PieceOfLook>().Launch(dir, 10f);
        lastLine = look.GetComponent<PieceOfLook>().Line(gameObject, lastLine, dir);
    }

    void Update()
    {
        if(!isOff)
        {
            See();
            walk();
        }
        IfItProgramm();
        ProgrammRobot();
    }

    void walk()
    {
        RaycastHit2D hitWall = Physics2D.Raycast(transform.position, new Vector2(dir, 0f), 0.5f, LayerMask.GetMask("Ground"));

        RaycastHit2D hitKub = Physics2D.Raycast(transform.position, new Vector2(dir, 0f), 0.5f, LayerMask.GetMask("Kub"));

        RaycastHit2D hitRobot = Physics2D.Raycast(transform.position + new Vector3(dir, 0f, 0f), new Vector2(dir, 0f), 0.5f, LayerMask.GetMask("Enemy"));

        RaycastHit2D hitGround = Physics2D.Raycast(transform.position, new Vector2(dir, -1f), 2f, LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position, new Vector2(1f * dir, 0f), Color.red, 0.1f);

        if (hitWall.collider != null || hitGround.collider == null || hitRobot.collider != null || hitKub.collider != null)
        {
            Destroy(lastLine);
            dir = -dir;
            transform.localScale = new Vector3((float)dir, 1f, 1f);
        }

        rb2d.velocity = new Vector2(speed * dir, rb2d.velocity.y);
    }

    void See()
    {
        if(lazB)
        {   
            look = Instantiate(LookPrefab, rb2d.position + new Vector2(dir, 0f), Quaternion.identity);
            look.GetComponent<PieceOfLook>().Launch(dir, 10f);
            lazF = 0f;
            lazB = false;
        }
        else
        {
            lazF += Time.deltaTime;
        }

        if(lazF >= 0.3f)
        {
            lazB = true;
        }

        try
        {
            lastLine = look.GetComponent<PieceOfLook>().Line(gameObject, lastLine, dir);
        }
        catch(MissingReferenceException)
        {
            a = 1;
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
                programmShield.GetComponent<Programm>().ItRobot();
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

    void ProgrammRobot()
    {
        if(ifStr.Trim() == "Ничего")
        {
            ifBool = true;
        }
        else if(ifStr.Trim() == "Увидит обьект")//увидет обьект+
        {
            ifBool = If1do(xInt);
        }
        else if(ifStr.Trim() == "Увидит игрока")//Увидит игрока+
        {
            ifBool = If2do(xInt);
        }
        else if(ifStr.Trim() == "Увидит робота")//увидет робота+
        {
            ifBool = If3do(xInt);
        }

        if(ifBool)
        {
            
            if(doStr.Trim() == "Окаменить игрока")//окаменелость игрока+
            {
                Do1do(xInt);
            }
            else if(doStr.Trim() == "Отключиться")//выключение+
            {
                Do2do();
            }
            else if(doStr.Trim() == "Взорваться")//взрыв+
            {
                Do3do(xInt);
            }
        }
    }

    void Do1do(int x)
    {
        if(If2do(xInt))
        {
            player.GetComponent<Player_Controller>().Die();
        }
    }

    public void Do2do()
    {
        Destroy(audioSource);
        animator.Play("RobotStop");
        isOff = true;
        Destroy(lastLine);
    }

    void Do3do(int x)
    {
        player.GetComponent<Player_Controller>().HelpToExplosionSleepRobor(1f);
        gameObject.GetComponentInChildren<Colider_Detector>().isExplosion = true;
    }

    bool If1do(int x)
    {
        try
        { 
            return lastLine.GetComponent<Line>().IsObject() || look.GetComponent<PieceOfLook>().IsObject();
        }
        catch(MissingReferenceException)
        {
            a = 1;
        }
        return false;
    }
    bool If2do(int x)
    {
        try
        { 
            return lastLine.GetComponent<Line>().IsPlayer() || look.GetComponent<PieceOfLook>().IsPlayer();
        }
        catch(MissingReferenceException)
        {
            a = 1;
        }
        return false;
    }
    bool If3do(int x)
    {
        try
        { 
            return lastLine.GetComponent<Line>().IsEnemy() || look.GetComponent<PieceOfLook>().IsEnemy();
        }
        catch(MissingReferenceException)
        {
            a = 1;
        }
        return false;
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
        Destroy(lastLine);
        Destroy(gameObject);
    }
}
