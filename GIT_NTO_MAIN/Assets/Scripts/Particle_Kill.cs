using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Kill : MonoBehaviour
{
    float timeOnLive = 0f;
    bool isDie = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(isDie)
        {   
            Destroy(gameObject);
        }
        else
        {
            timeOnLive += Time.deltaTime;
        }

        if(timeOnLive >= 0.8f)
        {
            isDie = true;
        }
    }
}
