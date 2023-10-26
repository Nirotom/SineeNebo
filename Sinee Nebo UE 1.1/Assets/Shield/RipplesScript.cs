using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class RipplesScript : MonoBehaviour
{
    GameObject ship;
    private VisualEffect ripples;
    private Vector3 sphereCenter;
    // Start is called before the first frame update
    void Start()
    {
        ship = FindObjectOfType<ShipControl>().gameObject;
        ripples = GetComponent<VisualEffect>();
        sphereCenter = ripples.GetVector3("SphereCenter");
        
    }
    void FixedUpdate()
    {
        transform.position = ship.transform.position + sphereCenter;
    }
}
