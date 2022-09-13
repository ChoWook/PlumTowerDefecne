using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    public float MoveSpeed;

    private Transform Target;
    public int WaypointIndex;

    public int Route = 0;
    
    private static Camera mainCamera;
    private static Camera UICamera;
    
    private static bool CameraLoad = false;

    private void Awake()
    {
        if (!CameraLoad)
        {
            mainCamera = Camera.main;
            UICamera = GameObject.Find("UICam").GetComponent<Camera>();
            CameraLoad = true;
        }
    }

    public void InitSpeed(EMonsterType monsterType)
    {
        if(Waypoints.points == null)
        {
            return;
        }

        MoveSpeed = GetComponent<Enemy>().Speed;
        Target = Waypoints.points[Route][WaypointIndex];
    }
    void FixedUpdate()
    {

        Vector3 dir = Target.position - transform.position; 
        transform.Translate(dir.normalized * MoveSpeed * Time.deltaTime, Space.World);
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, MoveSpeed * Time.deltaTime);
        
        if(Vector3.Distance(transform.position, Target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }
    }

    void GetNextWayPoint()
    {
        if(WaypointIndex <= 0)
        {
            SendDamagedText();
            
            GameManager.instance.currentEnemyNumber--;
            GameManager.instance.currentHp--;
            ObjectPools.Instance.ReleaseObjectToPool(gameObject);
            return;
        }

        WaypointIndex--;                 // ÁöÁ¡ ÀÎµ¦½º -1

        Target = Waypoints.points[Route][WaypointIndex];   // Å¸±êÀ» º¯°æ
    }

    void SendDamagedText()
    {
        
        GameObject obj = ObjectPools.Instance.GetPooledObject("BonusText");

        var text = obj.GetComponent<BonusText>();

        text.SetPosition(transform.position);

        text.AddText(string.Format(Tables.StringUI.Get("Monster_Intrusion")._Korean));


        // Ä«¸Þ¶ó Èçµé¸²
        mainCamera.transform.DOShakePosition(0.5f, 0.1F);
        
        // ÇÇ°Ý ÀÌÆÑÆ®
        GameObject ScreenEffect = GameObject.Find("EffectScreen");
        
        Image image = ScreenEffect.GetComponent<Image>();
        Sequence seq = DOTween.Sequence();

        seq.Append(image.DOFade(0.5f, 0.1f))
            .Append(image.DOFade(0, 0.3f));
    }
}
