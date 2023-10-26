using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LaserScript : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject explosion;
    public GameObject particlesSparks;
    public ShipControl shipControl;
    
    
    // Start is called before the first frame update
    
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag is not ("Meteor1" or "Meteor2" or "Allien")) return;
        if (other.gameObject.tag == "Meteor1")
        {
            gameManager.meteors1.Remove(other.gameObject);
        }
        if (other.gameObject.tag == "Meteor2")
        {
            gameManager.meteors2.Remove(other.gameObject);
        }
        if (other.gameObject.tag == "Allien")
        {
            other.gameObject.transform.position = new Vector3(0, 0, gameManager.spawnZ);
            var alienMover = other.gameObject.GetComponent<AllienMover>();
            alienMover.speedAlien = 2;
        }
        var crashPos = other.gameObject.transform.position;
        var expl = Instantiate(explosion, crashPos, Quaternion.identity);
        if (other.gameObject.tag != "Allien")
        {
            //уничтожение врага кроме пришельцев
            Destroy(other.gameObject);
        }

        Destroy(expl, 2);
        gameManager.AddScore();

    }
    
}
