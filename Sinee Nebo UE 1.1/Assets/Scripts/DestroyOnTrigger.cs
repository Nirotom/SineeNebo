using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEditor.Profiling.Memory.Experimental;

public class DestroyOntrigger : MonoBehaviour
{
    public GameManager gameManager;
    
    void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Meteor1" )
        {
            gameManager.meteors1.Remove(collision.gameObject);
            Destroy(collision.gameObject);

        }
        if (collision.tag == "Meteor2")
        {
            gameManager.meteors2.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Allien")
        {
            gameManager.aliens.Remove(collision.gameObject);
            Destroy(collision.gameObject);
           
        }
        if (collision.tag == "Bullet")
        {
            Destroy(collision.gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        ;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
