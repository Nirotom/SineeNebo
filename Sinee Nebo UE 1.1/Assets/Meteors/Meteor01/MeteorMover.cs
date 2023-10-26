using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMover : MonoBehaviour
{
    GameManager gameManager;
    Rigidbody rb;
   
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
        
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Allien"))
        {
            rb.AddForce(5,5, 2,ForceMode.Impulse);
        }
    }
    
    void FixedUpdate()
    {
        rb.AddForce(0f, 0f, gameManager.speedMeteor1, ForceMode.VelocityChange);
    }
}
