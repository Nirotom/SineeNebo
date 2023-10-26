using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Transform mainCamera; 
    public float size = 1f;

    // Start is called before the first frame update
    void Start()
    {

       
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float scale = Vector3.Distance(transform.position, mainCamera.position);
        transform.localScale = Vector3.one * scale * size;
    }
}
