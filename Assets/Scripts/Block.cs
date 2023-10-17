using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public AudioClip hitSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Play the sound at the block's position
        AudioSource.PlayClipAtPoint(hitSound, transform.position);
        Debug.Log("I was hit by: " + collision.gameObject.name);

        // Destroy the block
        Destroy(gameObject);
    }


}