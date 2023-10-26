using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System.Linq;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using Random = UnityEngine.Random;

public class MeteorSpawn : MonoBehaviour
{

    public GameManager gameManager;
    public GameObject meteor1Prefab;
    public float minSpawnDelay = 1f;
    public float maxSpawnDelay = 3f;

    private Vector3 gHH;

    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        if (gameManager.meteor1SpawnOn && gameManager.meteors1.Count <= gameManager.maxMeteor1 &&
            gameManager.spawnPosDicMeteor1.Keys.Count > 0)
        {
            var randomPos =
                gameManager.spawnPosDicMeteor1.Keys.ToArray()[
                    Random.Range(0, gameManager.spawnPosDicMeteor1.Count - 1)];
            if (gameManager.spawnPosDicMeteor1[randomPos])
            {
                var meteor1 = Instantiate(meteor1Prefab, randomPos, Quaternion.identity);
                gameManager.spawnPosDicMeteor1[randomPos] = false;
                StartCoroutine(gameManager.resetRandomPos(gameManager.spawnPosDicMeteor1 , randomPos, 
                    gameManager.timeActivSpawnPos));
                gameManager.meteors1.Add(meteor1);
            }
        }

        Invoke(nameof(Spawn), gameManager.meteorSpawnDeley);

    }

   



    // Update is called once per frame
    void Update()
    {

    }

}


