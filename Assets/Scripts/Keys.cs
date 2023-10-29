using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{

    public GameController gameController;
    //public AudioClip collected;

    //private AudioSource audioSource;


    private void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //audioSource.PlayOneShot(collected);
            gameController.AddKey();
            Destroy(gameObject);
        }
    }
}
