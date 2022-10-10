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

    Enemy enemy;
    EnemySound deadSound;

    private void Awake()
    {
        mainCamera = Camera.main;
        UICamera = GameObject.Find("UICam").GetComponent<Camera>();
        enemy = GetComponent<Enemy>();
        CameraLoad = true;
        deadSound = GameObject.Find("EnemySound").GetComponent<EnemySound>();
    }

    public void InitSpeed(EMonsterType monsterType)
    {
        if(Waypoints.points == null)
        {
            return;
        }

        MoveSpeed = enemy.CurSpeed;
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
        if(enemy.IsAlive == false)
        {
            MoveSpeed = 0;
        }
        else
        {
            MoveSpeed = enemy.CurSpeed;
        }
    }

    void GetNextWayPoint()
    {
        if(WaypointIndex <= 0)
        {
            SendDamagedText();
            //GetComponent<SoundPlay>().Play();
            deadSound.AttackEnemySound();
            GameManager.instance.CurrentEnemyNumber--;

            if(enemy.propertyType == EPropertyType.Cursing)
            {
                Curse();
            }
            GameManager.instance.CurrentHp--;
            Debug.Log("Current Enemy Num: " + GameManager.instance.CurrentEnemyNumber);
            ObjectPools.Instance.ReleaseObjectToPool(gameObject);
            return;
        }

        WaypointIndex--;                 // ���� �ε��� -1

        Target = Waypoints.points[Route][WaypointIndex];   // Ÿ���� ����
    }

    public void Curse()
    {
        GameManager.instance.isCursed = true;
    }

    void SendDamagedText()
    {
        
        GameObject obj = ObjectPools.Instance.GetPooledObject("BonusText");

        var text = obj.GetComponent<BonusText>();

        text.SetPosition(transform.position);

        text.AddText(string.Format(Tables.StringUI.Get("Monster_Intrusion")._Korean));


        // ī�޶� ��鸲
        mainCamera.transform.DOShakePosition(0.5f, 0.1F);
        
        // �ǰ� ����Ʈ
        GameObject ScreenEffect = GameObject.Find("EffectScreen");
        
        Image image = ScreenEffect.GetComponent<Image>();
        Sequence seq = DOTween.Sequence();

        seq.Append(image.DOFade(0.5f, 0.1f))
            .Append(image.DOFade(0, 0.3f));
    }
}
