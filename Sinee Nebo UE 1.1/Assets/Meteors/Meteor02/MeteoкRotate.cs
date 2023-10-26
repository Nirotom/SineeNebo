using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoêRotate : MonoBehaviour
{
    GameManager gameManager;
    public float angle;
    float angle1;
    public Vector3 v;
    Quaternion q;
    public GameObject meteor2;
   
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        angle1 = angle * (Mathf.PI / 180);
        v = v.normalized;
        q = new Quaternion(Mathf.Sin (angle1 / 2) * v.x, Mathf.Sin(angle1 / 2) * v.y, Mathf.Sin (angle1 / 2) * v.z, Mathf.Cos (angle1 / 2));
    }

    // Update is called once per frame
    void Update()
    {
        angle1 = angle * (Mathf.PI / 180);
        v = v.normalized;
        q = new Quaternion(Mathf.Sin(angle1 / 2) * v.x, Mathf.Sin(angle1 / 2) * v.y , Mathf.Sin(angle1 / 2) * v.z, Mathf.Cos(angle1 / 2));
        meteor2.transform.rotation = meteor2.transform.rotation * q;
        
    }
}
