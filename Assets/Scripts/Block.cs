using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip hitSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per framee
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.PlayOneShot(hitSound);
        Debug.Log("I was hit by: " + collision.gameObject.name);
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, hitSound.length);
    }


}