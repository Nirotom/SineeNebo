using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    Renderer rend;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = -0.5f;
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.time * speed;
        rend.material.mainTextureOffset = new Vector2(offset, 0);
        
    }
}
