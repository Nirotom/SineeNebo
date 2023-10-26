using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroFunc : MonoBehaviour
{

    public Vector3 CreateCircleGroupPosAlien(Vector2 centerGroupPosAliens, float posZ  ,int rowLong , int i, float radius)
    {
        var angle = 2 * Mathf.PI / rowLong * i;
        var posX = centerGroupPosAliens.x + Mathf.Cos(angle) * radius;
        var posY = centerGroupPosAliens.y + Mathf.Sin(angle) * radius;
        var groupPos = new Vector3(posX, posY, posZ);
        return groupPos;
    }
    public bool ifReadyAllAliens(HashSet<GameObject> aliens)
    {
        //Занял ли пришелец свою позицию ///////////////////
        var allGo = true;
        foreach (var alien in aliens)
        {
            var alienMover = alien.GetComponent<AllienMover>();
            if (!alienMover.goThisAlien)
            {
                allGo = false;
                return allGo;
            }
        }
        return allGo;
    }
}
