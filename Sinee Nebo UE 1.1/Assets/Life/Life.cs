using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.Linq;

public class Life : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject lifePrefab;
    GameObject shipControl;
    private Vector3 spawnPos;
    int lifeCount;
    public List<GameObject> lifeList = new List<GameObject>();
    // Start is called before the first frame update
 

    void Start()
    {
        shipControl = GameObject.Find("Ship");
        spawnPos = new Vector3(-14f, 12f, 1f);
        Spawn();   
    }
    void Spawn()
    {
        lifeList.Clear();
        lifeCount = gameManager.lives;
        for (int i = 0; i < lifeCount ; i++)
        {  
            GameObject life = Instantiate(lifePrefab, spawnPos, Quaternion.identity);
            life.transform.parent = shipControl.transform;
            lifeList.Add(life);
            spawnPos = spawnPos + new Vector3(1f, 0f, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeCount != gameManager.lives)
        {
            foreach(GameObject life in lifeList)
            {
                Destroy(life);
            }
            spawnPos = new Vector3(-14f, 12f, 1f) ;
            Spawn();
        }
    }
}
