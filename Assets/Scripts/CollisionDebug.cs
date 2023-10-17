using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDebug : MonoBehaviour
{
    [SerializeField] private bool logCollisionEnter = false;
    [SerializeField] private bool logCollisionEnterDetails = false;
    [SerializeField] private bool logCollisionExit = false;
    [SerializeField] private bool logTriggerEnter = false;
    [SerializeField] private bool logTriggerExit = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (logCollisionEnter)
        {
            Debug.Log("Collision2D Enter in " + transform.name + " from " + collision.transform.name);
        }
        if (logCollisionEnterDetails)
        {
            Debug.Log("Relative Velocity = " + collision.relativeVelocity);
            Debug.Log("# Contact Points = " + collision.contactCount);
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Debug.Log(" hit " + contact.otherCollider.name);
                Debug.Log(" normal = " + contact.normal);
                //Visualize the contact point
                //Debug.DrawRay(contact.point, contact.normal, Color.White);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (logCollisionExit)
            Debug.Log("Collision2D Exit in " + transform.name + " from " + collision.transform.name);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (logTriggerEnter)
        {
            Debug.Log("OnTriggerEnter for " + transform.name + " overlap with " + collision.name);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (logTriggerExit)
        {
            Debug.Log("OnTriggerExit for " + transform.name + " overlap with " + collision.name);
        }
    }
}