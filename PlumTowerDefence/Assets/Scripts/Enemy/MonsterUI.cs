using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterUI : MonoBehaviour
{
    [SerializeField]
    private Slider HpBar;
    [SerializeField]
    private Slider ShieldBar;
    [SerializeField]
    private GameObject bar;


    private Enemy enemy;

    private void Awake()
    {
        enemy = transform.parent.GetComponent<Enemy>();
        //transform.position = transform.parent.position;
    }

    private void Update()
    {
        transform.eulerAngles = new Vector3(-90.0f, 0.0f, transform.rotation.z);

    }

    public void HandleHp()
    {
        HpBar.value = (float)enemy.CurrentHP / (float)enemy.MaxHP;
        ShieldBar.value = (float)enemy.CurrentShield / (float)enemy.MaxShield;
    }
}
