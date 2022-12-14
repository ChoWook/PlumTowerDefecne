using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Obstacle : IObjectOnTile
{
    [SerializeField] GameObject[] Bodys;

    public int ObstacleType = 0;

    public int DeletePrice = 0;
    // Start is called before the first frame update

#if UNITY_EDITOR

    //
    //private void Update()
    //{
        //UpdateObstacleType();
    //}

#endif


    public void SetObstacleType(int Sender)
    {
        ObstacleType = Sender;

        DeletePrice = (int)(Tables.MapGimmickObstacle.Get(ObstacleType)._Removal *
                            (1 - GameManager.instance.DiscountObstacle));

        UpdateObstacleType();
    }


    void UpdateObstacleType()
    {
        for(int i = 0; i < Bodys.Length; i++)
        {
            if(ObstacleType == i + 1)
            {
                Bodys[i].SetActive(true);

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
                Bodys[i].SetActive(false);
            }
        }
    }

    public void OnObstacleClick()
    {
        UIManager.instance.ShowObstacleUI(this);

        Debug.Log("Obstacle Click");
    }

    public void DeleteObstacle()
    {
        if(GameManager.instance.Money >= DeletePrice){

            GameManager.instance.Money -= DeletePrice;

            ObjectPools.Instance.ReleaseObjectToPool(gameObject);
        }

    }

}
