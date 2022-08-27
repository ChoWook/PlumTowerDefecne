using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int ObstacleType = 0;
    // Start is called before the first frame update
    public void SetObstacleType(int Sender)
    {
        ObstacleType = Sender;
    }

}
