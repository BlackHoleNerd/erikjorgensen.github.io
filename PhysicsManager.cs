using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PhysicsManager : MonoBehaviour
{
    private Rigidbody[] allRigidbodies;

    //private PlayerMover mover;

    public Transform player;


    void Start()
    {
        //mover = GetComponent<PlayerMover>();
        // Use the non-obsolete method
        allRigidbodies = FindObjectsByType<Rigidbody>(FindObjectsSortMode.None);
    }


    Vector3 x0 = new Vector3(0f,0f,0f);

    //public static PhysicsManager Instance;
    void Update()
    {
        if (Input.GetKey(KeyCode.G))
        {
            //CGHscale.c *= 0.9f;
            //CGHscale.G *= 0.81f;
            //CGHscale.h *= 0.9f;
        }
        if (Input.GetKey(KeyCode.T))
        {
            //CGHscale.c *= 1.0f / 0.9f;
            //CGHscale.G *= 1.0f / 0.81f;
            //CGHscale.h *= 1.0f / 0.9f;
        }
        if (Input.GetKey(KeyCode.H))
        {
            //CGHscale.c *= 1.0f / 0.9f;
            //CGHscale.G *= 1.0f / 0.729f;
            //CGHscale.h *= 1.0f / 0.81f;
        }
        if (Input.GetKey(KeyCode.Y))
        {
            //CGHscale.c *= 0.9f;
            //CGHscale.G *= 0.729f;
            //CGHscale.h *= 0.81f;
        }
        if (Input.GetKey(KeyCode.J))
        {
            //CGHscale.c *= 1.0f;
            //CGHscale.G *= 0.9f;
            //CGHscale.h *= 1.0f / 0.9f;
        }
        if (Input.GetKey(KeyCode.U))
        {
            //CGHscale.c *= 1.0f;
            //CGHscale.G *= 1.0f / 0.9f;
            //CGHscale.h *= 0.9f;
        }

        foreach (Rigidbody rb in allRigidbodies)
        {
            x0 = player.position;
            //PlayerMover mover = GetComponent<PlayerMover>();
            if (Input.GetKey(KeyCode.T))
            {
                //mover.acceleration *= 1.0f / 0.81f; 
                rb.linearVelocity *= 1.0f / 0.9f;
                rb.angularVelocity *= 1.0f / 0.9f;
            }
            if (Input.GetKey(KeyCode.G))
            {
                //mover.acceleration *= 0.81f;
                rb.linearVelocity *= 0.9f;
                rb.angularVelocity *= 0.9f;
            }
            if (Input.GetKey(KeyCode.Y))
            {
                //mover.acceleration *= 0.9f;
                //mover.player.position;
                rb.linearVelocity *= 0.9f;
                Transform tf = rb.transform;
                tf.localScale = Vector3.one * 0.9f;
                rb.position = (rb.position - x0) * 0.9f + x0;
            }
            if (Input.GetKey(KeyCode.H))
            {
                //mover.acceleration *= 1.0f / 0.9f;
                //mover.player.position;
                rb.linearVelocity *= 1.0f / 0.9f;
                Transform tf = rb.transform;
                tf.localScale = Vector3.one / 0.9f;
                rb.position = (rb.position - x0) / 0.9f + x0;
            }
            if (Input.GetKey(KeyCode.U))
            {
                rb.mass *= 0.9f;
            }
            if (Input.GetKey(KeyCode.J))
            {
                rb.mass *= 1.0f / 0.9f;
            }




        }

    }

}


