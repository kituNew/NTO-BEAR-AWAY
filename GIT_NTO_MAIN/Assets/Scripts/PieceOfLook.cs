using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceOfLook : MonoBehaviour
{
    [SerializeField] GameObject linePrefab;
    Rigidbody2D rb2d;
    BoxCollider2D lookDestrouer;
    float timeOnLive = 0f;
    bool isDie = false;
    SpriteRenderer sp;
    void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        lookDestrouer = GetComponent<BoxCollider2D>();
    }

    public void Launch(int direction, float speed)
    {
        rb2d.velocity = new Vector2(direction * speed, rb2d.velocity.y);
    }

    void Update()
    {
        if(isDie)
        {   
            Die();
        }
        else
        {
            timeOnLive += Time.deltaTime;
        }

        if(timeOnLive >= 0.3f)
        {
            isDie = true;
        }

        if (lookDestrouer.IsTouchingLayers(LayerMask.GetMask("Enemy")) || lookDestrouer.IsTouchingLayers(LayerMask.GetMask("Ground")) || lookDestrouer.IsTouchingLayers(LayerMask.GetMask("Player")) || lookDestrouer.IsTouchingLayers(LayerMask.GetMask("Kub")))
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public GameObject Line(GameObject myEnemy, GameObject lastLine, int dir)
    {
        Destroy(lastLine);
        float newX = (myEnemy.transform.position.x + gameObject.transform.position.x)/2 + ((float)dir)/2;
        GameObject line = Instantiate(linePrefab, new Vector3(newX, myEnemy.transform.position.y, 0f), Quaternion.identity);
        line.transform.localScale = new Vector3((myEnemy.transform.position.x - gameObject.transform.position.x)/6, 1f, 1f);
        return line;
    }

    public bool IsPlayer()
    {
        return lookDestrouer.IsTouchingLayers(LayerMask.GetMask("Player"));
    }

    public bool IsEnemy()
    {
        return lookDestrouer.IsTouchingLayers(LayerMask.GetMask("Enemy"));
    }

    public bool IsObject()
    {
        return IsEnemy() || IsPlayer() || lookDestrouer.IsTouchingLayers(LayerMask.GetMask("Kub")) || lookDestrouer.IsTouchingLayers(LayerMask.GetMask("Door"));
    }
}
