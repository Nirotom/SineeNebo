using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public class AllienMover : MonoBehaviour
{
    
    
    GameManager gameManager;
    public int count;
    public Vector3 groupPos;
    public bool goThisAlien;
    public float speedAlien;
    public int rowAlien;
    private List<int> sizeRow = new List<int>();
    private int allPos;
    private List<List<Vector3>> vgrPodAliens = new List<List<Vector3>>();
    public float zPosStep;

    

    //START /////////////////// /////////////////// /////////////////// /////////////////// /////////////////// ///////////////////
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        speedAlien = 2;
        allPos = 0;
        if (!gameManager.runCorutine)
        {
            goThisAlien = false;
            count = gameManager.aliens.Count - 1;
            Row();
        }
        if (gameManager.runCorutine)
        {
            goThisAlien = true;
            count = gameManager.aliens.Count - 1;
            Row();
        }
        
    }

    //UPDATE /////////////////// /////////////////// /////////////////// /////////////////// /////////////////// ///////////////////
    void FixedUpdate()
    {
        if (Mathf.Round(transform.position.z) == Mathf.Round(groupPos.z) && gameManager.goAlienOn)
        {

            goThisAlien = true;
            speedAlien = gameManager.speedAliens;

        }
        

        transform.position = Vector3.MoveTowards(transform.position, groupPos, speedAlien);
    }

    private void Row()
    {
        // Определяет ряд и позицию по номеру пришельца ///////////////////
        for (int i = 0; i < gameManager.groupPosAliensList.Count; i++)
        {
            var rowLong = gameManager.groupPosAliensList[i].Count;
            allPos += rowLong;
            if (count < allPos)
            {
                rowAlien = i;
                groupPos = gameManager.groupPosAliensList[i][count-(allPos - rowLong)];
                var posAlienListNow = new List<Vector3>();
                foreach (var alien in gameManager.aliens)
                {
                    var alienMove = alien.GetComponent<AllienMover>();
                    posAlienListNow.Add(alienMove.groupPos);
                }

                if (posAlienListNow.Contains(groupPos))
                {
                    foreach (var pos in gameManager.groupPosAliensList[i])
                    {
                        if (!posAlienListNow.Contains(pos))
                        {
                            groupPos = pos;
                            break;
                        }
                    }
                }
                
                break;
            }
            
        }
        
    }
}   

