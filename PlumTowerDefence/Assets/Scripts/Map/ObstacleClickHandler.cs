using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObstacleClickHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Obstacle Parent;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if(Parent != null)
        {
            Parent.OnPointerClick();
        }
    }
}
