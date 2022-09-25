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

    private void OnEnable()
    {
        enemy = transform.parent.GetComponent<Enemy>();
    }

    private void Update()
    {
        HandleHp();
    }

    void HandleHp()
    {
        Transform parent = transform.parent;
        transform.position = parent.position;
        transform.eulerAngles = new Vector3(-90.0f, 0.0f, transform.rotation.z);
        HpBar.value = (float)enemy.CurrentHP / (float)enemy.MaxHP;
        ShieldBar.value = (float)enemy.CurrentShield / (float)enemy.MaxShield;
        
    }
}
