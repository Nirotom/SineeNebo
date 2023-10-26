using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles_collis_laserScript : MonoBehaviour
{
    public GameObject laser;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
        gameObject.transform.position = laser.gameObject.transform.position;
    }
}
