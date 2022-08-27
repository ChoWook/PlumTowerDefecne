using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapGimmicSpawner : MonoBehaviour
{
    [SerializeField] GameObject Obstacle;

    [SerializeField] GameObject Resource;

    [SerializeField] GameObject Chest;

    List<Tile> EmptyLands;

    int[] ChoosenSet;

    public void SpawnGimmick(EMapGimmickType GimmickType, int idx = -1)
    {
        // dix == -1 �̸� �� ��ü ��ȯ
        if (idx == -1)
        {
            SpawnGimmickHoleMap(GimmickType);
        }
        else
        {
            SpawnGimmickOnlyNextGround(GimmickType, idx);
        }
    }

    void SpawnGimmickOnlyNextGround(EMapGimmickType GimmickType, int GroundIdx)
    {
        EmptyLands = Map.Instance.Grounds[GroundIdx].GetEmptyLandTilesInGround();

        ChoosenSet = ChooseEmptySet(Tables.MapGimmick.Get(GimmickType)._Probability, EmptyLands.Count);

        // ��ֹ��� Ư���� �˰������� ����
        if (GimmickType == EMapGimmickType.Obstacle)
        {
            CreateObstacleInGround();
        }
        else
        {
            SpawnGimmickInEmptyLandTile(GimmickType);
        }
    }


    int[] ChooseEmptySet(float Probability, int LandsCnt)
    {
        int numRequired = (int)(Probability * LandsCnt);

        int[] result = new int[numRequired];

        int numToChoose = numRequired;

        for (int numLeft = LandsCnt; numLeft > 0; numLeft--)
        {

            float prob = (float)numToChoose / (float)numLeft;

            if (Random.value <= prob)
            {
                numToChoose--;
                result[numToChoose] = numLeft - 1;

                if (numToChoose == 0)
                {
                    break;
                }
            }
        }
        return result;
    }

    void CreateObstacleInGround()
    {
        int r = -1;
        int d = -1;
        int rd = -1;
        int ld = -1;

        for (int i = 0; i < ChoosenSet.Length; i++)
        {

            if (!EmptyLands[ChoosenSet[i]].IsFixedObstacle)
            {

                GameObject obj = ObjectPools.Instance.GetPooledObject(EMapGimmickType.Obstacle.ToString());

                Obstacle NewObstacle = obj.GetComponent<Obstacle>();

                EmptyLands[ChoosenSet[i]].ObjectOnTile = obj;

                EmptyLands[ChoosenSet[i]].IsFixedObstacle = true;

                obj.transform.parent = EmptyLands[ChoosenSet[i]].transform;

                obj.transform.localPosition = Vector3.zero;


                // �� ��ġ Ÿ���� �ε����� ������ ������
                r = -1;
                d = -1;
                rd = -1;
                ld = -1;

                // ��ֹ� ��ġ���� �ֺ� �κ��� ����ִ� �� Ȯ��
                for (int j = i + 1; j < ChoosenSet.Length; j++)
                {
                    // �ٸ� ��ֹ��� ���Ե� �ִ� Ÿ���� ��ŵ
                    if (EmptyLands[ChoosenSet[j]].IsFixedObstacle)
                    {
                        continue;
                    }

                    Vector2 dis = EmptyLands[ChoosenSet[i]].CalculateDistance(EmptyLands[ChoosenSet[j]]);
                    if ((int)dis.x == 1 && (int)dis.y == 0)
                    {
                        r = j;
                        continue;
                    }

                    if ((int)dis.x == 0 && (int)dis.y == 1)
                    {
                        d = j;
                        continue;
                    }

                    if((int)dis.x == 1 && (int)dis.y == -1)
                    {
                        ld = j;
                        continue;
                    }

                    if ((int)dis.x == 1 && (int)dis.y == 1)
                    {
                        rd = j;
                        break;
                    }

                    if ((int)dis.x > 1 && (int)dis.y > 1)
                    {
                        break;
                    }
                }

                // ��ȹ���� �ִ� �÷ο� ��Ʈ ������� ��ֹ� Ÿ�� ����
                int ObstacleType = 0;
                if(r >= 0)
                {
                    IncludeObstacle(r, obj);
                    if (rd>= 0)
                    {
                        IncludeObstacle(rd, obj);
                        if (d >= 0)
                        {
                            IncludeObstacle(d, obj);
                            ObstacleType = 7;
                        }
                        else
                        {
                            ObstacleType = 4;
                        }
                    }
                    else
                    {
                        if(d >= 0)
                        {
                            IncludeObstacle(d, obj);
                            ObstacleType = 3;
                        }
                        else
                        {
                            ObstacleType = 1;
                        }
                    }
                }
                else
                {
                    if(d >= 0)
                    {
                        IncludeObstacle(d, obj);
                        if (rd >= 0)
                        {
                            IncludeObstacle(rd, obj);
                            ObstacleType = 6;
                        }
                        else
                        {
                            if(ld >= 0)
                            {
                                IncludeObstacle(ld, obj);
                                ObstacleType = 5;
                            }
                            else
                            {
                                ObstacleType = 2;
                            }
                        }
                    }
                    else
                    {
                        ObstacleType = 0;
                    }
                }

                NewObstacle.SetObstacleType(ObstacleType);
            }
        }
    }

    public void IncludeObstacle(int TileIdx, GameObject Sender)
    {
        EmptyLands[ChoosenSet[TileIdx]].IsFixedObstacle = true;
        EmptyLands[ChoosenSet[TileIdx]].ObjectOnTile = Sender;
    }
    void SpawnGimmickHoleMap(EMapGimmickType GimmickType)
    {
        // ��ֹ��� ��ü �ʿ��� ������ �� ����
        if(GimmickType == EMapGimmickType.Obstacle)
        {
            Debug.LogWarning("Worng Spawn Obstacle");
            return;
        }
        else
        {
            EmptyLands = Map.Instance.GetEmptyLandTilesInMap();

            Debug.Log("EmptyLands" + EmptyLands.Count);

            ChoosenSet = ChooseEmptySet(Tables.MapGimmick.Get(GimmickType)._Probability, EmptyLands.Count);

            Debug.Log("_Probability" + Tables.MapGimmick.Get(GimmickType)._Probability);

            Debug.Log("ChoosenSet.Count" + ChoosenSet.Length);

            SpawnGimmickInEmptyLandTile(GimmickType);
        }
    }

    void SpawnGimmickInEmptyLandTile(EMapGimmickType GimmickType)
    {
        for (int i = 0; i < ChoosenSet.Length; i++)
        {
            GameObject obj = ObjectPools.Instance.GetPooledObject(GimmickType.ToString());

            EmptyLands[ChoosenSet[i]].ObjectOnTile = obj;

            obj.transform.parent = EmptyLands[ChoosenSet[i]].transform;

            obj.transform.localPosition = Vector3.zero;
        }
    }
}
