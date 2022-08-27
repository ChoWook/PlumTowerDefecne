using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapGimmicSpawner : MonoBehaviour
{
    [SerializeField] GameObject Obstacle;

    [SerializeField] GameObject Resource;

    [SerializeField] GameObject Chest;

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

    void SpawnGimmickOnlyNextGround(EMapGimmickType GimmickType, int idx)
    {
        List<Tile> EmptyLands = Map.Instance.Grounds[idx].GetEmptyLandTiles();

        if (GimmickType == EMapGimmickType.Obstacle)
        {

        }
        else
        {
            GameObject obj = ObjectPools.Instance.GetPooledObject(GimmickType.ToString());

            var Gimmick = obj.GetComponent<MapGimmick>();

            int[] ChoosenSet = ChooseEmptySet(Gimmick.Probability, EmptyLands.Count);

            for(int i = 0; i < ChoosenSet.Length; i++)
            {
                EmptyLands[i].ObjectOnTile = obj;

                obj.transform.parent = EmptyLands[i].transform;

                obj.transform.localPosition = Vector3.zero;
            }
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

    void SpawnGimmickHoleMap(EMapGimmickType GimmickType)
    {

    }

}
