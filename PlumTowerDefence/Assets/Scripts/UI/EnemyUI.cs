using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyUI : MonoBehaviour, IPointerClickHandler
{
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        switch (GameManager.instance.isSettingTarget)
        {
            case 3:
                UIManager.instance.AllTowerUI.GetComponent<UpdateAllTowerUI>().AttackEnemy(gameObject);
                break;
            case 2:
                UIManager.instance.GroundTowerUI.GetComponent<UpdateGroundTowerUI>().AttackEnemy(gameObject);
                break;
            case 1:
                UIManager.instance.TowerUI.GetComponent<UpdateTowerUI>().AttackEnemy(gameObject);
                break;
        }
    }
}
