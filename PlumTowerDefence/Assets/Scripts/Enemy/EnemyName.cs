using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyName : MonoBehaviour
{
    public GameObject Cam;
    [SerializeField] private TextMeshPro enemyName;

    private Enemy enemy;


    private void Awake()
    {
        Cam = GameObject.Find("Main Camera");
        enemy = transform.parent.GetComponent<Enemy>();
        enemyName = transform.GetComponent<TextMeshPro>();
    }

    private void FixedUpdate()
    {
        transform.rotation = Cam.transform.rotation;
    }

    public void ShowName()
    {
        string Name = Tables.Monster.Get((int)enemy.monsterType)?._Korean;
        string SpecialityType = Tables.MonsterSpeciality.Get(enemy.specialityType)?._Korean;
        string PropertyType = Tables.MonsterProperty.Get((int)enemy.propertyType)?._Korean;

        
        if(enemyName == null)
        {
            Debug.Log("NULL");
        }
        else
        {
            if (SpecialityType == "없음")
            {
                enemyName.text = Name;
                return;
            }
            else if (PropertyType == "없음")
            {
                enemyName.text = SpecialityType + " " + Name;
            }
            else
            {
                enemyName.text = PropertyType + " " + SpecialityType + " " + Name;
            }
        }
    }

}
