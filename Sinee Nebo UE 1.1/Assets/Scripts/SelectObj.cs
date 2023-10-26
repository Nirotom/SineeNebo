using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObj : MonoBehaviour
{
    private Color standartColor;
    public void Select()
    {
        if (gameObject.CompareTag("Allien") || gameObject.CompareTag("Meteor1"))
        {
            var alien = gameObject.transform.GetChild(0).gameObject;
            alien.GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        
    }

    public void Deselect()
    {
        if (gameObject.CompareTag("Allien") || gameObject.CompareTag("Meteor1"))
        {
            var alien = gameObject.transform.GetChild(0).gameObject;
            alien.GetComponent<Renderer>().material.color = standartColor;
        }
        else
        {
            GetComponent<Renderer>().material.color = standartColor;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
      
        if (gameObject.CompareTag("Allien") || gameObject.CompareTag("Meteor1"))
        {
            standartColor = gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color;
        }
        else
        {
            standartColor = gameObject.GetComponent<Renderer>().material.color;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
