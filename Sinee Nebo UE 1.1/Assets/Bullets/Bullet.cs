using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosion;
    public float speed;
    GameManager gameManager;
    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        
        var rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0f, 0f, speed);

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
        Vector3 explosionPos = gameObject.transform.position;
        GameObject expl = Instantiate(explosion, explosionPos, Quaternion.identity);
        Destroy(expl, 1);
        if (other.gameObject.tag != "Allien")
        {
            //уничтожение врага кроме пришельцев
            Destroy(other.gameObject);
        }
         
        gameManager.AddScore();//увеличение счёта
        Destroy(gameObject);//уничтожение снаряда


    }
   
        // Update is called once per frame
    void Update()
    {
        
    }
}
