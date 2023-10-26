using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Meteor2Spawn : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject meteor2Prefab;
    public float minSpawnDelay = 3f;
    public float maxSpawnDelay = 6f;
    public float spawnXLimit = 13f;
    
    // Start is called before the first frame update
    void Start()
    {
        //SpawnAllMeteors();
        SpawnMeteor2();
    }
    
    void SpawnMeteor2()
    {
        if (gameManager.meteor2SpawnOn && gameManager.meteors2.Count < gameManager.maxMeteor2
            && gameManager.spawnPosDicMeteor2.Keys.Count > 0)
        {
            var randomPos =
                gameManager.spawnPosDicMeteor2.Keys.ToArray()[
                    Random.Range(0, gameManager.spawnPosDicMeteor2.Count - 1)];
            if (gameManager.spawnPosDicMeteor2[randomPos])
            {
                var meteor2 = Instantiate(meteor2Prefab, randomPos, Quaternion.identity);
                gameManager.spawnPosDicMeteor2[randomPos] = false;
                StartCoroutine(gameManager.resetRandomPos(gameManager.spawnPosDicMeteor2, randomPos,
                    gameManager.timeActivSpawnPos));
                gameManager.meteors2.Add(meteor2);
            }
        }
        Invoke(nameof(SpawnMeteor2), gameManager.meteor2SpawnDeley);
    }

    void SpawnAllMeteors()
    {
        if (gameManager.spawnPosDicMeteor2.Count>0)
        {
            for (int i = 0; i < gameManager.spawnPosDicMeteor2.Count; i++)
            {
                var pos =gameManager.spawnPosDicMeteor2.Keys.ToArray()[i];
                var meteor2 = Instantiate(meteor2Prefab, pos, Quaternion.identity);
                gameManager.meteors2.Add(meteor2);
            }
        }
        Invoke(nameof(SpawnAllMeteors), gameManager.meteor2SpawnDeley);
       
    }
    void Update()
    {
        
    }
}
