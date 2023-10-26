using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Shield : MonoBehaviour
{
    public GameManager gameManager;
    public ShipControl shipControl;
    public float timeShield;
    public GameObject explosion;
    public GameObject shieldRipples;
    private VisualEffect shieldRipplesVFX;
    private CinemachineCollisionImpulseSource cinemachne;

        
    
    // Start is called before the first frame update
    void Start()
    {
        timeShield = 0f;
        cinemachne = GetComponent<CinemachineCollisionImpulseSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag is not ("Meteor1" or "Meteor2" or "Allien")) return;
        var ripples = Instantiate(shieldRipples, transform);
        shieldRipplesVFX = ripples.GetComponent<VisualEffect>();
        shieldRipplesVFX.SetVector3("SphereCenter", other.contacts[0].point);
        var crashPos = other.contacts[0].point;
        var expl = Instantiate(explosion, crashPos, Quaternion.identity);
        Destroy(expl, 1f);
        Destroy(ripples, 0.6f);
        Destroy(other.gameObject);
        gameManager.AddScore();
        cinemachne.GenerateImpulse();
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
            gameManager.aliens.Remove(other.gameObject);
        }
    }

    void Update()
    {
        
        timeShield += Time.deltaTime;
        if (timeShield >= 10)
        {
            shipControl.shield.gameObject.SetActive(false);
            timeShield = 0;  
        }
        
    }
}
