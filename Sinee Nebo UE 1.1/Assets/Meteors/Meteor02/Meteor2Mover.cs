using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Meteor2Mover : MonoBehaviour
{
    GameManager gameManager;
    Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rigidBody = GetComponent<Rigidbody>();
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Allien"))
        {
            rigidBody.AddForce(5,5, 2,ForceMode.Impulse);
        }
    }


    // Update is called once per frame
    void Update()
    {
        rigidBody.AddForce(0f,0f,gameManager.speedMeteor2, ForceMode.VelocityChange);
    }
}
