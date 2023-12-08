using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    BoxCollider2D lookDestrouer;

    int a = 0;

    void Start()
    {
        lookDestrouer = GetComponent<BoxCollider2D>();
    }

    public bool IsPlayer()
    {
        try
        { 
            return lookDestrouer.IsTouchingLayers(LayerMask.GetMask("Player"));
        }
        catch
        {
            a = 1;
        }
        return false;
    }

    public bool IsEnemy()
    {
        try
        { 
            return lookDestrouer.IsTouchingLayers(LayerMask.GetMask("Enemy"));
        }
        catch
        {
            a = 1;
        }
        return false;
    }

    bool IsKub()
    {
        try
        { 
            return lookDestrouer.IsTouchingLayers(LayerMask.GetMask("Kub"));
        }
        catch
        {
            a = 1;
        }
        return false;
    }

    bool IsDoor()
    {
        try
        { 
            return lookDestrouer.IsTouchingLayers(LayerMask.GetMask("Door"));
        }
        catch
        {
            a = 1;
        }
        return false;
    }

    public bool IsObject()
    {
        return IsEnemy() || IsPlayer() || IsKub() || IsDoor();
    }
}
