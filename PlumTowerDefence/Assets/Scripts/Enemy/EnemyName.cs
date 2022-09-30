using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyName : MonoBehaviour
{
    public GameObject Cam;
    [SerializeField] private TextMeshProUGUI enemyName;

    public GameObject shieldbar;

    private Enemy enemy;


    private void Awake()
    {
        Cam = GameObject.Find("Main Camera");
        enemy = transform.parent.parent.parent.GetComponent<Enemy>();
        enemyName = transform.GetComponent<TextMeshProUGUI>();
        //transform.position = new Vector3(shieldbar.transform.position.x, shieldbar.transform.position.y + 0.8f, shieldbar.transform.position.z);
    }

    private void FixedUpdate()
    {
        //transform.rotation = Cam.transform.rotation;
    }

    public void ShowName()
    {
        gameObject.SetActive(true);

        string Name = Tables.Monster.Get((int)enemy.monsterType)?._Korean;
        ESpecialityType SpecialityType = enemy.SpecialityType;
        EPropertyType PropertyType = enemy.propertyType;
        

        if(enemyName == null)
        {
            Debug.Log("NULL");
        }
        else
        {
            if (SpecialityType == ESpecialityType.None && PropertyType == EPropertyType.None)
            {
                return;
            }
            else if (PropertyType != EPropertyType.None && SpecialityType == ESpecialityType.None)
            {
                enemyName.text = Tables.MonsterProperty.Get((int)PropertyType)._Korean
                      + " " + Name;
            }
            else if (PropertyType == EPropertyType.None && SpecialityType != ESpecialityType.None)
            {
                enemyName.text = Tables.MonsterSpeciality.Get((int)SpecialityType)._Korean + " " + Name;
            }
            else
            {
                enemyName.text = Tables.MonsterProperty.Get((int)PropertyType)._Korean 
                    + " " + Tables.MonsterSpeciality.Get((int)SpecialityType)._Korean + " "+ Name;
            }
            
        }
    }

    public void HideName()
    {
        gameObject.SetActive(false);
    }

}
