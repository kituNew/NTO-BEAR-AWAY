using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Colider_Detector : MonoBehaviour
{
    [SerializeField] GameObject particlePrefab;

    public bool isColliderPlayer = false;
    public bool isColliderEnemy = false;
    public bool isColliderDoor = false;
    public bool isColliderKub = false;
    public bool isExplosion = false;
    public bool isDeactivation = false;
    public bool isOnDo = false;
    public bool isRobotStop = false;

    CapsuleCollider2D capsule;

    void Start()
    {
        capsule = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if (capsule.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            isColliderPlayer = true;
        }
        else
        {
            isColliderPlayer = false;
        }

        if (capsule.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            isColliderEnemy = true;
        }
        else
        {
            isColliderEnemy = false;
        }

        if (capsule.IsTouchingLayers(LayerMask.GetMask("Door")))
        {
            isColliderDoor = true;

            if(isExplosion)
            {
                GameObject.Find("Door").GetComponent<Door>().isBam = true;
                Instantiate(particlePrefab, transform.position, Quaternion.identity);
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
        else
        {
            isColliderDoor = false;
        }

        if (capsule.IsTouchingLayers(LayerMask.GetMask("Kub")))
        {
            isColliderKub = true;
        }
        else
        {
            isColliderKub = false;
        }

        StartCoroutine(WaitBam());
    }

    IEnumerator WaitBam()
    {
        if(isExplosion)
        {
            Instantiate(particlePrefab, transform.position, Quaternion.identity);
            GameObject.Find("ExplosionSound").GetComponent<ExplosionSound>().ExplosionSoundIt();

            yield return new WaitForSeconds(0.05f);

            if(gameObject.transform.parent.gameObject.GetComponent<Enemy_controller>() != null)
            {
                gameObject.transform.parent.gameObject.gameObject.GetComponent<Enemy_controller>().Bam();
            }
            else
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(isOnDo)
        {
            Box b = collision.GetComponent<Box>();
            if (b != null)
            {
                b.ifStr = "Ничего";
            }

            RedBox rb = collision.GetComponent<RedBox>();
            if (rb != null)
            {
                rb.ifStr = "Ничего";
            }

            Enemy_controller en = collision.GetComponent<Enemy_controller>();
            if(en != null)
            {
                en.ifStr = "Ничего";
            }

            SleepRobot sr = collision.GetComponent<SleepRobot>();
            if(sr != null)
            {
                sr.ifStr = "Ничего";
            }
        }

        if(isDeactivation)
        {
            Enemy_controller en = collision.GetComponent<Enemy_controller>();
            if(en != null)
            {
                isDeactivation = false;
                en.Do2do();
            }

            SleepRobot sr = collision.GetComponent<SleepRobot>();
            if(sr != null)
            {
                isDeactivation = false;
                sr.Do2do();
            }
        }

        if(isExplosion)
        {
            Player_Controller pl = collision.GetComponent<Player_Controller>();
            if (pl != null)
            {
               pl.Bam();
            }

            Enemy_controller en = collision.GetComponent<Enemy_controller>();
            if(en != null)
            {
                en.Bam();
            }

            Door door = collision.GetComponent<Door>();
            if(door != null)
            {
                door.isBam = true;
            }
        }
    }
}
