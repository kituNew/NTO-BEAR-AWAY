using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSound : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip soundBamMain;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ExplosionSoundIt()
    {
        audioSource.PlayOneShot(soundBamMain);
    }
}
