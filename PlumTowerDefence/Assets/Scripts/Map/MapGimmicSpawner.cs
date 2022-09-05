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
        // dix == -1 이면 맵 전체 소환
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

        // 장애물은 특수한 알고리즘으로 생성
        if (GimmickType == EMapGimmickType.Obstacle)
        {
            CreateObstacleInGround();
        }
        else
        {
            SpawnGimmickInChoosenSet(GimmickType);
        }
    }


    int[] ChooseEmptySet(float Probability, int LandsCnt)
    {
        int numRequired = (int)(Probability * LandsCnt);

        return ChooseEmptySet(numRequired, LandsCnt);
    }

    int[] ChooseEmptySet(int numRequired, int LandsCnt)
    {
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

                EmptyLands[ChoosenSet[i]].SetObjectOnTile(obj);

                EmptyLands[ChoosenSet[i]].IsFixedObstacle = true;

                obj.transform.parent = EmptyLands[ChoosenSet[i]].transform;

                obj.transform.localPosition = Vector3.zero;


                // 각 위치 타일의 인덱스를 저장할 변수들
                r = -1;
                d = -1;
                rd = -1;
                ld = -1;

                // 장애물 위치에서 주변 부분이 비어있는 지 확인
                for (int j = i + 1; j < ChoosenSet.Length; j++)
                {
                    // 다른 장애물에 포함되 있는 타일은 스킵
                    if (EmptyLands[ChoosenSet[j]].IsFixedObstacle)
                    {
                        continue;
                    }

                    Vector2 dis = EmptyLands[ChoosenSet[i]].CalculateDistance(EmptyLands[ChoosenSet[j]]._GroundPos);
                    if ((int)dis.x == 0 && (int)dis.y == 1)
                    {
                        r = j;
                        continue;
                    }

                    if ((int)dis.x == 1 && (int)dis.y == 0)
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

                // 기획서에 있는 플로우 차트 기반으로 장애물 타입 적용
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
                            ObstacleType = 8;
                        }
                        else
                        {
                            ObstacleType = 5;
                        }
                    }
                    else
                    {
                        if(d >= 0)
                        {
                            IncludeObstacle(d, obj);
                            ObstacleType = 4;
                        }
                        else
                        {
                            ObstacleType = 2;
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
                            ObstacleType = 7;
                        }
                        else
                        {
                            if(ld >= 0)
                            {
                                IncludeObstacle(ld, obj);
                                ObstacleType = 6;
                            }
                            else
                            {
                                ObstacleType = 3;
                            }
                        }
                    }
                    else
                    {
                        ObstacleType = 1;
                    }
                }

                NewObstacle.SetObstacleType(ObstacleType);
            }
        }
    }

    public void IncludeObstacle(int TileIdx, GameObject Sender)
    {
        EmptyLands[ChoosenSet[TileIdx]].IsFixedObstacle = true;
        EmptyLands[ChoosenSet[TileIdx]].SetObjectOnTile(Sender);
    }
    void SpawnGimmickHoleMap(EMapGimmickType GimmickType)
    {
        // 장애물은 전체 맵에서 생성할 수 없음
        if(GimmickType == EMapGimmickType.Obstacle)
        {
            Debug.LogWarning("Worng Spawn Obstacle");
            return;
        }
        else if(GimmickType == EMapGimmickType.Resource)
        {
            for (int i = 0; i < Map.Instance.OpenGroundCnt; i++)
            {
                if (Map.Instance.Grounds[i].ResourceTileCount == 0)
                {
                    EmptyLands = Map.Instance.Grounds[i].GetEmptyLandTilesInGround();

                    ChoosenSet = ChooseEmptySet(Random.Range(2, 6), EmptyLands.Count);

                    SpawnGimmickInChoosenSet(GimmickType);
                }
            }
        }
        else if(GimmickType == EMapGimmickType.Treasure)
        {
            EmptyLands = Map.Instance.GetEmptyLandTilesInMap();

            //ChoosenSet = ChooseEmptySet(Tables.MapGimmick.Get(GimmickType)._Probability, EmptyLands.Count);
            ChoosenSet = ChooseEmptySet(Random.Range(0, 5), EmptyLands.Count);

            SpawnGimmickInChoosenSet(GimmickType);
        }
    }

    void SpawnGimmickInChoosenSet(EMapGimmickType GimmickType)
    {
        for (int i = 0; i < ChoosenSet.Length; i++)
        {
            GameObject obj = ObjectPools.Instance.GetPooledObject(GimmickType.ToString());

            EmptyLands[ChoosenSet[i]].SetObjectOnTile(obj);

            obj.transform.parent = EmptyLands[ChoosenSet[i]].transform;

            if(GimmickType == EMapGimmickType.Resource)
            {
                EmptyLands[ChoosenSet[i]].ParentGround.ResourceTileCount++;
                // Tile > Tilemap > Grid > Ground
                //obj.transform.parent.parent.parent.parent.GetComponent<Ground>().ResourceTileCount++;
            }

            obj.transform.localPosition = Vector3.zero;
        }
    }
}
