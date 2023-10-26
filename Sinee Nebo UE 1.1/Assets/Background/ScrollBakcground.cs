using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBakcground : MonoBehaviour
{
    GameManager gameManager;
    public float speed = -2f;
    public float lowerYValue = -20f;
    public float upperYValue = 40f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0f, speed * Time.deltaTime, 0f);
        if(transform.position.y <= lowerYValue)
        {
            transform.Translate(0f, upperYValue, 0f);
        }
    }
}
