using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] GameObject[] Bodys;

    public int ObstacleType = 0;
    // Start is called before the first frame update

#if UNITY_EDITOR

    private void Update()
    {
        //UpdateObstacleType();
    }

#endif


    public void SetObstacleType(int Sender)
    {
        ObstacleType = Sender;

        UpdateObstacleType();
    }


    void UpdateObstacleType()
    {
        for(int i = 0; i < Bodys.Length; i++)
        {
            if(ObstacleType == i)
            {
                Bodys[i].gameObject.SetActive(true);

                int RandomBody = Random.Range(0, Bodys[i].transform.childCount);

                for(int j = 0; j < Bodys[i].transform.childCount; j++)
                {
                    if(j == RandomBody)
                    {
                        Bodys[i].transform.GetChild(j).gameObject.SetActive(true);
                    }
                    else
                    {
                        Bodys[i].transform.GetChild(j).gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                Bodys[i].gameObject.SetActive(false);
            }
        }
    }
}
