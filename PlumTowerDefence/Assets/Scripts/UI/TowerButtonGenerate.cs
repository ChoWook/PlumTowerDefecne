using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButtonGenerate : MonoBehaviour
{
    /// <summary>
    /// 타워버튼 생성을 관리할 스크립트
    /// </summary>
    /// 

    GameObject SelectedTower;

    private int tower_num = 12; //데이터베이스에서 받아야 함

    private void Awake()
    {
        for (int i = 0; i < tower_num; i++)
        {
            GameObject obj = ObjectPools.Instance.GetPooledObject("TowerButton");
            obj.transform.SetParent(transform);
            obj.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void OnBuildTowerBtnClick()
    {
        Debug.Log("OnBuildTowerBtnClick");

        SelectedTower = ObjectPools.Instance.GetPooledObject("ArrowTower");

        SelectedTower.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        Map.Instance.ShowAllGridLine();

        StartCoroutine(IE_FallowingMouse());
    }

    IEnumerator IE_FallowingMouse()
    {
        WaitForFixedUpdate wf = new WaitForFixedUpdate();

        while (true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit[] hits = Physics.RaycastAll(ray, 1000);
            Debug.Log(hits.Length);

            foreach (var hit in hits)
            {
                if (hit.collider.CompareTag("Tile"))
                {
                    SelectedTower.transform.position = hit.collider.transform.position;

                    break;
                }
                else if (hit.collider.CompareTag("Ground"))
                {
                    SelectedTower.transform.position = hit.point;
                }
            }

            yield return wf;
        }
        
    }

    private void Update()
    {
        
    }
}
