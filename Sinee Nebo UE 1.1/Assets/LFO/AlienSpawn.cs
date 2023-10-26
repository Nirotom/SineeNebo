using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class AlienSpawn : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject alienPrefab;
    void Start()
    {
        SpawnAlien();
        
    }
    void SpawnAlien()
    {
        if (gameManager.alienSpawnOn && gameManager.aliens.Count < gameManager.maxAliens &&
            gameManager.spawnPosDicAliens.Keys.Count > 0 && gameManager.groupPosAliensList.Count > 0)
        {
            var randomPos =
                gameManager.spawnPosDicAliens.Keys.ToArray()[
                    Random.Range(0, gameManager.spawnPosDicAliens.Count - 1)];
            if (gameManager.spawnPosDicAliens[randomPos])
            {
                var alien = Instantiate(alienPrefab, randomPos, quaternion.identity);
                gameManager.aliens.Add(alien);
                gameManager.spawnPosDicAliens[randomPos] = false;
                StartCoroutine(gameManager.resetRandomPos(gameManager.spawnPosDicAliens , randomPos, 
                    gameManager.timeActivSpawnPos));
            }
            
        }
        Invoke(nameof(SpawnAlien), gameManager.alienSpawnDeley);
    }
}
